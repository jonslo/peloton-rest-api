using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDS.PelotonWebAPI.Domain.Enums;
using Newtonsoft.Json;
using Sigil;

namespace JDS.PelotonWebAPI.Domain.Wrappers
{
    // ReSharper disable once InconsistentNaming
    public class IOEngine
    {
        private static Type ConnectionDefinitionType { get; }

        public string ApplicationName => GetApplicationName(Raw);

        public WConnectionDefinition ConnectionDefinition => new WConnectionDefinition
        {
            Database = null,
            Password = null,
            Provider = DBMS.Access,
            Server = null,
            Trusted = false,
            User = null
        };

        public string CurrentProfileName => GetCurrentProfileName(Raw);
        public string CurrentUnitSet => GetCurrentUnitSet(Raw);
        public string DatabaseUsername => GetDatabaseUsername(Raw);
        public string EntityNamePlural => GetEntityNamePlural(Raw);
        public string EntityNameSingular => GetEntityNameSingular(Raw);
        public string EntityNameSingularPlural => GetEntityNameSingularPlural(Raw);
        public string FolderCustom => GetFolderCustom(Raw);
        public string FolderSystem => GetFolderSystem(Raw);
        public string FolderTemp => GetFolderTemp(Raw);
        public string FolderUser => GetFolderUser(Raw);

        [JsonIgnore] public object Raw { get; }

        public LibraryIo Library { get; }

        public bool Readonly => GetReadonly(Raw);

        public Table TableMain => new Table(GetTableMain(Raw));

        public IDictionary<string, Table> Tables
        {
            get
            {
                var dictionary = new Dictionary<string, Table>();
                var d = GetTables(Raw);

                foreach (var key in d.Keys) dictionary.Add(key as string ?? throw new InvalidOperationException("Table key is null"), new Table(d[key as string]));

                return dictionary;
            }
        }

        public Version Version => GetVersion(Raw);

        internal static Func<object, string> GetApplicationName;
        internal static Func<object, object> GetConnectionDefinition;
        internal static Func<object, string> GetCurrentProfileName;
        internal static Func<object, string> GetCurrentUnitSet;
        internal static Func<object, string> GetDatabaseUsername;
        internal static Func<object, string> GetEntityNamePlural;
        internal static Func<object, string> GetEntityNameSingular;
        internal static Func<object, string> GetEntityNameSingularPlural;
        internal static Func<object, string> GetFolderCustom;
        internal static Func<object, string> GetFolderSystem;
        internal static Func<object, string> GetFolderTemp;
        internal static Func<object, string> GetFolderUser;
        internal static Func<object, object> GetLibraryIo;
        internal static Func<object, bool> GetReadonly;
        internal static Func<object, object> GetTableMain;
        internal static Func<object, IDictionary> GetTables;
        internal static Func<object, Version> GetVersion;

        internal static Func<string, string, string, string, string, string, object> FuncConstructor1;
        internal static Func<object, string> FuncAddEntity;
        internal static Action<object, DBMS, string, string, string, string, bool> ActConnect;
        internal static Action<object> ActDisconnect;
        internal static Action<object> ActDispose;
        internal static Func<object, string, bool> FuncIsEntityReadonly;
        internal static Func<object, string, IList, IList<string>> FuncSearch;
        internal static Func<object, string> FuncToString;
        internal static Action<object, string> ActUndoAddEntity;

        private FieldInfo Database { get; }
        private FieldInfo Password { get; }
        private FieldInfo Dbms { get; }
        private FieldInfo Server { get; }
        private FieldInfo Trusted { get; }
        private FieldInfo User { get; }

        private static ConstructorInfo ConstructorMethod { get; }

        static IOEngine()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.ManifestModule.Name == "Peloton.AppFrame.IO.dll");

            var ioEngineType = assembly.GetType("Peloton.AppFrame.IO.IOEngine");
            var connectionDbmsType = assembly.GetType("Peloton.AppFrame.IO.DBMS");
            ConnectionDefinitionType = assembly.GetType("Peloton.AppFrame.IO.ConnectionDefinition");

            // Properties

            var applicationName = ioEngineType.GetProperty("ApplicationName");
            var connectionDefinition = ioEngineType.GetProperty("ConnectionDefinition");
            var currentProfileName = ioEngineType.GetProperty("CurrentProfileName");
            var currentUnitSet = ioEngineType.GetProperty("CurrentUnitSet");
            var databaseUsername = ioEngineType.GetProperty("DatabaseUsername");
            var entityNamePlural = ioEngineType.GetProperty("EntityNamePlural");
            var entityNameSingular = ioEngineType.GetProperty("EntityNameSingular");
            var entityNameSingularPlural = ioEngineType.GetProperty("EntityNameSingularPlural");
            var folderCustom = ioEngineType.GetProperty("FolderCustom");
            var folderSystem = ioEngineType.GetProperty("FolderSystem");
            var folderTemp = ioEngineType.GetProperty("FolderTemp");
            var folderUser = ioEngineType.GetProperty("FolderUser");
            var libraryIo = ioEngineType.GetProperty("Library");
            var _readonly = ioEngineType.GetProperty("Readonly");
            var tableMain = ioEngineType.GetProperty("TableMain");
            var tables = ioEngineType.GetProperty("Tables");
            var version = ioEngineType.GetProperty("VersionPhysicalDB");

            var eGetApplicationName = Emit<Func<object, string>>
                .NewDynamicMethod("GetApplicationName")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(applicationName.GetGetMethod(true))
                .Return();

            //var e_get_connectionDefinition = Emit<Func<object, object>>
            //.NewDynamicMethod("GetConnectionDefinition")
            //.LoadArgument(0)
            //.CastClass(ioEngineType)
            //.Call(_connectionDefinition.GetGetMethod(nonPublic: true))
            //.CastClass(typeof(object))
            //.Return();

            var eGetCurrentProfileName = Emit<Func<object, string>>
                .NewDynamicMethod("GetCurrentProfileName")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(currentProfileName.GetGetMethod(true))
                .Return();

            var eGetCurrentUnitSet = Emit<Func<object, string>>
                .NewDynamicMethod("GetCurrentUnitSet")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(currentUnitSet.GetGetMethod(true))
                .Return();

            var eGetDatabaseUsername = Emit<Func<object, string>>
                .NewDynamicMethod("GetDatabaseUsername")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(databaseUsername.GetGetMethod(true))
                .Return();

            var eGetEntityNamePlural = Emit<Func<object, string>>
                .NewDynamicMethod("GetEntityNamePlural")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(entityNamePlural.GetGetMethod(true))
                .Return();

            var eGetEntityNameSingular = Emit<Func<object, string>>
                .NewDynamicMethod("GetEntityNameSingular")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(entityNameSingular.GetGetMethod(true))
                .Return();

            var eGetEntityNameSingularPlural = Emit<Func<object, string>>
                .NewDynamicMethod("GetEntityNameSingularPlural")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(entityNameSingularPlural.GetGetMethod(true))
                .Return();

            var eGetFolderSystem = Emit<Func<object, string>>
                .NewDynamicMethod("GetFolderSystem")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(folderSystem.GetGetMethod(true))
                .Return();

            var eGetFolderCustom = Emit<Func<object, string>>
                .NewDynamicMethod("GetFolderCustom")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(folderCustom.GetGetMethod(true))
                .Return();

            var eGetFolderUser = Emit<Func<object, string>>
                .NewDynamicMethod("GetFolderUser")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(folderUser.GetGetMethod(true))
                .Return();

            var eGetFolderTemp = Emit<Func<object, string>>
                .NewDynamicMethod("GetFolderTemp")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(folderTemp.GetGetMethod(true))
                .Return();

            var eGetLibraryIo = Emit<Func<object, object>>
                .NewDynamicMethod("GetLibraryIO")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(libraryIo.GetGetMethod(true))
                .Return();

            var eGetReadonly = Emit<Func<object, bool>>
                .NewDynamicMethod("GetReadonly")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(_readonly.GetGetMethod(true))
                .Return();

            var eGetTableMain = Emit<Func<object, object>>
                .NewDynamicMethod("GetTableMain")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(tableMain.GetGetMethod(true))
                .Return();

            var eGetTables = Emit<Func<object, IDictionary>>
                .NewDynamicMethod("GetTables")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(tables.GetGetMethod(true))
                .CastClass(typeof(IDictionary))
                .Return();

            var eGetVersion = Emit<Func<object, Version>>
                .NewDynamicMethod("GetVersionPhysicalDB")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(version.GetGetMethod(true))
                .CastClass(typeof(Version))
                .Return();

            GetApplicationName = eGetApplicationName.CreateDelegate();
            //_get_connectionDefinition = e_get_connectionDefinition.CreateDelegate();
            GetCurrentProfileName = eGetCurrentProfileName.CreateDelegate();
            GetCurrentUnitSet = eGetCurrentUnitSet.CreateDelegate();
            GetDatabaseUsername = eGetDatabaseUsername.CreateDelegate();
            GetEntityNamePlural = eGetEntityNamePlural.CreateDelegate();
            GetEntityNameSingular = eGetEntityNameSingular.CreateDelegate();
            GetEntityNameSingularPlural = eGetEntityNameSingularPlural.CreateDelegate();
            GetFolderCustom = eGetFolderCustom.CreateDelegate();
            GetFolderSystem = eGetFolderSystem.CreateDelegate();
            GetFolderTemp = eGetFolderTemp.CreateDelegate();
            GetFolderUser = eGetFolderUser.CreateDelegate();
            GetLibraryIo = eGetLibraryIo.CreateDelegate();
            GetReadonly = eGetReadonly.CreateDelegate();
            GetTableMain = eGetTableMain.CreateDelegate();
            GetTables = eGetTables.CreateDelegate();
            GetVersion = eGetVersion.CreateDelegate();

            // Methods

            ConstructorMethod = ioEngineType.GetConstructor(new[]
                {typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string)});
            var addEntity = ioEngineType.GetMethod("AddEntity");
            var connect = ioEngineType.GetMethod("Connect",
                new[]
                {
                    connectionDbmsType, typeof(string), typeof(string), typeof(string), typeof(string), typeof(bool)
                });
            var disconnect = ioEngineType.GetMethod("Disconnect", new Type[] { });
            var dispose = ioEngineType.GetMethod("Dispose", new Type[] { });
            var isEntityReadonly = ioEngineType.GetMethod("IsEntityReadonly", new[] {typeof(string)});
            var search = ioEngineType.GetMethod("Search", new[] {typeof(string), typeof(IList)});
            var toString = ioEngineType.GetMethod("ToString", new Type[] { });
            var undoAddEntity = ioEngineType.GetMethod("UndoAddEntity", new[] {typeof(string)});

            var eFuncConstructor1 = Emit<Func<string, string, string, string, string, string, object>>
                .NewDynamicMethod("FuncConstructor1")
                .LoadArgument(0)
                .LoadArgument(1)
                .LoadArgument(2)
                .LoadArgument(3)
                .LoadArgument(4)
                .LoadArgument(5)
                .NewObject(ConstructorMethod)
                .Return();

            var eFuncAddEntity = Emit<Func<object, string>>
                .NewDynamicMethod("FuncAddEntity")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(addEntity)
                .Return();

            var eActConnect = Emit<Action<object, DBMS, string, string, string, string, bool>>
                .NewDynamicMethod("ActConnect")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .LoadArgument(1)
                .LoadArgument(2)
                .LoadArgument(3)
                .LoadArgument(4)
                .LoadArgument(5)
                .LoadArgument(6)
                .Call(connect)
                .Return();

            var eActDisconnect = Emit<Action<object>>
                .NewDynamicMethod("ActDisconnect")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(disconnect)
                .Return();

            var eActDispose = Emit<Action<object>>
                .NewDynamicMethod("ActDispose")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(dispose)
                .Return();

            var eFuncIsEntityReadonly = Emit<Func<object, string, bool>>
                .NewDynamicMethod("FuncIsEntityReadonly")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .LoadArgument(1)
                .Call(isEntityReadonly)
                .Return();

            var eFuncSearch = Emit<Func<object, string, IList, IList<string>>>
                .NewDynamicMethod("FuncSearch")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .LoadArgument(1)
                .LoadArgument(2)
                .Call(search)
                .CastClass(typeof(IList<string>))
                .Return();

            var eFuncToString = Emit<Func<object, string>>
                .NewDynamicMethod("FuncToString")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .Call(toString)
                .Return();

            var eActUndoAddEntity = Emit<Action<object, string>>
                .NewDynamicMethod("ActUndoAddEntity")
                .LoadArgument(0)
                .CastClass(ioEngineType)
                .LoadArgument(1)
                .Call(undoAddEntity)
                .Return();

            FuncConstructor1 = eFuncConstructor1.CreateDelegate();
            FuncAddEntity = eFuncAddEntity.CreateDelegate();
            ActConnect = eActConnect.CreateDelegate();
            ActDisconnect = eActDisconnect.CreateDelegate();
            ActDispose = eActDispose.CreateDelegate();
            FuncIsEntityReadonly = eFuncIsEntityReadonly.CreateDelegate();
            FuncSearch = eFuncSearch.CreateDelegate();
            FuncToString = eFuncToString.CreateDelegate();
            ActUndoAddEntity = eActUndoAddEntity.CreateDelegate();
        }

        public IOEngine(string systemFolder, string customFolder, string userFolder, string profileName,
            string profilePassword, string unitSetName)
        {
            Database = ConnectionDefinitionType.GetField("Database");
            Password = ConnectionDefinitionType.GetField("Password");
            Dbms = ConnectionDefinitionType.GetField("Provider");
            Server = ConnectionDefinitionType.GetField("Server");
            Trusted = ConnectionDefinitionType.GetField("Trusted");
            User = ConnectionDefinitionType.GetField("User");

            Raw = FuncConstructor1(systemFolder, customFolder, userFolder, profileName, profilePassword, unitSetName);

            Library = new LibraryIo(GetLibraryIo(Raw));
        }

        public string AddEntity()
        {
            return FuncAddEntity(Raw);
        }

        public void Connect(DBMS connectionDbms, string connectionServer, string connectionDatabase,
            string connectionUser, string connectionPassword, bool connectionTrusted)
        {
            ActConnect(Raw, connectionDbms, connectionServer, connectionDatabase, connectionUser, connectionPassword,
                connectionTrusted);
        }

        public void Disconnect()
        {
            ActDisconnect(Raw);
        }

        public void Dispose()
        {
            ActDispose(Raw);
        }

        public bool IsEntityReadonly(string entityId)
        {
            return FuncIsEntityReadonly(Raw, entityId);
        }

        public IList<string> Search(string sql, IList parameters)
        {
            return FuncSearch(Raw, sql, parameters);
        }

        public override string ToString()
        {
            return FuncToString(Raw);
        }

        public void UndoAddEntity(string entityId)
        {
            ActUndoAddEntity(Raw, entityId);
        }
    }
}