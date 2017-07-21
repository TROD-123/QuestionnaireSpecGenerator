using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSpecGenerator
{
    // Programmer note: When adding flags, flag needs to be added in the enum and in the 
    // Response.SetResponseAttributes() method. ResponseFlagTypes and ResponseFlagConstants are already set.

    /// <summary>
    /// Display and behavioral attributes for each individual response. If the list of <see cref="ResponseFlags"/>
    /// is empty for a <see cref="Response"/>, then response is <c>Generic</c>.
    /// <para>Note: Because a list of <see cref="ResponseFlags"/> is defined, only one flag can be 
    /// selected within each "subclass" of flags.</para>
    /// </summary>
    public enum ResponseFlag
    {
        // DetRText: Auto sets the RText
        Other = 0,
        OtherSpec,
        None,
        All,
        PrefNoAnswer,
        Idk,
        OpenEnd,

        // Classify: Classifies responses as generic, row or column responses
        Generic = 10,
        RowResponse,
        ColumnResponse,

        // Anchor: Append [ANCHOR TEXT] to the RText
        AnchorBottom = 20,
        AnchorTop,
        AnchorLeft,
        AnchorCenter,
        AnchorRight,

        // Terminate: For terminate responses
        TermSelected = 100,
        TermNotSelected,

        // Misc: RFlags that can be combined with other flags
        MutuallyExclusive = 500,
        Test,
    }

    /// <summary>
    /// Used to define the boundaries of the <see cref="ResponseFlag"/> enumeration to prevent overlapping types.
    /// </summary>
    public enum ResponseFlagType
    {
        DetRText,
        Classify,
        Anchor,
        Terminate,
        Misc,
    }

    /// <summary>
    /// Defines upper bound enum values for each <see cref="ResponseFlag"/> types.
    /// </summary>
    internal class ResponseFlagConstants
    {
        internal const int DetRText = 9;
        internal const int Classify = 19;
        internal const int Anchor = 29;
        internal const int Terminate = 499;
        internal const int Misc = 999;
    }
}

