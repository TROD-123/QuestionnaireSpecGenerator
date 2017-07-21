using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSpecGenerator
{
    /// <summary>
    /// A simple helper class that links a <see cref="DataContainer"/> with the <see cref="ExcelGenerator"/> to create
    /// the Questionnaire Spec Excel Workbook. An instance of this class with a passed <see cref="DataContainer"/>
    /// object needs to be created before it can be used. Once instantiated, allows only read access of the 
    /// <see cref="DataContainer"/>, but does not provide write access.
    /// </summary>
    public class ExcelBuilder
    {
        private DataContainer Container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelBuilder"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ExcelBuilder(DataContainer container)
        {
            Container = container;
        }

        /// <summary>
        /// Generates the Questionnaire Spec Excel Workbook and projects the workbook on the screen.
        /// </summary>
        public void Generate()
        {
            ExcelGenerator excelApp = new ExcelGenerator();

            // keep track of current row for incrementing
            int currentRow = Constants.defaultFirstRow;
            // for tracking the first row of a question block
            int startRow = currentRow;
            // for tracking the last row of a question block
            int endRow = currentRow;

            // Module level
            List<Module> modules = Container.modules;
            foreach (Module module in modules)
            {
                bool sheetMade = false;
                String mTitle = module.MTitle;

                // Section level
                List<Section> sections = module.Children;
                foreach (Section section in sections)
                {
                    // Only create a new sheet for the first section of the module
                    if (!sheetMade)
                    {
                        currentRow = excelApp.CreateNewSheetSectionHeader(mTitle, section.SLetter, section.STitle);
                        sheetMade = true;
                    }
                    else
                    {
                        currentRow = excelApp.CreateSectionHeader(currentRow++, Constants.defaultFirstColumn, section.SLetter, section.STitle);
                    }

                    // Now that the section header is added, add the questions!

                    // Question level
                    List<QuestionBlock> questions = section.Children;
                    foreach (QuestionBlock question in questions)
                    {
                        startRow = currentRow;
                        // Add the info module
                        currentRow = excelApp.CreateInfoModule(currentRow, Constants.defaultFirstColumn, question);
                        // Add the response module
                        currentRow = excelApp.CreateResponseModule(currentRow, Constants.defaultFirstColumn, question);
                        // Border the question block
                        endRow = currentRow + Constants.rowCountOffset;
                        excelApp.BorderQuestionBlock(startRow, endRow, Constants.defaultFirstColumn);

                        // Increment current row
                        currentRow++;
                    }

                }
            }

            // open up the excel once all rows are constructed
            excelApp.SetWindowVisibility(true);
            excelApp.ShowFirstSheet(); // this only works after window is visible

            //excelApp.CleanUpExcelInteropObjs();
        }

        /// <summary>
        /// Gets the data container.
        /// </summary>
        /// <returns></returns>
        public DataContainer GetDataContainer()
        {
            return Container;
        }
    }
}
