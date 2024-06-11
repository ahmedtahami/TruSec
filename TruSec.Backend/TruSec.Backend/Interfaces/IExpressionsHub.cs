using TruSec.Backend.Controllers;
using TruSec.BLL.DTOs;

namespace TruSec.Backend.Interfaces
{
    public interface IExpressionsHub
    {
        Task SendExpression(TruckDataLogRequestDto data);
    }
}
