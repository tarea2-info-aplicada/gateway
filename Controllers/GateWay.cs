using System;
using gateway.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace gateway.Controllers;

[ApiController]
[Route("/")]

public class GateWay : ControllerBase
{

    [HttpPost]
    public async Task<string> sendData([FromBody] CattleModel cattle) {

        string result = "";
        
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("http://127.0.0.1:8000/"),
            Content = new StringContent(JsonConvert.SerializeObject(cattle))
            {
                Headers ={
                        ContentType = new MediaTypeHeaderValue("application/json")}
            }
        };

                    using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<string>(body);
                Console.WriteLine(body);
            }

        return result;

    }

    [HttpGet]
    public string index() {
        return "server running";
    }

}

