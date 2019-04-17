//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace JDS.PelotonWebAPI.Domain.Wrappers.Reflection
//{
//    public class TableData : IwTableData
//    {
//        private object _tableData;
//        private Type _tableDataType;

//        public DateTime Date { get { return (DateTime)_date.GetValue(_tableData); } }
//        public string EntityId { get { return _entityId.GetValue(_tableData) as string; } }
//        public IEnumerable<IwTableDataRecord> Records { get
//            {
//                return ((IEnumerable<object>)_records.GetValue(_tableData)).Select(r => new TableDataRecord(r));
//            }
//        }
//        public object Raw => _tableData;

//        private PropertyInfo _date;
//        private PropertyInfo _entityId;
//        private PropertyInfo _records;

//        private MethodInfo _refresh;
//        private MethodInfo _revert;
//        private MethodInfo _toString;
//        private MethodInfo _update1;
//        private MethodInfo _update2;
//        private MethodInfo _update3;

//        public TableData(object tableData)
//        {
//            var assembly = Assembly.LoadFrom(@"C:\Peloton\WellView 10.2.20161107\app\system\bin\Peloton.AppFrame.IO.dll");

//            _tableDataType = assembly.GetType("Peloton.AppFrame.IO.TableData");

//            _tableData = tableData;

//            _date = _tableDataType.GetProperty("Date");
//            _entityId = _tableDataType.GetProperty("EntityId");
//            _records = _tableDataType.GetProperty("Records");

//            _refresh = _tableDataType.GetMethod("Refresh");
//            _revert = _tableDataType.GetMethod("Revert");
//            _toString = _tableDataType.GetMethod("ToString");
//            _update1 = _tableDataType.GetMethod("Update", new Type[] { typeof(string) });
//            _update2 = _tableDataType.GetMethod("Update", new Type[] { typeof(bool) });
//            _update3 = _tableDataType.GetMethod("Update", new Type[] { typeof(string), typeof(bool) });
//        }

//        public void Refresh()
//        {
//            _refresh.Invoke(_tableData, new object[] { });
//        }

//        public void Revert()
//        {
//            _revert.Invoke(_tableData, new object[] { });
//        }

//        public override string ToString()
//        {
//            return _toString.Invoke(_tableData, new object[] { }) as string;
//        }

//        public void Update(string tag)
//        {
//            _update1.Invoke(_tableData, new object[] { tag });
//        }

//        public void Update(bool updateParentTables)
//        {
//            _update2.Invoke(_tableData, new object[] { updateParentTables });
//        }

//        public void Update(string tag, bool updateParentTables)
//        {
//            _update3.Invoke(_tableData, new object[] { tag, updateParentTables });
//        }
//    }
//}
