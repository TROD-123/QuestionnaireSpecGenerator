﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSpecGenerator
{
    /// <summary>
    ///  A class for a single response and its attributes.
    /// </summary>
    public class Response : QreObjBase
    {
        #region outward expressions

        /// <summary>
        /// The coded value of the response.
        /// </summary>
        public int RCode { get; set; }

        /// <summary>
        /// The text of the response as it appears to the respondent (e.g. "Male").
        /// </summary>
        public string RText { get; set; }

        /// <summary>
        /// The list of response flags that specify additional attributes to the response.
        /// </summary>
        public List<ResponseFlag> RFlags { get; set; }

        private List<ResponseFlagType> RFlagTypes;

        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="Response"/> class from being created. Used for deserialization
        /// by <see cref="JsonHandler"/>.
        /// </summary>
        private Response()
        {
            // NOTE: Beware to NOT call UpdateDate() when merely DESERIALIZING.
        }

        // How to treat open ends? Should open ends have their own response object for "[INSERT TEXT BOX]"?
        // What about auto generating codes and rTexts, based on the Response Flag or Question type? i.e. have less
        // reliance on static values? Codes to be automatically ordered, and only reserve manual code entry for
        // special response types

        // TODO: If there are multiple of the same code in a question...

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="pId">The parent identifier.</param>
        /// <param name="rFlags">The list of response flags. If null, assumes a <c>generic</c> respone.</param>
        /// <param name="rText">The response text. Blank by default. May be overridden by response flag.</param>
        /// <param name="rCode">The response code. -1 by default. May be overridden by response flag.</param>
        public Response(int pId, List<ResponseFlag> rFlags, string rText = LanguageStringsUS.Blank, int rCode = Constants.ResponseCodes.Blank)
        {
            DateCreated = DateTime.Now;

            ParentId = pId;
            RFlags = new List<ResponseFlag>();
            RFlagTypes = new List<ResponseFlagType>();

            // Add the flags
            if (rFlags != null)
            {
                foreach (ResponseFlag flag in rFlags)
                {
                    try
                    {
                        AddFlag(flag);
                    } catch (ArgumentException e)
                    {
                        Console.WriteLine(Toolbox.GenerateConsoleErrorMessage("Response Public Constructor", e));
                    }
                }
            }

            // Set response attributes based on RespondentFlags
            RCode = rCode;
            RText = rText;
            SetResponseAttributes();

            UpdateDate();
        }

        /// <summary>
        /// Adds the flag to the list of flags while also checking for overlapping flag types
        /// </summary>
        /// <param name="flag">The flag to add.</param>
        private void AddFlag(ResponseFlag flag)
        {
            // Get the flag type
            ResponseFlagType rType = Toolbox.GetResponseFlagType(flag);

            // Test to see if flag type is contained or not.
            if (RFlagTypes.Contains(rType))
            {
                if (rType != ResponseFlagType.Misc)
                {
                    throw new ArgumentException("The provided flag type already exists for this response. Flag: " + rType, "flag");
                }
            } 
            RFlagTypes.Add(rType);
            RFlags.Add(flag);            
        }

        /// <summary>
        /// Helper method to set the response attributes (RText, RCode) by perusing all the 
        /// <see cref="ResponseFlag"/> types against the response's list of <see cref="Response.RFlags"/>
        /// </summary>
        private void SetResponseAttributes()
        {
            // DetRText: Auto sets the RText and RCode (only one can be selected)
            if (RFlags.Contains(ResponseFlag.Other))
            {
                RText = LanguageStringsUS.ResponseFlagResponses.DetRText.Other;
                RCode = Constants.ResponseCodes.Other;

            }
            else if (RFlags.Contains(ResponseFlag.OtherSpec))
            {
                RText = LanguageStringsUS.ResponseFlagResponses.DetRText.OtherSpec;
                RCode = Constants.ResponseCodes.Other;
            }
            else if (RFlags.Contains(ResponseFlag.None))
            {
                RText = LanguageStringsUS.ResponseFlagResponses.DetRText.None;
                RCode = Constants.ResponseCodes.None;
            }
            else if (RFlags.Contains(ResponseFlag.All))
            {
                RText = LanguageStringsUS.ResponseFlagResponses.DetRText.All;
                RCode = Constants.ResponseCodes.All;
            }
            else if (RFlags.Contains(ResponseFlag.PrefNoAnswer))
            {
                RText = LanguageStringsUS.ResponseFlagResponses.DetRText.PreferNoAnswer;
                RCode = Constants.ResponseCodes.Idk;
            }
            else if (RFlags.Contains(ResponseFlag.Idk))
            {
                RText = LanguageStringsUS.ResponseFlagResponses.DetRText.Idk;
                RCode = Constants.ResponseCodes.Idk;
            }
            else if (RFlags.Contains(ResponseFlag.OpenEnd))
            {
                RText = LanguageStringsUS.ResponseFlagResponses.DetRText.OpenEnd;
                RCode = Constants.ResponseCodes.Blank;
            }

            // Classify: Classifies responses as generic, row or column responses (only one can be selected)
            if (RFlags.Contains(ResponseFlag.Generic))
            {
                RText = LanguageStringsUS.Blank;
                RCode = Constants.ResponseCodes.GenericCode;
            }
            else if (RFlags.Contains(ResponseFlag.RowResponse))
            {
                RText = LanguageStringsUS.Blank;
                RCode = Constants.ResponseCodes.RowCode;
            }
            else if (RFlags.Contains(ResponseFlag.ColumnResponse))
            {
                RText = LanguageStringsUS.Blank;
                RCode = Constants.ResponseCodes.ColumnCode;
            }

            // Anchor: Append [ANCHOR TEXT] to the RText (only one can be selected)
            if (RFlags.Contains(ResponseFlag.AnchorBottom))
            {
                RText += LanguageStringsUS.Whitespace + LanguageStringsUS.ResponseFlagResponses.Anchor.Bottom;
            }
            else if (RFlags.Contains(ResponseFlag.AnchorTop))
            {
                RText += LanguageStringsUS.Whitespace + LanguageStringsUS.ResponseFlagResponses.Anchor.Top;

            }
            else if (RFlags.Contains(ResponseFlag.AnchorLeft))
            {
                RText += LanguageStringsUS.Whitespace + LanguageStringsUS.ResponseFlagResponses.Anchor.Left;

            }
            else if (RFlags.Contains(ResponseFlag.AnchorCenter))
            {
                RText += LanguageStringsUS.Whitespace + LanguageStringsUS.ResponseFlagResponses.Anchor.Center;

            }
            else if (RFlags.Contains(ResponseFlag.AnchorRight))
            {
                RText += LanguageStringsUS.Whitespace + LanguageStringsUS.ResponseFlagResponses.Anchor.Right;

            }

            // Terminate: For terminate responses (only one can be selected)
            if (RFlags.Contains(ResponseFlag.TermSelected))
            {
                RText += LanguageStringsUS.Whitespace + LanguageStringsUS.ResponseFlagResponses.Terminate.TermSelected;

            }
            else if (RFlags.Contains(ResponseFlag.TermNotSelected))
            {
                RText += LanguageStringsUS.Whitespace + LanguageStringsUS.ResponseFlagResponses.Terminate.TermNotSelected;

            }

            // Misc: RFlags that can be combined with other flags
            if (RFlags.Contains(ResponseFlag.MutuallyExclusive))
            {
                RText += LanguageStringsUS.Whitespace + LanguageStringsUS.ResponseFlagResponses.Misc.MutuallyExclusive;
            } 
        }

        public override void UpdateDate()
        {
            DateModified = DateTime.Now;

            // TODO: Need to implement method that will store the changes user makes to the object
        }
    }
}