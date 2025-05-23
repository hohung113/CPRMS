namespace Core.Application.Dtos
{
    public class BasePagedResultDto<T>
    {
        public BasePagedResultDto(List<T> items, int totalItemsCount)
        {
            Items = items;
            TotalItems = totalItemsCount;
        }

        public BasePagedResultDto()
        {

        }
        public required List<T> Items { get; init; }
        public int TotalItems { get; init; }

        public BasePagedResultDto<TDto> ToPagedResultDto<TDto>(List<TDto> list, int count)
        {
            return new BasePagedResultDto<TDto>
            {
                Items = list,
                TotalItems = count
            };
        }
    }
}