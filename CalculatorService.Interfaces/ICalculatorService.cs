using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace CalculatorService.Interfaces
{
    [ServiceContract]
    public interface ICalculatorService : IService
    {
        [OperationContract]
        Task<string> Add(int a, int b);
        [OperationContract]
        Task<string> Subtract(int a, int b);
    }
}
