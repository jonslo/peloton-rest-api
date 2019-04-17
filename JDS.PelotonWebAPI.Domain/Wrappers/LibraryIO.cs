using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Sigil;

namespace JDS.PelotonWebAPI.Domain.Wrappers
{
    public class LibraryIo
    {
        [JsonIgnore] public object Raw { get; }

        internal static Func<object, string, DataTable> FuncGetDataTable;
        internal static Action<object, string, DataTable> ActWrite;

        static LibraryIo()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.ManifestModule.Name == "Peloton.AppFrame.IO.dll");

            var libraryIoType = assembly.GetType("Peloton.AppFrame.IO.LibraryIO");

            var getDataTable = libraryIoType.GetMethod("GetDataTable", new[] { typeof(string) });
            var write = libraryIoType.GetMethod("Write", new[] { typeof(string), typeof(DataTable) });

            var eFuncGetDataTable = Emit<Func<object, string, DataTable>>
                .NewDynamicMethod("FuncGetDataTable")
                .LoadArgument(0)
                .CastClass(libraryIoType)
                .LoadArgument(1)
                .Call(getDataTable)
                .Return();

            var eActWrite = Emit<Action<object, string, DataTable>>
                .NewDynamicMethod("ActWrite")
                .LoadArgument(0)
                .CastClass(libraryIoType)
                .LoadArgument(1)
                .LoadArgument(2)
                .Call(write)
                .Return();

            FuncGetDataTable = eFuncGetDataTable.CreateDelegate();
            ActWrite = eActWrite.CreateDelegate();
        }

        public LibraryIo(object libraryIo)
        {
            Raw = libraryIo;
        }

        public DataTable GetDataTable(string tableKey)
        {
            return FuncGetDataTable(Raw, tableKey);
        }

        public void Write(string tableKey, DataTable dataTable)
        {
            ActWrite(Raw, tableKey, dataTable);
        }
    }
}