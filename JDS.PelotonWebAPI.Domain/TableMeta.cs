using System.Collections.Generic;
using System.Linq;
using JDS.PelotonWebAPI.Domain.Wrappers;

namespace JDS.PelotonWebAPI.Domain
{
    public class TableMeta
    {
        public TableMeta(Table table)
        {
            CaptionLong = table.CaptionLong;
            CaptionShort = table.CaptionShort;
            ChildTables = table.ChildTables.Select(t => t.Key);

            Fields = new Dictionary<string, FieldMeta>();

            foreach (var field in table.Fields.OrderBy(f => f.Key))
            {
                Fields.Add(field.Key, new FieldMeta(field.Value));
            }

            HasAttachment = table.HasAttachment;
            IsTableMain = table.IsTableMain;
            Key = table.Key;
            ParentTable = table.ParentTable?.Key;
            Sequenced = table.Sequenced;
        }

        public string CaptionLong { get; }
        public string CaptionShort { get; }
        public IEnumerable<string> ChildTables { get; }
        public Dictionary<string, FieldMeta> Fields { get; }
        public bool HasAttachment { get; }
        public bool IsTableMain { get; }
        public string Key { get; }
        public string ParentTable { get; }
        public bool Sequenced { get; }
    }
}
