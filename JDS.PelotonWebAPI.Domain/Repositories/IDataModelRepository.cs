using System.Collections.Generic;

namespace JDS.PelotonWebAPI.Domain.Repositories
{
    public interface IDataModelRepository
    {
        IDictionary<string, string> SystemFields { get; set; }
        IDictionary<string, TableNameInfo> Tables { get; set; }
        string GetTableName(string tableKey);
        string GetFieldName(string tableKey, string fieldKey);
    }
}
