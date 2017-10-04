using Abp.Runtime.Caching;

namespace PanelMasterMVC5Separate.Authorization.Claim
{
    public static class SwitchToLinkedAccountCacheManagerExtensions
    {
        public static ITypedCache<string, SwitchToLinkedAccountCacheItem> GetSwitchToLinkedAccountCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, SwitchToLinkedAccountCacheItem>(SwitchToLinkedAccountCacheItem.CacheName);
        }
    }
}