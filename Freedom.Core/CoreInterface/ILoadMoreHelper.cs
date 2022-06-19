using Freedom.Utility.Request;
using System;

namespace Freedom.Core.CoreInterface
{
    public interface ILoadMoreHelper
    {
        int OffSetStatus { get; set; }

        Func<int, int, bool> ResolveLoadMore { get; set; }

        Func<int, int, int, string, RequestBySearchText> GenerateRequest { get; set; }

        Func<int, int, int, RequestByLimit> GenerateRequestLimit { get; set; }
    }
}