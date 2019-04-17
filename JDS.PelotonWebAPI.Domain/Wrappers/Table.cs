using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Sigil;

namespace JDS.PelotonWebAPI.Domain.Wrappers
{
    public class Table
    {
        private object _table;

        public string CaptionLong => GetCaptionLong(_table) as string;
        public string CaptionShort => GetCaptionShort(_table) as string;
        public IList<Table> ChildTables
        {
            get
            {
                var children = GetChildTables(_table);

                var list = new List<Table>();

                foreach (var child in children)
                {
                    list.Add(new Table(child));
                }

                return list;
            }
        }
        public Dictionary<string, Field> Fields
        {
            get
            {
                var dictionary = new Dictionary<string, Field>();
                var d = GetFields(_table);

                foreach (var key in d.Keys)
                {
                    dictionary.Add(key as string, new Field(d[key]));
                }

                return dictionary;
            }
        }
        public bool HasAttachment => GetHasAttachment(_table);
        public bool IsTableMain => GetIsTableMain(_table);
        public string Key => GetKey(_table);
        public Table ParentTable
        {
            get
            {
                var value = GetParentTable(_table);

                if (value == null)
                {
                    return null;
                }

                return new Table(value);
            }
        }
        public bool Sequenced => GetSequenced(_table);

        [JsonIgnore]
        public object Raw => _table;

        internal static Func<object, string> GetCaptionLong;
        internal static Func<object, string> GetCaptionShort;
        internal static Func<object, IList> GetChildTables;
        internal static Func<object, IDictionary> GetFields;
        internal static Func<object, bool> GetHasAttachment;
        internal static Func<object, bool> GetIsTableMain;
        internal static Func<object, string> GetKey;
        internal static Func<object, object> GetParentTable;
        internal static Func<object, bool> GetSequenced;

        internal static Func<object, string, object> FuncGetData1;
        internal static Func<object, string, bool, object> FuncGetData2;
        internal static Func<object, string> FuncToString;

        static Table()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.ManifestModule.Name == "Peloton.AppFrame.IO.dll");

            var tableType = assembly.GetType("Peloton.AppFrame.IO.Table");

            // Properties

            var captionLong = tableType.GetProperty("CaptionLong");
            var captionShort = tableType.GetProperty("CaptionShort");
            var childTables = tableType.GetProperty("ChildTables");
            var fields = tableType.GetProperty("Fields");
            var hasAttachment = tableType.GetProperty("HasAttachment");
            var isTableMain = tableType.GetProperty("IsTableMain");
            var key = tableType.GetProperty("Key");
            var parentTable = tableType.GetProperty("ParentTable");
            var sequenced = tableType.GetProperty("Sequenced");

            var eGetCaptionLong = Emit<Func<object, string>>
            .NewDynamicMethod("GetCaptionLong")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(captionLong.GetGetMethod(nonPublic: true))
            .Return();

            var eGetCaptionShort = Emit<Func<object, string>>
            .NewDynamicMethod("GetCaptionShort")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(captionShort.GetGetMethod(nonPublic: true))
            .Return();

            var eGetChildTables = Emit<Func<object, IList>>
            .NewDynamicMethod("GetChildTables")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(childTables.GetGetMethod(nonPublic: true))
            .CastClass(typeof(IList))
            .Return();

            var eGetFields = Emit<Func<object, IDictionary>>
            .NewDynamicMethod("GetFields")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(fields.GetGetMethod(nonPublic: true))
            .CastClass(typeof(IDictionary))
            .Return();

            var eGetHasAttachment = Emit<Func<object, bool>>
            .NewDynamicMethod("GetHasAttachment")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(hasAttachment.GetGetMethod(nonPublic: true))
            .Return();

            var eGetIsTableMain = Emit<Func<object, bool>>
            .NewDynamicMethod("GetIsTableMain")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(isTableMain.GetGetMethod(nonPublic: true))
            .Return();

            var eGetKey = Emit<Func<object, string>>
            .NewDynamicMethod("GetKey")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(key.GetGetMethod(nonPublic: true))
            .Return();

            var eGetParentTable = Emit<Func<object, object>>
            .NewDynamicMethod("GetParentTable")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(parentTable.GetGetMethod(nonPublic: true))
            .Return();

            var eGetSequenced = Emit<Func<object, bool>>
            .NewDynamicMethod("GetSequenced")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(sequenced.GetGetMethod(nonPublic: true))
            .Return();

            GetCaptionLong = eGetCaptionLong.CreateDelegate();
            GetCaptionShort = eGetCaptionShort.CreateDelegate();
            GetChildTables = eGetChildTables.CreateDelegate();
            GetFields = eGetFields.CreateDelegate();
            GetHasAttachment = eGetHasAttachment.CreateDelegate();
            GetIsTableMain = eGetIsTableMain.CreateDelegate();
            GetKey = eGetKey.CreateDelegate();
            GetParentTable = eGetParentTable.CreateDelegate();
            GetSequenced = eGetSequenced.CreateDelegate();

            // Methods

            var getData1 = tableType.GetMethod("GetData", new Type[] { typeof(string) });
            var getData2 = tableType.GetMethod("GetData", new Type[] { typeof(string), typeof(bool) });
            var toString = tableType.GetMethod("ToString", new Type[] { });

            var eFuncGetData1 = Emit<Func<object, string, object>>
            .NewDynamicMethod("FuncGetData1")
            .LoadArgument(0)
            .CastClass(tableType)
            .LoadArgument(1)
            .Call(getData1)
            .Return();

            var eFuncGetData2 = Emit<Func<object, string, bool, object>>
            .NewDynamicMethod("FuncGetData2")
            .LoadArgument(0)
            .CastClass(tableType)
            .LoadArgument(1)
            .LoadArgument(2)
            .Call(getData2)
            .Return();

            var eFuncToString = Emit<Func<object, string>>
            .NewDynamicMethod("FuncToString")
            .LoadArgument(0)
            .CastClass(tableType)
            .Call(toString)
            .Return();

            FuncGetData1 = eFuncGetData1.CreateDelegate();
            FuncGetData2 = eFuncGetData2.CreateDelegate();
            FuncToString = eFuncToString.CreateDelegate();
        }

        public Table(object table)
        {
            _table = table;
        }

        public TableData GetData(string entityId) => new TableData(FuncGetData1(_table, entityId));

        public TableData GetData(string entityId, bool checkDbModDate) => new TableData(FuncGetData2(_table, entityId, checkDbModDate));

        public override string ToString() => FuncToString(_table);

    }
}
