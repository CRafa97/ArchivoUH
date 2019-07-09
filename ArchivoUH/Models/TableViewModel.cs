using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchivoUH.Models
{
    public class TableViewModel
    {
        public TableViewModel(string name, IEnumerable<string> headers, IEnumerable<object> rows)
        {
            TableName = name;
            Headers = headers;
            Rows = rows;
        }

        public TableViewModel()
        {
            TableName = "";
            Headers = new HashSet<string>();
            Rows = new HashSet<object>();
        }

        public bool WithCreateLink { get; set; }

        public bool WithExportToExcel { get; set; }

        public bool WithImport { get; set; }

        public string TableName { get; set; }

        public IEnumerable<string> Headers { get; set; }

        // Anonymous type with headers like properties
        public IEnumerable<object> Rows { get; set; }
    }
}