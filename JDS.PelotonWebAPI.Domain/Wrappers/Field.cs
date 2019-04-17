using System;
using System.Linq;
using System.Reflection;
using JDS.PelotonWebAPI.Domain.Enums;
using Newtonsoft.Json;
using Sigil;

namespace JDS.PelotonWebAPI.Domain.Wrappers
{
    public class Field
    {
        internal static readonly Func<object, bool> GetApplyDatum;
        internal static readonly Func<object, bool> GetCalculated;
        internal static readonly Func<object, string> GetCaptionLong;
        internal static readonly Func<object, string> GetCaptionShort;
        internal static readonly Func<object, double> GetCurrentUnitExponent;
        internal static readonly Func<object, double> GetCurrentUnitFactor;
        internal static readonly Func<object, double> GetCurrentUnitOffset;
        internal static readonly Func<object, string> GetCurrentUnitLabel;
        internal static readonly Func<object, int> GetDataType;
        internal static readonly Func<object, string> GetHelp;
        internal static readonly Func<object, bool> GetHidden;
        internal static readonly Func<object, bool> GetIsId;
        internal static readonly Func<object, bool> GetIsSystem;
        internal static readonly Func<object, string> GetKey;
        internal static readonly Func<object, string> GetLibraryFieldName;
        internal static readonly Func<object, string> GetLibraryTableName;
        internal static readonly Func<object, int> GetLibraryType;
        internal static readonly Func<object, int> GetSize;
        internal static readonly Func<object, object> GetTable;

        internal static readonly Func<object, string, string, object, bool, string> FuncRawToUser;
        internal static readonly Func<object, string> FuncToString;


        static Field()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.ManifestModule.Name == "Peloton.AppFrame.IO.dll");

            var fieldType = assembly.GetType("Peloton.AppFrame.IO.Field");

            var applyDatum = fieldType.GetProperty("ApplyDatum");
            var calculated = fieldType.GetProperty("Calculated");
            var captionLong = fieldType.GetProperty("CaptionLong");
            var captionShort = fieldType.GetProperty("CaptionShort");
            var currentUnitExponent = fieldType.GetProperty("CurrentUnitExponent");
            var currentUnitFactor = fieldType.GetProperty("CurrentUnitFactor");
            var currentUnitOffset = fieldType.GetProperty("CurrentUnitOffset");
            var currentUnitLabel = fieldType.GetProperty("CurrentUnitLabel");
            var dataType = fieldType.GetProperty("DataType");
            var help = fieldType.GetProperty("Help");
            var hidden = fieldType.GetProperty("Hidden");
            var isId = fieldType.GetProperty("IsId");
            var isSystem = fieldType.GetProperty("IsSystem");
            var key = fieldType.GetProperty("Key");
            var libraryFieldName = fieldType.GetProperty("LibraryFieldName");
            var libraryTableName = fieldType.GetProperty("LibraryTableName");
            var libraryType = fieldType.GetProperty("LibraryType");
            var size = fieldType.GetProperty("Size");
            var table = fieldType.GetProperty("Table");

            var eGetApplyDatum = Emit<Func<object, bool>>
                .NewDynamicMethod("GetApplyDatum")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(applyDatum.GetGetMethod(true))
                .Return();

            var eGetCalculated = Emit<Func<object, bool>>
                .NewDynamicMethod("GetCalculated")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(calculated.GetGetMethod(true))
                .Return();

            var eGetCaptionLong = Emit<Func<object, string>>
                .NewDynamicMethod("GetCaptionLong")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(captionLong.GetGetMethod(true))
                .Return();

            var eGetCaptionShort = Emit<Func<object, string>>
                .NewDynamicMethod("GetCaptionShort")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(captionShort.GetGetMethod(true))
                .Return();

            var eGetCurrentUnitExponent = Emit<Func<object, double>>
                .NewDynamicMethod("GetCurrentUnitExponent")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(currentUnitExponent.GetGetMethod(true))
                .Return();

            var eGetCurrentUnitFactor = Emit<Func<object, double>>
                .NewDynamicMethod("GetCurrentUnitFactor")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(currentUnitFactor.GetGetMethod(true))
                .Return();

            var eGetCurrentUnitOffset = Emit<Func<object, double>>
                .NewDynamicMethod("GetCurrentUnitOffset")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(currentUnitOffset.GetGetMethod(true))
                .Return();

            var eGetCurrentUnitLabel = Emit<Func<object, string>>
                .NewDynamicMethod("GetCurrentUnitLabel")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(currentUnitLabel.GetGetMethod(true))
                .Return();

            var eGetDataType = Emit<Func<object, int>>
                .NewDynamicMethod("GetDataType")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(dataType.GetGetMethod(true))
                .Return();

            var eGetHelp = Emit<Func<object, string>>
                .NewDynamicMethod("GetHelp")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(help.GetGetMethod(true))
                .Return();

            var eGetHidden = Emit<Func<object, bool>>
                .NewDynamicMethod("GetHidden")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(hidden.GetGetMethod(true))
                .Return();

            var eGetIsId = Emit<Func<object, bool>>
                .NewDynamicMethod("GetIsId")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(isId.GetGetMethod(true))
                .Return();

            var eGetIsSystem = Emit<Func<object, bool>>
                .NewDynamicMethod("GetIsSystem")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(isSystem.GetGetMethod(true))
                .Return();

            var eGetKey = Emit<Func<object, string>>
                .NewDynamicMethod("GetKey")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(key.GetGetMethod(true))
                .Return();

            var eGetLibraryFieldName = Emit<Func<object, string>>
                .NewDynamicMethod("GetLibraryFieldName")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(libraryFieldName.GetGetMethod(true))
                .Return();

            var eGetLibraryTableName = Emit<Func<object, string>>
                .NewDynamicMethod("GetLibraryTableName")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(libraryTableName.GetGetMethod(true))
                .Return();

            var eGetLibraryType = Emit<Func<object, int>>
                .NewDynamicMethod("GetLibraryType")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(libraryType.GetGetMethod(true))
                .Return();

            var eGetSize = Emit<Func<object, int>>
                .NewDynamicMethod("GetSize")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(size.GetGetMethod(true))
                .Return();

            var eGetTable = Emit<Func<object, object>>
                .NewDynamicMethod("GetTable")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(table.GetGetMethod(true))
                .Return();

            GetApplyDatum = eGetApplyDatum.CreateDelegate();
            GetCalculated = eGetCalculated.CreateDelegate();

            GetCaptionLong = eGetCaptionLong.CreateDelegate();
            GetCaptionShort = eGetCaptionShort.CreateDelegate();
            GetCurrentUnitExponent = eGetCurrentUnitExponent.CreateDelegate();
            GetCurrentUnitFactor = eGetCurrentUnitFactor.CreateDelegate();
            GetCurrentUnitOffset = eGetCurrentUnitOffset.CreateDelegate();
            GetCurrentUnitLabel = eGetCurrentUnitLabel.CreateDelegate();
            GetDataType = eGetDataType.CreateDelegate();
            GetHelp = eGetHelp.CreateDelegate();
            GetHidden = eGetHidden.CreateDelegate();
            GetIsId = eGetIsId.CreateDelegate();
            GetIsSystem = eGetIsSystem.CreateDelegate();
            GetKey = eGetKey.CreateDelegate();
            GetLibraryFieldName = eGetLibraryFieldName.CreateDelegate();
            GetLibraryTableName = eGetLibraryTableName.CreateDelegate();
            GetLibraryType = eGetLibraryType.CreateDelegate();
            GetSize = eGetSize.CreateDelegate();
            GetTable = eGetTable.CreateDelegate();

            var rawToUser = fieldType.GetMethod("RawToUser");
            var toString = fieldType.GetMethod("ToString");

            var eToString = Emit<Func<object, string>>
                .NewDynamicMethod("FuncToString")
                .LoadArgument(0)
                .CastClass(fieldType)
                .Call(toString)
                .Return();

            var eRawToUser = Emit<Func<object, string, string, object, bool, string>>
                .NewDynamicMethod("FuncRawToUser")
                .LoadArgument(0)
                .CastClass(fieldType)
                .LoadArgument(1)
                .LoadArgument(2)
                .LoadArgument(3)
                .LoadArgument(4)
                .Call(rawToUser)
                .Return();

            FuncRawToUser = eRawToUser.CreateDelegate();
            FuncToString = eToString.CreateDelegate();
        }

        public Field(object field)
        {
            Raw = field;
        }

        public bool ApplyDatum => GetApplyDatum(Raw);
        public bool Calculated => GetCalculated(Raw);
        public string CaptionLong => GetCaptionLong(Raw);
        public string CaptionShort => GetCaptionShort(Raw);
        public double CurrentUnitExponent => GetCurrentUnitExponent(Raw);
        public double CurrentUnitFactor => GetCurrentUnitFactor(Raw);
        public string CurrentUnitLabel => GetCurrentUnitLabel(Raw);
        public double CurrentUnitOffset => GetCurrentUnitOffset(Raw);
        public FieldDataType DataType => (FieldDataType) GetDataType(Raw);
        public string Help => GetHelp(Raw);
        public bool Hidden => GetHidden(Raw);
        public bool IsId => GetIsId(Raw);
        public bool IsSystem => GetIsSystem(Raw);
        public string Key => GetKey(Raw);
        public string LibraryFieldName => GetLibraryFieldName(Raw);
        public string LibraryTableName => GetLibraryTableName(Raw);
        public LibraryType LibraryType => (LibraryType) GetLibraryType(Raw);
        public int Size => GetSize(Raw);
        public Table Table => new Table(GetTable(Raw));

        [JsonIgnore] public object Raw { get; }

        public string RawToUser(string entityId, string recordId, object rawValue, bool showUnitLabel)
        {
            return FuncRawToUser(Raw, entityId, recordId, rawValue, showUnitLabel);
        }

        public override string ToString()
        {
            return FuncToString(Raw);
        }
    }
}