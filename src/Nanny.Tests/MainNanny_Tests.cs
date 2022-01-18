using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Nanny.Tests
{
    public class MainNanny_Tests
    {
        [Fact]
        public void ThrowsIfNoFunctionRegistered_Test()
        {
            var nanny = new AsyncNanny(new Configuration.NannyConfig());

            Assert.ThrowsAsync<NotImplementedException>(() => nanny.StartAsync());
        }

        [Fact]
        public async void NormalOperation_Test()
        {
            var nanny = new AsyncNanny(new Configuration.NannyConfig
            {
                StartOptions = Configuration.StartOptions.TryOnce,
                ErrorOptions = Configuration.ErrorOptions.CatchLogContinue,
            });

            Configuration.NannyConfig.Cts = new CancellationTokenSource();

            Configuration.NannyConfig.Cts.CancelAfter(1000);

            await nanny
                .RegisterStart(async token =>
             {
                 await Task.Delay(500);

             })
                .RegisterRestart(async token =>
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(500);
                }
            })
                .StartAsync();
        }

        [Fact]
        public async void NormalOperationNoThrow_Test()
        {
            var nanny = new AsyncNanny(new Configuration.NannyConfig
            {
                StartOptions = Configuration.StartOptions.TryOnce,
                ErrorOptions = Configuration.ErrorOptions.CatchLogContinue,
            });

            Configuration.NannyConfig.Cts = new CancellationTokenSource();


            await nanny
                .RegisterStart(async token =>
             {
                 await Task.Delay(500);
             })
                .RegisterRestart(async token =>
            {
                await Task.Delay(500);
            })
                .StartAsync();
        }
    }
}
