using Cafe.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenemesController : ControllerBase
    {
      /*  private readonly IIngredientService _ingredientService;

        public DenemesController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public IActionResult GetAll()
        {
            Console.WriteLine("deneme contoller çalıştı");
            return Ok("geldi");
        }


        [HttpPost("add")]
        public IActionResult Add()
        {
            Console.WriteLine("Add metodu geldi!");
            return Ok("geldi");
        }*/
    }
}