using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSpecGenerator
{
    /// <summary>
    ///  This class represents a single question, containing its attributes as well as its list
    ///  of possible responses.
    /// </summary>
    public class QuestionBlock : QuestionnaireObject<Response>
    {
        #region outward expressions

        /// <summary>
        /// The question number. 
        /// <para>This field is <b>required</b> (can't be <c>null</c>).</para>
        /// </summary>
        public string QNum { get; set; } // required

        /// <summary>
        /// The title of the question.
        /// <para>This field is <b>required</b> (can't be <c>null</c>).</para>
        /// </summary>
        public string QTitle { get; set; } // required

        /// <summary>
        /// The base label, i.e. the user-friendly label stating for whom this question is asked 
        ///  (e.g. "All respondents", or "Male respondents"). 
        /// <para>This field is <b>required</b> (can't be <c>null</c>).</para>
        /// <para>
        /// Note: If there is a special <c>baseLabel</c>, then a <c>baseDef</c> is appended to the end of 
        ///  <c>baseLabel</c> in the <c>baseLabel</c> field in the questionnaire spec (e.g. "Male respondents: 
        ///  Q1 = 1 ['Males']").
        /// </para>
        /// </summary>
        public string BaseLabel { get; set; } // required

        /// <summary>
        /// The programmer base definition, i.e. to whom this question is asked. This is how <c>baseLabel</c> is
        /// defined for the programmers (e.g. "Q1 = 1 ['Males']"). This definition is appended to the <c>baseLabel</c>
        /// field in the questionnaire spec (e.g. "Male respondents: Q1 = 1 ['Males']").
        /// <para>This field is <b>optional</b> (can be <c>null</c>).</para>
        /// <para>Note: If there is no special <c>baseLabel</c>, then this can be left <c>null</c>.</para>
        /// </summary>
        public string BaseDef { get; set; } // optional

        /// <summary>
        /// Relevant comments or advice to show in the <c>comments</c> field in the questionnaire spec.
        /// <para>
        /// This field is <b>optional</b> (can be <c>null</c>). If <c>null</c> then this field is hidden from
        /// the questionnaire spec.
        /// </para>
        /// </summary>
        public string Comments { get; set; } // optional

        /// <summary>
        /// This describes how the question should be shown to the respondent, including:
        /// <list type="bullet">
        ///     <item><description>How statements should be displayed</description></item>
        ///     <item><description>Use of a text box, and its attributes</description></item>
        ///     <item><description>Termination instructions</description></item>
        ///     <item><description>Response coding instructions</description></item>
        ///     <item><description>Piping instructions</description></item>
        /// </list>
        /// <para>
        /// Note: Because there can be several components to a PI, each of these properties are abstracted via <see cref="ProgFlag.NonADC"/>
        /// which are used to auto-generate this programming instruction string. This is typically chosen auto-generated, 
        /// but can also be user-defined.
        /// </para>
        /// </summary>
        public string ProgInst { get; set; }

        /// <summary>
        /// The list of programming flags for Non ADC questions. This list generates the <see cref="ProgInst"/> string.
        /// <para>
        /// Note: If this question is ADC, this must be set to <c>null</c>. At least one of <see cref="ProgFlag.NonADC"/> or
        /// <see cref="ProgFlag.ADC"/> must not be <c>null.</c>
        /// </para>
        /// </summary>
        public List<ProgFlag.NonADC> ProgFlagsNonADC { get; set; }

        /// <summary>
        /// The list of programming flags for ADC questions. This list generates the <see cref="ProgInst"/> string.
        /// <para>
        /// Note: If this question is not ADC, this must be set to <see cref="ProgFlag.ADC.None"/>. 
        /// At least one of <see cref="ProgFlag.NonADC"/> or <see cref="ProgFlag.ADC"/> must not be <c>null.</c>
        /// </para>
        /// </summary>
        public ProgFlag.ADC ProgFlagADC { get; set; }

        /// <summary>
        /// The routing flag used to generate the <see cref="RInst"/> string.
        /// </summary>
        public RoutingFlag RFlag { get; set; }

        /// <summary>
        /// Describes which question or actions should follow after the current question. This is typically chosen auto-generated, 
        /// but can also be user-defined.
        /// </summary>
        public string RInst { get; set; }

        /// <summary>
        /// The question type flag used to generate the <see cref="QType"/> string.
        /// </summary>
        public QuestionType QTypeInt { get; set; }

        /// <summary>
        /// Details the <see cref="QuestionType"/>, which translates the <see cref="QuestionType"/>
        /// flag defined in <see cref="QTypeInt"/>. This is typically auto-generated, but can also be user-defined.
        /// </summary>
        public string QType { get; set; }

        /// <summary>
        /// The question text that is shown to the respondent (e.g. "What is your gender?"). This is typically <b>user-defined</b>.
        /// </summary>
        public string QText { get; set; }

        /// <summary>
        /// The instruction for the respondent (e.g. "Please select one answer below."). This is typically chosen from a bank
        /// of respondent intsructions, but can also be user-defined.
        /// </summary>
        public string RespInst { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Prevents a default instance of the <see cref="QuestionBlock"/> class from being created. Used for deserialization
        /// by <see cref="JsonHandler"/>.
        /// </summary>
        private QuestionBlock()
        {
            // NOTE: Beware to NOT call UpdateDate() when merely DESERIALIZING.
        }

        // TODO: Default values of the prog flags non adc tuple should be -1 for ints and null for strings. They won't be read
        // anyway depending on the prog flag passed.

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionBlock" /> class.
        /// </summary>
        /// <param name="pId">The parent identifier (<b>required</b>).</param>
        /// <param name="qNum">The question number.</param>
        /// <param name="qTitle">The question title.</param>
        /// <param name="baseLabel">The base label.</param>
        /// <param name="baseDef">The base definition.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="progFlagsNonADC">The list of Non-ADC programming flags. Used to set the <see cref="ProgInst"/> string.</param>
        /// <param name="progFlagADC">The ADC programming flag. Used to set the <see cref="ProgInst"/> string.</param>
        /// <param name="rFlag">The routing flag. Used to set the <see cref="RInst" /> string.</param>
        /// <param name="qTypeInt">The question type. Used to set the <see cref="QType" /> string.</param>
        /// <param name="qText">The question text.</param>
        /// <param name="respInst">The respondent instruction.</param>
        /// <param name="responses">The list of responses.</param>
        /// <param name="customProgInst">The programming instruction string. Default is null, for auto-generation based on
        /// <see cref="QuestionBlock.ProgFlag.ADC"/> or <see cref="QuestionBlock.ProgFlag.NonADC"/>.</param>
        /// <param name="customRoutingInst">The routing instruction string. Default is null, for auto-generation based
        /// on <see cref="QuestionBlock.RFlag" /></param>
        /// <param name="customQType">The question type string. Default is null, for auto-generation based
        /// on <see cref="QuestionBlock.QTypeInt" /></param>
        public QuestionBlock(int pId, string qNum, string qTitle,
            string baseLabel, string baseDef, string comments,
            List<Tuple<ProgFlag.NonADC, int, int, string>> progFlagsNonADC, ProgFlag.ADC progFlagADC, 
            RoutingFlag rFlag, QuestionType qTypeInt, string qText, string respInst, List<Response> responses, 
            string customProgInst = null, string customRoutingInst = null, string customQType = null)
        {
            DateCreated = DateTime.Now;

            ParentId = pId;
            QNum = qNum;
            QTitle = qTitle;
            BaseLabel = baseLabel;
            BaseDef = baseDef;
            Comments = comments;
            
            RFlag = rFlag;
            QTypeInt = qTypeInt;
            QText = qText;

            // TODO: Dynamically generate this the same way as the programming instruction string
            RespInst = respInst;

            // Generate or set the routing instruction string
            if (customRoutingInst == null)
            {
                try
                {
                    RInst = GenerateRoutingInstructionString(rFlag);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(Toolbox.GenerateConsoleErrorMessage("QuestionBlock Constructor: Generate Routing Instruction", e));
                }
            } else
            {
                RInst = customRoutingInst;
            }

            // Generate the programming instruction string
            try
            {
                ProgInst = GenerateProgInstString(qTypeInt, customQType, progFlagsNonADC, progFlagADC, customProgInst);
            } catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(Toolbox.GenerateConsoleErrorMessage("QuestionBlock Constructor: Generate Programming Instruction", e));
            }

            // Add the responses
            Children = new List<Response>();
            if (responses != null)
            {
                foreach (Response response in responses)
                {
                    AddChild(response);
                }
            }
            UpdateDate();
        }

        /// <summary>
        /// Generates the programming instruction sting by forming and combining the <see cref="QuestionType"/>
        /// string with the <see cref="ProgFlag"/> instruction string.
        /// </summary>
        /// <param name="qTypeInt">The question type</param>
        /// <param name="customQType">A custom question type string</param>
        /// <param name="progFlagsNonADC">The list of Non-ADC programming flags and their attributes.</param>
        /// <param name="customProgInst">A custom programming instruction string.</param>
        /// <param name="progFlagADC">The ADC programming flag.</param>
        /// <returns></returns>
        public string GenerateProgInstString(QuestionType qTypeInt, string customQType, 
            List<Tuple<ProgFlag.NonADC, int, int, string>> progFlagsNonADC, ProgFlag.ADC progFlagADC, string customProgInst)
        {
            // Generate the question type string
            if (customQType == null)
            {
                try
                {
                    QType = GenerateQuestionTypeString(qTypeInt);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    throw e;
                }
            }
            else
            {
                QType = customQType;
            }

            // Generate the programming instruction appendage
            string progInstTemp = LanguageStringsUS.Blank;
            try
            {
                if (progFlagsNonADC != null)
                {
                    progInstTemp = GenerateNonADCProgInstString(progFlagsNonADC, customProgInst);
                }
                else if (progFlagADC != ProgFlag.ADC.None)
                {
                    progInstTemp = GenerateADCProgInstString(progFlagADC);

                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }
            return (QType + LanguageStringsUS.Whitespace + progInstTemp).Trim();
        }

        /// <summary>
        /// Generates the question type string.
        /// </summary>
        /// <param name="qType">Type of the q.</param>
        private string GenerateQuestionTypeString(QuestionType qType)
        {
            switch (qType)
            {
                case QuestionType.BreakScreen:
                    return LanguageStringsUS.QuestionType.BreakScreen;
                case QuestionType.SingleCode:
                    return LanguageStringsUS.QuestionType.SingleCode;
                case QuestionType.MultiCode:
                    return LanguageStringsUS.QuestionType.MultiCode;
                case QuestionType.BrandTextField:
                    return LanguageStringsUS.QuestionType.BrandTextField;
                case QuestionType.FullTextField:
                    return LanguageStringsUS.QuestionType.FullTextField;
                case QuestionType.NumericTextField:
                    return LanguageStringsUS.QuestionType.NumericTextField;
                case QuestionType.MultiTextField:
                    return LanguageStringsUS.QuestionType.MultiTextField;
                case QuestionType.HorizontalScale:
                    return LanguageStringsUS.QuestionType.HorizontalScale;
                case QuestionType.Grid:
                    return LanguageStringsUS.QuestionType.Grid;
                case QuestionType.Marker:
                    return LanguageStringsUS.QuestionType.Marker;
                default:
                    throw new ArgumentOutOfRangeException("qType", "The passed QuestionType is invalid: " + qType);
            }
        }

        /// <summary>
        /// Generates the Non-ADC programming instruction string.
        /// </summary>
        /// <param name="progFlagsNonADC">A quadruple containing the list of Non-ADC programming flags along with their extra params</param>
        /// <returns></returns>
        private string GenerateNonADCProgInstString(List<Tuple<ProgFlag.NonADC, int, int, string>> progFlagsNonADC, string custom = null)
        {
            string NonADCProgInstString = "";
            List<ProgFlag.NonADC> flags = progFlagsNonADC.Select(t => t.Item1).ToList();

            // Response sort
            if (flags.Contains(ProgFlag.NonADC.RandomizeRows))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.ResponseSort.RandomizeRows 
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.RandomizeColumns))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.ResponseSort.RandomizeColumns 
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.NoRandomizeRows))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.ResponseSort.NoRandomizeRows 
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.NoRandomizeColumns))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.ResponseSort.NoRandomizeColumns 
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.SortAlphabeticalAscending))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.ResponseSort.SortAlphabeticalAscending
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.SortAlphabeticalDescending))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.ResponseSort.SortAlphabeticalDescending
                    + LanguageStringsUS.Whitespace;
            }
            // KeepOrderFromQX uses Item4 (String) from Tuple
            if (flags.Contains(ProgFlag.NonADC.KeepOrderFromQX))
            {
                int index = flags.IndexOf(ProgFlag.NonADC.KeepOrderFromQX);
                string qNum = progFlagsNonADC[index].Item4;
                string inst = LanguageStringsUS.ProgrammingFlags.NonADC.ResponseSort.KeepOrderFromQX(qNum);
                NonADCProgInstString += inst + LanguageStringsUS.Whitespace;
            }

            // Visual
            if (flags.Contains(ProgFlag.NonADC.ShowRowCodes))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Visual.ShowRowCodes
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.ShowColumnCodes))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Visual.ShowColumnCodes
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.HideRowCodes))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Visual.HideRowCodes
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.HideColumnCodes))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Visual.HideColumnCodes
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.ShowBrandLogos))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Visual.ShowBrandLogos
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.NewScreenPerStatement))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Visual.NewScreenPerStatement
                    + LanguageStringsUS.Whitespace;
            }

            // OE
            // ShowNumBoxes uses Item2 (int) from Tuple
            if (flags.Contains(ProgFlag.NonADC.ShowNumBoxes))
            {
                int index = flags.IndexOf(ProgFlag.NonADC.ShowNumBoxes);
                int num = progFlagsNonADC[index].Item2;
                string inst = LanguageStringsUS.ProgrammingFlags.NonADC.OE.ShowNumBoxes(num);
                NonADCProgInstString += inst + LanguageStringsUS.Whitespace;
            }
            // ForceNumBoxesOthersOptional uses Item2 (int) from Tuple
            if (flags.Contains(ProgFlag.NonADC.ForceNumBoxesOthersOptional))
            {
                int index = flags.IndexOf(ProgFlag.NonADC.ForceNumBoxesOthersOptional);
                int num = progFlagsNonADC[index].Item2;
                string inst = LanguageStringsUS.ProgrammingFlags.NonADC.OE.ForceNumBoxesOthersOptional(num);
                NonADCProgInstString += inst + LanguageStringsUS.Whitespace;
            }
            // RecordNumMentionsSeparately uses Item2 (int) from Tuple
            if (flags.Contains(ProgFlag.NonADC.RecordNumMentionsSeparately))
            {
                int index = flags.IndexOf(ProgFlag.NonADC.RecordNumMentionsSeparately);
                int num = progFlagsNonADC[index].Item2;
                string inst = LanguageStringsUS.ProgrammingFlags.NonADC.OE.RecordNumMentionsSeparately(num);
                NonADCProgInstString += inst + LanguageStringsUS.Whitespace;
            }
            // ForceAtLeastNumChars uses Item2 (int) from Tuple
            if (flags.Contains(ProgFlag.NonADC.ForceAtLeastNumChars))
            {
                int index = flags.IndexOf(ProgFlag.NonADC.ForceAtLeastNumChars);
                int num = progFlagsNonADC[index].Item2;
                string inst = LanguageStringsUS.ProgrammingFlags.NonADC.OE.ForceAtLeastNumChars(num);
                NonADCProgInstString += inst + LanguageStringsUS.Whitespace;
            }
            // ForceNumChars uses Item2 (int) from Tuple
            if (flags.Contains(ProgFlag.NonADC.ForceNumChars))
            {
                int index = flags.IndexOf(ProgFlag.NonADC.ForceNumChars);
                int num = progFlagsNonADC[index].Item2;
                string inst = LanguageStringsUS.ProgrammingFlags.NonADC.OE.ForceNumChars(num);
                NonADCProgInstString += inst + LanguageStringsUS.Whitespace;
            }
            // AcceptBtwnXandY uses Item2 (int) and Item3 from Tuple
            if (flags.Contains(ProgFlag.NonADC.AcceptBtwnXandY))
            {
                int index = flags.IndexOf(ProgFlag.NonADC.AcceptBtwnXandY);
                int num1 = progFlagsNonADC[index].Item2;
                int num2 = progFlagsNonADC[index].Item3;
                string inst = LanguageStringsUS.ProgrammingFlags.NonADC.OE.AcceptBtwnXandY(num1, num2);
                NonADCProgInstString += inst + LanguageStringsUS.Whitespace;
            }

            // Selection
            // MaxNumSelected uses Item2 (int) from Tuple
            if (flags.Contains(ProgFlag.NonADC.MaxNumSelected))
            {
                int index = flags.IndexOf(ProgFlag.NonADC.MaxNumSelected);
                int num = progFlagsNonADC[index].Item2;
                string inst = LanguageStringsUS.ProgrammingFlags.NonADC.Selection.MaxNumSelected(num);
                NonADCProgInstString += inst + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.SingleCodePerRow))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Selection.SingleCodePerRow
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.SingleCodePerColumn))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Selection.SingleCodePerColumn
                    + LanguageStringsUS.Whitespace;
            }

            // Programming
            if (flags.Contains(ProgFlag.NonADC.FlagStraightLiner))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Programming.FlagStraightLiner
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.IncludeInDQ))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Programming.IncludeInDQ
                    + LanguageStringsUS.Whitespace;
            }
            if (flags.Contains(ProgFlag.NonADC.CodeOE))
            {
                NonADCProgInstString += LanguageStringsUS.ProgrammingFlags.NonADC.Programming.CodeOE
                    + LanguageStringsUS.Whitespace;
            }
            // Quota uses Item4 (string) from Tuple
            if (flags.Contains(ProgFlag.NonADC.Quota))
            {
                int index = flags.IndexOf(ProgFlag.NonADC.Quota);
                string quota = progFlagsNonADC[index].Item4;
                string inst = LanguageStringsUS.ProgrammingFlags.NonADC.Programming.Quota(quota);
                NonADCProgInstString += inst + LanguageStringsUS.Whitespace;
            }

            // Custom. Only add the custom prog inst text if the NonADC Custom flag is checked (which should be default)
            if (flags.Contains(ProgFlag.NonADC.Custom))
            {
                NonADCProgInstString += custom;
            }

            ProgFlagsNonADC = flags;
            return NonADCProgInstString.Trim();           
        }

        // TODO: For ADC programming flag, need to also include a set of custom parameters for the ADC
        /// <summary>
        /// Generates the ADC programming instuction string.
        /// </summary>
        /// <param name="progFlagsADC">The ADC programming flag.</param>
        /// <returns></returns>
        private string GenerateADCProgInstString(ProgFlag.ADC progFlagADC)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates the routing instruction string.
        /// </summary>
        /// <param name="rFlag">The r flag.</param>
        private string GenerateRoutingInstructionString(RoutingFlag rFlag)
        {
            switch (rFlag)
            {
                case RoutingFlag.NextQuestion:
                    return LanguageStringsUS.RoutingInstructions.NextQuestion;
                case RoutingFlag.SkipTo:
                    return LanguageStringsUS.RoutingInstructions.SkipTo;
                case RoutingFlag.EndSurvey:
                    return LanguageStringsUS.RoutingInstructions.EndSurvey;
                default:
                    throw new ArgumentOutOfRangeException("rFlag", "The passed RoutingFlag is invalid: " + rFlag);
            }
        }

        // TODO: REMOVE FROM HERE ONCE IMPLEMENTED IN BASE CLASS
        //public void ChangeParent(int newPId)
        //{
        //    // Need to get the current section and the target sections, and modify the question lists from there
        //    int oldPId = ParentId;

        //    //Section oldParent = DataContainer.GetSectionById(oldPId);
        //    //Section newParent = DataContainer.GetSectionById(newPId);

        //    //oldParent.RemoveQuestion(this);
    
        //    // will also need to reflect the changed parent id here
        //    ParentId = newPId;
        //    //newParent.AddQuestion(this);
        //}   

        #endregion
    }
}
