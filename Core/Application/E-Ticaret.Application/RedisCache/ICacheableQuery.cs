using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.RedisCache;

public interface ICacheableQuery
{
    string CacheKey {  get; }
    double CacheTime {  get; }
}
