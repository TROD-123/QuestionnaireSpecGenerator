using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSpecGenerator
{
    public class Tester
    {
        public static void TestCreateQreSerializeToJsonFile()
        {
            DataContainer container = TestCreateQreNoJson();

            string path = @".\..\..\test.json";
            JsonHandler.SerializeQuestionnaireIntoJsonFile(container.GetQre(), path);
        }


        public static void TestConsoleErrorMessageGeneration()
        {
            Console.WriteLine(Toolbox.GenerateConsoleErrorMessage("TestMethod", new Exception()));
            Console.WriteLine(Toolbox.GenerateConsoleErrorMessage("TestMethod", new Exception(), "1"));
        }

        public static DataContainer TestCreateQreNoJson()
        {
            Console.WriteLine("-- Testing creating qre without json --");
            Console.WriteLine("Create the qre and data container objects. Data container houses the empty qre.");
            Questionnaire qre = new Questionnaire("Test Questionnaire", "This is a test", "Test client", null, Office.LA, Country.US, null);
            DataContainer container = new DataContainer(qre);

            Console.WriteLine();

            // Add modules
            Console.WriteLine("Create 2 modules. First module pos 1, second module pos 2.");
            Module modA = new Module(mNum: 1, mTitle: "Demographics", mDesc: "The demographics section", mShowDesc: false, sections: null);
            container.AddChild(modA, 0);
            Console.WriteLine("ID of first module: " + modA.SelfId);
            Module modB = new Module(mNum: 2, mTitle: "Brand Engagement", mDesc: "The Pre-Ad section", mShowDesc: false, sections: null);
            container.AddChild(modB, 1);
            Console.WriteLine("ID of second module: " + modB.SelfId);
            Console.WriteLine("Name of second module: " + modB.MTitle);
            foreach (Module module in container.modules)
            {
                Console.WriteLine("Module found! " + module.MNum);
            }

            //Console.WriteLine();

            //// Test modules
            //Console.WriteLine("Test modify mod in container through modA and modB");
            //Console.WriteLine("Mod A description via container, before change: " + container.GetModuleById(1).MDesc);
            //Console.WriteLine("Mod B title via container, before change: " + container.GetModuleById(2).MTitle);

            //modA.MDesc = "The demographics section... with a twist!";
            //modB.MTitle = "No longer Brand Engagement, suckers!";

            //Console.WriteLine("Mod A description via container: " + container.GetModuleById(1).MDesc);
            //Console.WriteLine("Mod B title via container: " + container.GetModuleById(2).MTitle);

            ////Console.WriteLine("Reassign modA to modB to break original reference to modA and assess container");
            ////modA = modB;
            ////Console.WriteLine("Mod A description via container: " + container.GetModuleById(1).MDesc);
            ////Console.WriteLine("Mod B title via container: " + container.GetModuleById(2).MTitle);

            //Console.WriteLine("Results: REFERENCES RULE.");

            Console.WriteLine();

            // Add sections
            Console.WriteLine("Create and add sections for the first module. Search via module id. modA = 1, modB = 2");
            Console.WriteLine("5 sections: 3 for modA, 2 for modB");
            Section secA = new Section(modA.SelfId, "A", "A great deal on Age", "So much can be learned about one's age.", true, null);
            Section secB = new Section(modA.SelfId, "B", "A great deal on Gender", "", false, null);
            Section secC = new Section(modA.SelfId, "C", "A great deal on Employement status", "", false, null);
            Section secD = new Section(modB.SelfId, "D", "5-pt scales", "These questions have 5-pt scales.", true, null);
            Section secE = new Section(modB.SelfId, "E", "Checkboxes", "These questions are multi-select.", true, null);
            Console.WriteLine("Adding modA sections via bulk adds");
            List<Tuple<Section, int>> childrenModA = new List<Tuple<Section, int>>
            {
                new Tuple<Section, int>(secA, 0),
                new Tuple<Section, int>(secB, 1),
                new Tuple<Section, int>(secC, 2)
            };
            container.AddChildren(childrenModA);

            // Test sections
            Module run = container.GetModuleById(1);
            foreach (Section section in run.Children)
            {
                Console.WriteLine("Section: " + section.SLetter + ", Title: " + section.STitle);
            }

            Console.WriteLine("Adding modB sections via individual adds");
            container.AddChild(secD, 0);
            container.AddChild(secE, 1);

            run = container.GetModuleById(2);
            foreach (Section section in run.Children)
            {
                Console.WriteLine("Section: " + section.SLetter + ", Title: " + section.STitle);
            }
            Console.WriteLine();

            // Add questions
            Console.WriteLine("Create and add questions for the secA and secD");

            List<Tuple<ProgFlag.NonADC, int, int, string>> progFlagsA = new List<Tuple<ProgFlag.NonADC, int, int, string>>
            {
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.ShowNumBoxes, 1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.NoRandomizeColumns, -1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.AcceptBtwnXandY, 1, 99, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.CodeOE, -1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.Custom, -1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.KeepOrderFromQX, -1, -1, "Q10"),
            };
            QuestionBlock qbA = new QuestionBlock(secA.SelfId, "A10", "Age", "All respondents", "", "", progFlagsA, ProgFlag.ADC.None,
                RoutingFlag.NextQuestion, QuestionType.NumericTextField, "What is your age?", "Type your answer in the box below.", null, 
                customProgInst: "Appendage test.");

            List<Tuple<ProgFlag.NonADC, int, int, string>> progFlagsB = new List<Tuple<ProgFlag.NonADC, int, int, string>>
            {
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.RandomizeRows, 1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.ShowColumnCodes, 1, -1, null),
            };
            QuestionBlock qbB = new QuestionBlock(secA.SelfId, "A20", "Age 2", "All respondents", "", "", progFlagsB, ProgFlag.ADC.None,
                RoutingFlag.NextQuestion, QuestionType.Grid, "This is a test grid question.", "Please select an answer for each statement.", null);

            List<Tuple<ProgFlag.NonADC, int, int, string>> progFlagsC = new List<Tuple<ProgFlag.NonADC, int, int, string>>
            {
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.NoRandomizeRows, -1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.HideRowCodes, -1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.SingleCodePerRow, -1, -1, null),
            };
            QuestionBlock qbC = new QuestionBlock(secA.SelfId, "A30", "Age 3", "All respondents", "", "", progFlagsC, ProgFlag.ADC.None,
                RoutingFlag.NextQuestion, QuestionType.SingleCode, "I don't get what you said. Which of the below applies to you?",
                "Please select one.", null);

            List<Tuple<ProgFlag.NonADC, int, int, string>> progFlagsD = new List<Tuple<ProgFlag.NonADC, int, int, string>>
            {
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.ShowColumnCodes, -1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.SingleCodePerColumn, -1, -1, null),
            };
            QuestionBlock qbD = new QuestionBlock(secD.SelfId, "D10", "Scale 1", "All respondents", "", "", progFlagsD, ProgFlag.ADC.None,
                RoutingFlag.NextQuestion, QuestionType.HorizontalScale, "Rate how you're feeling right now.", "Select one below", null);

            List<Tuple<ProgFlag.NonADC, int, int, string>> progFlagsE = new List<Tuple<ProgFlag.NonADC, int, int, string>>
            {
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.FlagStraightLiner, -1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.ShowRowCodes, -1, -1, null),
            };
            QuestionBlock qbE = new QuestionBlock(secD.SelfId, "D20", "Scale 2", "All respondents", "", "", progFlagsE, ProgFlag.ADC.None,
                RoutingFlag.NextQuestion, QuestionType.SingleCode, "What? I didn't hear you!", "Select one below.", null);

            List<Tuple<ProgFlag.NonADC, int, int, string>> progFlagsF = new List<Tuple<ProgFlag.NonADC, int, int, string>>
            {
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.RandomizeRows, -1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.HideRowCodes, -1, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.MaxNumSelected, 3, -1, null),
                new Tuple<ProgFlag.NonADC, int, int, string>(ProgFlag.NonADC.FlagStraightLiner, -1, -1, null),
            };
            QuestionBlock qbF = new QuestionBlock(secD.SelfId, "D30", "Multi code", "All respondents", "", "", progFlagsF, ProgFlag.ADC.None,
                RoutingFlag.NextQuestion, QuestionType.MultiCode, "Piss off!!", "Select all that apply. 3 at most!", null);

            Console.WriteLine("Add all questions as a single batch.");
            List<Tuple<QuestionBlock, int>> childrenSecAD = new List<Tuple<QuestionBlock, int>>
            {
                new Tuple<QuestionBlock, int>(qbA, 0),
                new Tuple<QuestionBlock, int>(qbB, 1),
                new Tuple<QuestionBlock, int>(qbC, 2),
                new Tuple<QuestionBlock, int>(qbD, 0),
                new Tuple<QuestionBlock, int>(qbE, 1),
                new Tuple<QuestionBlock, int>(qbF, 2)
            };
            container.AddChildren(childrenSecAD);

            // Test questions
            Section srun = container.GetSectionById(1);
            foreach (QuestionBlock question in srun.Children)
            {
                Console.WriteLine("Question: " + question.QNum + ", Title: " + question.QTitle);
            }

            srun = container.GetSectionById(4);
            foreach (QuestionBlock question in srun.Children)
            {
                Console.WriteLine("Section: " + question.QNum + ", Title: " + question.QTitle);
            }

            Console.WriteLine();

            Console.WriteLine("Create and add responses for each of the questions");

            Console.WriteLine("rAA is Open End");
            Response rAA = new Response(qbA.SelfId, new List<ResponseFlag>() { ResponseFlag.OpenEnd });
            container.AddChild(rAA, 0);

            Console.WriteLine("rBA is Grid");
            Response rBA = new Response(qbB.SelfId, new List<ResponseFlag>() { ResponseFlag.ColumnResponse });
            Response rBB = new Response(qbB.SelfId, new List<ResponseFlag>() { ResponseFlag.AnchorLeft }, "Hate it.", 1);
            Response rBC = new Response(qbB.SelfId, new List<ResponseFlag>() { ResponseFlag.AnchorCenter }, "Meh", 5);
            Response rBD = new Response(qbB.SelfId, new List<ResponseFlag>() { ResponseFlag.AnchorRight }, "Love it.", 10);
            Response rBE = new Response(qbB.SelfId, new List<ResponseFlag>() { ResponseFlag.RowResponse });
            Response rBF = new Response(qbB.SelfId, null, "The Beatles", 1);
            Response rBG = new Response(qbB.SelfId, null, "Coldplay", 2);
            Response rBH = new Response(qbB.SelfId, null, "Linkin Park", 3);
            Response rBI = new Response(qbB.SelfId, null, "My Ass", 4);
            List<Tuple<Response, int>> childrenQB = new List<Tuple<Response, int>>
            {
                new Tuple<Response, int>(rBA, 0),
                new Tuple<Response, int>(rBB, 1),
                new Tuple<Response, int>(rBC, 2),
                new Tuple<Response, int>(rBD, 3),
                new Tuple<Response, int>(rBE, 4),
                new Tuple<Response, int>(rBF, 5),
                new Tuple<Response, int>(rBG, 6),
                new Tuple<Response, int>(rBH, 7),
                new Tuple<Response, int>(rBI, 8),
            };
            container.AddChildren(childrenQB);

            Console.WriteLine("rCx are Closed Ends");
            Response rCA = new Response(qbC.SelfId, null, "16-24", 1);
            Response rCB = new Response(qbC.SelfId, null, "25-34", 2);
            Response rCC = new Response(qbC.SelfId, null, "35-44", 3);
            Response rCD = new Response(qbC.SelfId, null, "45-54", 4);
            Response rCE = new Response(qbC.SelfId, null, "55-64", 5);
            Response rCF = new Response(qbC.SelfId, null, "65+", 6);
            Console.WriteLine("rCG is a Prefer No Answer closed end!");
            Response rCG = new Response(qbC.SelfId, new List<ResponseFlag>() { ResponseFlag.PrefNoAnswer });
            List<Tuple<Response, int>> childrenQC = new List<Tuple<Response, int>>
            {
                new Tuple<Response, int>(rCA, 0),
                new Tuple<Response, int>(rCB, 1),
                new Tuple<Response, int>(rCC, 2),
                new Tuple<Response, int>(rCD, 3),
                new Tuple<Response, int>(rCE, 4),
                new Tuple<Response, int>(rCF, 5),
                new Tuple<Response, int>(rCG, 6),
            };
            container.AddChildren(childrenQC);

            Console.WriteLine("rDx is a horizontal scale closed end, with 3 Anchors!!!");
            Response rDA = new Response(qbD.SelfId, new List<ResponseFlag>() { ResponseFlag.AnchorLeft }, "Terrible!", 1);
            Response rDB = new Response(qbD.SelfId, new List<ResponseFlag>() { ResponseFlag.AnchorCenter }, "Meh!", 5);
            Response rDC = new Response(qbD.SelfId, new List<ResponseFlag>() { ResponseFlag.AnchorRight }, "High on life!", 10);
            List<Tuple<Response, int>> childrenQD = new List<Tuple<Response, int>>
            {
                new Tuple<Response, int>(rDA, 0),
                new Tuple<Response, int>(rDB, 1),
                new Tuple<Response, int>(rDC, 2),
            };
            container.AddChildren(childrenQD);

            Console.WriteLine("rEx is a vertical scale closed end, with 3 anchors and 2 blank placeholders!!!");
            Response rEA = new Response(qbE.SelfId, new List<ResponseFlag>() { ResponseFlag.AnchorTop }, "Ugh...", 1);
            Response rEB = new Response(qbE.SelfId, null, "", 2);
            Response rEC = new Response(qbE.SelfId, new List<ResponseFlag>() { ResponseFlag.AnchorCenter }, "Hmmm.", 3);
            Response rED = new Response(qbE.SelfId, null, "", 4);
            Response rEE = new Response(qbE.SelfId, new List<ResponseFlag>() { ResponseFlag.AnchorBottom }, "Woo!", 5);
            List<Tuple<Response, int>> childrenQE = new List<Tuple<Response, int>>
            {
                new Tuple<Response, int>(rEA, 0),
                new Tuple<Response, int>(rEB, 1),
                new Tuple<Response, int>(rEC, 2),
                new Tuple<Response, int>(rED, 3),
                new Tuple<Response, int>(rEE, 4),
            };
            container.AddChildren(childrenQE);

            Console.WriteLine("rFx is a multi select, with None, All, Terms, and OtherSpecs!");
            Response rFA = new Response(qbF.SelfId, null, "Captain Crunch", 1);
            Response rFB = new Response(qbF.SelfId, new List<ResponseFlag>() { ResponseFlag.TermNotSelected }, "Honey Nut Cheerios", 2);
            Response rFC = new Response(qbF.SelfId, null, "Frosted Flakes", 3);
            Response rFD = new Response(qbF.SelfId, null, "Fruit Loops", 4);
            Response rFE = new Response(qbF.SelfId, new List<ResponseFlag>() { ResponseFlag.TermSelected }, "Cinnamon Toast Crunch", 5);
            Response rFF = new Response(qbF.SelfId, null, "Cookie Crisp", 6);
            Response rFG = new Response(qbF.SelfId, new List<ResponseFlag>() { ResponseFlag.All, ResponseFlag.MutuallyExclusive, ResponseFlag.Test });
            Response rFH = new Response(qbF.SelfId, new List<ResponseFlag>() { ResponseFlag.None, ResponseFlag.MutuallyExclusive, ResponseFlag.TermSelected });
            Response rFI = new Response(qbF.SelfId, new List<ResponseFlag>() { ResponseFlag.OtherSpec });
            List<Tuple<Response, int>> childrenQF = new List<Tuple<Response, int>>
            {
                new Tuple<Response, int>(rFA, 0),
                new Tuple<Response, int>(rFB, 1),
                new Tuple<Response, int>(rFC, 2),
                new Tuple<Response, int>(rFD, 3),
                new Tuple<Response, int>(rFE, 4),
                new Tuple<Response, int>(rFF, 5),
                new Tuple<Response, int>(rFG, 6),
                new Tuple<Response, int>(rFH, 7),
                new Tuple<Response, int>(rFI, 8),
            };
            container.AddChildren(childrenQF);

            return container;

            // TODO: Serialize the qre into JSON

        }

        public static void TestGetObjectById(Questionnaire qre)
        {
            Console.WriteLine("-- Testing Get Object by Id --");
            DataContainer container = new DataContainer(qre);

            Module existsModule = container.GetModuleById(1);
            Console.WriteLine("Module id 1: " + existsModule.MTitle);

            Module nonExistsModule = container.GetModuleById(5);
            Console.WriteLine("Module id 5: Null? " + (nonExistsModule == null));

            Section existsSection = container.GetSectionById(1);
            Console.WriteLine("Section id 1: " + existsSection.STitle);

            Section nonExistsSection = container.GetSectionById(100);
            Console.WriteLine("Section id 100: Null? " + (nonExistsSection == null));

            QuestionBlock existsQuestion = container.GetQuestionById(2);
            Console.WriteLine("Question id 2: " + existsQuestion.QTitle);

            QuestionBlock nonExistsQuestion = container.GetQuestionById(6);
            Console.WriteLine("Question id 6: Null? " + (nonExistsQuestion == null));


            //// here, a section is added to the container automatically after creation
            //Console.WriteLine("-- Testing add a new section into first module, previously non-existant --");
            //Section section = new Section(container, 1, 0)
            //{
            //    STitle = "This is a newly added section!"
            //};

            //// here, a module is added to the container automatically after creation
            //Console.WriteLine("-- Testing adding a new module automatically through constructor, previously non-existant --");
            //Module module = new Module();

            //Console.WriteLine("-- Testing adding a new question automatically through constructor, previously non-existant. To section id 3 --");
            //QuestionBlock question = new QuestionBlock(container, 3);

        }

        public static void TestRandomIntGenerator(Questionnaire qre)
        {
            Console.WriteLine("-- Testing Generation of Random Ids --");
            DataContainer container = new DataContainer(qre);

            Console.WriteLine("-- Getting the current ids --");
            List<int> moduleIds = container.GetIds(QreObjTypes.Module);
            List<int> sectionIds = container.GetIds(QreObjTypes.Section);
            List<int> questionIds = container.GetIds(QreObjTypes.QuestionBlock);

            Console.Write("Module ids: ");
            foreach (int i in moduleIds)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.Write("Section ids: ");
            foreach (int i in sectionIds)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.Write("Question ids: ");
            foreach (int i in questionIds)
            {
                Console.Write(i + " ");
            }

            //Console.WriteLine();
            //Console.WriteLine("-- Generating random ids --");

            //int moduleId = Toolbox.GenerateRandomId(container, QreObjTypes.Module);
            //int sectionId = Toolbox.GenerateRandomId(container, QreObjTypes.Section);
            //int questionId = Toolbox.GenerateRandomId(container, QreObjTypes.QuestionBlock);

            //Console.WriteLine("New module id: " + moduleId);
            //Console.WriteLine("New section id: " + sectionId);
            //Console.WriteLine("New question id: " + questionId);

            //Console.WriteLine();
            //Console.WriteLine("-- End Testing Generation of Random Ids --");

        }


        /// <summary>
        /// Tests the modify qre existing objects indirectly. Purpose of this is to show that modifying the individual
        /// list of qre objects would be reflected in the parent qre object - as long as the lists are pointing to
        /// the qre reference that contains the lists (e.g. qre.Modules, or qre.Modules[0].Sections)
        /// </summary>
        /// <param name="qre">The qre.</param>
        public static void TestModifyQreExistingObjectsIndirectly(Questionnaire qre)
        {
            List<Module> modules = new List<Module>();
            List<Section> sections = new List<Section>();
            List<QuestionBlock> questions = new List<QuestionBlock>();


            foreach (Module module in qre.Children)
            {
                modules.Add(module);
            }

            foreach (Module module in modules)
            {
                foreach (Section section in module.Children)
                {
                    sections.Add(section);
                    Console.WriteLine("Section title:" + section.STitle);
                }
            }

            Console.WriteLine("----------");

            foreach (Section section in sections)
            {
                foreach (QuestionBlock question in section.Children)
                {
                    questions.Add(question);
                    Console.WriteLine("Question comments: " + question.Comments);
                    Console.WriteLine("Question prog inst: " + question.ProgInst);
                }
            }

            Console.WriteLine("Initial counts: " + modules.Count() + " " + sections.Count() + " " + questions.Count());


            Console.WriteLine("---- CHANGE FIRST QUESTION COMMENTS AND PROG, AND FIRST SECTION TITLE------");

            if (questions.Count() > 0)
            {
                questions[0].Comments = "THE END IS NIGH!!!";
                questions[0].ProgInst = "fuck off";
            }

            if (sections.Count() > 0)
            {
                sections[0].STitle = "I've changed this property haha.";
            }

            Console.WriteLine("---- Checking lists ------");


            foreach (Section section in sections)
            {
                Console.WriteLine("Section title:" + section.STitle);
            }

            foreach (QuestionBlock question in questions)
            {
                Console.WriteLine("Question comments: " + question.Comments);
                Console.WriteLine("Question prog inst: " + question.ProgInst);
            }

            Console.WriteLine("---- Add new question to first section via section object (NOT list of questions) ---");

            if (sections.Count() > 0)
                //{
                //    QuestionBlock newQuestion = new QuestionBlock();
                //    newQuestion.Comments = "Q100_NEWNEWNEW!!!";
                //    newQuestion.ProgInst = "NEW NEW NEW NEW NEW NEW NEWDNEWNEWNEWNEWNENW";

                //    sections[0].QuestionBlocks.Add(newQuestion);
                //}

                Console.WriteLine("----- Add new section to first module via module object (NOT list of sections) ---");

            if (modules.Count() > 0)
                //{
                //    Section newSection = new Section(container);
                //    newSection.STitle = "AAAAAAAAAAAAAAAAAAAAAAA";
                //    newSection.QuestionBlocks = new List<QuestionBlock>();

                //    modules[0].Sections.Add(newSection);
                //}

                Console.WriteLine("--- Clear objects and Loop through qre elements again ----");

            modules.Clear();
            sections.Clear();
            questions.Clear();

            Console.WriteLine("Lengths: " + modules.Count() + " " + sections.Count() + " " + questions.Count());

            foreach (Module module in qre.Children)
            {
                modules.Add(module);
            }

            foreach (Module module in modules)
            {
                foreach (Section section in module.Children)
                {
                    sections.Add(section);
                    Console.WriteLine("Section title:" + section.STitle);
                }
            }

            Console.WriteLine("----------");

            foreach (Section section in sections)
            {
                foreach (QuestionBlock question in section.Children)
                {
                    questions.Add(question);
                    Console.WriteLine("Question comments: " + question.Comments);
                    Console.WriteLine("Question prog inst: " + question.ProgInst);
                }
            }

            Console.WriteLine("Final counts: " + modules.Count() + " " + sections.Count() + " " + questions.Count());

        }


    }
}
