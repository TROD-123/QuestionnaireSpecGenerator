using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marshal = System.Runtime.InteropServices.Marshal;


namespace QuestionnaireSpecGenerator
{
    internal class ExcelGenerator
    {
        /// <summary>
        /// Used for creating the Excel environment and process
        /// </summary>
        private Application app = null;

        /// <summary>
        /// For creating the workbook file
        /// </summary>
        private Workbook workbook = null;

        /// <summary>
        /// For allowing us to work in the current worksheet. This is basically the current worksheet.
        /// </summary>
        private Worksheet worksheet = null;

        /// <summary>
        /// For allowing us to modify specific cells (i.e. ranges) in the current worksheet
        /// </summary>
        private Range worksheetRange = null;

        /// <summary>
        /// Indicates the current 1-based index of the list of worksheets. Set to 0 for initialization.
        /// When the first worksheet is selected for editing, this is set to 1.
        /// </summary>
        private int currentSheet = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelGenerator"/> class.
        /// </summary>
        public ExcelGenerator()
        {
            createAppInstance();
        }

        #region Private Methods

        /// <summary>
        /// Creates the application instance.
        /// </summary>
        private void createAppInstance()
        {
            try
            {
                app = new Application();
                workbook = app.Workbooks.Add();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }
            finally
            {
            }
        }

        private void releaseObject(object obj) // note ref!
        {
            // Do not catch an exception from this.
            // You may want to remove these guards depending on
            // what you think the semantics should be.
            if (obj != null && Marshal.IsComObject(obj))
            {
                Marshal.ReleaseComObject(obj);
            }
            // Since passed "by ref" this assingment will be useful
            // (It was not useful in the original, and neither was the
            //  GC.Collect.)
            obj = null;
        }

        #region PRIVATE: Creating modules

        /// <summary>
        /// Sets the default column widths.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        private void setDefaultColumnWidths(Worksheet worksheet)
        {
            worksheetRange = worksheet.get_Range("A:A");
            worksheetRange.ColumnWidth = Constants.leftColSize;

            worksheetRange = worksheet.get_Range("B:B");
            worksheetRange.ColumnWidth = Constants.rightColSize;
        }

        /// <summary>
        /// Sets the default row heights.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        private void setDefaultRowHeights(Worksheet worksheet)
        {
            worksheetRange = worksheet.Cells; // gets all the cells in the worksheet
            worksheetRange.RowHeight = Constants.defaultRowHeight;
        }

        /// <summary>
        /// Sets the default word wrapping.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        private void setDefaultWordWrapping(Worksheet worksheet)
        {
            worksheetRange = worksheet.Cells;
            worksheetRange.Cells.WrapText = true;
        }

        /// <summary>
        /// Auto adjusts the row heights.
        /// </summary>
        /// <param name="range">The range.</param>
        private void AutoFitRows(Range range)
        {
            range.Rows.AutoFit();

            // TODO: Not all rows need to be autosized. Section headers should NOT be autosized.
        }

        #endregion

        #region PRIVATE: Cell formatting helpers

        /// <summary>
        /// Sets the color of the cell background.
        /// </summary>
        /// <param name="worksheetRange">The worksheet range.</param>
        /// <param name="color">The color.</param>
        private void formatCellColor(Range worksheetRange, string color)
        {
            ColorConverter cv = new ColorConverter();
            switch (color)
            {
                case Constants.Colors.LIGHT_GREEN:
                    worksheetRange.Interior.Color = (Color)cv.ConvertFromString(Constants.Colors.LIGHT_GREEN);
                    break;
                case Constants.Colors.DARK_GREEN:
                    worksheetRange.Interior.Color = (Color)cv.ConvertFromString(Constants.Colors.DARK_GREEN);
                    break;
                case Constants.Colors.GREY:
                    worksheetRange.Interior.Color = (Color)cv.ConvertFromString(Constants.Colors.GREY);
                    break;
                default:
                    //cellRange.Interior.Color = (Color) cv.ConvertFromString();
                    break;
            }
        }

        /// <summary>
        /// Sets the color of the cell text.
        /// </summary>
        /// <param name="worksheetRange">The worksheet range.</param>
        /// <param name="color">The color.</param>
        private void formatCellTextColor(Range worksheetRange, string color)
        {
            if (color == Constants.Colors.WHITE)
            {
                ColorConverter cv = new ColorConverter();
                worksheetRange.Font.Color = (Color)cv.ConvertFromString(color);
            }

        }

        /// <summary>
        /// Sets the cell alignment.
        /// </summary>
        /// <param name="worksheetRange">The worksheet range.</param>
        /// <param name="hAlign">The h align.</param>
        /// <param name="vAlign">The v align.</param>
        private void formatCellAlignment(Range worksheetRange, XlHAlign hAlign, XlVAlign vAlign)
        {
            worksheetRange.HorizontalAlignment = hAlign;
            worksheetRange.VerticalAlignment = vAlign;
        }

        /// <summary>
        /// Sets the cell border.
        /// </summary>
        /// <param name="worksheetRange">The worksheet range.</param>
        /// <param name="rowType">Type of the row.</param>
        private void formatCellBorder(Range worksheetRange, RowType rowType)
        {
            if (rowType == RowType.None)
            {
                // set a thick box border around range if RowType isn't specified
                worksheetRange.BorderAround2(Type.Missing, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, Type.Missing);
                return;
            }

            if (rowType != RowType.SectionHeader)
            {
                // do not set border if RowType is Section Header
                worksheetRange.Borders.Color = Color.Black.ToArgb();
            }
        }

        /// <summary>
        /// Sets the cell font.
        /// </summary>
        /// <param name="worksheetRange">The worksheet range.</param>
        /// <param name="textFormat">The text format.</param>
        private void formatCellFont(Range worksheetRange, TextFormat textFormat)
        {
            switch (textFormat)
            {
                case TextFormat.Normal:
                    // do nothing
                    break;
                case TextFormat.Bold:
                    worksheetRange.Font.Bold = true;
                    break;
                case TextFormat.Italics:
                    worksheetRange.Font.Italic = true;
                    break;
                case TextFormat.Strikethrough:
                    worksheetRange.Font.Strikethrough = true;
                    break;
                default:
                    break;
            }
        }

        #endregion

        /// <summary>
        /// Sets the text of a given cell. Function uses r1c1 notation.
        /// </summary>
        /// <param name="row">The row number.</param>
        /// <param name="col">The column number.</param>
        /// <param name="text">The text.</param>
        private void setCellText(int row, int col, string text)
        {
            worksheet.Cells[row, col] = text;
        }

        /// <summary>
        /// Creates a row and adds it to the Excel.
        /// </summary>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="leftColText">The left col text.</param>
        /// <param name="rightColText">The right col text.</param>
        /// <param name="rowType">Type of the row.</param>
        private void createRow(int startRow, int startCol, string leftColText, string rightColText, RowType rowType)
        {
            int endCol = startCol + Constants.defaultColumnOffset;

            // 1. DETERMINE PHYSICAL CELL CHARACTERISTICS

            // statically set rowType default characteristics based on rowType
            TextFormat textFormat = QuestionBlockHeaders.QreRowObjs[rowType].TextFormat;
            string cellColor = QuestionBlockHeaders.QreRowObjs[rowType].RowColor;
            string textColor = Constants.Colors.BLACK; // by default, color is black
            bool mergeCells = false; // by default, do not merge cells
            bool separateCellFormatting = false; // by default, both cells formatted the same
            bool autofitRow = true; // by default, autofit rows

            // dynamically set rowType characteristics based on rowType
            switch (rowType)
            {
                // if RowType is Section Header, then leftColText is Section Letter and rightColText is Section Title
                case RowType.SectionHeader:
                    leftColText = String.Format("SECTION {0}: {1}", leftColText, rightColText);
                    rightColText = null;
                    // merge the cells into a banner
                    mergeCells = true;
                    // override default colors
                    textColor = Constants.Colors.WHITE;
                    // do not autofit row
                    autofitRow = false;
                    break;

                // if RowType is Response, then col texts are explicitly provided. format code column and response column
                // differently
                case RowType.Response:
                    separateCellFormatting = true;
                    break;

                // if RowType is a code, set text by predefined array and format both cells differently
                case RowType.Code_Generic:
                case RowType.Code_Column:
                case RowType.Code_Row:
                    leftColText = QuestionBlockHeaders.QreRowObjs[rowType].LeftColString;
                    separateCellFormatting = true;
                    break;

                // if RowType is not Question Num/Title, then set text by predefined array
                default:
                    if (rowType != RowType.QuestionNumTitle)
                    {
                        leftColText = QuestionBlockHeaders.QreRowObjs[rowType].LeftColString;
                    }
                    break;
            }

            // 2. APPLY PHYSICAL CELL CHARACTERISTICS AND TEXT TO THE EXCEL

            // For the left column, define the excel range for formatting and center indentation
            // Right column remains left indented (no change here)
            if (separateCellFormatting)
            {
                worksheetRange = worksheet.get_Range(String.Format("{0}{1}", Toolbox.GetExcelColumnName(startCol), startRow));
                formatCellAlignment(worksheetRange, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignBottom);
            }
            // TODO: This is very process intensive. Need to figure out how to do this in a higher, single batch way
            if (autofitRow)
            {
                worksheetRange = worksheet.get_Range(String.Format("{0}{1}", Toolbox.GetExcelColumnName(startCol), startRow));
                AutoFitRows(worksheetRange);
            }

            // sets the formatting
            worksheetRange = worksheet.get_Range(String.Format("{0}{2}:{1}{2}", Toolbox.GetExcelColumnName(startCol), Toolbox.GetExcelColumnName(endCol), startRow));
            formatCellFont(worksheetRange, textFormat);
            formatCellTextColor(worksheetRange, textColor);
            formatCellColor(worksheetRange, cellColor);
            formatCellBorder(worksheetRange, rowType);
            if (mergeCells)
            {
                worksheetRange.Merge();
                formatCellAlignment(worksheetRange, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignCenter);
                worksheetRange.RowHeight = Constants.defaultSectionRowHeight;
                worksheetRange.Font.Size = Constants.defaultSectionFontSize;
            }

            // set the text of the cell
            setCellText(startRow, startCol, leftColText);
            if (rightColText != null)
            {
                setCellText(startRow, endCol, rightColText);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the window visibility and maximizes the window if true.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        public void SetWindowVisibility(bool visible)
        {
            app.Visible = true;
            if (visible)
            {
                app.WindowState = XlWindowState.xlMaximized;
            }
            else
            {
                app.WindowState = XlWindowState.xlMinimized;
            }
        }

        // from https://stackoverflow.com/questions/158706/how-do-i-properly-clean-up-excel-interop-objects
        public void CleanUpExcelInteropObjs()
        {
            // Release all COM RCWs.
            // The "releaseObject" will just "do nothing" if null is passed,
            // so no need to check to find out which need to be released.
            // The "finally" is run in all cases, even if there was an exception
            // in the "try". 
            // Note: passing "by ref" so afterwords "xlWorkSheet" will
            // evaluate to null. See "releaseObject".
            releaseObject(worksheet);
            releaseObject(workbook);
            // The Quit is done in the finally because we always
            // want to quit. It is no different than releasing RCWs.
            if (app != null)
            {
                app.Quit();
            }
            releaseObject(app);
        }

        /// <summary>
        /// Creates a new module. This is basically just a new sheet. Sets the name and the standard physical 
        /// properties of the sheet.
        /// <para>
        /// If we're on the first sheet, this sets the current <see cref="worksheet"/> to the first sheet. Note
        /// that Excel sheet numbers use 1-based indexing. So 1 notes the first sheet, 2 is the second sheet, etc.
        /// </para>
        /// <para>
        /// If we're not on the first sheet, then this adds a new sheet after the previous one, and sets the current
        /// <see cref="worksheet"/> to the new one. Increments <see cref="currentSheet"/>.
        /// </para>
        /// <para>
        /// Note: This is private, and called within the <see cref="CreateNewSheetSectionHeader"/> method.
        /// </para>
        /// </summary>
        private void CreateModule(string moduleTitle)
        {
            if (currentSheet == 1)
            {
                worksheet = (Worksheet)workbook.Sheets[currentSheet];
            }
            else
            {
                worksheet = (Worksheet)workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
            }

            // Sheet names must:
            // Not be > 31 chars
            // Not contain these chars:  : \ / ? * [ or ]
            // Not be blank
            workbook.Worksheets[currentSheet].Name = moduleTitle;
            currentSheet++;

            app.StandardFont = Constants.defaultFont;
            app.StandardFontSize = Constants.defaultFontSize;

            setDefaultColumnWidths(worksheet);
            setDefaultRowHeights(worksheet);
            setDefaultWordWrapping(worksheet);
        }

        // TODO: Method for module description at the top of the module?
        private void CreateModuleDesc(string moduleDesc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates the section header. This is a row that spans across 2 columns and is used to physically
        /// mark the beginning of a new section. This operation consumes 2 rows (header + empty row). 
        /// Returns the current row after the operation.
        /// </summary>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="sectionLetter">The section letter.</param>
        /// <param name="sectionTitle">The section title.</param>
        /// <returns></returns>
        public int CreateSectionHeader(int startRow, int startCol, string sectionLetter, string sectionTitle)
        {
            int currentRow = startRow;
            createRow(currentRow++, startCol, sectionLetter, sectionTitle, RowType.SectionHeader);

            // Add an empty row below section header
            return ++currentRow;
        }

        /// <summary>
        /// Creates a new sheet with a section header for that new sheet. Returns the current row after the operation.
        /// </summary>
        /// <param name="sectionLetter">The section letter.</param>
        /// <param name="sectionTitle">The section title.</param>
        /// <returns></returns>
        public int CreateNewSheetSectionHeader(string moduleTitle, string sectionLetter, string sectionTitle)
        {
            CreateModule(moduleTitle);
            return CreateSectionHeader(Constants.defaultFirstRow, Constants.defaultFirstColumn, sectionLetter, sectionTitle);
        }

        /// <summary>
        /// Creates the string array of right column values for the info module of a <see cref="QuestionBlock"/>
        /// </summary>
        /// <param name="q">The question block object that contains the strings./param>
        /// <returns></returns>
        private string[] CreateInfoModuleRightColStrArr(QuestionBlock q)
        {
            var rightColStrArr = new string[]
            {
                q.QTitle,
                (q.BaseLabel + LanguageStringsUS.Whitespace + q.BaseDef).Trim(),
                q.Comments,
                q.ProgInst,
                q.RInst,
                q.QType,
                q.QText,
                q.RespInst
            };

            return rightColStrArr;
        }

        // assumes: rightColStrArray is aligned with RowType enumeration

        /// <summary>
        /// Generates the info rows within the <see cref="QuestionBlock" />. These are basically all the rows but the responses.
        /// Returns the current row after the operation.
        /// <para>
        /// Procedure: This basically enumerates through <see cref="QuestionBlockHeaders.QreRowObjs" /> and creates a row
        /// for each element that is part of the info module using <see cref="createRow(int, int, string, string, RowType)" /></para>
        /// </summary>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="qNum">The Question Number.</param>
        /// <param name="qBlock">The Question Block containing the strings for the right column.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// rightColStrArr - The passed string array is null. A non-null string array is required.
        /// or
        /// rightColStrArr - The passed string array does not contain 8 strings. Size: " + rightColStrArr.Length
        /// </exception>
        public int CreateInfoModule(int startRow, int startCol, QuestionBlock qBlock)
        {
            if (qBlock == null)
            {
                throw new ArgumentOutOfRangeException("qBlock",
                    "The passed QuestionBlock is null. A non-null QuestionBlock is required.");
            }
            string[] rightColStrArr = CreateInfoModuleRightColStrArr(qBlock);

            int currentRow = startRow;

            // Create dictionary of qre row objects with the right column strings included
            var qreRowObjs = QuestionBlockHeaders.CreateInfoModuleRowObjs(rightColStrArr);

            int i = 0;
            foreach (var rowType in qreRowObjs.Keys)
            {
                // skip the below as they are not part of the info module
                switch (rowType)
                {
                    case RowType.SectionHeader:
                    case RowType.Code_Generic:
                    case RowType.Code_Column:
                    case RowType.Code_Row:
                    case RowType.Response:
                    case RowType.Custom:
                    case RowType.None:
                        continue;
                    default:
                        createRow(currentRow++, Constants.defaultFirstColumn, qBlock.QNum, rightColStrArr[i++], rowType);
                        break;
                }
            }
            return currentRow;
        }

        ///// <summary>
        ///// Creates the string array of right column values for the response module of a <see cref="QuestionBlock"/>
        ///// </summary>
        ///// <param name="q">The question block object that contains the responses./param>
        ///// <returns></returns>
        //private List<string> CreateResponseModuleRightColStrArr(List<Response> responseList)
        //{
        //    var rightColStrArr = new List<string>();
        //    foreach (Response response in responseList)
        //    {
        //        rightColStrArr.Add(response.RText);
        //    }

        //    return rightColStrArr;
        //}

        // response options
        // counting assumes responseArray is in the form of:
        //      FIELD:      { GENERIC, "Special Response A", [...] }
        //      GENERIC:    { GENERIC, "Response A", "Response B", "Response C", [...] }
        //      ROW/COLUMN: { COLUMN, "Response A", "Response B", "Response C", [...], ROW, "Response A", "Response B", "Response C", [...] }

        /// <summary>
        /// Generates the response rows within the <see cref="QuestionBlock"/>. These are basically all the rows after the info rows.
        /// Returns the current row after the operation.
        /// <para>
        /// Procedure: Loops through the list of responses for a passed <see cref="QuestionBlock"/> object and creates
        /// rows for each of them after extracting the appropriate strings.
        /// </para>
        /// </summary>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <returns></returns>
        public int CreateResponseModule(int startRow, int startCol, QuestionBlock qBlock)
        {
            if (qBlock == null)
            {
                throw new ArgumentOutOfRangeException("qBlock",
                    "The passed QuestionBlock is null. A non-null QuestionBlock is required.");
            }
            List<Response> responses = qBlock.Children;

            int currentRow = startRow;

            foreach (Response resp in responses)
            {
                string leftColText, rightColText;
                // Set the leftColText. Be wary of special (internal) code values, which should be translated here.
                switch (resp.RCode)
                {
                    case Constants.ResponseCodes.Blank:
                        leftColText = LanguageStringsUS.Blank;
                        break;
                    case Constants.ResponseCodes.GenericCode:
                        leftColText = LanguageStringsUS.ResponseFlagResponses.Classify.Generic;
                        break;
                    case Constants.ResponseCodes.RowCode:
                        leftColText = LanguageStringsUS.ResponseFlagResponses.Classify.Row;
                        break;
                    case Constants.ResponseCodes.ColumnCode:
                        leftColText = LanguageStringsUS.ResponseFlagResponses.Classify.Column;
                        break;
                    default:
                        leftColText = resp.RCode.ToString();
                        break;
                }
                // Set the rightColText (simpler, right)
                rightColText = resp.RText;

                createRow(currentRow++, Constants.defaultFirstColumn, leftColText, rightColText, RowType.Response);
            }

            //for (int i = 0; i < rightColRespArr.Length; i++)
            //{
            //    string leftColText = "";
            //    string rightColText = "";
            //    RowType rowType = RowType.Response;

            //    switch (rightColRespArr[i])
            //    {
            //        // no value expected in right column
            //        case Constants.ResponseCodes.GENERIC:
            //            rowType = RowType.Code_Generic;
            //            currentCode = Constants.ResponseCodes.firstRow;
            //            break;
            //        case Constants.ResponseCodes.COLUMN:
            //            rowType = RowType.Code_Column;
            //            currentCode = Constants.ResponseCodes.firstColumn;
            //            break;
            //        case Constants.ResponseCodes.ROW:
            //            rowType = RowType.Code_Row;
            //            currentCode = Constants.ResponseCodes.firstRow;
            //            break;

            //        // special codes
            //        case Constants.SpecialResponses.DONT_KNOW:

            //        case Constants.SpecialResponses.PREFER_NO_ANSWER:

            //        case Constants.SpecialResponses.PREFER_NO_SAY:
            //            leftColText = Constants.ResponseCodes.Idk.ToString();
            //            rightColText = rightColRespArr[i];
            //            break;
            //        case Constants.SpecialResponses.OTHER:

            //        case Constants.SpecialResponses.OTHER_SPECIFY:
            //            leftColText = Constants.ResponseCodes.Other.ToString();
            //            rightColText = rightColRespArr[i];
            //            break;
            //        case Constants.SpecialResponses.ALL_OF_THE_ABOVE:
            //            leftColText = Constants.ResponseCodes.All.ToString();
            //            rightColText = rightColRespArr[i];
            //            break;
            //        case Constants.SpecialResponses.NONE_OF_THE_ABOVE:
            //            leftColText = Constants.ResponseCodes.None.ToString();
            //            rightColText = rightColRespArr[i];
            //            break;
            //        default:
            //            leftColText = currentCode.ToString();
            //            rightColText = rightColRespArr[i];
            //            currentCode++;
            //            break;
            //    }

            //    createRow(currentRow++, Constants.defaultFirstColumn, leftColText, rightColText, rowType);
            //}

            return currentRow;
        }


        // wraps a thick box border around the question block
        public void BorderQuestionBlock(int startRow, int endRow, int startCol)
        {
            string range = String.Format("{0}{1}:{2}{3}", Toolbox.GetExcelColumnName(startCol), startRow, Toolbox.GetExcelColumnName(startCol + 1), endRow);
            worksheetRange = worksheet.get_Range(range);
            formatCellBorder(worksheetRange, RowType.None);
        }

        // display the first sheet
        public void ShowFirstSheet()
        {
            worksheet = (Worksheet)workbook.Sheets[1];
            worksheet.Activate();
        }

        #endregion Public Methods

    }
}
