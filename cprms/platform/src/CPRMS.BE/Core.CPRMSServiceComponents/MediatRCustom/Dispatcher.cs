using MediatR;

namespace Core.Api.MediatRCustom
{
    public class Dispatcher : IDispatcher
    {
        private readonly IMediator _mediator;
        public Dispatcher(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return (TResponse)result;
        }

        public Task Send(IRequest request, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(request, cancellationToken);
        }
    }
}
