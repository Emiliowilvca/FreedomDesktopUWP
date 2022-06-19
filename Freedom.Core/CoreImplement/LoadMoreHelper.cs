using Freedom.Core.CoreInterface;
using Freedom.Utility.Request;
using System;

namespace Freedom.Core.Implement
{
    public class LoadMoreHelper : ILoadMoreHelper
    {
        public int OffSetStatus { get; set; }

        public Func<int, int, bool> ResolveLoadMore { get; set; }

        public Func<int, int, int, string, RequestBySearchText> GenerateRequest { get; set; }

        public Func<int, int, int, RequestByLimit> GenerateRequestLimit { get; set; }

        public LoadMoreHelper()
        {
            ResolveLoadMore = (collectionCount, takeItem) => collectionCount >= takeItem;

            GenerateRequest = (skip, take, companyId, search) =>
           new RequestBySearchText()
           {
               CompanyId = companyId,
               SearchText = search ?? "",
               OffSet = skip,
               Limit = take
           };
            GenerateRequestLimit = (companyId, offset, limit) =>
            new RequestByLimit()
            {
                CompanyId = companyId,
                OffSet = offset,
                Limit = limit
            };
        }
    }
}