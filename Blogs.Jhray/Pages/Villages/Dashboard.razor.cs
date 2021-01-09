using Blogs.Jhray.Services.GRPC;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blogs.Jhray.Pages.Villages
{
    public class DashboardBase : ComponentBase, IDisposable
    {
        private CancellationTokenSource _cts;
        protected class Response
        {
            public string Message { get; set; }
            public string Color { get; set; }
        }

        protected object _lock = new object();
        protected List<Response> GrpcResponse { get; set; } = new List<Response>();
        //var chan = GrpcChannel.ForAddress("https://jhray.com:1443", new GrpcChannelOptions());
        private GrpcChannel Channel = GrpcChannel.ForAddress("http://localhost:7777", new GrpcChannelOptions());
        protected async Task GrpcCall()
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
            GrpcResponse.Clear();
            StateHasChanged();
            
            _cts = new CancellationTokenSource();
            var create = new Creater.CreaterClient(Channel);

            await Task.WhenAll(Task.Run(async () =>
            {
                await VillageStream(_cts, create.StartVillage(new VillageStartup { Name = "Vikings", People = 200 }), "green");
            }, _cts.Token),
            Task.Run(async () =>
            {
                await VillageStream(_cts, create.StartVillage(new VillageStartup { Name = "Ninjas", People = 200 }), "blue");
            }, _cts.Token),
            Task.Run(async () =>
            {
                await VillageStream(_cts, create.StartVillage(new VillageStartup { Name = "Pirates", People = 200 }), "magenta");
            }, _cts.Token));
            StateHasChanged();
        }

        private async Task VillageStream(CancellationTokenSource cts, AsyncServerStreamingCall<VillageStatus> stream, string color)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                var villageStatus = new VillageStatus();
                while (await stream.ResponseStream.MoveNext(cts.Token))
                {
                    villageStatus = stream.ResponseStream.Current;
                    if (villageStatus.Message != "")
                    {
                        lock (_lock)
                        {
                            GrpcResponse.Add(new Response { Message = $"({sw.ElapsedMilliseconds,-4}ms):: {villageStatus.Message}", Color = color });
                        }
                        await InvokeAsync(() => StateHasChanged());
                        sw.Restart();
                    }
                }
                lock (_lock)
                {
                    GrpcResponse.Add(new Response { Message = $"({sw.ElapsedMilliseconds,-4}ms):: {villageStatus.Time}", Color = color });
                }
            }
            catch
            {

            }
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
    }
}
