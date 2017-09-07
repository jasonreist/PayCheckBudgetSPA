using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BudgetThis2017.Proxy
{
  public class PBProxy
  {
    private HttpResponseMessage response = null;
    public async Task<string> CallApi<T>(string url)
    {
      using (HttpClient client = new HttpClient())
      {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        response = client.GetAsync(url).Result;
      }
       return await response.Content.ReadAsStringAsync();
    }
  }
}
