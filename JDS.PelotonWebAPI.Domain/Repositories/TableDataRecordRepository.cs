using System;
using System.Collections.Generic;
using System.Linq;
using JDS.PelotonWebAPI.Domain.Wrappers;
using Microsoft.AspNetCore.Http;

namespace JDS.PelotonWebAPI.Domain.Repositories
{
    public class TableDataRecordRepository : ITableDataRecordRepository
    {
        private readonly IDataModelRepository _dataModelRepository;

        public TableDataRecordRepository(IDataModelRepository dataModelRepository)
        {
            _dataModelRepository = dataModelRepository;
        }

        public TableDataRecord Get(IOEngine io, string tableName, string entityId, string uniqueId,
            IQueryCollection queryParams = null)
        {
            var td = io.Tables[tableName].GetData(entityId);

            var tdr = td.Records[uniqueId];

            tdr.Properties = new Dictionary<string, object>();

            if (queryParams != null && queryParams.ContainsKey("select"))
            {
                string selectedFieldsList = queryParams["select"];

                var selectedFields = selectedFieldsList.Split(',');

                foreach (var p in selectedFields)
                {
                    if (io.Tables[tableName].Fields.ContainsKey(p.ToLower()))
                    {
                        tdr.Properties.Add(_dataModelRepository.GetFieldName(tableName, p), tdr.get_ItemConverted(p));
                    }
                }
            }
            else
            {
                foreach (var p in io.Tables[tableName.ToLower()].Fields.Keys)
                {
                    if (io.Tables[tableName].Fields.ContainsKey(p.ToLower()))
                    {
                        tdr.Properties.Add(_dataModelRepository.GetFieldName(tableName, p), tdr.get_ItemConverted(p));
                    }
                }
            }

            return tdr;
        }

        public IEnumerable<TableDataRecord> Query(IOEngine io, string tableName, string entityId,
            string parentIdFilter = null, IQueryCollection queryParams = null)
        {
            var td = io.Tables[tableName].GetData(entityId);

            td.Records.ParentIdFilter = parentIdFilter;

            var result = td.Records.ToList();

            if (queryParams != null && queryParams.ContainsKey("select"))
            {
                string selectedFieldsList = queryParams["select"];

                var selectedFields = selectedFieldsList.Split(',');

                foreach (var tdr in result)
                {
                    tdr.Properties = new Dictionary<string, object>();

                    foreach (var p in selectedFields)
                    {
                        if (io.Tables[tableName].Fields.ContainsKey(p.ToLower()))
                        {
                            tdr.Properties.Add(_dataModelRepository.GetFieldName(tableName, p), tdr.get_ItemConverted(p));
                        }
                    }
                }
            }

            return result;
        }

        public TableDataRecord Update(IOEngine io, string tableName, string entityId, string uniqueId,
            IDictionary<string, object> properties)
        {
            properties = new Dictionary<string, object>(properties, StringComparer.OrdinalIgnoreCase);

            var td = io.Tables[tableName].GetData(entityId);

            var tdr = td.Records[uniqueId];

            foreach (var f in io.Tables[tableName].Fields)
            {
                try
                {
                    tdr.set_ItemConverted(f.Key, properties.ContainsKey(f.Key) ? properties[f.Key] : null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            td.Update(true);

            td.Refresh();

            tdr = td.Records[uniqueId];

            tdr.Properties = new Dictionary<string, object>();

            foreach (var p in io.Tables[tableName.ToLower()].Fields.Keys)
            {
                tdr.Properties.Add(_dataModelRepository.GetFieldName(tableName, p), tdr.get_ItemConverted(p));
            }


            return tdr;
        }

        public TableDataRecord Patch(IOEngine io, string tableName, string entityId, string uniqueId,
            IDictionary<string, object> properties)
        {
            properties = new Dictionary<string, object>(properties, StringComparer.OrdinalIgnoreCase);

            var td = io.Tables[tableName].GetData(entityId);

            var tdr = td.Records[uniqueId];

            foreach (var p in properties.Where(prop => io.Tables[tableName].Fields.ContainsKey(prop.Key.ToLower())))
            {
                try
                {
                    tdr.set_ItemConverted(p.Key.ToLower(), p.Value);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            td.Update(true);

            td.Refresh();

            tdr = td.Records[uniqueId];

            tdr.Properties = new Dictionary<string, object>();

            foreach (var p in io.Tables[tableName.ToLower()].Fields.Keys)
            {
                tdr.Properties.Add(_dataModelRepository.GetFieldName(tableName, p), tdr.get_ItemConverted(p));
            }
                
            return tdr;
        }

        public TableDataRecord Create(IOEngine io, string parentTableName, string tableName, string entityId, string parentId, IDictionary<string, object> properties)
        {
            properties = new Dictionary<string, object>(properties, StringComparer.OrdinalIgnoreCase);

            TableData td;
            TableDataRecord tdr;

            if (io.TableMain.Key == tableName)
            {
                entityId = io.AddEntity();
                td = io.Tables[tableName].GetData(entityId);
                tdr = td.Records[entityId];
            }
            else
            {
                td = io.Tables[tableName].GetData(entityId);
                tdr = td.Records.Add(parentId);
            }

            var newUniqueId = tdr.UniqueId;

            foreach (var p in properties.Where(prop => io.Tables[tableName].Fields.ContainsKey(prop.Key)))
            {
                try
                {
                    tdr.set_ItemConverted(p.Key.ToLower(), p.Value);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            td.Update(true);

            td.Refresh();

            tdr = td.Records[newUniqueId];

            tdr.Properties = new Dictionary<string, object>();

            foreach (var p in io.Tables[tableName.ToLower()].Fields.Keys)
            {
                tdr.Properties.Add(_dataModelRepository.GetFieldName(tableName, p), tdr.get_ItemConverted(p));
            }

            return tdr;
        }

        public void Delete(IOEngine io, string tableName, string entityId, string uniqueId)
        {
            var td = io.Tables[tableName].GetData(entityId);

            if (td.Records.Contains(uniqueId))
            {
                td.Records.Remove(uniqueId);

                td.Update(true);
            }
        }

        public IList<string> GetTables(IOEngine io)
        {
            return io.Tables.Keys.OrderBy(t => t).ToList();
        }

        public TableMeta GetTable(IOEngine io, string tableName)
        {
            return new TableMeta(io.Tables[tableName]);
        }

        public IList<string> SearchEntities(IOEngine io, string sql, List<object> parameters)
        {
            var result = io.Search(sql, parameters);

            return result;
        }
    }
}