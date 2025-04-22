using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace RmsWorkflow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateComment()
        {
            return Ok();
        }


        [HttpGet("aggregateddata")]
        public async Task<IActionResult> GetAggregatedData()
        {
            var authenClient = _httpClientFactory.CreateClient();
            var groupClient = _httpClientFactory.CreateClient();

            var authenResponse = await authenClient.GetStringAsync("https://localhost:7107/gateway/authen/getnameproject");
            var groupResponse = await groupClient.GetStringAsync("https://localhost:7107/gateway/group/getallmember");
            var aggregatedResult = new
            {
                AuthenData = authenResponse,
                GroupData = groupResponse
            };

            return Ok(aggregatedResult);
        }
    }
}
