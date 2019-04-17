//using JDS.PelotonWebAPI.Domain.Enums;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;

//namespace JDS.PelotonWebAPI.Domain.Wrappers.Reflection
//{
//    public class IOEngine : IwIOEngine
//    {

//        private readonly object _ioEngine;

//        private Type _ioEngineType;
//        private Type _connectionDefinitionType;

//        private MethodInfo _addEntity;
//        private MethodInfo _connect;
//        private MethodInfo _disconnect;
//        private MethodInfo _dispose;
//        private MethodInfo _isEntityReadonly;
//        private MethodInfo _search;
//        private MethodInfo _toString;
//        private MethodInfo _undoAddEntity;

//        public string ApplicationName { get { return _applicationName.GetValue(_ioEngine) as string; } }
//        public wConnectionDefinition ConnectionDefinition
//        {
//            get
//            {
//                if (_ioEngine == null)
//                {
//                    return new wConnectionDefinition()
//                    {
//                        Database = null,
//                        Password = null,
//                        Provider = DBMS.Access,
//                        Server = null,
//                        Trusted = false,
//                        User = null
//                    };
//                }
//                var cd = _connectionDefinition.GetValue(_ioEngine);
//                return new wConnectionDefinition()
//                {
//                    Database = _database.GetValue(cd) as string,
//                    Password = _password.GetValue(cd) as string,
//                    Provider = (DBMS)_dbms.GetValue(cd),
//                    Server = _server.GetValue(cd) as string,
//                    Trusted = (bool)_trusted.GetValue(cd),
//                    User = _user.GetValue(cd) as string
//                };
//            }
//        }
//        public string CurrentProfileName { get { return _currentProfileName.GetValue(_ioEngine) as string; } }
//        public string CurrentUnitSet { get { return _currentUnitSet.GetValue(_ioEngine) as string; } }
//        public string DatabaseUsername { get { return _databaseUsername.GetValue(_ioEngine) as string; } }
//        public string EntityNamePlural { get { return _entityNamePlural.GetValue(_ioEngine) as string; } }
//        public string EntityNameSingular { get { return _entityNameSingular.GetValue(_ioEngine) as string; } }
//        public string EntityNameSingularPlural { get { return _entityNameSingularPlural.GetValue(_ioEngine) as string; } }
//        public string FolderCustom { get { return _folderCustom.GetValue(_ioEngine) as string; } }
//        public string FolderSystem { get { return _folderSystem.GetValue(_ioEngine) as string; } }
//        public string FolderTemp { get { return _folderTemp.GetValue(_ioEngine) as string; } }
//        public string FolderUser { get { return _folderUser.GetValue(_ioEngine) as string; } }
//        public object Raw => _ioEngine;

//        public IwLibraryIO Library { get; }

//        public bool Readonly { get { return (bool)_readonly.GetValue(_ioEngine); } }

//        public IwTable TableMain { get { return new Table(_tableMain.GetValue(_ioEngine)); } }
//        public IDictionary<string, IwTable> Tables
//        {
//            get
//            {
//                var dictionary = new Dictionary<string, IwTable>();
//                var d = (IDictionary)_tables.GetValue(_ioEngine);

//                foreach (var key in d.Keys)
//                {
//                    dictionary.Add(key as string, new Table(d[key]));
//                }

//                return dictionary;
//            }
//        }

//        public Version Version
//        {
//            get
//            {
//                return (Version)_version.GetValue(_ioEngine);
//            }
//        }

//        private PropertyInfo _applicationName;
//        private PropertyInfo _connectionDefinition;
//        private PropertyInfo _currentProfileName;
//        private PropertyInfo _currentUnitSet;
//        private PropertyInfo _databaseUsername;
//        private PropertyInfo _entityNamePlural;
//        private PropertyInfo _entityNameSingular;
//        private PropertyInfo _entityNameSingularPlural;
//        private PropertyInfo _folderCustom;
//        private PropertyInfo _folderSystem;
//        private PropertyInfo _folderTemp;
//        private PropertyInfo _folderUser;
//        private PropertyInfo _libraryIO;
//        private PropertyInfo _readonly;
//        private PropertyInfo _tableMain;
//        private PropertyInfo _tables;
//        private PropertyInfo _version;

//        private FieldInfo _database { get; }
//        private FieldInfo _password { get; }
//        private FieldInfo _dbms { get; }
//        private FieldInfo _server { get; }
//        private FieldInfo _trusted { get; }
//        private FieldInfo _user { get; }

//        public IOEngine(string systemFolder, string customFolder, string userFolder, string profileName, string profilePassword, string unitSetName)
//        {
//            var assembly = Assembly.LoadFrom(@"C:\Peloton\WellView 10.2.20161107\app\system\bin\Peloton.AppFrame.IO.dll");

//            _ioEngineType = assembly.GetType("Peloton.AppFrame.IO.IOEngine");
//            var connectionDBMSType = assembly.GetType("Peloton.AppFrame.IO.DBMS");
//            _connectionDefinitionType = assembly.GetType("Peloton.AppFrame.IO.ConnectionDefinition");

//            var constructorMethod = _ioEngineType.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) });
//            _addEntity = _ioEngineType.GetMethod("AddEntity");
//            _connect = _ioEngineType.GetMethod("Connect", new Type[] { connectionDBMSType, typeof(string), typeof(string), typeof(string), typeof(string), typeof(bool) });
//            _disconnect = _ioEngineType.GetMethod("Disconnect", new Type[] { });
//            _dispose = _ioEngineType.GetMethod("Dispose", new Type[] { });
//            _isEntityReadonly = _ioEngineType.GetMethod("IsEntityReadonly", new Type[] { typeof(string) });
//            _search = _ioEngineType.GetMethod("Search", new Type[] { typeof(string), typeof(IList) });
//            _toString = _ioEngineType.GetMethod("ToString", new Type[] { });
//            _undoAddEntity = _ioEngineType.GetMethod("UndoAddEntity", new Type[] { typeof(string) });

//            _applicationName = _ioEngineType.GetProperty("ApplicationName");
//            _connectionDefinition = _ioEngineType.GetProperty("ConnectionDefinition");
//            _currentProfileName = _ioEngineType.GetProperty("CurrentProfileName");
//            _currentUnitSet = _ioEngineType.GetProperty("CurrentUnitSet");
//            _databaseUsername = _ioEngineType.GetProperty("DatabaseUsername");
//            _entityNamePlural = _ioEngineType.GetProperty("EntityNamePlural");
//            _entityNameSingular = _ioEngineType.GetProperty("EntityNameSingular");
//            _entityNameSingularPlural = _ioEngineType.GetProperty("EntityNameSingularPlural");
//            _folderCustom = _ioEngineType.GetProperty("FolderCustom");
//            _folderSystem = _ioEngineType.GetProperty("FolderSystem");
//            _folderTemp = _ioEngineType.GetProperty("FolderTemp");
//            _folderUser = _ioEngineType.GetProperty("FolderUser");
//            _libraryIO = _ioEngineType.GetProperty("Library");
//            _readonly = _ioEngineType.GetProperty("Readonly");
//            _tableMain = _ioEngineType.GetProperty("TableMain");
//            _tables = _ioEngineType.GetProperty("Tables");
//            _version = _ioEngineType.GetProperty("VersionPhysicalDB");

//            _database = _connectionDefinitionType.GetField("Database");
//            _password = _connectionDefinitionType.GetField("Password");
//            _dbms = _connectionDefinitionType.GetField("Provider");
//            _server = _connectionDefinitionType.GetField("Server");
//            _trusted = _connectionDefinitionType.GetField("Trusted");
//            _user = _connectionDefinitionType.GetField("User");

//            _ioEngine = constructorMethod.Invoke(new object[] { systemFolder, customFolder, userFolder, profileName, profilePassword, unitSetName });

//            Library = new LibraryIO(_libraryIO.GetValue(_ioEngine));
//        }

//        public string AddEntity()
//        {
//            return _addEntity.Invoke(_ioEngine, new object[] { }) as string;
//        }

//        public void Connect(DBMS connectionDBMS, string connectionServer, string connectionDatabase, string connectionUser, string connectionPassword, bool connectionTrusted)
//        {
//            _connect.Invoke(_ioEngine, new object[] { connectionDBMS, connectionServer, connectionDatabase, connectionUser, connectionPassword, connectionTrusted });
//        }

//        public void Disconnect()
//        {
//            _disconnect.Invoke(_ioEngine, new object[] { });
//        }

//        public void Dispose()
//        {
//            _dispose.Invoke(_ioEngine, new object[] { });
//        }

//        public bool IsEntityReadonly(string entityId)
//        {
//            return (bool)_isEntityReadonly.Invoke(_ioEngine, new object[] { entityId });
//        }

//        public IList<string> Search(string sql, IList parameters)
//        {
//            return (IList<string>)_search.Invoke(_ioEngine, new object[] { sql, parameters });
//        }

//        public override string ToString()
//        {
//            return _toString.Invoke(_ioEngine, new object[] { }) as string;
//        }

//        public void UndoAddEntity(string entityId)
//        {
//            _undoAddEntity.Invoke(_ioEngine, new object[] { });
//        }
//    }
//}
