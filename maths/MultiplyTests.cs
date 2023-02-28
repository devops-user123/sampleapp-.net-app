using maths;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Maths.Tests
{
    public class MultiplyTests
    {
        [Fact]
        public async Task Multiply_ReturnsCorrectResult()
        {
            // Arrange
            var request = new DefaultHttpContext().Request;
            request.Body = new MemoryStream();
            var requestData = new { Number1 = 5, Number2 = 7 };
            var requestBody = JsonConvert.SerializeObject(requestData);
            var writer = new StreamWriter(request.Body);
            await writer.WriteAsync(requestBody);
            await writer.FlushAsync();
            request.Body.Position = 0;

            var loggerMock = new Mock<ILogger>();
            var functionResult = (OkObjectResult)await multiply.Run(request, loggerMock.Object);

            // Assert
            Assert.IsType<OkObjectResult>(functionResult);
            var response = JsonConvert.DeserializeObject<multiply.MultiResponse>(functionResult.Value.ToString());
            Assert.Equal(35, response.Result);
        }

        [Fact]
        public async Task Multiply_ReturnsWrongResult()
        {
            // Arrange
            var request = new DefaultHttpContext().Request;
            request.Body = new MemoryStream();
            var requestData = new { Number1 = 5, Number2 = 7 };
            var requestBody = JsonConvert.SerializeObject(requestData);
            var writer = new StreamWriter(request.Body);
            await writer.WriteAsync(requestBody);
            await writer.FlushAsync();
            request.Body.Position = 0;

            var loggerMock = new Mock<ILogger>();
            var functionResult = (OkObjectResult)await multiply.Run(request, loggerMock.Object);

            // Assert
            Assert.IsType<OkObjectResult>(functionResult);
            var response = JsonConvert.DeserializeObject<multiply.MultiResponse>(functionResult.Value.ToString());
            Assert.NotEqual(34, response.Result);
        }

        [Fact]
        public async Task Multiply_ReturnsMultiplyByZero()
        {
            // Arrange
            var request = new DefaultHttpContext().Request;
            request.Body = new MemoryStream();
            var requestData = new { Number1 = 5, Number2 = 0 };
            var requestBody = JsonConvert.SerializeObject(requestData);
            var writer = new StreamWriter(request.Body);
            await writer.WriteAsync(requestBody);
            await writer.FlushAsync();
            request.Body.Position = 0;

            var loggerMock = new Mock<ILogger>();
            var functionResult = (OkObjectResult)await multiply.Run(request, loggerMock.Object);

            // Assert
            Assert.IsType<OkObjectResult>(functionResult);
            var response = JsonConvert.DeserializeObject<multiply.MultiResponse>(functionResult.Value.ToString());
            Assert.NotEqual(5, response.Result);
        }
    }
}
