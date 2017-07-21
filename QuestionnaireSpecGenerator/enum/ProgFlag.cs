using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSpecGenerator
{
    // Programmer note: When adding Programming Flags, add them here, and also in 
    // QuestionBlock.GenerateProgFlagString(), and don't forget to add the LanguageStrings.ProgrammingFlags. 
    // Note when the "subclass" structure of NonADC changes, the change should also be reflected in 
    // LanguageStrings.ProgrammingFlags.NonADC

    /// <summary>
    /// The point of these Programming Instruction flags is to save users time from writing long,
    /// redundant and convoluted programming instructions in the spec. These flags are intended to
    /// auto-generate these PIs - users would simply have to pick and choose their relevant flags.
    /// These auto-generated PIs will append to PIs auto-generated from the <see cref="QuestionType"/>
    /// </summary>
    public class ProgFlag
    {
        #region NonADC

        /// <summary>
        /// Note: These are used for ADC questions. Please see <see cref="ProgFlagsNonADC"/> for Non-ADC
        /// question flags. For a question block, it is required that either <see cref="ProgFlagsNonADC"/> or
        /// <see cref="ProgFlagsADC"/> is not <c>null</c>.
        /// <para><b>Use:</b> These flags should be stored in a list. Multiple flags can be used.</para>
        /// </summary>
        public enum NonADC
        {
            // Response sort
            RandomizeRows = 0,
            RandomizeColumns,
            NoRandomizeRows,
            NoRandomizeColumns,
            SortAlphabeticalAscending,
            SortAlphabeticalDescending,
            KeepOrderFromQX, // extra field needed

            // Visual
            ShowRowCodes = 10,
            ShowColumnCodes,
            HideRowCodes,
            HideColumnCodes,
            ShowBrandLogos,
            NewScreenPerStatement,

            // OE
            ShowNumBoxes = 20, // extra field needed
            ForceNumBoxesOthersOptional, // extra field needed
            RecordNumMentionsSeparately, // extra field needed
            ForceAtLeastNumChars, // extra field needed
            ForceNumChars, // extra field needed
            AcceptBtwnXandY, // 2 extra fields needed

            // Selection
            MaxNumSelected = 30, // extra field needed
            SingleCodePerRow,
            SingleCodePerColumn,

            // Programming
            FlagStraightLiner = 100,
            IncludeInDQ,
            CodeOE,
            Quota, // extra field required

            // Custom
            Custom = 1000,
        }

        public enum NonADCFlagType
        {
            ResponseSort,
            Visual,
            OE,
            Selection,
            Programming,
            Custom
        }

        internal class NonADCFlagConstants
        {
            internal const int ResponseSort = 9;
            internal const int Visual = 19;
            internal const int OE = 29;
            internal const int Selection = 39;
            internal const int Programming = 999;
            internal const int Custom = 1000; // fixed value
        }

        #endregion

        #region ADC

        /// <summary>
        /// Note: These are used for ADC questions. Please see <see cref="ProgFlagsNonADC"/> for Non-ADC
        /// question flags. For a question block, it is required that either <see cref="ProgFlagsNonADC"/> or
        /// <see cref="ProgFlagsADC"/> is not <c>null</c>.
        /// <para><b>Use:</b> Unlike Non-ADC flags, only one ADC flag can be used for a question.</para>
        /// </summary>
        // TODO: "Advanced Flags" to define the specific configuration of ADC
        public enum ADC
        {
            None = 0,
            Gender, // overall hw, individual hw, font size
            SingleOpenTextBox, // overall hw, default text, font size, color
            MultipleOpenTextBox, // overall hw, default text for each box, # of boxes, font size, color
            HeartMatrix, // overall hw, hw of circle matrix, color, gradient, and # of rings
            BrandListLogoSelect,
            BrandListTextSelect,
            HorizontalScaleMultipleBrandLogos,
            HorizontalScaleSingleStatement,
            VerticalScaleDragDropLogos,
            MultipleSliderMultipleStatement // TO BE CONTINUED
        }

        #endregion
    }
}
