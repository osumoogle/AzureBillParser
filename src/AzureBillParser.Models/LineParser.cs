using System.Collections.Generic;
using System.IO;

namespace AzureBillParser.Models
{
    public class LineParser
    {
        public static List<LineItem> Parse(string path)
        {
            var lineItems = new List<LineItem>();
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var fileLines = File.ReadAllLines(file);
                var i = 0;
                foreach (var line in fileLines)
                {
                    if (i != 0)
                        lineItems.Add(new LineItem(line));
                    i++;
                }
            }

            return lineItems;
        }
    }
}
