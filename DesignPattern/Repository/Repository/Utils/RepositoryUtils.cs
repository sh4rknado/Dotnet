using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Utils
{
    public static class RepositoryUtils<T>
    {

        public static string BuildUpdateRequest(T model)
        {
            StringBuilder sb = new StringBuilder("SET VALUES (");
            Dictionary<string, string> dico = GetColumnFromModel(model);
            int counter = dico.Count;

            for (int i=0; i < counter; i++)
            {
                string key = dico.Keys.ToList()[i];
                sb.Append($" {key} = @{dico[key]} ");

                if(i < counter-1)
                    sb.Append(',');
            }
            sb.Append(')');

            return sb.ToString();
        }

        public static string GetTableNameFromModel(T model)
        {
            object[] atts = model.GetType().GetCustomAttributes(true);

            for (int i = 0; i < atts.Length; i++)
            {
                if (atts[i] is Repository.Model.DatabaseAttribute dbAttribute)
                    return dbAttribute.DatabaseName;

            }
            return null;
        }

        public static Dictionary<string, string> GetColumnFromModel(T model)
        {
            Dictionary<string, string> dico = new Dictionary<string, string>();
            MemberInfo[] members = model.GetType().GetMembers();

            foreach (MemberInfo member in members)
            {
                if (Attribute.GetCustomAttribute(member, typeof(ColumnAttribute), false) is ColumnAttribute attribute)
                {
                    dico.Add(attribute.ColumnName, member.Name);
                }
            }

            return dico;
        }

        public static object GetValue(MemberInfo memberInfo, object forObject)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)memberInfo).GetValue(forObject);
                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo).GetValue(forObject);
                case MemberTypes.Method:
                    return null;
                default:
                    throw new NotImplementedException();
            }
        }


    }
}
