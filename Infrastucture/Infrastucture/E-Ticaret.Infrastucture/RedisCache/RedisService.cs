using E_Ticaret.Application.RedisCache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Infrastucture.RedisCache;

public class RedisService : IRedisService
{
    private readonly ConnectionMultiplexer redisConnection;
    private readonly IDatabase database;
    private readonly RedisCacheSettings settings;
    public RedisService(IOptions<RedisCacheSettings> options)
    {
        settings = options.Value;
        var opt = ConfigurationOptions.Parse(settings.ConnectionString);
        redisConnection=ConnectionMultiplexer.Connect(opt);
        database=redisConnection.GetDatabase();
    }

    public async Task<T> GetAsync<T>(string key)
    {
       var value=await database.StringGetAsync(key);
        if (value.HasValue)
            return JsonConvert.DeserializeObject<T>(value);
        return default;
    }

    public async Task SetAsync<T>(string key, T value, DateTime? expritationTime = null)
    {
        TimeSpan timeUnitExprition=expritationTime.Value-DateTime.Now;
        await database.StringSetAsync(key, JsonConvert.SerializeObject(value), timeUnitExprition);
    }
}
