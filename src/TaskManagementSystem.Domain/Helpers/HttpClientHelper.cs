using System.Text.Json;
using TMS.Domain.Exceptions;

namespace TMS.Domain.Helpers
{
    public static class HttpClientHelper
    {
        public static async Task<T> ExecuteHttpGet<T>(this HttpClient _httpClient, string apiUrl) where T : class
        {
            var response = await _httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            var httpStringContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(httpStringContent);

            if (result is null)
                throw new CustomException(CustomExceptionType.NoContent);

            return result;
        }
    }
}
