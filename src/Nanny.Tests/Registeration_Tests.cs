using AutoFixture.Xunit2;
using FluentAssertions;
using Nanny.Configuration;
using System;
using System.Threading;
using Xunit;

namespace Nanny.Tests
{
    public class Registeration_Tests
    {
        [Fact]
        public void UnregisteredProceduresRaisesErrorr()
        {
            var nanny = new AsyncNanny(new Configuration.NannyConfig
            {
                StartOptions = StartOptions.TryForever,
            });

            Assert.Throws<NotImplementedException>(() =>
            {
                nanny.StartAsync();
            });
        }
    }
}