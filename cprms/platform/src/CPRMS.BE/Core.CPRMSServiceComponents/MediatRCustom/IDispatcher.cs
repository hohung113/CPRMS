using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.MediatRCustom
{
    /// <summary>
    /// Interface để gửi command hoặc query qua Mediator pattern.
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        /// Gửi một request (command hoặc query) và nhận lại kết quả.
        /// </summary>
        /// <typeparam name="TResponse">Kiểu dữ liệu trả về.</typeparam>
        /// <param name="request">Command hoặc query cần xử lý.</param>
        /// <param name="cancellationToken">Token hủy bỏ.</param>
        /// <returns>Kết quả xử lý.</returns>
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gửi một request (command hoặc query) không trả về kết quả.
        /// </summary>
        /// <param name="request">Command hoặc query cần xử lý.</param>
        /// <param name="cancellationToken">Token hủy bỏ.</param>
        /// <returns>Task biểu thị hoàn thành.</returns>
        Task Send(IRequest request, CancellationToken cancellationToken = default);
    }
    /// <summary>
    /// Marker interface cho các command hoặc query.
    /// </summary>
    public interface IRequest : MediatR.IRequest<Unit> { }

    public interface IRequest<TResponse> : MediatR.IRequest<TResponse> { }
}
