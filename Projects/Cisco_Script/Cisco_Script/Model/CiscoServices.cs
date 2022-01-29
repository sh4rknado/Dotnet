using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Cisco_Script.Model {

    [Serializable()]
    class CiscoServices : ISerializable, ICiscoServices {

        #region Serialize


        /// <summary>
        /// GetInfos Serialize
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // info.addvalue("type", type);
            // info.addvalue("nom", nom);
        }


        /// <summary>
        /// Build From Serialize datas
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public CiscoServices(SerializationInfo info, StreamingContext context)
        {
            // this.type = (string)info.getvalue("type", typeof(string));
            // this.nom = (string)info.getvalue("nom", typeof(string));
        }

        #endregion



    }
}
