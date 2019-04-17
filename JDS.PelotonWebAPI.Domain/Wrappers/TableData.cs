using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sigil;

namespace JDS.PelotonWebAPI.Domain.Wrappers
{
    public class TableData
    {
        public DateTime Date => GetDate(Raw);
        public string EntityId => GetEntityId(Raw);
        public TableDataRecordCollection Records => new TableDataRecordCollection(GetRecords(Raw));

        [JsonIgnore] public object Raw { get; }

        internal static Func<object, DateTime> GetDate;
        internal static Func<object, string> GetEntityId;
        internal static Func<object, object> GetRecords;

        internal static Action<object> ActRefresh;
        internal static Action<object> ActRevert;
        internal static Func<object, string> FuncToString;
        internal static Action<object, string> ActUpdate1;
        internal static Action<object, bool> ActUpdate2;
        internal static Action<object, string, bool> ActUpdate3;


        static TableData()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.ManifestModule.Name == "Peloton.AppFrame.IO.dll");

            var tableDataType = assembly.GetType("Peloton.AppFrame.IO.TableData");

            // Properties

            var date = tableDataType.GetProperty("Date");
            var entityId = tableDataType.GetProperty("EntityId");
            var records = tableDataType.GetProperty("Records");

            var eGetDate = Emit<Func<object, DateTime>>
                .NewDynamicMethod("GetDate")
                .LoadArgument(0)
                .CastClass(tableDataType)
                .Call(date.GetGetMethod(true))
                .Return();

            var eGetEntityId = Emit<Func<object, string>>
                .NewDynamicMethod("GetEntityId")
                .LoadArgument(0)
                .CastClass(tableDataType)
                .Call(entityId.GetGetMethod(true))
                .Return();

            var eGetRecords = Emit<Func<object, object>>
                .NewDynamicMethod("GetRecords")
                .LoadArgument(0)
                .CastClass(tableDataType)
                .Call(records.GetGetMethod(true))
                .Return();

            GetDate = eGetDate.CreateDelegate();
            GetEntityId = eGetEntityId.CreateDelegate();
            GetRecords = eGetRecords.CreateDelegate();

            // Methods

            var refresh = tableDataType.GetMethod("Refresh");
            var revert = tableDataType.GetMethod("Revert");
            var toString = tableDataType.GetMethod("ToString");
            var update1 = tableDataType.GetMethod("Update", new[] {typeof(string)});
            var update2 = tableDataType.GetMethod("Update", new[] {typeof(bool)});
            var update3 = tableDataType.GetMethod("Update", new[] {typeof(string), typeof(bool)});

            var eActRefresh = Emit<Action<object>>
                .NewDynamicMethod("ActionRefresh")
                .LoadArgument(0)
                .CastClass(tableDataType)
                .Call(refresh)
                .Return();

            var eActRevert = Emit<Action<object>>
                .NewDynamicMethod("ActionRevert")
                .LoadArgument(0)
                .CastClass(tableDataType)
                .Call(revert)
                .Return();

            var eFuncToString = Emit<Func<object, string>>
                .NewDynamicMethod("FuncToString")
                .LoadArgument(0)
                .CastClass(tableDataType)
                .Call(toString)
                .Return();

            var eActUpdate1 = Emit<Action<object, string>>
                .NewDynamicMethod("ActionUpdate1")
                .LoadArgument(0)
                .CastClass(tableDataType)
                .LoadArgument(1)
                .Call(update1)
                .Return();

            var eActUpdate2 = Emit<Action<object, bool>>
                .NewDynamicMethod("ActionUpdate2")
                .LoadArgument(0)
                .CastClass(tableDataType)
                .LoadArgument(1)
                .Call(update2)
                .Return();

            var eActUpdate3 = Emit<Action<object, string, bool>>
                .NewDynamicMethod("ActionUpdate3")
                .LoadArgument(0)
                .CastClass(tableDataType)
                .LoadArgument(1)
                .LoadArgument(2)
                .Call(update3)
                .Return();

            ActRefresh = eActRefresh.CreateDelegate();
            ActRevert = eActRevert.CreateDelegate();
            FuncToString = eFuncToString.CreateDelegate();
            ActUpdate1 = eActUpdate1.CreateDelegate();
            ActUpdate2 = eActUpdate2.CreateDelegate();
            ActUpdate3 = eActUpdate3.CreateDelegate();
        }

        public TableData(object tableData)
        {
            Raw = tableData;
        }

        public void Refresh()
        {
            ActRefresh(Raw);
        }

        public void Revert()
        {
            ActRevert(Raw);
        }

        public override string ToString()
        {
            return FuncToString(Raw);
        }

        public void Update(string tag)
        {
            ActUpdate1(Raw, tag);
        }

        public void Update(bool updateParentTables)
        {
            ActUpdate2(Raw, updateParentTables);
        }

        public void Update(string tag, bool updateParentTables)
        {
            ActUpdate3(Raw, tag, updateParentTables);
        }
    }
}