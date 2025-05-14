using MediatR;

namespace Core.Api.MediatRCustom
{
    /// <summary>
    /// Triển khai của IDispatcher sử dụng MediatR.
    /// </summary>
    public class Dispatcher : IDispatcher
    {
        private readonly IMediator _mediator;

        public Dispatcher(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(request, cancellationToken);
        }

        public Task Send(IRequest request, CancellationToken cancellationToken = default)
        {
            // Ép kiểu sang IRequest<Unit> để tương thích với MediatR.
            if (request is MediatR.IRequest<Unit> unitRequest)
            {
                return _mediator.Send(unitRequest, cancellationToken);
            }

            throw new ArgumentException("Request phải implement MediatR.IRequest<Unit> nếu không có kết quả trả về.", nameof(request));
        }
    }
}