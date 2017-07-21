using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static QuestionnaireSpecGenerator.LanguageStringsUS.ProgrammingFlags.NonADC;

namespace UnitTests
{
    [TestClass]
    public class ConstantsTests
    {
        [TestMethod]
        public void TestKeepOrderFromQX()
        {
            string expected = "Keep order from Q10.";
            string actual = ResponseSort.KeepOrderFromQX("Q10");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestShowNumBoxes()
        {
            string expected = "Show 10 text boxes.";
            string actual = OE.ShowNumBoxes(10);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestForceNumBoxesOthersOptionalValidInputSingle()
        {
            string expected = "Force first mention; others optional.";
            string actual = OE.ForceNumBoxesOthersOptional(1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestForceNumBoxesOthersOptionalValidInputMulti()
        {
            string expected = "Force first 3 mentions; others optional.";
            string actual = OE.ForceNumBoxesOthersOptional(3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestForceNumBoxesOthersOptionalInvalidInput()
        {
            try
            {
                OE.ForceNumBoxesOthersOptional(-1);
                Assert.AreEqual(1, 0); // if no exception is thrown, then test fails
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(1, 1);
            }
        }

        [TestMethod]
        public void TestRecordNumMentionsSeparatelyValidInputSingle()
        {
            string expected = "Record first mention separately.";
            string actual = OE.RecordNumMentionsSeparately(1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRecordNumMentionsSeparatelyValidInputMulti()
        {
            string expected = "Record first 3 mentions separately.";
            string actual = OE.RecordNumMentionsSeparately(3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRecordNumMentionsSeparatelyInvalidInput()
        {
            try
            {
                OE.RecordNumMentionsSeparately(-1);
                Assert.AreEqual(1, 0); // if no exception is thrown, then test fails
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(1, 1);
            }
        }

        [TestMethod]
        public void TestForceAtLeastNumCharsValidInputSingle()
        {
            string expected = "Force at least 1 character.";
            string actual = OE.ForceAtLeastNumChars(1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestForceAtLeastNumCharsValidInputMulti()
        {
            string expected = "Force at least 3 characters.";
            string actual = OE.ForceAtLeastNumChars(3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestForceAtLeastNumCharsInvalidInput()
        {
            try
            {
                OE.ForceAtLeastNumChars(0);
                Assert.AreEqual(1, 0); // if no exception is thrown, then test fails
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(1, 1);
            }
        }

        [TestMethod]
        public void TestAcceptBtwnXandYValidInput()
        {
            string expected = "Accept only values between 1 and 99.";
            string actual = OE.AcceptBtwnXandY(1, 99);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAcceptBtwnXandYInvalidInputEquals()
        {
            try
            {
                OE.AcceptBtwnXandY(1, 1);
                Assert.AreEqual(1, 0); // if no exception is thrown, then test fails
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(1, 1);
            }
        }

        [TestMethod]
        public void TestAcceptBtwnXandYInvalidInputSecondSmalller()
        {
            try
            {
                OE.AcceptBtwnXandY(50, 49);
                Assert.AreEqual(1, 0); // if no exception is thrown, then test fails
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(1, 1);
            }
        }

        [TestMethod]
        public void TestMaxNumSelectedValidInput()
        {
            string expected = "Allow selection up to 3 MAX.";
            string actual = Selection.MaxNumSelected(3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMaxNumSelectedInvalidInput()
        {
            try
            {
                Selection.MaxNumSelected(0);
                Assert.AreEqual(1, 0); // if no exception is thrown, then test fails
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual(1, 1);
            }
        }


    }
}
