namespace TruSec.Backend.Interfaces
{
    public interface IExpressionsHub
    {
        Task SendExpression(string message);
    }
}
