using Microsoft.AspNetCore.Mvc;

namespace Product.Api.Controllers;

[ApiController]
[Route("api/v1/info")]
public class InfoController : Controller
{
    [HttpGet("base-img-path")]
    public IActionResult ProductBaseImagePath()
    {
        string baseUrl = $"{Request.Scheme}://{Request.Host}{Url.Content("~/products/")}";

        return Ok(baseUrl);
    }
}
