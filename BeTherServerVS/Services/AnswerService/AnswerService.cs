using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services.AnswerService
{
    public class AnswerService : IAnswerService
    {
        private readonly Dictionary<string, List<UserAnswer>> m_Answers = new Dictionary<string, List<UserAnswer>>();
        private static readonly object sr_DictionaryLock = new object();
        private IQuestionAnswersDBContext m_QuestionAnswersDatabaseService;

        public AnswerService(IQuestionAnswersDBContext i_QuestionAnswersDatabaseService)
        {
            m_QuestionAnswersDatabaseService = i_QuestionAnswersDatabaseService;
        }
        public async Task<ResultUnit<string>> PostNewAnswer(string i_UserName, UserAnswer i_AnswerToInsert)
        {
            ResultUnit<string> result = new ResultUnit<string>();


            await m_QuestionAnswersDatabaseService.InsertAnswerByQuestionId(i_AnswerToInsert);


            lock (sr_DictionaryLock)
            {
                if(m_Answers.ContainsKey(i_UserName))
                {
                    m_Answers[i_UserName].Add(i_AnswerToInsert);
                }
                else
                {
                    m_Answers.Add(i_UserName, new List<UserAnswer>());
                    m_Answers[i_UserName].Add(i_AnswerToInsert);
                }
            }
            result.IsSuccess = true;
            return result;
        }

        public List<UserAnswer> GetUsersNewAnswers(string i_UserName)
        {
            lock (sr_DictionaryLock)
            {
                List<UserAnswer> newAnswers = new List<UserAnswer>();
                if (!m_Answers.ContainsKey(i_UserName))
                {
                    return newAnswers;
                }
                else
                {
                    newAnswers = m_Answers[i_UserName];
                    m_Answers.Remove(i_UserName);
                    return newAnswers;
                }
            }
        }
    }
}
