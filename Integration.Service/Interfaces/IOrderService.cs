using Integration.Domain.Entities.Response;
using System.Threading.Tasks;

namespace Integration.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> ProcessOrder(string input);
    }
}
