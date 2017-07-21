using System.Drawing;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace QuestionnaireSpecGenerator
{
    class Program
    {
        // when run, an excel file gets created and populated with the below "specs". Sheet is not saved
        // anywhere, so must be saved manually once it is made.
        //
        // creates a new spreadsheet each time program is run. code continues to run in the background
        // after the excel sheet is created, and then 'takes control' of the recently created excel doc
        // and manipulates it as spec'd
        public static void Main(string[] args)
        {
            //List<Response> _data = new List<Response>();
            //_data.Add(new Response()
            //{
            //    RCode = 1,
            //    RespText = "Mark here if this does not apply",
            //    Flags = null,
            //    RType = ResponseType.None
            //});

            ////open file stream
            //using (StreamWriter file = File.CreateText(@"D:\path.txt"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    //serialize object directly into file stream
            //    serializer.Serialize(file, _data);
            //}

            Tester.TestCreateQreSerializeToJsonFile();

        }
    }
}
