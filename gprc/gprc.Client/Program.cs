using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf;
using Greet;
using Grpc.Core;

namespace gprc
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "image.png");
            var file = await File.ReadAllBytesAsync(path);

            // Include port of the gRPC server as an application argument
            var port = args.Length > 0 ? args[0] : "50051";

            var channel = new Channel("localhost:" + port, ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest
            {
                Name = "GreeterClient",
                Number = 2,
                Detail = new HelloRequestDetail
                {
                    Name = "Hi..",
                    File = ByteString.CopyFrom(file)
                }
            });
            Console.WriteLine("Greeting: " + reply.Message + " Number reply: " + reply.Number);

            await channel.ShutdownAsync();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
