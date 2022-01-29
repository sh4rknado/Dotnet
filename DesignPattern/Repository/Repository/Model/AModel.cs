using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository
{
    [Database("model_exemple")]
    public class AModel
    {
        [Column("id")]
        public string ID { get; set; }

        [Column("var_1")]
        public string Var1 { get; set; }

        [Column("var_2")]
        public string Var2 { get; set; }


    }
}
