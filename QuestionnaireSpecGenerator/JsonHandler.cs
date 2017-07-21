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
    class JsonHandler
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

        public static string SerializeQuestionnaireIntoJson(Questionnaire qre)
        {
            JsonSerializerSettings serializer = new JsonSerializerSettings();  
            // Store enum strings instead of ints
            serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            string jsonSerialized = JsonConvert.SerializeObject(qre, Formatting.Indented, serializer);

            return jsonSerialized;
        }

        public static void SerializeJsonIntoFile(string filepath)
        {

        }
    }
}
