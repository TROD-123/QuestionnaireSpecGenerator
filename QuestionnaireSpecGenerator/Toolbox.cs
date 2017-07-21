using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSpecGenerator
{
    /// <summary>
    /// Set of helper functions
    /// </summary>
    public class Toolbox
    {
        /// <summary>
        /// Gets the name of the excel column. 
        /// From https://stackoverflow.com/questions/181596/how-to-convert-a-column-number-eg-127-into-an-excel-column-eg-aa
        /// </summary>
        /// <param name="colNum">The column number.</param>
        /// <returns>The letter name of the excel column.</returns>
        public static string GetExcelColumnName(int colNum)
        {
            int dividend = colNum;
            string colName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                colName = Convert.ToChar(65 + modulo).ToString() + colName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return colName;
        }

        /// <summary>
        /// Updates the last modified date when a questionnaire object is changed. Also updates the change log.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        public static void UpdateDate<T>(T obj) where T : QreObjBase
        {
            // TODO: Implement UpdateDate
            obj.DateModified = DateTime.Now;
        }

        /// <summary>
        /// Determines the type of the <see cref="ResponseFlag"/>.
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static ResponseFlagType GetResponseFlagType(ResponseFlag flag)
        {
            if ((int)flag < ResponseFlagConstants.DetRText)
            {
                return ResponseFlagType.DetRText;
            }
            else if ((int)flag < ResponseFlagConstants.Classify)
            {
                return ResponseFlagType.Classify;
            }
            else if ((int)flag < ResponseFlagConstants.Anchor)
            {
                return ResponseFlagType.Anchor;
            }
            else if ((int)flag < ResponseFlagConstants.Terminate)
            {
                return ResponseFlagType.Terminate;
            }
            else
            {
                return ResponseFlagType.Misc;
            }
        }

        public static ProgFlag.NonADCFlagType GetNonADCProgFlagType(ProgFlag.NonADC nonADCFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates an error string to print to the console.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="e"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static string GenerateConsoleErrorMessage(string methodName, Exception e, string returnValue = LanguageStringsUS.ConsoleError.ActionProceed + LanguageStringsUS.Ellipsis)
        {
            String action;
            if (returnValue.Equals(LanguageStringsUS.ConsoleError.ActionProceed + LanguageStringsUS.Ellipsis))
            {
                action = returnValue;
            }
            else
            {
                action = LanguageStringsUS.ConsoleError.ReturnValue + returnValue + LanguageStringsUS.Ellipsis;
            }

            return String.Format("{0}{1}\n{2}\n{3}", methodName, LanguageStringsUS.ConsoleError.ThrewException, e, action);
        }

        /// <summary>
        /// Append ordinal suffix to a cardinal number (e.g. 1 => 1st, 5 => 5th, 100 => 100th)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToOrdinal(long number)
        {
            if (number < 0) return number.ToString();
            long rem = number % 100;
            if (rem >= 11 && rem <= 13) return number + "th";
            switch (number % 10)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }
        }

        public static string ConvertStringSingularOrPlural(string word, int num)
        {
            PluralizationService pluralizationService = PluralizationService
                .CreateService(System.Globalization.CultureInfo.InstalledUICulture);

            if (num == 1)
            {
                return word;
            }
            else if (num > 1)
            {
                return pluralizationService.Pluralize(word);
            }
            else
            {
                throw new ArgumentOutOfRangeException("num", "Invalid input number passed: " + num);
            }
        }

    }
}
