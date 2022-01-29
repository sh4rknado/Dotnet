using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Repository.Interfaces;
using Repository.Model;
using System.IO;

namespace Repository.Persistence
{
    public class DatabaseConfigPersistence : IGetDBConfig
    {
        public bool TryLoad(string path, out DBConfig dBConfig, out string error)
        {
            error = null;
            dBConfig = null;
            try
            {
                if (!File.Exists(path))
                    throw new Exception($"file not found : {path}");
                
                // Read File
                JsonTextReader reader = new JsonTextReader(new StringReader(File.ReadAllText(path)));

                // JSON Schema 
                JSchemaValidatingReader validatingReader = new JSchemaValidatingReader(reader);
                JSchemaGenerator generator = new JSchemaGenerator();
                validatingReader.Schema = generator.Generate(typeof(DBConfig));

                // Deserialize
                JsonSerializer serializer = new JsonSerializer();
                dBConfig = serializer.Deserialize<DBConfig>(validatingReader);

                return dBConfig != null;
            }
            catch(Exception ex)
            {
                error = $"error occured when load the configuration of the database with the reason {ex.Message}";
                return false;
            }
        }

        public bool TrySave(string path, DBConfig dbConfig, out string error)
        {
            error = null;
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(dbConfig));
                return true;
            }
            catch (Exception ex)
            {
                error = $"error occured when save the configuration of the database with the reason {ex.Message}";
                return false;
            }
        }
    }
}
