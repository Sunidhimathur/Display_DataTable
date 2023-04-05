using System.Collections.Generic;
using System.IO;

namespace DatabaseToCsvUsingSRPAndISP
{
    public class CsvWriter : ICsvWriter
    {
        public void WriteToCsvFile(IEnumerable<List<string>> data, string filePath)
        {
            using (var streamWriter = new StreamWriter(filePath))
            {
                foreach (var row in data)
                {
                    var line = string.Join(",", row);
                    streamWriter.WriteLine(line);
                }
            }
        }
    }
}
