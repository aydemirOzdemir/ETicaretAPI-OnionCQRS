using E_Ticaret.Application.RedisCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Behaviours;

public class RedisCacheBahevior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IRedisService redisService;

    public RedisCacheBahevior(IRedisService redisService)
    {
        this.redisService = redisService;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(request is ICacheableQuery query)
        {
            var cacheKey= query.CacheKey;
            var cacheTime=query.CacheTime;
            var cacheData = await redisService.GetAsync<TResponse>(cacheKey);
            if(cacheData is not null)
            
                return cacheData;
            var response = await next();
            if(response is not null) await  redisService.SetAsync(cacheKey,response,DateTime.Now.AddMinutes(cacheTime));
            return response;
        }
        return await next();
    }
}
