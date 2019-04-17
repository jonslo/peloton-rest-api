using JDS.PelotonWebAPI.Domain.Enums;
using JDS.PelotonWebAPI.Domain.Wrappers;

namespace JDS.PelotonWebAPI.Domain
{
    public class FieldMeta
    {
        public FieldMeta(Field field)
        {
            ApplyDatum = field.ApplyDatum;
            CaptionLong = field.CaptionLong;
            CaptionShort = field.CaptionShort;
            CurrentUnitExponent = field.CurrentUnitExponent;
            CurrentUnitFactor = field.CurrentUnitFactor;
            CurrentUnitLabel = field.CurrentUnitLabel;
            CurrentUnitOffset = field.CurrentUnitOffset;
            DataType = field.DataType;
            Help = field.Help;
            Hidden = field.Hidden;
            IsId = field.IsId;
            IsSystem = field.IsSystem;
            Key = field.Key;
            LibraryFieldName = field.LibraryFieldName;
            LibraryTableName = field.LibraryTableName;
            LibraryType = field.LibraryType;
            Size = field.Size;
            Table = field.Table.Key;
        }

        public bool ApplyDatum { get; }
        public string CaptionLong { get; }
        public string CaptionShort { get; }
        public double CurrentUnitExponent { get; }
        public double CurrentUnitFactor { get; }
        public string CurrentUnitLabel { get; }
        public double CurrentUnitOffset { get; }
        public FieldDataType DataType { get; }
        public string Help { get; }
        public bool Hidden { get; }
        public bool IsId { get; }
        public bool IsSystem { get; }
        public string Key { get; }
        public string LibraryFieldName { get; }
        public string LibraryTableName { get; }
        public LibraryType LibraryType { get; }
        public int Size { get; }
        public string Table { get; }
    }
}
