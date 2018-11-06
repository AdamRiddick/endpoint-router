using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using EndpointRouter.Contracts;
using Microsoft.AspNetCore.Http;

namespace EndpointRouterDemo.Endpoints.Results
{
    public class DemoEndpointResult : IEndpointResult
    {
        public async Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 200;
            httpContext.Response.ContentType = "application/json; charset=UTF-8";

            var resultDto = new ResultDto
            {
                Message = "Demo Result"
            };

            // Crude Serialization, do this how you please. 
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ResultDto));
            ser.WriteObject(ms, resultDto);
            byte[] json = ms.ToArray();
            ms.Close();

            await httpContext.Response.WriteAsync(Encoding.UTF8.GetString(json, 0, json.Length));
        }

        [DataContract]
        internal class ResultDto
        {
            [DataMember]
            public string Message { get; set; }
        }
    }
}