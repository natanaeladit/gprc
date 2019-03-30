using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Greet;
using Grpc.Core;

namespace gprc
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "image.png");
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                fs.Write(request.Detail.File.ToByteArray(), 0, request.Detail.File.Length);
            }

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name + " Number " + request.Number + " Detail " + request.Detail.Name,
                Number = 100
            });
        }
    }
}
