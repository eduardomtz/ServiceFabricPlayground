using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.ServiceFabric.Services.Communication.Runtime;

namespace gRPCServer
{
    public class gRPCCommunicationListener : ICommunicationListener
    {
        private Server mServer;
        private string mReplicaId;

        public gRPCCommunicationListener(string replicaId)
        {
            mReplicaId = replicaId;
        }
        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            var endpoint = FabricRuntime.GetActivationContext().GetEndpoint("ServiceEndpoint");
            mServer = new Server
            {
                Services = {Calculator.Calculator.BindService(new CalculatorImpl(mReplicaId))},
                Ports = {new ServerPort(endpoint.IpAddressOrFqdn, endpoint.Port, ServerCredentials.Insecure)}
            };
            mServer.Start();
            return Task.FromResult<string>(endpoint.IpAddressOrFqdn + ":" + endpoint.Port.ToString());
        }

        public async Task CloseAsync(CancellationToken cancellationToken)
        {
            if (mServer != null)
            {
                await mServer.ShutdownAsync();
            }
        }

        public async void Abort()
        {
            if (mServer != null)
            {
                await mServer.KillAsync();
            }
        }
    }
}
