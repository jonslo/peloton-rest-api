using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sigil;

namespace JDS.PelotonWebAPI.Domain.Wrappers
{
    public class TableDataRecordCollection : IEnumerable<TableDataRecord>
    {
        internal static Func<object, int> GetCount;
        internal static Func<object, string> GetParentIdFilter;
        internal static Action<object, string> SetParentIdFilter;
        internal static Func<object, int, object> GetItemIndex;
        internal static Func<object, string, object> GetItemId;

        internal static Func<object, string, object> FuncAdd;
        internal static Func<object, string, bool> FuncContains;
        internal static Action<object, string> ActFilter;
        internal static Func<object, string, bool> FuncRemove;
        internal static Action<object, string> ActSort;

        static TableDataRecordCollection()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.ManifestModule.Name == "Peloton.AppFrame.IO.dll");

            var tableDataRecordType = assembly.GetType("Peloton.AppFrame.IO.TableDataRecord");
            var tableDataRecordCollectionType = assembly.GetType("Peloton.AppFrame.IO.TableDataRecordCollection");

            var count = tableDataRecordCollectionType.GetProperty("Count");
            var parentIdFilter = tableDataRecordCollectionType.GetProperty("ParentIdFilter");

            var itemIndex =
                tableDataRecordCollectionType.GetProperty("Item", tableDataRecordType, new[] {typeof(int)});
            var itemId =
                tableDataRecordCollectionType.GetProperty("Item", tableDataRecordType, new[] {typeof(string)});

            var eGetCount = Emit<Func<object, int>>
                .NewDynamicMethod("GetCount")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .Call(count.GetGetMethod(true))
                .Return();

            var eGetParentIdFilter = Emit<Func<object, string>>
                .NewDynamicMethod("GetParentIdFilter")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .Call(parentIdFilter.GetGetMethod(true))
                .Return();

            var eSetParentIdFilter = Emit<Action<object, string>>
                .NewDynamicMethod("SetParentIdFilter")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .LoadArgument(1)
                .Call(parentIdFilter.GetSetMethod(true))
                .Return();

            var eGetItemIndex = Emit<Func<object, int, object>>
                .NewDynamicMethod("GetItemByIndex")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .LoadArgument(1)
                .Call(itemIndex.GetGetMethod(true))
                .Return();

            var eGetItemId = Emit<Func<object, string, object>>
                .NewDynamicMethod("GetItemByIndex")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .LoadArgument(1)
                .Call(itemId.GetGetMethod(true))
                .Return();

            GetCount = eGetCount.CreateDelegate();
            GetParentIdFilter = eGetParentIdFilter.CreateDelegate();
            SetParentIdFilter = eSetParentIdFilter.CreateDelegate();
            GetItemIndex = eGetItemIndex.CreateDelegate();
            GetItemId = eGetItemId.CreateDelegate();

            var add = tableDataRecordCollectionType.GetMethod("Add");
            var contains = tableDataRecordCollectionType.GetMethod("Contains");
            var filter = tableDataRecordCollectionType.GetMethod("Filter");
            var remove = tableDataRecordCollectionType.GetMethod("Remove");
            var sort = tableDataRecordCollectionType.GetMethod("Sort");

            var eFuncAdd = Emit<Func<object, string, object>>
                .NewDynamicMethod("FuncAdd")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .LoadArgument(1)
                .Call(add)
                .Return();

            var eFuncContains = Emit<Func<object, string, bool>>
                .NewDynamicMethod("FuncContains")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .LoadArgument(1)
                .Call(contains)
                .Return();

            var eActFilter = Emit<Action<object, string>>
                .NewDynamicMethod("ActFilter")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .LoadArgument(1)
                .Call(filter)
                .Return();

            var eFuncRemove = Emit<Func<object, string, bool>>
                .NewDynamicMethod("FuncContains")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .LoadArgument(1)
                .Call(remove)
                .Return();

            var eActSort = Emit<Action<object, string>>
                .NewDynamicMethod("ActSort")
                .LoadArgument(0)
                .CastClass(tableDataRecordCollectionType)
                .LoadArgument(1)
                .Call(sort)
                .Return();

            FuncAdd = eFuncAdd.CreateDelegate();
            FuncContains = eFuncContains.CreateDelegate();
            ActFilter = eActFilter.CreateDelegate();
            FuncRemove = eFuncRemove.CreateDelegate();
            ActSort = eActSort.CreateDelegate();
        }

        public TableDataRecordCollection(object raw)
        {
            Raw = raw;
        }

        public int Count => GetCount(Raw);

        public TableDataRecord this[int index] => new TableDataRecord(GetItemIndex(Raw, index));

        public TableDataRecord this[string id] => new TableDataRecord(GetItemId(Raw, id));

        public string ParentIdFilter
        {
            get => GetParentIdFilter(Raw);
            set => SetParentIdFilter(Raw, value);
        }

        [JsonIgnore] public object Raw { get; }

        public IEnumerator<TableDataRecord> GetEnumerator()
        {
            foreach (var val in (IEnumerable<object>) Raw) yield return new TableDataRecord(val);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TableDataRecord Add(string parentId)
        {
            return new TableDataRecord(FuncAdd(Raw, parentId));
        }

        public bool Contains(string recordId)
        {
            return FuncContains(Raw, recordId);
        }

        public void Filter(string value)
        {
            ActFilter(Raw, value);
        }

        public bool Remove(string uniqueId)
        {
            return FuncRemove(Raw, uniqueId);
        }

        public void Sort(string sqlOrderBy)
        {
            ActSort(Raw, sqlOrderBy);
        }
    }
}