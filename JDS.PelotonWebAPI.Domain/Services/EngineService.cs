using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JDS.PelotonWebAPI.Domain.Wrappers;

namespace JDS.PelotonWebAPI.Domain.Services
{
    public class EngineService : IEngineService
    {
        private readonly ObjectPool<IOEngine> _pool;

        public EngineService(ObjectPool<IOEngine> pool)
        {
            _pool = pool;
        }

        public IOEngine CheckOut()
        {
            return _pool.GetObject();
        }

        public void CheckIn(IOEngine io)
        {
            _pool.PutObject(io);
        }
    }
}
