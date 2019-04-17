//using JDS.PelotonWebAPI.Domain.Enums;
//using System;
//using System.Reflection;

//namespace JDS.PelotonWebAPI.Domain.Wrappers.Reflection
//{
//    public class Field : IwField
//    {
//        private object _field;
//        private Type _fieldType;

//        public bool ApplyDatum { get { return (bool)_applyDatum.GetValue(_field); } }
//        public bool Calculated { get { return (bool)_calculated.GetValue(_field); } }
//        public string CaptionLong { get { return _captionLong.GetValue(_field) as string; } }
//        public string CaptionShort { get { return _captionShort.GetValue(_field) as string; } }
//        public double CurrentUnitExponent { get { return (double)_currentUnitExponent.GetValue(_field); } }
//        public double CurrentUnitFactor { get { return (double)_currentUnitFactor.GetValue(_field); } }
//        public string CurrentUnitLabel { get { return _currentUnitLabel.GetValue(_field) as string; } }
//        public double CurrentUnitOffset { get { return (double)_currentUnitOffset.GetValue(_field); } }
//        public FieldDataType DataType { get { return (FieldDataType)_dataType.GetValue(_field); } }
//        public string Help { get { return _help.GetValue(_field) as string; } }
//        public bool Hidden { get { return (bool)_hidden.GetValue(_field); } }
//        public bool IsId { get { return (bool)_isId.GetValue(_field); } }
//        public bool IsSystem { get { return (bool)_isSystem.GetValue(_field); } }
//        public string Key { get { return _key.GetValue(_field) as string; } }
//        public string LibraryFieldName { get { return _libraryFieldName.GetValue(_field) as string; } }
//        public string LibraryTableName { get { return _libraryTableName.GetValue(_field) as string; } }
//        public LibraryType LibraryType { get { return (LibraryType)_libraryType.GetValue(_field); } }
//        public int Size { get { return (int)_size.GetValue(_field); } }
//        public IwTable Table { get { return new Table(_table.GetValue(_field)); } }
//        public object Raw => _field;

//        private PropertyInfo _applyDatum;
//        private PropertyInfo _calculated;
//        private PropertyInfo _captionLong;
//        private PropertyInfo _captionShort;
//        private PropertyInfo _currentUnitExponent;
//        private PropertyInfo _currentUnitFactor;
//        private PropertyInfo _currentUnitOffset;
//        private PropertyInfo _currentUnitLabel;
//        private PropertyInfo _dataType;
//        private PropertyInfo _help;
//        private PropertyInfo _hidden;
//        private PropertyInfo _isId;
//        private PropertyInfo _isSystem;
//        private PropertyInfo _key;
//        private PropertyInfo _libraryFieldName;
//        private PropertyInfo _libraryTableName;
//        private PropertyInfo _libraryType;
//        private PropertyInfo _size;
//        private PropertyInfo _table;

//        private MethodInfo _rawToUser;
//        private MethodInfo _toString;

//        public Field(object field)
//        {
//            var assembly = Assembly.LoadFrom(@"C:\Peloton\WellView 10.2.20161107\app\system\bin\Peloton.AppFrame.IO.dll");

//            _fieldType = assembly.GetType("Peloton.AppFrame.IO.Field");

//            _field = field;

//            _applyDatum = _fieldType.GetProperty("ApplyDatum");
//            _calculated = _fieldType.GetProperty("Calculated");
//            _captionLong = _fieldType.GetProperty("CaptionLong");
//            _captionShort = _fieldType.GetProperty("CaptionShort");
//            _currentUnitExponent = _fieldType.GetProperty("CurrentUnitExponent");
//            _currentUnitFactor = _fieldType.GetProperty("CurrentUnitFactor");
//            _currentUnitOffset = _fieldType.GetProperty("CurrentUnitOffset");
//            _currentUnitLabel = _fieldType.GetProperty("CurrentUnitLabel");
//            _dataType = _fieldType.GetProperty("DataType");
//            _help = _fieldType.GetProperty("Help");
//            _hidden = _fieldType.GetProperty("Hidden");
//            _isId = _fieldType.GetProperty("IsId");
//            _isSystem = _fieldType.GetProperty("IsSystem");
//            _key = _fieldType.GetProperty("Key");
//            _libraryFieldName = _fieldType.GetProperty("LibraryFieldName");
//            _libraryTableName = _fieldType.GetProperty("LibraryTableName");
//            _libraryType = _fieldType.GetProperty("LibraryType");
//            _size = _fieldType.GetProperty("Size");
//            _table = _fieldType.GetProperty("Table");

//            _rawToUser = _fieldType.GetMethod("RawToUser");
//            _toString = _fieldType.GetMethod("ToString");
//        }

//        public string RawToUser(string entityId, string recordId, object rawValue, bool showUnitLabel)
//        {
//            return _rawToUser.Invoke(_field, new object[] { entityId, recordId, rawValue, showUnitLabel }) as string;
//        }

//        public override string ToString()
//        {
//            return _toString.Invoke(_field, new object[] { }) as string;
//        }
//    }
//}
