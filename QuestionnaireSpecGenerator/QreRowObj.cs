using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSpecGenerator
{
    /// <summary>
    /// Holds row properties for the Excel Generator
    /// </summary>
    public class QreRowObj
    {
        public String LeftColString { get; set; }
        public String RightColString { get; set; }
        public TextFormat TextFormat { get; set; }
        public String RowColor { get; set; }

        public QreRowObj()
        {

        }

        public QreRowObj(String leftColString, String rightColString, TextFormat textFormat, String rowColor)
        {
            LeftColString = leftColString;
            RightColString = RightColString;
            TextFormat = textFormat;
            RowColor = rowColor;
        }
    }
}
