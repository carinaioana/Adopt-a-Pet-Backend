using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ImageSimilarityService
{
    private readonly HttpClient _httpClient;

    public ImageSimilarityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JObject> GetSimilarImagesAsync(string imageUrl, string label, int k = 3)
    {
        var requestData = new
        {
            image_url = imageUrl,
            label = label,
            k = k
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://your-flask-api-url/similarity", jsonContent);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<JObject>(responseContent);
    }
}
