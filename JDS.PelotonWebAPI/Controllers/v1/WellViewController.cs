using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JDS.PelotonWebAPI.Domain;
using JDS.PelotonWebAPI.Domain.Services;
using JDS.PelotonWebAPI.Domain.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace JDS.PelotonWebAPI.Controllers.v1
{
    //[Authorize]
    /// <summary>
    /// 
    /// </summary>
    [Route("wv/v1")]
    [Consumes("application/json")]
    public class WellViewController : Controller
    {
        private readonly IRequestService _requestService;
        private const string Root = "wvwellheader";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestService"></param>
        public WellViewController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET wvwellheader
        /// <summary>
        /// Get a list of entity ids
        /// </summary>
        /// <returns></returns>
        [HttpGet(Root)]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IEnumerable<string>> GetEntities()
        {
            return await _requestService.GetEntities(Root);
        }

        /// <summary>
        /// Get a list of libraries
        /// </summary>
        /// <returns></returns>
        [HttpGet("library")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IEnumerable<string>> GetLibraries()
        {
            return await _requestService.GetLibraries();
        }

        /// <summary>
        /// Retrieve a library
        /// </summary>
        /// <param name="libraryName"></param>
        /// <returns></returns>
        [HttpGet("library/{libraryName}")]
        [ProducesResponseType(typeof(IDictionary<string, object>), 200)]
        public IEnumerable<IDictionary<string, object>> GetLibrary(string libraryName)
        {
            return _requestService.GetLibrary(libraryName);
        }

        // GET tables
        /// <summary>
        /// Get list of tables
        /// </summary>
        /// <returns></returns>
        [HttpGet("tables")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<IEnumerable<string>> GetTables()
        {
            return await _requestService.GetTables();
        }

        // GET tables
        /// <summary>
        /// Get meta for table
        /// </summary>
        /// <returns></returns>
        [HttpGet("tables/{tableName}")]
        [ProducesResponseType(typeof(TableMeta), 200)]
        public async Task<TableMeta> GetTable(string tableName)
        {
            return await _requestService.GetTable(tableName);
        }

        // GET wvjob/{idrec}
        /// <summary>
        /// Get a record by unique id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        [HttpGet("{tableName}/{uniqueId}")]
        [ProducesResponseType(typeof(TableDataRecord), 200)]
        public TableDataRecord Get(string tableName, string uniqueId)
        {
            return _requestService.Get(tableName, null, uniqueId, Request.Query);
        }

        // GET wvwellheader/{idwell}/wvjob/{idrec}
        /// <summary>
        /// Get a record by entity id and unique id
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="tableName"></param>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        [HttpGet(Root + "/{entityId}/{tableName}/{uniqueId}")]
        [ProducesResponseType(typeof(TableDataRecord), 200)]
        public TableDataRecord Get(string entityId, string tableName, string uniqueId)
        {
            return _requestService.Get(tableName, entityId, uniqueId, Request.Query);
        }

        // GET wvwellheader/{idwell}/wvjob
        /// <summary>
        /// Get collection of records by entity id
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet(Root + "/{entityId}/{tableName}")]
        [ProducesResponseType(typeof(IEnumerable<TableDataRecord>), 200)]
        public IEnumerable<TableDataRecord> QueryFromEntity(string entityId, string tableName)
        {
            return _requestService.Query(tableName, entityId, null, Request.Query);
        }

        // GET wvjob/{idrec}/wvjobreport
        /// <summary>
        /// Get collection of records by parent id
        /// </summary>
        /// <param name="parentTable"></param>
        /// <param name="uniqueId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet("{parentTable}/{uniqueId}/{tableName}")]
        [ProducesResponseType(typeof(IEnumerable<TableDataRecord>), 200)]
        public IEnumerable<TableDataRecord> QueryFromParent(string parentTable, string uniqueId, string tableName)
        {
            return _requestService.Query(tableName, null, uniqueId, Request.Query);
        }

        /// <summary>
        /// Create a new entity record
        /// </summary>
        /// <param name="properties"></param>
        [HttpPost(Root)]
        [ProducesResponseType(typeof(TableDataRecord), 200)]
        public TableDataRecord Create([FromBody] IDictionary<string, object> properties)
        {
            return _requestService.Add(null, Root, null, properties);
        }

        /// <summary>
        /// Create a new record
        /// </summary>
        /// <param name="parentTable"></param>
        /// <param name="parentId"></param>
        /// <param name="tableName"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        [HttpPost("{parentTable}/{parentId}/{tableName}")]
        [ProducesResponseType(typeof(TableDataRecord), 200)]
        public TableDataRecord Create(string parentTable, string parentId, string tableName, [FromBody] IDictionary<string, object> properties)
        {
            return _requestService.Add(parentTable, tableName, parentId, properties);
        }

        // PUT wvwellheader/{idwell}/wvjob/{idrec}
        /// <summary>
        /// Put a record by entity and unique id
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="tableName"></param>
        /// <param name="uniqueId"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        [HttpPut(Root + "/{entityId}/{tableName}/{uniqueId}")]
        [ProducesResponseType(typeof(TableDataRecord), 200)]
        public TableDataRecord Update(string entityId, string tableName, string uniqueId,
            [FromBody] IDictionary<string, object> properties)
        {
            return _requestService.Update(tableName, entityId, uniqueId, properties);
        }

        // PUT wvjob/{idrec}
        /// <summary>
        /// Put a record by unique id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="uniqueId"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        [HttpPut("{tableName}/{uniqueId}")]
        [ProducesResponseType(typeof(TableDataRecord), 200)]
        public TableDataRecord Update(string tableName, string uniqueId,
            [FromBody] IDictionary<string, object> properties)
        {
            return _requestService.Update(tableName, null, uniqueId, properties);
        }

        // PATCH wvwellheader/{idwell}/wvjob/{idrec}
        /// <summary>
        /// Patch a record by entity and unique id
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="tableName"></param>
        /// <param name="uniqueId"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        [HttpPatch(Root + "/{entityId}/{tableName}/{uniqueId}")]
        [ProducesResponseType(typeof(TableDataRecord), 200)]
        public TableDataRecord Patch(string entityId, string tableName, string uniqueId,
            [FromBody] IDictionary<string, object> properties)
        {
            return _requestService.Patch(tableName, entityId, uniqueId, properties);
        }

        // PATCH wvjob/{idrec}
        /// <summary>
        /// Patch a record by unique id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="uniqueId"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        [HttpPatch("{tableName}/{uniqueId}")]
        [ProducesResponseType(typeof(TableDataRecord), 200)]
        public TableDataRecord Patch(string tableName, string uniqueId,
            [FromBody] IDictionary<string, object> properties)
        {
            return _requestService.Patch(tableName, null, uniqueId, properties);
        }

        // DELETE api/values/5
        /// <summary>
        /// Delete a record by unique id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="uniqueId"></param>
        [HttpDelete("{tableName}/{uniqueId}")]
        [ProducesResponseType(204)]
        public IActionResult Delete(string tableName, string uniqueId)
        {
            try
            {
                _requestService.Delete(tableName, null, uniqueId);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}