using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DatabaseToCsvUsingSRPAndISP
{
    public class DataReaderConverter : IDataReaderConverter
    {
        public IEnumerable<List<string>> Convert(IDataReader dataReader)
        {
            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);

            var data = new List<List<string>>();
            var columnNames = dataTable.Columns.Cast<DataColumn>()
                .Select(column => column.ColumnName)
                .ToList();
            data.Add(columnNames);

            foreach (DataRow row in dataTable.Rows)
            {
                var rowValues = row.ItemArray.Select(field => $"\"{field}\"").ToList();
                data.Add(rowValues);
            }

            return data;
        }
    }
}
