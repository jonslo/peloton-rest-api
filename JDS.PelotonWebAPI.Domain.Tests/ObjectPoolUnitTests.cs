using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JDS.PelotonWebAPI.Domain.Tests
{
    public class ObjectPoolUnitTests
    {
        [Fact]
        public void CheckOutThenCheckIn()
        {
            var pool = new ObjectPool<string>(() => "testString");

            Assert.Equal(0, pool.Count());

            var s1 = pool.GetObject();

            pool.PutObject(s1);

            Assert.Equal(1, pool.Count());

            s1 = pool.GetObject();

            Assert.Equal(0, pool.Count());
        }

        [Fact]
        public void NullConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var pool = new ObjectPool<string>(null);
            });
        }
    }
}
