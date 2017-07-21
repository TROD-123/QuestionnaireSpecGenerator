using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireSpecGenerator
{
    /// <summary>
    /// Handles the deserialization and serialization of a <see cref="Questionnaire"/> object and its
    /// children and components.
    /// </summary>
    public class JsonHandler
    {
        public static Questionnaire DeserializeJsonFromFile(string filepath)
        {
            Questionnaire qre;
            using (StreamReader file = File.OpenText(filepath))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Populate,
                    NullValueHandling = NullValueHandling.Ignore,
                    // By setting the below, forces use of the Private constructor (which is reserved for
                    // deserialization anyway
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                };
                JsonSerializer serializer = JsonSerializer.Create(settings);                
                qre = (Questionnaire)serializer.Deserialize(file, typeof(Questionnaire));
            }
            return qre;
        }

        /// <summary>
        /// Serializes the questionnaire into json.
        /// </summary>
        /// <param name="qre">The qre.</param>
        /// <returns></returns>
        private static string SerializeQuestionnaireIntoJson(Questionnaire qre)
        {
            JsonSerializerSettings serializer = new JsonSerializerSettings();  
            // Store enum strings instead of ints
            serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            return JsonConvert.SerializeObject(qre, Formatting.Indented, serializer);
        }

        /// <summary>
        /// Serializes the questionnaire into a json file.
        /// </summary>
        /// <param name="qre">The qre.</param>
        /// <param name="path">The path.</param>
        public static void SerializeQuestionnaireIntoJsonFile(Questionnaire qre, string path)
        {
            string json = SerializeQuestionnaireIntoJson(qre);
            File.WriteAllText(path, json);
        }
    }
}
