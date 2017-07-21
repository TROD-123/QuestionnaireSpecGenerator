using System;
using System.Collections.Generic;

namespace QuestionnaireSpecGenerator
{
    public class Constants
    {
        public class Colors
        {
            // #RGBs
            public const string NO_FILL = "";
            public const string LIGHT_GREEN = "#D6EAB0"; // 214, 234, 176
            public const string DARK_GREEN = "#99CA3C"; // 153, 202, 60
            public const string GREY = "#BFBFBF"; // 191, 191, 191
            public const string BLACK = "#000000"; // 0, 0, 0
            public const string WHITE = "#FFFFFF"; // 255, 255, 255
        }

        // currently assumes number of responses is 96 or less
        public class ResponseCodes
        {
            public const int firstRow = 1;
            public const int firstColumn = 1;

            public const int Blank = -1;
            public const int All = 96;
            public const int Idk = 97;
            public const int Other = 98;
            public const int None = 99;
            public const int GenericCode = 4094;
            public const int RowCode = 4095;
            public const int ColumnCode = 4096;
        }

        // Column sizes
        public const double leftColSize = 31;
        public const double rightColSize = 100;

        // row heights
        public const double defaultRowHeight = 14.25;
        public const double defaultSectionRowHeight = 27.75;

        // font
        public const string defaultFont = "Lucida Sans";
        public const double defaultFontSize = 10;
        public const double defaultSectionFontSize = 12;

        // firsts
        public const int defaultFirstRow = 1;
        public const int defaultFirstColumn = 1;
        public const int rowCountOffset = -1;
        public const int defaultColumnOffset = 1;

        // TODO: POTENTIALLY DEPRECATE- initial row counter values
        public const int bufferHeight = 1;
        public const int questionBlockInitHeight = 8 + bufferHeight;
        public const int sectionInitHeight = 1 + bufferHeight;
    }

    /// <summary>
    /// US-based language strings for user- and console-based output
    /// </summary>
    public class LanguageStringsUS
    {
        public const string Blank = "";
        public const string Whitespace = " ";
        public const string Ellipsis = "...";

        /// <summary>
        /// Console-based error output strings
        /// </summary>
        public class ConsoleError
        {
            public const string ThrewException = " threw an exception: ";
            public const string ActionProceed = "Proceeding";
            public const string ReturnValue = "Returning ";
        }

        /// <summary>
        /// User-based <see cref="QuestionBlock"/> header strings for the left column of the excel qre
        /// </summary>
        public class QuestionBlockHeaders
        {
            public const string BaseLabelDefinition = "Base Label/Definition";
            public const string Comments = "Comment/Advice";
            public const string ProgInst = "Programming Instructions";
            public const string RoutInst = "Routing Instructions";
            public const string QType = "Question Type";
            public const string QText = "Question Text";
            public const string RespInst = "Respondent instruction";
            public const string GenericCode = "CODE";
            public const string ColumnCode = "COLUMN CODE";
            public const string RowCode = "ROW CODE";
        }

        /// <summary>
        /// User-based <see cref="Response"/> strings
        /// </summary>
        public class ResponseFlagResponses
        {
            public class DetRText
            {
                public const string Other = "Other";
                public const string OtherSpec = "Other (please specify)";
                public const string None = "None of the above";
                public const string All = "All of the above";
                public const string PreferNoAnswer = "Prefer not to answer";
                public const string Idk = "Don't know";
                public const string OpenEnd = "[INSERT TEXT BOX]";
            }

            public class Classify
            {
                public const string Generic = "CODE";
                public const string Column = "COLUMN CODE";
                public const string Row = "ROW CODE";
            }

            public class Anchor
            {
                public const string Bottom = "[ANCHOR ON BOTTOM]";
                public const string Top = "[ANCHOR ON TOP]";
                public const string Left = "[ANCHOR LEFT]";
                public const string Center = "[ANCHOR IN CENTER]";
                public const string Right = "[ANCHOR RIGHT]";
            }

            public class Terminate
            {
                public const string TermSelected = "[TERM IF SELECTED]";
                public const string TermNotSelected = "[TERM IF NOT SELECTED]";
            }

            public class Misc
            {
                public const string MutuallyExclusive = "[MUTUALLY EXCLUSIVE]";
            }
        }

        /// <summary>
        /// User-based <see cref="ProgFlag"/> strings
        /// </summary>
        public class ProgrammingFlags
        {
            public class NonADC
            {
                public class ResponseSort
                {
                    public const string RandomizeRows = "Randomize rows.";
                    public const string RandomizeColumns = "Randomize columns.";
                    public const string NoRandomizeRows = "Do not randomize rows.";
                    public const string NoRandomizeColumns = "Do not randomize columns.";
                    public const string SortAlphabeticalAscending = "Sort in ascending alphabetical order.";
                    public const string SortAlphabeticalDescending = "Sort in descending alphabetical order.";

                    public static string KeepOrderFromQX(string qNum)
                    {
                        return String.Format("Keep order from {0}.", qNum);
                    }
                }

                public class Visual
                {
                    public const string ShowRowCodes = "Show row codes.";
                    public const string ShowColumnCodes = "Show column codes.";
                    public const string HideRowCodes = "Hide row codes.";
                    public const string HideColumnCodes = "Hide column codes";
                    public const string ShowBrandLogos = "Show brand logos.";
                    public const string NewScreenPerStatement = "Show each statement on a new screen.";
                }

                public class OE
                {
                    public static string ShowNumBoxes(int num)
                    {
                        if (num == 1)
                        {
                            return "Show 1 text box.";
                        }
                        else if (num > 1)
                        {
                            return String.Format("Show {0} text boxes.", num);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("num", "Invalid input number passed: " + num);
                        }
                    }

                    public static string ForceNumBoxesOthersOptional(int num)
                    {
                        if (num == 1)
                        {
                            return "Force first mention; others optional.";
                        }
                        else if (num > 1)
                        {
                            return String.Format("Force first {0} mentions; others optional.", num);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("num", "Invalid input number passed: " + num);
                        }
                    }

                    public static string RecordNumMentionsSeparately(int num)
                    {
                        if (num == 1)
                        {
                            return "Record first mention separately.";
                        }
                        else if (num > 1)
                        {
                            return String.Format("Record first {0} mentions separately.", num);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("num", "Invalid input number passed: " + num);
                        }
                    }

                    public static string ForceAtLeastNumChars(int num)
                    {
                        if (num >= 1)
                        {
                            return String.Format("Force at least {0} {1}.", num, Toolbox.ConvertStringSingularOrPlural("character", num));
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("num", "Invalid input number passed: " + num);
                        }
                    }

                    public static string ForceNumChars(int num)
                    {
                        if (num >= 1)
                        {
                            return String.Format("Force {0} {1}.", num, Toolbox.ConvertStringSingularOrPlural("character", num));
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("num", "Invalid input number passed: " + num);
                        }
                    }

                    public static string AcceptBtwnXandY(int num1, int num2)
                    {
                        if (num2 > num1)
                        {
                            return String.Format("Only accept values between {0} and {1}.", num1, num2);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("num2", "Second number must be greater than first." +
                                "First input: " + num1 + " Second input: " + num2);
                        }
                    }
                }

                public class Selection
                {
                    public static string MaxNumSelected(int num)
                    {
                        if (num >= 1)
                        {
                            return String.Format("Allow selection up to {0} MAX.", num);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("num", "Invalid input number passed: " + num);
                        }
                    }

                    public const string SingleCodePerRow = "Single code per row statement.";
                    public const string SingleCodePerColumn = "Single code per column statement.";
                }

                public class Programming
                {
                    public const string FlagStraightLiner = "Flag straightliner respondents.";
                    public const string IncludeInDQ = "Include question in DQ report.";
                    public const string CodeOE = "Code OEs.";

                    public static string Quota(string quota)
                    {
                        return String.Format("QUOTA: {0}.", quota);
                    }
                }
            }

            public class ADC
            {

            }
        }

        /// <summary>
        /// User-based respondent instruction strings
        /// </summary>
        public class RespondentInstructions
        {
            public class NonADCDefaults
            {
                public const string SingleCode = "Please select one answer below.";
                public const string MultiCode = "Please select all that apply.";
                public string MultiCodeMaxSelect(int num)
                {
                    if (num >= 1)
                    {
                        return String.Format("Please select all that apply, up to {0} at most.", num);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("num", "Invalid input number passed: " + num);
                    }
                }
                public const string SingleTextField = "Please enter your answer below.";
                public const string MultiTextField = "Please enter your answers in the boxes below, one answer per box only.";
                public const string Grid = "Please select one answer for each statement.";
            }

            public class Special
            {
                public const string AgeOE = "Please enter your age in years in the box below.";
                public const string ZipOE = "Please enter your 5 digit zip code in the box below.";
                public const string RegionSingleCode = "Please select the region that applies to you.";

            }
        }

        /// <summary>
        /// User-based routing instruction strings
        /// </summary>
        public class RoutingInstructions
        {
            public const string NextQuestion = "Next question";
            public const string SkipTo = "Skip to ";
            public const string EndSurvey = "End survey";
        }

        /// <summary>
        /// User-based question type strings
        /// </summary>
        public class QuestionType
        {
            public const string BreakScreen = "Break Screen.";
            public const string SingleCode = "Single code. Radio buttons.";
            public const string MultiCode = "Multi code. Checkboxes.";
            public const string BrandTextField = "Brand OE.";
            public const string FullTextField = "Full OE.";
            public const string NumericTextField = "Numeric OE.";
            public const string MultiTextField = "Multiple OE.";
            public const string HorizontalScale = "Horizontal Scale.";
            public const string Grid = "Grid.";
            public const string Marker = "Mark respondents based on the below.";
        }
    }

    /// <summary>
    /// Contains a dictionary that holds attributes for each row in the <see cref="QuestionBlock"/>, including
    /// left column strings
    /// </summary>
    public class QuestionBlockHeaders
    {
        public static Dictionary<RowType, QreRowObj> QreRowObjs = new Dictionary<RowType, QreRowObj>()
        {
            { RowType.SectionHeader, new QreRowObj()
                { LeftColString = LanguageStringsUS.Blank,
                  TextFormat = TextFormat.Bold,
                  RowColor = Constants.Colors.DARK_GREEN }
            },
            { RowType.QuestionNumTitle, new QreRowObj()
                { LeftColString = LanguageStringsUS.Blank,
                  TextFormat = TextFormat.Bold,
                  RowColor = Constants.Colors.NO_FILL }
            },
            { RowType.BaseDefinition, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.BaseLabelDefinition,
                  TextFormat = TextFormat.Normal,
                  RowColor = Constants.Colors.LIGHT_GREEN }
            },
            { RowType.Comments, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.Comments,
                  TextFormat = TextFormat.Normal,
                  RowColor = Constants.Colors.GREY }
            },
            { RowType.ProgrammingInstruction, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.ProgInst,
                  TextFormat = TextFormat.Normal,
                  RowColor = Constants.Colors.LIGHT_GREEN }
            },
            { RowType.RoutingInstruction, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.RoutInst,
                  TextFormat = TextFormat.Normal,
                  RowColor = Constants.Colors.LIGHT_GREEN }
            },
            { RowType.QuestionType, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.QType,
                  TextFormat = TextFormat.Normal,
                  RowColor = Constants.Colors.LIGHT_GREEN }
            },
            { RowType.QuestionText, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.QText,
                  TextFormat = TextFormat.Bold,
                  RowColor = Constants.Colors.NO_FILL }
            },
            { RowType.RespondentInstruction, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.RespInst,
                  TextFormat = TextFormat.Italics,
                  RowColor = Constants.Colors.NO_FILL }
            },
            { RowType.Code_Generic, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.GenericCode,
                  TextFormat = TextFormat.Bold,
                  RowColor = Constants.Colors.NO_FILL }
            },
            { RowType.Code_Column, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.ColumnCode,
                  TextFormat = TextFormat.Bold,
                  RowColor = Constants.Colors.NO_FILL }
            },
            { RowType.Code_Row, new QreRowObj()
                { LeftColString = LanguageStringsUS.QuestionBlockHeaders.RowCode,
                  TextFormat = TextFormat.Bold,
                  RowColor = Constants.Colors.NO_FILL }
            },
            { RowType.Response, new QreRowObj()
                { LeftColString = LanguageStringsUS.Blank,
                  TextFormat = TextFormat.Normal,
                  RowColor = Constants.Colors.NO_FILL }
            },
            { RowType.Custom, new QreRowObj()
                { LeftColString = LanguageStringsUS.Blank,
                  TextFormat = TextFormat.Normal,
                  RowColor = Constants.Colors.NO_FILL }
            },
            { RowType.None, new QreRowObj()
                { LeftColString = LanguageStringsUS.Blank,
                  TextFormat = TextFormat.Normal,
                  RowColor = Constants.Colors.NO_FILL }
            }
        };

        // Programmer note: This method assumes that string[] rightColStrArr follows the same structure as RowType,
        // except for the skipped elements. This can potentially cause problems?

        /// <summary>
        /// Creates the qre row objs for the info module of the <see cref="QuestionBlock"/>
        /// </summary>
        /// <param name="rightColStrArr">A string array holding all the right column values. Note, this starts from
        /// <see cref="RowType.QuestionNumTitle"/> and NOT from <see cref="RowType.SectionHeader"/>, and stops after
        /// <see cref="RowType.RespondentInstruction"/>.</param>
        /// <returns></returns>
        public static Dictionary<RowType, QreRowObj> CreateInfoModuleRowObjs(string[] rightColStrArr)
        {
            // Hold a workable reference to the dictionary
            var qreRowObjs = QreRowObjs;

            // Iterate through the dictionary and insert the Right Column strings into each RowType
            int i = 0;
            foreach (var row in qreRowObjs)
            {
                // skip the below cases as they are not part of the info module
                switch (row.Key)
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
                        row.Value.RightColString = rightColStrArr[i++];
                        break;
                }
            }
            return qreRowObjs;
        }
    }

    public enum RowType
    {
        SectionHeader = 0,
        QuestionNumTitle,
        BaseDefinition,
        Comments,
        ProgrammingInstruction,
        RoutingInstruction,
        QuestionType,
        QuestionText,
        RespondentInstruction,
        Code_Generic,
        Code_Column,
        Code_Row,
        Response,
        Custom,
        None = -4096
    }

    public enum TextFormat
    {
        Normal = 0,
        Bold,
        Italics,
        Strikethrough
    }

    //// Arrays ordered based on RowType enum
    //// TODO: Turn this into a dictionary
    //public class RowTypeArrays
    //{
    //    // left column strings
    //    public static string[] leftColStrings =
    //    {
    //        "Section Title", // placeholder, should not show
    //        "Question Number", // placeholder, should not show
    //        "Base Label/Definition",
    //        "Comment/Advice",
    //        "Programming Instructions",
    //        "Routing Instructions",
    //        "Question Type",
    //        "Question Text",
    //        "Respondent instruction",
    //        "CODE",
    //        "COLUMN CODE",
    //        "ROW CODE",
    //        "Response Code", // placeholder, should not show
    //        "Custom text" // placeholder, should not show
    //    };

    //    // bold cells
    //    public static TextFormat[] textFormats =
    //    {
    //        TextFormat.Bold,
    //        TextFormat.Bold,
    //        TextFormat.Normal,
    //        TextFormat.Normal,
    //        TextFormat.Normal,
    //        TextFormat.Normal,
    //        TextFormat.Normal,
    //        TextFormat.Bold,
    //        TextFormat.Italics,
    //        TextFormat.Bold,
    //        TextFormat.Bold,
    //        TextFormat.Bold,
    //        TextFormat.Normal,
    //        TextFormat.Normal
    //    };

    //    // row colors
    //    public static string[] cellColors =
    //    {
    //        Constants.Colors.DARK_GREEN,
    //        Constants.Colors.NO_FILL,
    //        Constants.Colors.LIGHT_GREEN,
    //        Constants.Colors.GREY,
    //        Constants.Colors.LIGHT_GREEN,
    //        Constants.Colors.LIGHT_GREEN,
    //        Constants.Colors.LIGHT_GREEN,
    //        Constants.Colors.NO_FILL,
    //        Constants.Colors.NO_FILL,
    //        Constants.Colors.NO_FILL,
    //        Constants.Colors.NO_FILL,
    //        Constants.Colors.NO_FILL,
    //        Constants.Colors.NO_FILL,
    //        Constants.Colors.NO_FILL
    //    };
    //}
}
