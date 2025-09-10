using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SchoolApiGW.Helper
{
    public static class ApiHelper
    {
        public static async Task<TResponse?> ApiConnection<TResponse>(
            IHttpClientFactory httpClientFactory,
            string host,
            string endpoint,
            HttpMethod method,
            object? requestBody = null)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            var client = httpClientFactory.CreateClient("Default");
            //  using var client = new HttpClient("Default"); 

            var requestUrl = $"{host.TrimEnd('/')}/{endpoint.TrimStart('/')}";

            var requestMessage = new HttpRequestMessage(method, requestUrl);

            if (requestBody is not null && method != HttpMethod.Get && method != HttpMethod.Delete)
            {
                if (HasFormFile(requestBody))
                {
                    requestMessage.Content = BuildMultipartContent(requestBody);
                }
                else
                {
                    var jsonContent = JsonConvert.SerializeObject(requestBody);
                    requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                }
            }

            var response = await client.SendAsync(requestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
              
                try
                {
                    var result = JsonConvert.DeserializeObject<TResponse>(responseContent);

                    // Return deserialized response even if the status code is not success
                    return result;
                }
                catch (JsonException)
                {
                    throw new Exception($"Invalid JSON response from API. HTTP {(int)response.StatusCode} - {response.ReasonPhrase}");
                }
            }

            return string.IsNullOrEmpty(responseContent)
                ? default
                : JsonConvert.DeserializeObject<TResponse>(responseContent);
        }


        private static bool HasFormFile(object obj)
        {
            return obj.GetType().GetProperties()
                      .Any(p => typeof(IFormFile).IsAssignableFrom(p.PropertyType) ||
                                (typeof(IEnumerable<IFormFile>).IsAssignableFrom(p.PropertyType)));
        }

        // Build multipart/form-data dynamically
        private static MultipartFormDataContent BuildMultipartContent(object dto)
        {
            var formData = new MultipartFormDataContent();

            foreach (var prop in dto.GetType().GetProperties())
            {
                var value = prop.GetValue(dto);
                if (value == null) continue;

                if (value is IFormFile file) // single file
                {
                    var streamContent = new StreamContent(file.OpenReadStream());
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    formData.Add(streamContent, prop.Name, file.FileName);
                }
                else if (value is IEnumerable<IFormFile> files) // multiple files
                {
                    foreach (var f in files)
                    {
                        if (f == null) continue;
                        var streamContent = new StreamContent(f.OpenReadStream());
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue(f.ContentType);
                        formData.Add(streamContent, prop.Name, f.FileName);
                    }
                }
                else
                {
                    formData.Add(new StringContent(value.ToString()!), prop.Name);
                }
            }

            return formData;
        }

    }

}



