using System.Linq;
using System.Text.RegularExpressions;
namespace Core.Models
{
    public   class RegexAnswerVerifier : IAnswerVerifier
    {
        public bool IsValid(Answer answer)
        {
            return answer.Question.TestCases.All(testCase => Regex.Matches(testCase.Text, answer.Input)
.Select(match => new Position(match.Index, match.Index + match.Length - 1)).SequenceEqual(testCase.Positions));
        }
    }
}
