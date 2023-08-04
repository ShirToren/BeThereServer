using System;
using Microsoft.AspNetCore.Mvc;
using BeTherServer.Models;
using BeTherServer.Services;
using BeTherServer.Services.Utils;

namespace BeTherServer.Controllers;

[Controller]
[Route("api/[controller]")]
public class QuestionsAskedController : Controller
{

    private readonly IQuestionsAskedService m_PreiousQuestionsLogic;

    public QuestionsAskedController(IQuestionsAskedService i_PreiousQuestionsLogic)
    {
        m_PreiousQuestionsLogic = i_PreiousQuestionsLogic;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPreviousQuestions(string UserName)
    {
        ResultUnit<List<QuestionAsked>> previousQuestions = await m_PreiousQuestionsLogic.GetUsersPreviousQuestions(UserName);
        try
        {
            if (previousQuestions.IsSuccess)
            {
                return Ok(previousQuestions.ReturnValue);
            }
            else
            {
                return NotFound(previousQuestions.ResultMessage);
            }
        }
        catch(Exception ex)
        {
            return StatusCode(500);
        }
    
    }


    [HttpPost]
    public async Task<IActionResult> Post(string UserName, [FromBody] QuestionAsked i_previousQuestion)
    {
        try
        {
            i_previousQuestion.username = UserName;
            await m_PreiousQuestionsLogic.InsertQuestionAsked(i_previousQuestion);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    //[HttpPut("{id}")] 
    //public async Task<IActionResult> AddToPreviousQuestion(string id, [FromBody] string i_previousQuestionId)
    //{
    //    await m_mongoDBServices.AddToPreviousQuestionsAsync(id, i_previousQuestionId);
    //    return NoContent();

    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(string id)
    //{
    //    await m_mongoDBServices.DeleteAsync(id);
    //    return NoContent();

    //}
}