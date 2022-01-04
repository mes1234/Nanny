using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Nanny.Tests
{
    public class UnitTest1
    {
        [Theory]
        [AutoData]
        public void Test1(int value)
        {
            var nanny =  new Nanny.Dummy(value);

            nanny.MyProperty.Should().Be(value);

        }
    }
}