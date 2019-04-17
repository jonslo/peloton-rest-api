using JDS.PelotonWebAPI.Domain.Wrappers;

namespace JDS.PelotonWebAPI.Domain.Services
{
    public interface IEngineService
    {
        void CheckIn(IOEngine io);
        IOEngine CheckOut();
    }
}