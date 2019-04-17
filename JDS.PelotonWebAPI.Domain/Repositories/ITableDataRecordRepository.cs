using System.Collections.Generic;
using JDS.PelotonWebAPI.Domain.Wrappers;
using Microsoft.AspNetCore.Http;

namespace JDS.PelotonWebAPI.Domain.Repositories
{
    public interface ITableDataRecordRepository
    {
        TableDataRecord Get(IOEngine io, string tableName, string entityId, string uniqueId, IQueryCollection queryParams = null);
        IEnumerable<TableDataRecord> Query(IOEngine io, string tableName, string entityId, string parentIdFilter = null, IQueryCollection queryParams = null);
        IList<string> SearchEntities(IOEngine io, string sql, List<object> parameters);
        TableDataRecord Update(IOEngine io, string tableName, string entityId, string uniqueId, IDictionary<string, object> properties);
        TableDataRecord Patch(IOEngine io, string tableName, string entityId, string uniqueId, IDictionary<string, object> properties);
        TableDataRecord Create(IOEngine io, string parentTableName, string tableName, string entityId, string parentId, IDictionary<string, object> properties);
        void Delete(IOEngine io, string tableName, string entityId, string uniqueId);
        IList<string> GetTables(IOEngine io);
        TableMeta GetTable(IOEngine io, string tableName);
    }
}