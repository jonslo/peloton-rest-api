using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace JDS.PelotonWebAPI.Domain.Repositories
{
    public class DataModelRepository : IDataModelRepository
    {
        public IDictionary<string, string> SystemFields { get; set; }
        public IDictionary<string, TableNameInfo> Tables { get; set; }
        private readonly PelotonOptions _options;

        public DataModelRepository(IOptions<PelotonOptions> options)
        {
            _options = options.Value;
            InitSystemFields();
            InitTables();
        }

        private void InitSystemFields()
        {
            SystemFields = new ConcurrentDictionary<string, string>();

            try
            {
                var dataModelAssembly = Assembly.LoadFile(_options.DataModel);

                foreach (var exportedType in dataModelAssembly.ExportedTypes.Where(e => e.FullName.Contains(".SystemFields.") && !e.FullName.Contains("MDLListValues")).OrderBy(e => e.Name))
                {
                    try
                    {
                        var fieldKey = exportedType.GetField("Name").GetValue(exportedType);
                        var fieldName = exportedType.Name;

                        SystemFields.Add(fieldKey.ToString(), fieldName);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Data model not loaded; SystemFields will be null");
            }
        }

        private void InitTables()
        {
            try
            {
                Tables = new ConcurrentDictionary<string, TableNameInfo>();

                var dataModelAssembly = Assembly.LoadFile(_options.DataModel);

                foreach (var exportedType in dataModelAssembly.ExportedTypes.Where(e => !e.FullName.Contains("+Fields") && !e.FullName.Contains("SysIntegration") && !e.FullName.Contains(".SystemFields.")).OrderBy(e => e.Name))
                {
                    try
                    {
                        var tableKey = exportedType.GetField("Name").GetValue(exportedType).ToString();
                        var tableName = exportedType.Name;

                        //Console.WriteLine($"{tableName} - {tableKey}");

                        Tables.Add(tableKey, new TableNameInfo() { Fields = new ConcurrentDictionary<string, string>(), TableName = tableName });

                        var fieldMemberSet = dataModelAssembly.ExportedTypes.FirstOrDefault(f => f.FullName == exportedType.FullName + "+Fields");

                        foreach (var fieldMember in fieldMemberSet.GetNestedTypes())
                        {

                            //Console.WriteLine($"\t\t{fieldMember.Name} - {fieldMember.GetField("Name").GetValue(fieldMember)}");

                            Tables[tableKey].Fields.Add(fieldMember.GetField("Name").GetValue(fieldMember).ToString(), fieldMember.Name);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Data model not loaded; SystemFields will be null");
            }
        }

        public string GetTableName(string tableKey)
        {
            if (Tables != null && Tables.ContainsKey(tableKey))
            {
                return Tables[tableKey].TableName;
            }

            return tableKey;
        }

        public string GetFieldName(string tableKey, string fieldKey)
        {
            if (Tables != null && Tables.ContainsKey(tableKey))
            {
                if (Tables[tableKey].Fields.ContainsKey(fieldKey))
                {
                    return Tables[tableKey].Fields[fieldKey];
                }

                if (SystemFields != null && SystemFields.ContainsKey(fieldKey))
                {
                    return SystemFields[fieldKey];
                }

                return fieldKey;
            }
            else
            {
                return fieldKey;
            }
        }
    }
}
