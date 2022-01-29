using NUnit.Framework;
using Repository.Model;
using Repository.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryUnitTest
{
    public class DataBaseConfig
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestSavePersistence()
        {
            string path = "file.json";
            DBConfig model = new DBConfig()
            {
                DBName = "database_name",
                IP = "127.0.0.1",
                Password = "password",
                Port = 3301,
                Username = "username",
                Type = DBType.MySQL
            };

            bool saved = new DatabaseConfigPersistence().TrySave(path, model, out string error);
            
            Assert.IsTrue(string.IsNullOrEmpty(error) || saved, $"Somes errors occured : {error}");
        }

        [Test]
        public void TestSaveWithMissingRequirement()
        {
            string path = "file.json";
            DBConfig model = new DBConfig()
            {
                DBName = null,
                IP = null,
                Password = null,
                Port = 0,
                Username = null,
                Type = DBType.MySQL
            };

            bool saved = new DatabaseConfigPersistence().TrySave(path, model, out string error);

            Assert.IsTrue(!string.IsNullOrEmpty(error) || !saved, $"Somes errors occured : {error}");
        }

        [Test]
        public void TestLoadPersistence()
        {
            string path = "file.json";

            if (File.Exists(path))
            {
                File.Delete(path);
                TestSavePersistence();
            }

            bool loaded = new DatabaseConfigPersistence().TryLoad(path, out DBConfig model, out string error);

            Assert.IsTrue(string.IsNullOrEmpty(error) || loaded, $"Somes errors occured : {error}");

            DBConfig db = new DBConfig()
            {
                DBName = "database_name",
                IP = "127.0.0.1",
                Password = "password",
                Port = 3301,
                Username = "username",
                Type = DBType.MySQL
            };

            Assert.IsTrue(db.Equals(model));
        }

        [Test]
        public void TestLoadMissingFilePersistence()
        {
            string path = "file_missing.json";
            bool loaded = new DatabaseConfigPersistence().TryLoad(path, out DBConfig model, out string error);
            Assert.IsTrue(!string.IsNullOrEmpty(error) || !loaded, $"Somes errors occured : {error}");
        }

        [Test]
        public void TestLoadMissingRequirementPersistence()
        {
            string missingContent = "{\"ip\":\"127.0.0.1\",\"port\":3301,\"type\":0}";
            string path = "file.json";
            File.WriteAllText(path, missingContent);

            bool loaded = new DatabaseConfigPersistence().TryLoad(path, out DBConfig model, out string error);
            Assert.IsTrue(!string.IsNullOrEmpty(error) || !loaded, $"Somes errors occured : {error}");
        }

    }
}
