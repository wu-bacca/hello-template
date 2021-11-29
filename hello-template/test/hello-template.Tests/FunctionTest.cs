using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

namespace hello_template.Tests
{
    public class FunctionTest
    {
        public FunctionTest()
        {
        }

        [Theory]
        [InlineData("John Doe")]
        [InlineData("any name.")]
        [InlineData("short..")]
        [InlineData("or really long names should work as well.")]
        public void TestMethodWithBody(string input)
        {
            var functions = new Functions();

            var request = new APIGatewayProxyRequest()
            {
                Path = input
            };
            var context = new TestLambdaContext();
            var response = functions.Get(request, context);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal($"Hello {input}", response.Body);
        }
        
        [Theory]
        [InlineData("            ")]
        [InlineData("   ")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TestMethodWithEmptyBody(string input)
        {
            var functions = new Functions();

            var request = new APIGatewayProxyRequest()
            {
                Path = input
            };
            var context = new TestLambdaContext();
            var response = functions.Get(request, context);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Hello anonymous!", response.Body);
        }
    }
}