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
        protected List<Response> PirateResponse { get; set; } = new List<Response>();
        protected List<Response> NinjaResponse { get; set; } = new List<Response>();
        protected List<Response> VikingResponse { get; set; } = new List<Response>();
        //var chan = GrpcChannel.ForAddress("https://jhray.com:1443", new GrpcChannelOptions());
        private readonly GrpcChannel Channel = GrpcChannel.ForAddress("http://localhost:7777", new GrpcChannelOptions());
        
        protected void Stop()
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }

        protected void Clear()
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
            if (_lock != null)
            {
                lock(_lock)
                {
                    GrpcResponse.Clear();
                    PirateResponse.Clear();
                    VikingResponse.Clear();
                    NinjaResponse.Clear();
                }
            }
            else
            {
                GrpcResponse.Clear();
                PirateResponse.Clear();
                VikingResponse.Clear();
                NinjaResponse.Clear();
            }
        }

        protected async Task Start()
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
            Clear();
            StateHasChanged();
            
            _cts = new CancellationTokenSource();
            var create = new Creater.CreaterClient(Channel);

            await Task.WhenAll(Task.Run(async () =>
            {
                await VillageStream(_cts, create.StartVillage(new VillageStartup { Name = "Vikings", People = 200 }), "#a7f29c", VikingResponse);
            }, _cts.Token),
            Task.Run(async () =>
            {
                await VillageStream(_cts, create.StartVillage(new VillageStartup { Name = "Ninjas", People = 200 }), "#a8d2f5", NinjaResponse);
            }, _cts.Token),
            Task.Run(async () =>
            {
                await VillageStream(_cts, create.StartVillage(new VillageStartup { Name = "Pirates", People = 200 }), "#f59c9c", PirateResponse);
            }, _cts.Token));
            StateHasChanged();
        }

        private async Task VillageStream(CancellationTokenSource cts, AsyncServerStreamingCall<VillageStatus> stream, string color, List<Response> queue)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                var villageStatus = new VillageStatus();
                var ct = 0;
                while (await stream.ResponseStream.MoveNext(cts.Token))
                {
                    villageStatus = stream.ResponseStream.Current;
                    if (villageStatus.Message != "")
                    {
                        lock (_lock)
                        {
                            var rsp = new Response { Message = $"({sw.ElapsedMilliseconds,-4}ms):: {villageStatus.Message}", Color = color };
                            GrpcResponse.Insert(0, rsp);
                            queue.Insert(0, rsp);
                            if (ct >= 1000)
                            {
                                GrpcResponse.RemoveAt(GrpcResponse.Count - 1);
                                queue.RemoveAt(queue.Count - 1);
                            }
                        }
                        await InvokeAsync(() => StateHasChanged());
                        sw.Restart();
                    }
                    if (ct < 1000)
                    {
                        ct++;
                    }
                }
                lock (_lock)
                {
                    var rsp = new Response { Message = $"({sw.ElapsedMilliseconds,-4}ms):: {villageStatus.Time}", Color = color };
                    GrpcResponse.Insert(0, rsp);
                    queue.Insert(0, rsp);
                }
            }
            catch
            {

            }
        }

        public void Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
            GC.SuppressFinalize(this);
        }
    }
}
