using Microsoft.AspNetCore.Mvc;

namespace JobFit.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    [HttpGet]
    public IActionResult    Get()
    {
        return Ok(new { app = "JobFit", status = "Ok"});
    }
}