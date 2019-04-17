using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JDS.PelotonWebAPI.Domain.Wrappers;
using Microsoft.Extensions.Options;

namespace JDS.PelotonWebAPI.Domain.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly PelotonOptions _options;

        public LibraryRepository(IOptions<PelotonOptions> options)
        {
            _options = options.Value;
        }

        public async Task<IEnumerable<string>> GetLibraries()
        {
            return await Task.Run(() => Directory.GetFiles(_options.RootFolder + @"\custom\library", "*.lib").Select(Path.GetFileNameWithoutExtension));
        }

        public IEnumerable<IDictionary<string, object>> GetLibrary(IOEngine io, string libraryName)
        {
            var list = new List<Dictionary<string, object>>();

            var datatable = io.Library.GetDataTable(libraryName);

            var idx = 0;

            foreach (DataRow row in datatable.Rows)
            {
                var val = new Dictionary<string, object>();

                val["recordId"] = idx;

                foreach (DataColumn col in datatable.Columns)
                {
                    val[col.ColumnName] = row[col.ColumnName];
                }

                list.Add(val);

                idx++;
            }

            return list;

        }
    }
}
