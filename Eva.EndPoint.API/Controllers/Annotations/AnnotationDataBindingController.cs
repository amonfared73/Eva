using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eva.EndPoint.API.Controllers.Annotations
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AnnotationDataBindingController : ControllerBase
    {
        private readonly IUserService _userService;
        public AnnotationDataBindingController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserFromRoute([FromRoute] int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException();

            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserFromRouteWithName([FromRoute(Name ="id")] int userId)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException();

            var user = await _userService.GetByIdAsync(userId);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFromQuery([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException();

            var requestOptions = BaseRequestViewModel.SampleBaseRequestViewModel();
            requestOptions.SearchTermRequest.SearchTerm = email;
            var result = await _userService.GetAllAsync(requestOptions);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFromQueryWithName([FromQuery(Name ="userEmail")] string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException();

            var requestOptions = BaseRequestViewModel.SampleBaseRequestViewModel();
            requestOptions.SearchTermRequest.SearchTerm = email;
            var result = await _userService.GetAllAsync(requestOptions);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetUserFromBody([FromBody] string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException();

            var requestOptions = BaseRequestViewModel.SampleBaseRequestViewModel();
            requestOptions.SearchTermRequest.SearchTerm = username;
            var result = await _userService.GetAllAsync(requestOptions);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetUserFromForm([FromForm] string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException();

            var requestOptions = BaseRequestViewModel.SampleBaseRequestViewModel();
            requestOptions.SearchTermRequest.SearchTerm = username;
            var result = await _userService.GetAllAsync(requestOptions);
            return Ok(result);
        }
    }
}
