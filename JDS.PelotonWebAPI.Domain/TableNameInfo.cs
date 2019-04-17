using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDS.PelotonWebAPI.Domain
{
    public class TableNameInfo
    {
        public string TableName { get; set; }
        public IDictionary<string, string> Fields { get; set; }
    }
}
