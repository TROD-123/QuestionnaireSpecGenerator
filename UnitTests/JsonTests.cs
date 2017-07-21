using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestionnaireSpecGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class JsonTests
    {
        [TestMethod]
        public void TestDeserializeSerializeQre()
        {
            DataContainer container = Tester.TestCreateQreNoJson();

            string path = @".\..\..\test.json";
            JsonHandler.SerializeQuestionnaireIntoJsonFile(container.GetQre(), path);

            Questionnaire qre = JsonHandler.DeserializeJsonFromFile(@".\..\..\test.json");

            Assert.AreEqual(container.GetQre(), qre);
        }
    }
}
