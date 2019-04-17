//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace JDS.PelotonWebAPI.Domain.Wrappers.Reflection
//{
//    public class TableDataRecord : IwTableDataRecord
//    {
//        private object _tableDataRecord;
//        private Type _tableDataRecordType;
//        private Type _tableType;

//        public DateTime CreateDate { get { return (DateTime)_createDate.GetValue(_tableDataRecord); } }
//        public string CreateUser { get { return _createUser.GetValue(_tableDataRecord) as string; } }
//        public string Descriptor { get { return _descriptor.GetValue(_tableDataRecord) as string; } }
//        public string EntityId { get { return _entityId.GetValue(_tableDataRecord) as string; } }
//        public DateTime LastModDate { get { return (DateTime)_lastModDate.GetValue(_tableDataRecord); } }
//        public string LastModUser { get { return _lastModUser.GetValue(_tableDataRecord) as string; } }
//        public string ParentId { get { return _parentId.GetValue(_tableDataRecord) as string; } }
//        public string UniqueId { get { return _uniqueId.GetValue(_tableDataRecord) as string; } }
//        //public object ItemRawOriginal { get { return (object)_itemRawOriginal.GetValue(_tableDataRecord); } }
//        public IDictionary<string, object> Properties { get; set; }
//        public object Raw => _tableDataRecord;

//        private PropertyInfo _createDate;
//        private PropertyInfo _createUser;
//        private PropertyInfo _descriptor;
//        private PropertyInfo _entityId;
//        private PropertyInfo _lastModDate;
//        private PropertyInfo _lastModUser;
//        private PropertyInfo _parentId;
//        private PropertyInfo _uniqueId;
//        private PropertyInfo _itemRawOriginal;

//        private MethodInfo _getItemConverted1;
//        private MethodInfo _getItemConverted2;
//        private MethodInfo _setItemConverted1;
//        private MethodInfo _setItemConverted2;

//        private MethodInfo _getItemRaw;
//        private MethodInfo _getItemUser;
//        private MethodInfo _setItemRaw;
//        private MethodInfo _setItemUser;

//        private MethodInfo _addChildRecord;
//        private MethodInfo _getAttachment;
//        private MethodInfo _setAttachment1;
//        private MethodInfo _setAttachment2;
//        private MethodInfo _toString;

//        public TableDataRecord(object tableDataRecord)
//        {
//            var assembly = Assembly.LoadFrom(@"C:\Peloton\WellView 10.2.20161107\app\system\bin\Peloton.AppFrame.IO.dll");

//            _tableDataRecordType = assembly.GetType("Peloton.AppFrame.IO.TableDataRecord");
//            _tableType = assembly.GetType("Peloton.AppFrame.IO.Table");

//            _tableDataRecord = tableDataRecord;

//            _createDate = _tableDataRecordType.GetProperty("CreateDate");
//            _createUser = _tableDataRecordType.GetProperty("CreateUser");
//            _descriptor = _tableDataRecordType.GetProperty("Descriptor");
//            _entityId = _tableDataRecordType.GetProperty("EntityId");
//            _lastModDate = _tableDataRecordType.GetProperty("LastModDate");
//            _lastModUser = _tableDataRecordType.GetProperty("LastModUser");
//            _parentId = _tableDataRecordType.GetProperty("ParentId");
//            _uniqueId = _tableDataRecordType.GetProperty("UniqueId");
//            _itemRawOriginal = _tableDataRecordType.GetProperty("ItemRawOriginal");
            
//            _getItemConverted1 = _tableDataRecordType.GetMethod("get_ItemConverted", new Type[] { typeof(string) });
//            _getItemConverted2 = _tableDataRecordType.GetMethod("get_ItemConverted", new Type[] { typeof(string), typeof(string) });

//            _setItemConverted1 = _tableDataRecordType.GetMethod("set_ItemConverted", new Type[] { typeof(string), typeof(object) });
//            _setItemConverted2 = _tableDataRecordType.GetMethod("set_ItemConverted", new Type[] { typeof(string), typeof(string), typeof(object) });

//            _getItemRaw = _tableDataRecordType.GetMethod("get_ItemRaw", new Type[] { typeof(string) });
//            _getItemUser = _tableDataRecordType.GetMethod("get_ItemUser", new Type[] { typeof(string) });

//            _setItemRaw = _tableDataRecordType.GetMethod("set_ItemRaw", new Type[] { typeof(string), typeof(object) });
//            _setItemUser = _tableDataRecordType.GetMethod("set_ItemUser", new Type[] { typeof(string), typeof(string) });

//            _addChildRecord = _tableDataRecordType.GetMethod("AddChildRecord", new Type[] { _tableType });
//            _getAttachment = _tableDataRecordType.GetMethod("GetAttachment", new Type[] { typeof(string) });
//            _setAttachment1 = _tableDataRecordType.GetMethod("SetAttachment", new Type[] { typeof(string) });
//            _setAttachment2 = _tableDataRecordType.GetMethod("SetAttachment", new Type[] { typeof(string), typeof(bool) });
//        }

//        public object get_ItemConverted(string fieldname)
//        {
//            return _getItemConverted1.Invoke(_tableDataRecord, new object[] { fieldname });
//        }

//        public object get_ItemConverted(string fieldname, string unitLabel)
//        {
//            return _getItemConverted2.Invoke(_tableDataRecord, new object[] { fieldname, unitLabel });
//        }

//        public void set_ItemConverted(string fieldname, object value)
//        {
//            _setItemConverted1.Invoke(_tableDataRecord, new object[] { fieldname, value });
//        }

//        public void set_ItemConverted(string fieldname, string unitLabel, object value)
//        {
//            _setItemConverted2.Invoke(_tableDataRecord, new object[] { fieldname, unitLabel, value });
//        }

//        public object get_ItemRaw(string fieldname)
//        {
//            return _getItemRaw.Invoke(_tableDataRecord, new object[] { fieldname });
//        }

//        public void set_ItemRaw(string fieldname, object value)
//        {
//            _setItemRaw.Invoke(_tableDataRecord, new object[] { fieldname, value });
//        }

//        public object get_ItemUser(string fieldname)
//        {
//            return _getItemUser.Invoke(_tableDataRecord, new object[] { fieldname });
//        }

//        public void set_ItemUser(string fieldname, string value)
//        {
//            _setItemUser.Invoke(_tableDataRecord, new object[] { fieldname, value });
//        }
//    }
//}
