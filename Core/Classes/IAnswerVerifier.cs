namespace Core.Models
{
    public interface IAnswerVerifier
    {
        public bool IsValid(Answer answer);
    }
}
