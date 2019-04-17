using System.Collections.Generic;
using System.Threading.Tasks;
using JDS.PelotonWebAPI.Domain.Wrappers;

namespace JDS.PelotonWebAPI.Domain.Repositories
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<string>> GetLibraries();
        IEnumerable<IDictionary<string,object>> GetLibrary(IOEngine io, string libraryName);
    }
}
