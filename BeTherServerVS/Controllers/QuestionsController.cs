﻿using System;
using Microsoft.AspNetCore.Mvc;
using BeTherServer.Models;
using BeTherServer.Services;
using BeTherServer.Services.Utils;

namespace BeTherServer.Controllers;

[Controller]
[Route("api/[controller]")]
public class QuestionsController : Controller
{
    private readonly IAskedQuestionService m_PreiousQuestionsLogic;

    public QuestionsController(IAskedQuestionService i_PreiousQuestionsLogic)
    {
        m_PreiousQuestionsLogic = i_PreiousQuestionsLogic;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPreviousQuestionsAndAnswers(string UserName)
    {
        ResultUnit<Dictionary<string, Tuple<QuestionAsked, QuestionAnswers>>> previousQuestionsAndAnswers = await m_PreiousQuestionsLogic.GetUsersQuestionsAndAnswers(UserName);
        try
        {
            if (previousQuestionsAndAnswers.IsSuccess)
            {
                return Ok(previousQuestionsAndAnswers.ReturnValue);

            }
            else
            {
                return NotFound(previousQuestionsAndAnswers.ResultMessage);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }

    }

    [HttpGet("List")]
    public async Task<IActionResult> GetAnswersList(string QuestionId)
    {
        ResultUnit<List<UserAnswer>> result = await m_PreiousQuestionsLogic.GetListOfAnswers(QuestionId);
        try
        {
            if (result.IsSuccess)
            {
                return Ok(result.ReturnValue);

            }
            else
            {
                return NotFound(result.ResultMessage);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }

    }


    [HttpPost]
    public async Task<IActionResult> NewUsersQuestion(string UserName, [FromBody] QuestionAsked i_previousQuestion)
    {
        try
        {
            await Task.Delay(3000);
            i_previousQuestion.username = UserName;
            ResultUnit<string> questionId = await m_PreiousQuestionsLogic.handleNewQuestionAsked(i_previousQuestion);
            return Ok(questionId.ReturnValue);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpPost("Answer")]
    public async Task<IActionResult> NewUsersAnswer([FromBody] QuestionAnswers i_previousQuestion)
    {
        try
        {
            await m_PreiousQuestionsLogic.InsertQuestionAnswer(i_previousQuestion);
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