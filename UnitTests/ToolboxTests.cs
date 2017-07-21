using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestionnaireSpecGenerator;

namespace UnitTests
{
    [TestClass]
    public class ToolboxTests
    {
        [TestMethod]
        public void TestGetResponseFlagType()
        {
            // Test Column Response => Grid
            ResponseFlagType type = Toolbox.GetResponseFlagType(ResponseFlag.ColumnResponse);
            Assert.AreEqual(ResponseFlagType.Classify, type);

            // Test OpenEnd => DetRText
            type = Toolbox.GetResponseFlagType(ResponseFlag.OpenEnd);
            Assert.AreEqual(ResponseFlagType.DetRText, type);

            // Test TermNot => Terminate
            type = Toolbox.GetResponseFlagType(ResponseFlag.TermNotSelected);
            Assert.AreEqual(ResponseFlagType.Terminate, type);

            // Test MutuallyExclusive => Misc
            type = Toolbox.GetResponseFlagType(ResponseFlag.MutuallyExclusive);
            Assert.AreEqual(ResponseFlagType.Misc, type);
        }
    }
}
