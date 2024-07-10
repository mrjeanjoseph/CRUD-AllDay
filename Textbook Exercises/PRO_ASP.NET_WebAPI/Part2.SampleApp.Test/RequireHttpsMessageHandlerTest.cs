using PingYourPackage.WebAPI;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PingYourPackage.Test
{
    public class RequireHttpsMessageHandlerTest
    {
        [Fact]
        public async Task ReturnsForbiddenIfRequestIsNotOverHTTPS()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:44311");
            var requireHttpsMessageHandler = new RequireHttpsMessageHandler();

            //Act
            var response = await requireHttpsMessageHandler.InvokeAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ReturnsDelegatedStatusCodeWhenRequestIsOverHTTPS()
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:44311");
            var requireHttpsMessageHandler = new RequireHttpsMessageHandler();

            //Act
            var response = await requireHttpsMessageHandler.InvokeAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
