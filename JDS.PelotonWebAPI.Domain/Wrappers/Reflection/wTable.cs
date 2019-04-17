//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace JDS.PelotonWebAPI.Domain.Wrappers.Reflection
//{
//    public class Table : IwTable
//    {
//        private Type _tableType;
//        private object _table;

//        public string CaptionLong { get { return _captionLong.GetValue(_table) as string; } }
//        public string CaptionShort { get { return _captionShort.GetValue(_table) as string; } }
//        public IList<IwTable> ChildTables
//        {
//            get
//            {
//                var children = (IList)_childTables.GetValue(_table);

//                var list = new List<IwTable>();

//                foreach (var child in children)
//                {
//                    list.Add(new Table(child));
//                }

//                return list;
//            }
//        }
//        public Dictionary<string, IwField> Fields
//        {
//            get
//            {
//                var dictionary = new Dictionary<string, IwField>();
//                var d = (IDictionary)_fields.GetValue(_table);

//                foreach (var key in d.Keys)
//                {
//                    dictionary.Add(key as string, new Field(d[key]));
//                }

//                return dictionary;
//            }
//        }
//        public bool HasAttachment { get { return (bool)_hasAttachment.GetValue(_table); } }
//        public bool IsTableMain { get { return (bool)_isTableMain.GetValue(_table); } }
//        public string Key { get { return _key.GetValue(_table) as string; } }
//        public IwTable ParentTable
//        {
//            get
//            {
//                var value = _parentTable.GetValue(_table);

//                if (value == null)
//                {
//                    return null;
//                }

//                return new Table(value);
//            }
//        }
//        public bool Sequenced { get { return (bool)_sequenced.GetValue(_table); } }
//        public object Raw => _table;

//        private PropertyInfo _captionLong;
//        private PropertyInfo _captionShort;
//        private PropertyInfo _childTables;
//        private PropertyInfo _fields;
//        private PropertyInfo _hasAttachment;
//        private PropertyInfo _isTableMain;
//        private PropertyInfo _key;
//        private PropertyInfo _parentTable;
//        private PropertyInfo _sequenced;

//        private MethodInfo _getData1;
//        private MethodInfo _getData2;
//        private MethodInfo _toString;

//        public Table(object table)
//        {
//            var assembly = Assembly.LoadFrom(@"C:\Peloton\WellView 10.2.20161107\app\system\bin\Peloton.AppFrame.IO.dll");

//            _tableType = assembly.GetType("Peloton.AppFrame.IO.Table");

//            _table = table;

//            _captionLong = _tableType.GetProperty("CaptionLong");
//            _captionShort = _tableType.GetProperty("CaptionShort");
//            _childTables = _tableType.GetProperty("ChildTables");
//            _fields = _tableType.GetProperty("Fields");
//            _hasAttachment = _tableType.GetProperty("HasAttachment");
//            _isTableMain = _tableType.GetProperty("IsTableMain");
//            _key = _tableType.GetProperty("Key");
//            _parentTable = _tableType.GetProperty("ParentTable");
//            _sequenced = _tableType.GetProperty("Sequenced");

//            _getData1 = _tableType.GetMethod("GetData", new Type[] { typeof(string) });
//            _getData2 = _tableType.GetMethod("GetData", new Type[] { typeof(string), typeof(bool) });
//            _toString = _tableType.GetMethod("ToString", new Type[] { });
//        }

//        public IwTableData GetData(string entityId)
//        {
//            return new TableData(_getData1.Invoke(_table, new object[] { entityId }));
//        }

//        public IwTableData GetData(string entityId, bool checkDbModDate)
//        {
//            return new TableData(_getData2.Invoke(_table, new object[] { entityId, checkDbModDate }));
//        }

//        public override string ToString()
//        {
//            return _toString.Invoke(_table, new object[] { }) as string;
//        }

//    }
//}
