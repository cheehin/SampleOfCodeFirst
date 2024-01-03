using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace Nation
{
    public static class Generics
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                dataTable.Columns.Add(prop.Name,Nullable.GetUnderlyingType(prop.PropertyType)??prop.PropertyType);
            foreach(T item in data)
            {
                DataRow row = dataTable.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
