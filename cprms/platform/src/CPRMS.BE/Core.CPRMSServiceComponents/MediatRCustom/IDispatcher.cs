using Azure.Core;
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
        /// Gửi một command hoặc query và nhận kết quả.
        /// </summary>
        /// <typeparam name="TResponse">Kiểu của kết quả trả về.</typeparam>
        /// <param name="request">Command hoặc query cần xử lý.</param>
        /// <param name="cancellationToken">Token hủy bỏ.</param>
        /// <returns>Kết quả của command/query.</returns>
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gửi một command hoặc query không trả về kết quả.
        /// </summary>
        /// <param name="request">Command hoặc query cần xử lý.</param>
        /// <param name="cancellationToken">Token hủy bỏ.</param>
        /// <returns>Task biểu thị hoàn thành.</returns>
        Task Send(IRequest request, CancellationToken cancellationToken = default);
    }
    /// <summary>
    /// Marker interface cho các command hoặc query.
    /// </summary>
    public interface IRequest
    {
    }

    /// <summary>
    /// Marker interface cho các command hoặc query có kết quả.
    /// </summary>
    /// <typeparam name="TResponse">Kiểu của kết quả trả về.</typeparam>
    public interface IRequest<TResponse> : IRequest
    {
    }
}
