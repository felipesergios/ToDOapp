using Microsoft.AspNetCore.Mvc;

namespace api_app.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TestingController : ControllerBase
{
    [HttpGet]
    public string Get(){
         for (int i = 10 - 1; i >= 0; i--)
        {
            Console.WriteLine(i);

        }
        return "Hello Word";
    }
}
