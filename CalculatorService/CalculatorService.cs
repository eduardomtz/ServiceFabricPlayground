using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using CalculatorService.Interfaces;
using Microsoft.ServiceFabric.Services.Communication.Wcf;
using Microsoft.ServiceFabric.Services.Communication.Wcf.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Remoting;

namespace CalculatorService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class CalculatorService : StatelessService, ICalculatorService
    {
        public CalculatorService(StatelessServiceContext context)
            : base(context)
        { }
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[]
            {
                // default communication stack RPC proxy, page Fundamentals 32. Used with web application
                // new ServiceInstanceListener(context => this.CreateServiceRemotingListener(context)) };
                
                // WCF listener, used with console application
                new ServiceInstanceListener((context) => new WcfCommunicationListener<ICalculatorService>(
                        wcfServiceObject: this,
                        serviceContext: context,
                        endpointResourceName: "ServiceEndpoint",
                        listenerBinding: WcfUtility.CreateTcpListenerBinding()
                    ))
            };
        }
        public Task<string> Add(int a, int b)
        {
            return Task.FromResult<string>(string.Format("Instance: {0}, returns: {1}", this.Context.InstanceId, a + b));
        }
        public Task<string> Subtract(int a, int b)
        {
            return Task.FromResult<string>(string.Format("Instance: {0}, returns: {1}", this.Context.InstanceId, a - b));
        }
    }
}