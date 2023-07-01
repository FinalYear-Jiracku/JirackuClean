﻿using Microsoft.AspNetCore.Hosting;
using StackExchange.Redis;
using System.Text.Json;
using TaskServices.Application.Interfaces.IServices;

namespace TaskServices.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        IDatabase _cacheDb;

        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("redisstack");
            _cacheDb = redis.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var data = _cacheDb.StringGet(key);
            if (!string.IsNullOrEmpty(data))
            {
                return JsonSerializer.Deserialize<T>(data);
            }
            return default;
        }

        public object RemoveData<T>(string key)
        {
            var exist = _cacheDb.KeyExists(key);
            if (exist)
            {
                return _cacheDb.KeyDelete(key);
            }
            return false;
        }
        public bool SetData<T>(string key, T data, DateTimeOffset expirationTime)
        {
            var expireTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet(key, JsonSerializer.Serialize(data), expireTime);
        }
    }
}
