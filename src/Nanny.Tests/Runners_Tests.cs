using FluentAssertions;
using Microsoft.Extensions.Logging;
using Nanny.Configuration;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Nanny.Tests
{
    public class Runners_Tests
    {
        [Fact]
        public async Task ForeverRunner_Test()
        {
            var counter = new Counter();

            var function = FunctionFactory(counter);

            var runner = Nanny.Configuration.StartOptions.TryForever.Runner;

            var config = new Nanny.Configuration.NannyConfig();

            NannyConfig.Cts = new CancellationTokenSource();

            NannyConfig.Cts.CancelAfter(1000);

            try
            {
                await runner(function, config);
            }
            catch (OperationCanceledException)
            {

                counter.Value.Should().BePositive();
            }
        }

        [Fact]
        public async Task NTimesRunner_Test()
        {
            var counter = new Counter();

            var function = FunctionFactory(counter);

            var runner = Nanny.Configuration.StartOptions.TryMultiple.Runner;

            var config = new Nanny.Configuration.NannyConfig
            {
                Retries = 5,
            };
            NannyConfig.Cts = new CancellationTokenSource();

            NannyConfig.Cts.CancelAfter(10000);

            try
            {
                await runner(function, config);
            }
            catch (OperationCanceledException)
            {

                counter.Value.Should().Be(5);
            }
        }

        private static Func<CancellationToken, Task> FunctionFactory(Counter counter)
        {
            return (token) =>
               {
                   if (token.IsCancellationRequested)
                       return Task.CompletedTask;

                   counter.Value++;

                   return Task.CompletedTask;
               };
        }
    }

    public class Counter
    {
        public int Value { get; set; }
    }

}
