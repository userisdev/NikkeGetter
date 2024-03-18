namespace TABBotApp.Libs.Utils
{
    /// <summary> HttpHelper class. </summary>
    internal static class HttpHelper
    {
        /// <summary> The HTTP client </summary>
        private static readonly HttpClient httpClient;

        /// <summary> Initializes the <see cref="HttpHelper" /> class. </summary>
        static HttpHelper()
        {
            httpClient = new HttpClient();

            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0";
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
        }

        /// <summary> Creates this instance. </summary>
        /// <returns> </returns>
        public static HttpClient Create()
        {
            return httpClient;
        }

        /// <summary> Gets the request. </summary>
        /// <param name="url"> The URL. </param>
        /// <returns> </returns>
        /// <exception cref="HttpRequestException"> Failed to fetch {endpoint} </exception>
        public static async Task<string> GetRequestAsync(string url)
        {
            Console.WriteLine( $"url:{url}");
            HttpResponseMessage response = await httpClient.GetAsync(url);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsStringAsync()
                : throw new HttpRequestException($"Failed : {response.StatusCode}");
        }

        /// <summary> Gets the request byte array asynchronous. </summary>
        /// <param name="url"> The URL. </param>
        /// <returns> </returns>
        /// <exception cref="HttpRequestException"> Failed to fetch {endpoint} </exception>
        public static async Task<byte[]> GetRequestByteArrayAsync(string url)
        {
            Console.WriteLine($"url:{url}");
            HttpResponseMessage response = await httpClient.GetAsync(url);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsByteArrayAsync()
                : throw new HttpRequestException($"Failed : {response.StatusCode}");
        }
    }
}
