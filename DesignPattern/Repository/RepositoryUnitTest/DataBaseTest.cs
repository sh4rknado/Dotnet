using NUnit.Framework;
using Repository;
using System;
using Repository.Utils;
using System.Collections.Generic;

namespace RepositoryUnitTest
{
    public class DataBaseTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestTableName()
        {
            AModel model = new AModel();
            string tableName = RepositoryUtils<AModel>.GetTableNameFromModel(model);

            Assert.IsTrue(!string.IsNullOrEmpty(tableName), "Error the tableName is null");
            Assert.IsTrue(tableName?.Equals("model_exemple"), $"Expected value => model_exemple and received => {tableName}");
        }

        [Test]
        public void TestColumnsName()
        {
            Dictionary<string, string> dico  = RepositoryUtils<AModel>.GetColumnFromModel(new AModel());
            
            // test dictionary
            Assert.AreEqual(dico.Count, 3, $"Expected 3 columns and received {dico.Count} columns");

            // Check Key
            Assert.IsTrue(dico.ContainsKey("id"));
            Assert.IsTrue(dico.ContainsKey("var_1"));
            Assert.IsTrue(dico.ContainsKey("var_2"));

            // Check Value
            dico.TryGetValue("id", out string value);
            Assert.IsTrue(value.ToString().Equals("ID"));

            dico.TryGetValue("var_1", out string value2);
            Assert.IsTrue(value2.ToString().Equals("Var1"));

            dico.TryGetValue("var_2", out string value3);
            Assert.IsTrue(value3.ToString().Equals("Var2"));
        }

        [Test]
        public void TestRequestUpdate()
        {
            string id = Guid.NewGuid().ToString();
            AModel model = new AModel()
            {
                ID = id,
                Var1 = "Var 1",
                Var2 = "Var 2"
            };

            string request  = RepositoryUtils<AModel>.BuildUpdateRequest(model);
            Assert.IsTrue(request.Equals("SET VALUES ( id = @ID , var_1 = @Var1 , var_2 = @Var2 )"));
        }

    }
}