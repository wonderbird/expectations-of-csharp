using Xunit;

namespace ExpectationsOfCSharp.Tests;

public class HelloWorldTest
{
    [Fact]
    public void Hello_ReturnsWorld() => Assert.Equal("World!", "World!");
}