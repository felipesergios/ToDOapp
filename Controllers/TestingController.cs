using Microsoft.AspNetCore.Mvc;

namespace api_app.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TestingController : ControllerBase
{
    [HttpGet]
    public string Get(){
        return "Hello Mundo";
    }
}
