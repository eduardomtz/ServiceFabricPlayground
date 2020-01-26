using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.ComIntegration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Logging;
using Microsoft.ServiceFabric.Services.Client;

namespace gRPCClient
{
    public class Program
    {
        static Random mRand = new Random();
        
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("GRPC_VERBOSITY", "DEBUG");
            Environment.SetEnvironmentVariable("GRPC_TRACE", "api,cares_resolver,cares_address_sorting");
            Environment.SetEnvironmentVariable("GRPC_DNS_RESOLVER", "native");

            // Call this before you do any gRPC work
            GrpcEnvironment.SetLogger(new ConsoleLogger());

            ServicePartitionResolver partitionResolver = new ServicePartitionResolver("localhost:19000");
            var partition = partitionResolver.ResolveAsync(new Uri("fabric:/gRPCApplication/gRPCServer"),
                ServicePartitionKey.Singleton, new CancellationToken()).Result;
            var endpoint = partition.Endpoints.ElementAt(mRand.Next(0, partition.Endpoints.Count));

            var address = endpoint.Address.Substring(endpoint.Address.IndexOf("\":\"") + 3);
            address = address.Substring(0, address.IndexOf("\""));

            GrpcEnvironment.Logger.Info(string.Format("GRPC SERVER: {0}", address));

            Channel channel = new Channel(address, ChannelCredentials.Insecure);
            var client = new Calculator.Calculator.CalculatorClient(channel);

            var reply = client.Add(new Calculator.CalculateRequest {A = 100, B = 200});
            Console.WriteLine(string.Format("Replica {0} returned: {1} ", reply.ReplicaId, reply.Result));
            Console.ReadLine();
        }
        
    }
}
