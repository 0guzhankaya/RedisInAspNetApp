using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
	public class RedisService
	{
		private readonly ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;

        public RedisService(string url)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(url);
        }

        public IDatabase GetDb(int dbIndex)
        {
            return _connectionMultiplexer.GetDatabase(dbIndex);
        }
    }
}
