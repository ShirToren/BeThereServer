using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services.AnswerService
{
    public class AnswerService : IAnswerService
    {
        private readonly Dictionary<string, List<Answer>> m_Answers = new Dictionary<string, List<Answer>>();
        private static readonly object sr_DictionaryLock = new object();
        public async Task<ResultUnit<string>> PostNewAnswer(string i_UserName, Answer i_AnswerToInsert)
        {
            ResultUnit<string> result = new ResultUnit<string>();
            //insert to data base
            
            lock (sr_DictionaryLock)
            {
                if(m_Answers.ContainsKey(i_UserName))
                {
                    m_Answers[i_UserName].Add(i_AnswerToInsert);
                }
                else
                {
                    m_Answers.Add(i_UserName, new List<Answer>());
                    m_Answers[i_UserName].Add(i_AnswerToInsert);
                }
            }
            result.IsSuccess = true;
            return result;
        }

        public List<Answer> GetUsersNewAnswers(string i_UserName)
        {
            lock (sr_DictionaryLock)
            {
                List<Answer> newAnswers = new List<Answer>();
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
