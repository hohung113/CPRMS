using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rms.Domain.Common
{
    public class PagedResultDto<T>
    {
        public PagedResultDto(List<T> items, int totalItemsCount)
        {
            Items = items;
            TotalItemsCount = totalItemsCount;
        }

        public PagedResultDto()
        {

        }
        public required List<T> Items { get; init; }
        public int TotalItemsCount { get; init; }

        public PagedResultDto<TDto> ToPagedResultDto<TDto>(List<TDto> list, int count)
        {
            return new PagedResultDto<TDto>
            {
                Items = list,
                TotalItemsCount = count
            };
        }
    }
}
