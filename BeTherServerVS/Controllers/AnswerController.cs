using BeTherServer.Models;
using BeTherServer.Services.AnswerService;
using Microsoft.AspNetCore.Mvc;

namespace BeTherServer.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        private readonly IAnswerService r_AnswerLogic;
        public AnswerController(IAnswerService i_AnswerLogic)
        {
            r_AnswerLogic = i_AnswerLogic;
        }
        [HttpPost]
        public async Task<IActionResult> Post(string UserName, [FromBody] Answer i_Answer)
        {
            try
            {
                await r_AnswerLogic.PostNewAnswer(UserName, i_Answer);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult GetUsersNewAnswers(string UserName)
        {
            List<Answer> newAnswersList = r_AnswerLogic.GetUsersNewAnswers(UserName);
            try
            {
                return Ok(newAnswersList);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
