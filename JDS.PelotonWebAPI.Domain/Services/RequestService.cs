using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDS.PelotonWebAPI.Domain.Enums;
using JDS.PelotonWebAPI.Domain.Repositories;
using JDS.PelotonWebAPI.Domain.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace JDS.PelotonWebAPI.Domain.Services
{
    public class RequestService : IRequestService
    {
        private readonly IEngineService _engineService;
        private readonly PelotonOptions _options;
        private readonly ITableDataRecordRepository _tableDataRecordRepository;
        private readonly ILibraryRepository _libraryRepository;

        private ConcurrentDictionary<string, string> _entityNameLookup;

        public RequestService(IEngineService engineService, ITableDataRecordRepository tableDataRecordRepository,
            IOptions<PelotonOptions> options, ILibraryRepository libraryRepository)
        {
            _engineService = engineService;
            _tableDataRecordRepository = tableDataRecordRepository;
            _libraryRepository = libraryRepository;
            _options = options.Value;

            _entityNameLookup = new ConcurrentDictionary<string, string>();
            _entityNameLookup.TryAdd("WellView", "idwell");
            _entityNameLookup.TryAdd("SiteView", "idsite");
            _entityNameLookup.TryAdd("RigView", "idrig");
        }

        public TableDataRecord Get(string tableName, string entityId, string uniqueId, IQueryCollection queryParams)
        {
            var io = _engineService.CheckOut();

            try
            {
                io.Connect(DBMS.SQLCompact, _options.ConnectionServer, _options.ConnectionDatabase,
                    _options.DatabaseUsername, _options.DatabasePassword, _options.Trusted);

                var tableMain = io.TableMain.Key.ToLower();

                if (tableName.ToLower() == tableMain)
                    entityId = uniqueId;
                else
                    entityId = _tableDataRecordRepository.SearchEntities(io,
                            $"SELECT {_entityNameLookup[io.ApplicationName]} from {tableName} where idrec = ?", new List<object> {uniqueId})
                        .FirstOrDefault();

                var result = _tableDataRecordRepository.Get(io, tableName, entityId, uniqueId, queryParams);

                _engineService.CheckIn(io);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                _engineService.CheckIn(io);

                throw;
            }
        }

        public IEnumerable<TableDataRecord> Query(string tableName, string entityId, string parentIdFilter,
            IQueryCollection queryParams)
        {
            var io = _engineService.CheckOut();

            try
            {
                io.Connect(DBMS.SQLCompact, _options.ConnectionServer, _options.ConnectionDatabase,
                    _options.DatabaseUsername, _options.DatabasePassword, _options.Trusted);

                if (string.IsNullOrEmpty(entityId))
                    entityId = _tableDataRecordRepository.SearchEntities(io,
                        $"SELECT {_entityNameLookup[io.ApplicationName]} from {io.Tables[tableName].ParentTable.Key} where idrec = ?",
                        new List<object> {parentIdFilter}).FirstOrDefault();

                var result = _tableDataRecordRepository.Query(io, tableName, entityId, parentIdFilter, queryParams);

                _engineService.CheckIn(io);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                _engineService.CheckIn(io);

                throw;
            }
        }

        public TableDataRecord Update(string tableName, string entityId, string uniqueId,
            IDictionary<string, object> properties)
        {
            var io = _engineService.CheckOut();

            try
            {
                io.Connect(DBMS.SQLCompact, _options.ConnectionServer, _options.ConnectionDatabase,
                    _options.DatabaseUsername, _options.DatabasePassword, _options.Trusted);

                if (entityId == null)
                {
                    if (tableName != io.TableMain.Key)
                    {
                        entityId = _tableDataRecordRepository.SearchEntities(io,
                            $"SELECT {_entityNameLookup[io.ApplicationName]} from {io.Tables[tableName].Key} where idrec = ?",
                            new List<object> { uniqueId }).FirstOrDefault();
                    }
                    else
                    {
                        entityId = uniqueId;
                    }
                }

                var result = _tableDataRecordRepository.Update(io, tableName, entityId, uniqueId, properties);

                _engineService.CheckIn(io);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _engineService.CheckIn(io);
                throw;
            }
        }

        public TableDataRecord Patch(string tableName, string entityId, string uniqueId,
            IDictionary<string, object> properties)
        {
            var io = _engineService.CheckOut();

            try
            {
                io.Connect(DBMS.SQLCompact, _options.ConnectionServer, _options.ConnectionDatabase,
                    _options.DatabaseUsername, _options.DatabasePassword, _options.Trusted);

                if (entityId == null)
                {
                    if (tableName != io.TableMain.Key)
                    {
                        entityId = _tableDataRecordRepository.SearchEntities(io,
                            $"SELECT {_entityNameLookup[io.ApplicationName]} from {io.Tables[tableName].Key} where idrec = ?",
                            new List<object> { uniqueId }).FirstOrDefault();
                    }
                    else
                    {
                        entityId = uniqueId;
                    }
                }

                var result = _tableDataRecordRepository.Patch(io, tableName, entityId, uniqueId, properties);

                _engineService.CheckIn(io);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine();
                _engineService.CheckIn(io);
                throw;
            }
        }

        public void Delete(string tableName, string entityId, string uniqueId)
        {
            var io = _engineService.CheckOut();

            try
            {
                io.Connect(DBMS.SQLCompact, _options.ConnectionServer, _options.ConnectionDatabase,
                    _options.DatabaseUsername, _options.DatabasePassword, _options.Trusted);

                if (string.IsNullOrEmpty(entityId))
                    entityId = _tableDataRecordRepository.SearchEntities(io,
                        $"SELECT {_entityNameLookup[io.ApplicationName]} from {io.Tables[tableName].Key} where idrec = ?",
                        new List<object> { uniqueId }).FirstOrDefault();

                _tableDataRecordRepository.Delete(io, tableName, entityId, uniqueId);

                _engineService.CheckIn(io);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                _engineService.CheckIn(io);
                throw;
            }
        }

        public TableDataRecord Add(string parentTableName, string tableName, string parentId,
            IDictionary<string, object> properties)
        {
            var io = _engineService.CheckOut();

            try
            {
                io.Connect(DBMS.SQLCompact, _options.ConnectionServer, _options.ConnectionDatabase,
                    _options.DatabaseUsername, _options.DatabasePassword, _options.Trusted);

                string entityId = parentId;

                if (parentTableName != io.TableMain.Key && tableName != io.TableMain.Key)
                {
                    entityId = _tableDataRecordRepository.SearchEntities(io,
                        $"SELECT {_entityNameLookup[io.ApplicationName]} from {io.Tables[parentTableName].Key} where idrec = ?",
                        new List<object> { parentId }).FirstOrDefault();
                }

                var result =
                    _tableDataRecordRepository.Create(io, parentTableName, tableName, entityId, parentId, properties);

                _engineService.CheckIn(io);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _engineService.CheckIn(io);
                throw;
            }
        }

        public async Task<IList<string>> GetEntities(string tableMainName)
        {
            return await Task.Run(() =>
            {
                var io = _engineService.CheckOut();

                try
                {
                    io.Connect(DBMS.SQLCompact, _options.ConnectionServer, _options.ConnectionDatabase,
                        _options.DatabaseUsername, _options.DatabasePassword, _options.Trusted);

                    var result = _tableDataRecordRepository.SearchEntities(io, $"SELECT {_entityNameLookup[io.ApplicationName]} from {tableMainName}",
                        new List<object>());

                    _engineService.CheckIn(io);

                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _engineService.CheckIn(io);
                    throw;
                }
            });
        }

        public async Task<IList<string>> GetTables()
        {
            return await Task.Run(() =>
            {
                var io = _engineService.CheckOut();

                try
                {
                    var result = _tableDataRecordRepository.GetTables(io);

                    _engineService.CheckIn(io);

                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _engineService.CheckIn(io);
                    throw;
                }
            });
        }

        public async Task<TableMeta> GetTable(string tableName)
        {
            return await Task.Run(() =>
            {
                var io = _engineService.CheckOut();

                try
                {
                    var result = _tableDataRecordRepository.GetTable(io, tableName);

                    _engineService.CheckIn(io);

                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _engineService.CheckIn(io);
                    throw;
                }
            });

            
        }

        public async Task<IEnumerable<string>> GetLibraries()
        {
            return await _libraryRepository.GetLibraries();
        }

        public IEnumerable<IDictionary<string, object>> GetLibrary(string libraryName)
        {
            var io = _engineService.CheckOut();

            try
            {
                var result = _libraryRepository.GetLibrary(io, libraryName);

                _engineService.CheckIn(io);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _engineService.CheckIn(io);
                throw;
            }
        }
    }
}