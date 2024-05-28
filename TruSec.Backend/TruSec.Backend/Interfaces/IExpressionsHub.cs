using TruSec.Backend.Controllers;

namespace TruSec.Backend.Interfaces
{
    public interface IExpressionsHub
    {
        Task SendExpression(ExpressionData data);
    }
}
