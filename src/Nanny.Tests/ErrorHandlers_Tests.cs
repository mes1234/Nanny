using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nanny.Tests
{
    public class ErrorHandlers_Tests
    {
        [Fact]
        public async Task ErrorRaisedIsHandled_Test()
        {
            var errorHandler = Nanny.Configuration.ErrorOptions.CatchLogContinue;

            var logger = Substitute.For<MockLogger>();

            await errorHandler.Handle(() =>
             {
                 throw new InvalidOperationException();
             }, logger);

            logger.Received().Log(Arg.Any<LogLevel>(), Arg.Any<string>());
        }
        [Fact]
        public async Task ErrorRaisedIsThrown_Test()
        {
            var errorHandler = Nanny.Configuration.ErrorOptions.ThrowAndLog;

            var logger = Substitute.For<MockLogger>();

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
             {
                 await errorHandler.Handle(() =>
                  {
                      throw new InvalidOperationException();
                  }, logger);
             });

            logger.Received().Log(Arg.Any<LogLevel>(), Arg.Any<string>());
        }
    }


    public abstract class MockLogger : ILogger
    {
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
         => Log(logLevel, formatter(state, exception));
        public abstract void Log(LogLevel logLevel, string message);
        public virtual bool IsEnabled(LogLevel logLevel) => true;
        public abstract IDisposable BeginScope<TState>(TState state);
    }
}
