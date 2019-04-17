//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace JDS.PelotonWebAPI.Domain.Wrappers.Reflection
//{
//    public class LibraryIO : IwLibraryIO
//    {
//        private readonly object _libraryIo;

//        private Type _libraryIoType;

//        public object Raw => _libraryIo;

//        private MethodInfo _getDataTable;
//        private MethodInfo _write;

//        public LibraryIO(object libraryIo)
//        {
//            var assembly = Assembly.LoadFrom(@"C:\Peloton\WellView 10.2.20161107\app\system\bin\Peloton.AppFrame.IO.dll");

//            _libraryIoType = assembly.GetType("Peloton.AppFrame.IO.LibraryIO");

//            _libraryIo = libraryIo;

//            _getDataTable = _libraryIoType.GetMethod("GetDataTable", new Type[] { typeof(string) });
//            _write = _libraryIoType.GetMethod("Write", new Type[] { typeof(string), typeof(DataTable) });
//        }

//        public DataTable GetDataTable(string tableKey)
//        {
//            return (DataTable)_getDataTable.Invoke(_libraryIo, new object[] { tableKey });
//        }

//        public void Write(string tableKey, DataTable dataTable)
//        {
//            _write.Invoke(_libraryIo, new object[] { tableKey, dataTable });
//        }
//    }
//}
