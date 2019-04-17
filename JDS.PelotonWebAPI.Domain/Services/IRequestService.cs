using System.Collections.Generic;
using System.Threading.Tasks;
using JDS.PelotonWebAPI.Domain.Wrappers;
using Microsoft.AspNetCore.Http;

namespace JDS.PelotonWebAPI.Domain.Services
{
    public interface IRequestService
    {
        TableDataRecord Get(string tableName, string entityId, string uniqueId, IQueryCollection queryParams);
        IEnumerable<TableDataRecord> Query(string tableName, string entityId, string parentIdFilter, IQueryCollection queryParams);
        TableDataRecord Update(string tableName, string entityId, string uniqueId, IDictionary<string, object> properties);
        TableDataRecord Patch(string tableName, string entityId, string uniqueId, IDictionary<string, object> properties);
        void Delete(string tableName, string entityId, string uniqueId);
        TableDataRecord Add(string parentTableName, string tableName, string parentId, IDictionary<string, object> properties);
        Task<IList<string>> GetEntities(string tableMainName);
        Task<IList<string>> GetTables();
        Task<TableMeta> GetTable(string tableName);
        Task<IEnumerable<string>> GetLibraries();
        IEnumerable<IDictionary<string, object>> GetLibrary(string libraryName);
    }
}
