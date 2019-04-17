using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Sigil;

namespace JDS.PelotonWebAPI.Domain.Wrappers
{
    public class TableDataRecord
    {
        public DateTime CreateDate => GetCreateDate(Raw);
        public string CreateUser => GetCreateUser(Raw);
        public string Descriptor => GetDescriptor(Raw);
        public string EntityId => GetEntityId(Raw);
        public DateTime LastModDate => GetLastModDate(Raw);
        public string LastModUser => GetLastModUser(Raw);
        public string ParentId => GetParentId(Raw);

        public string UniqueId => GetUniqueId(Raw);

        //public object ItemRawOriginal { get { return (object)_itemRawOriginal.GetValue(_tableDataRecord); } }
        public IDictionary<string, object> Properties { get; set; }

        [JsonIgnore] public object Raw { get; }

        internal static readonly Func<object, DateTime> GetCreateDate;
        internal static readonly Func<object, string> GetCreateUser;
        internal static readonly Func<object, string> GetDescriptor;
        internal static readonly Func<object, string> GetEntityId;
        internal static readonly Func<object, DateTime> GetLastModDate;
        internal static readonly Func<object, string> GetLastModUser;
        internal static readonly Func<object, string> GetParentId;

        private static readonly Func<object, string> GetUniqueId;
        //private static Func<object, object> _get_itemRawOriginal;

        internal static readonly Func<object, string, object> FuncGetItemConverted1;
        internal static readonly Func<object, string, string, object> FuncGetItemConverted2;
        internal static readonly Action<object, string, object> FuncSetItemConverted1;
        internal static readonly Action<object, string, string, object> FuncSetItemConverted2;
        internal static readonly Func<object, string, object> FuncGetItemRaw;
        internal static readonly Action<object, string, object> FuncSetItemRaw;
        internal static readonly Func<object, string, object> FuncGetItemUser;
        internal static readonly Action<object, string, string> FuncSetItemUser;
        internal static readonly Func<object, object, object> FuncAddChildRecord;
        internal static readonly Action<object, string> ActGetAttachment;
        internal static readonly Action<object, string> ActSetAttachment1;
        internal static readonly Action<object, string, bool> ActSetAttachment2;
        internal static readonly Func<object, string> FuncToString;

        static TableDataRecord()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.ManifestModule.Name == "Peloton.AppFrame.IO.dll");

            var tableDataRecordType = assembly.GetType("Peloton.AppFrame.IO.TableDataRecord");
            var tableType = assembly.GetType("Peloton.AppFrame.IO.Table");

            var createDate = tableDataRecordType.GetProperty("CreateDate");
            var createUser = tableDataRecordType.GetProperty("CreateUser");
            var descriptor = tableDataRecordType.GetProperty("Descriptor");
            var entityId = tableDataRecordType.GetProperty("EntityId");
            var lastModDate = tableDataRecordType.GetProperty("LastModDate");
            var lastModUser = tableDataRecordType.GetProperty("LastModUser");
            var parentId = tableDataRecordType.GetProperty("ParentId");
            var uniqueId = tableDataRecordType.GetProperty("UniqueId");
            var itemRawOriginal = tableDataRecordType.GetProperty("ItemRawOriginal");

            Debug.Assert(createDate != null, nameof(createDate) + " != null");
            var eGetCreateDate = Emit<Func<object, DateTime>>
                .NewDynamicMethod("GetCreateDate")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .Call(createDate.GetGetMethod(true))
                .Return();

            var eGetCreateUser = Emit<Func<object, string>>
                .NewDynamicMethod("GetCreateUser")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .Call(createUser.GetGetMethod(true))
                .Return();

            var eGetDescriptor = Emit<Func<object, string>>
                .NewDynamicMethod("GetDescriptor")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .Call(descriptor.GetGetMethod(true))
                .Return();

            var eGetEntityId = Emit<Func<object, string>>
                .NewDynamicMethod("GetEntityId")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .Call(entityId.GetGetMethod(true))
                .Return();

            var eGetLastModDate = Emit<Func<object, DateTime>>
                .NewDynamicMethod("GetLastModDate")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .Call(lastModDate.GetGetMethod(true))
                .Return();

            var eGetLastModUser = Emit<Func<object, string>>
                .NewDynamicMethod("GetLastModUser")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .Call(lastModUser.GetGetMethod(true))
                .Return();

            var eGetParentId = Emit<Func<object, string>>
                .NewDynamicMethod("GetParentId")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .Call(parentId.GetGetMethod(true))
                .Return();

            var eGetUniqueId = Emit<Func<object, string>>
                .NewDynamicMethod("GetUniqueId")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .Call(uniqueId.GetGetMethod(true))
                .Return();

            //var e_get_itemRawOriginal = Emit<Func<object, object>>
            //.NewDynamicMethod("GetItemRawOriginal")
            //.LoadArgument(0)
            //.CastClass(tableDataRecordType)
            //.Call(_itemRawOriginal.GetGetMethod(nonPublic: true))
            //.Return();

            GetCreateDate = eGetCreateDate.CreateDelegate();
            GetCreateUser = eGetCreateUser.CreateDelegate();
            GetDescriptor = eGetDescriptor.CreateDelegate();
            GetEntityId = eGetEntityId.CreateDelegate();
            GetLastModDate = eGetLastModDate.CreateDelegate();
            GetLastModUser = eGetLastModUser.CreateDelegate();
            GetParentId = eGetParentId.CreateDelegate();
            GetUniqueId = eGetUniqueId.CreateDelegate();
            //_get_itemRawOriginal = e_get_itemRawOriginal.CreateDelegate();

            // Methods

            var getItemConverted1 = tableDataRecordType.GetMethod("get_ItemConverted", new[] {typeof(string)});
            var getItemConverted2 =
                tableDataRecordType.GetMethod("get_ItemConverted", new[] {typeof(string), typeof(string)});

            var setItemConverted1 =
                tableDataRecordType.GetMethod("set_ItemConverted", new[] {typeof(string), typeof(object)});
            var setItemConverted2 = tableDataRecordType.GetMethod("set_ItemConverted",
                new[] {typeof(string), typeof(string), typeof(object)});

            var getItemRaw = tableDataRecordType.GetMethod("get_ItemRaw", new[] {typeof(string)});
            var getItemUser = tableDataRecordType.GetMethod("get_ItemUser", new[] {typeof(string)});

            var setItemRaw = tableDataRecordType.GetMethod("set_ItemRaw", new[] {typeof(string), typeof(object)});
            var setItemUser = tableDataRecordType.GetMethod("set_ItemUser", new[] {typeof(string), typeof(string)});

            var addChildRecord = tableDataRecordType.GetMethod("AddChildRecord", new[] {tableType});
            var getAttachment = tableDataRecordType.GetMethod("GetAttachment", new[] {typeof(string)});
            var setAttachment1 = tableDataRecordType.GetMethod("SetAttachment", new[] {typeof(string)});
            var setAttachment2 = tableDataRecordType.GetMethod("SetAttachment", new[] {typeof(string), typeof(bool)});
            var toString = tableDataRecordType.GetMethod("ToString", new Type[] { });

            var eFuncGetItemConverted1 = Emit<Func<object, string, object>>
                .NewDynamicMethod("FuncGetItemConverted1")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .Call(getItemConverted1)
                .Return();

            var eFuncGetItemConverted2 = Emit<Func<object, string, string, object>>
                .NewDynamicMethod("FuncGetItemConverted2")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .LoadArgument(2)
                .Call(getItemConverted2)
                .Return();

            var eFuncSetItemConverted1 = Emit<Action<object, string, object>>
                .NewDynamicMethod("FuncSetItemConverted1")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .LoadArgument(2)
                .Call(setItemConverted1)
                .Return();

            var eFuncSetItemConverted2 = Emit<Action<object, string, string, object>>
                .NewDynamicMethod("FuncSetItemConverted2")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .LoadArgument(2)
                .LoadArgument(3)
                .Call(setItemConverted2)
                .Return();

            var eFuncGetItemRaw = Emit<Func<object, string, object>>
                .NewDynamicMethod("FuncGetItemRaw")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .Call(getItemRaw)
                .Return();

            var eFuncSetItemRaw = Emit<Action<object, string, object>>
                .NewDynamicMethod("FuncSetItemRaw")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .LoadArgument(2)
                .Call(setItemRaw)
                .Return();

            var eFuncGetItemUser = Emit<Func<object, string, object>>
                .NewDynamicMethod("FuncGetItemUser")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .Call(getItemUser)
                .Return();

            var eFuncSetItemUser = Emit<Action<object, string, string>>
                .NewDynamicMethod("FuncSetItemUser")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .LoadArgument(2)
                .Call(setItemUser)
                .Return();

            var eFuncAddChildRecord = Emit<Func<object, object, object>>
                .NewDynamicMethod("FuncAddChildRecord")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .CastClass(tableType)
                .Call(addChildRecord)
                .Return();

            var eActGetAttachment = Emit<Action<object, string>>
                .NewDynamicMethod("ActGetAttachment")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .Call(getAttachment)
                .Return();

            var eActSetAttachment1 = Emit<Action<object, string>>
                .NewDynamicMethod("ActSetAttachment1")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .Call(setAttachment1)
                .Return();

            var eActSetAttachment2 = Emit<Action<object, string, bool>>
                .NewDynamicMethod("ActSetAttachment2")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .LoadArgument(1)
                .LoadArgument(2)
                .Call(setAttachment2)
                .Return();

            var eFuncToString = Emit<Func<object, string>>
                .NewDynamicMethod("FuncToString")
                .LoadArgument(0)
                .CastClass(tableDataRecordType)
                .Call(toString)
                .Return();

            FuncGetItemConverted1 = eFuncGetItemConverted1.CreateDelegate();
            FuncGetItemConverted2 = eFuncGetItemConverted2.CreateDelegate();
            FuncSetItemConverted1 = eFuncSetItemConverted1.CreateDelegate();
            FuncSetItemConverted2 = eFuncSetItemConverted2.CreateDelegate();
            FuncGetItemRaw = eFuncGetItemRaw.CreateDelegate();
            FuncSetItemRaw = eFuncSetItemRaw.CreateDelegate();
            FuncGetItemUser = eFuncGetItemUser.CreateDelegate();
            FuncSetItemUser = eFuncSetItemUser.CreateDelegate();
            FuncAddChildRecord = eFuncAddChildRecord.CreateDelegate();
            ActGetAttachment = eActGetAttachment.CreateDelegate();
            ActSetAttachment1 = eActSetAttachment1.CreateDelegate();
            ActSetAttachment2 = eActSetAttachment2.CreateDelegate();
            FuncToString = eFuncToString.CreateDelegate();
        }

        public TableDataRecord(object tableDataRecord)
        {
            Raw = tableDataRecord;
        }

        public object get_ItemConverted(string fieldname)
        {
            return FuncGetItemConverted1(Raw, fieldname);
        }

        public object get_ItemConverted(string fieldname, string unitLabel)
        {
            return FuncGetItemConverted2(Raw, fieldname, unitLabel);
        }

        public void set_ItemConverted(string fieldname, object value)
        {
            FuncSetItemConverted1(Raw, fieldname, value);
        }

        public void set_ItemConverted(string fieldname, string unitLabel, object value)
        {
            FuncSetItemConverted2(Raw, fieldname, unitLabel, value);
        }

        public object get_ItemRaw(string fieldname)
        {
            return FuncGetItemRaw(Raw, fieldname);
        }

        public void set_ItemRaw(string fieldname, object value)
        {
            FuncSetItemRaw(Raw, fieldname, value);
        }

        public object get_ItemUser(string fieldname)
        {
            return FuncGetItemUser(Raw, fieldname);
        }

        public void set_ItemUser(string fieldname, string value)
        {
            FuncSetItemUser(Raw, fieldname, value);
        }

        public object AddChildRecord(object childTable)
        {
            return FuncAddChildRecord(Raw, childTable);
        }

        public void GetAttachment(string destinationPath)
        {
            ActGetAttachment(Raw, destinationPath);
        }

        public void SetAttachment(string filePath)
        {
            ActSetAttachment1(Raw, filePath);
        }

        public void SetAttachment(string filePath, bool linkOnly)
        {
            ActSetAttachment2(Raw, filePath, linkOnly);
        }

        public override string ToString()
        {
            return FuncToString(Raw);
        }
    }
}