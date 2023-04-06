using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using System;
using Google.Api.Gax.ResourceNames;

namespace GDF_POC_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GDFController : ControllerBase
    {
        [HttpGet]
        public string DetectIntent()
        {
            var credential = GoogleCredential.FromFile("./GDFKeyfile.json");

            // Create a client for the Dialogflow API
            var clientBuilder = new SessionsClientBuilder();
            clientBuilder.GoogleCredential = credential;
            var client = clientBuilder.Build();

            // Set up the session parameters
            var sessionName = new SessionName("gdf-poc-1-jimg", "session-id");
            var queryInput = new QueryInput
            {
                Text = new TextInput
                {
                    Text = "Please help me connect revathi",
                    LanguageCode = "en-US"
                }
            };

            // Call the DetectIntent API
            var response = client.DetectIntent(new DetectIntentRequest
            {
                SessionAsSessionName = sessionName,
                QueryInput = queryInput
            });

            // Print the response
            return response.QueryResult.FulfillmentText;
        }
    }
}
