using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace ApiCore.Services
{
    /// <summary>
    /// Для запросов на другие rest сервисы
    /// </summary>
    public class JsonWebService : IJSONService
    {
        private readonly JsonSerializer serializer = new JsonSerializer();

        public JsonWebService() { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="urlService"></param>
        public JsonWebService(string urlService)
        {
            UrlService = urlService;
        }
        /// <summary>
        /// Адрес веб сервера
        /// </summary>
        public string UrlService { get; set; }

        /// <summary>
        /// GET запрос
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <returns></returns>
        public TResponse Get<TResponse>(string url)
        {
            TResponse resp = default(TResponse);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // List data response.
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    resp = this.GetResponse<TResponse>(response);
                }
            }
            return resp;
        }

        /// <summary>
        /// POST запрос
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <typeparam name="TRequestContent">Тип запроса для сирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <param name="content">Объект запроса</param>
        /// <returns></returns>
        public TResponse Post<TResponse, TRequestContent>(string url, TRequestContent content)
        {
            TResponse resp = default(TResponse);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(url, new JsonContent(content)).Result;
                if (response.IsSuccessStatusCode)
                {
                    resp = this.GetResponse<TResponse>(response);
                }
            }
            return resp;
        }

        /// <summary>
        /// PUT запрос
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <typeparam name="TRequestContent">Тип запроса для сирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <param name="content">Объект запроса</param>
        /// <returns></returns>
        public TResponse Put<TResponse, TRequestContent>(string url, TRequestContent content)
        {
            TResponse resp = default(TResponse);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PutAsync(url, new JsonContent(content)).Result;
                if (response.IsSuccessStatusCode)
                {
                    resp = this.GetResponse<TResponse>(response);
                }
            }
            return resp;
        }

        /// <summary>
        /// Обработка ответа
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <param name="response">Ответ</param>
        /// <returns></returns>
        private TResponse GetResponse<TResponse>(HttpResponseMessage response)
        {
            TResponse resp = default(TResponse);
            Task task = response.Content.ReadAsStreamAsync().ContinueWith(t =>
            {
                var dataObjects = t.Result;
                using (var reader = new StreamReader(dataObjects))
                {
                    resp = serializer.Deserialize<TResponse>(new JsonTextReader(reader));
                }
            });

            task.Wait();
            return resp;
        }


        #region Async


        /// <summary>
        /// GET запрос асинхронный
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <returns></returns>
        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            TResponse resp = default(TResponse);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    resp = await GetResponseAsync<TResponse>(response);
                }

            }
            return resp;
        }

        /// <summary>
        /// POST запрос асинхронный
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <typeparam name="TRequestContent">Тип запроса для сирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <param name="content">Объект запроса</param>
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TResponse, TRequestContent>(string url, TRequestContent content)
        {
            TResponse resp = default(TResponse);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(url, new JsonContent(content));
                if (response.IsSuccessStatusCode)
                {
                    resp = await GetResponseAsync<TResponse>(response);
                }
            }
            return resp;
        }

        /// <summary>
        /// PUT запрос асинхронный
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <typeparam name="TRequestContent">Тип запроса для сирилизации</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <param name="content">Объект запроса</param>
        /// <returns></returns>
        public async Task<TResponse> PutAsync<TResponse, TRequestContent>(string url, TRequestContent content)
        {
            TResponse resp = default(TResponse);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsync(url, new JsonContent(content));
                if (response.IsSuccessStatusCode)
                {
                    resp = await GetResponseAsync<TResponse>(response);
                }
            }
            return resp;
        }

        /// <summary>
        /// Получение ответа на запрос (асинхронный)
        /// </summary>
        /// <typeparam name="TResponse">Тип ответа для десирилизации</typeparam>
        /// <param name="response">Ответ</param>
        /// <returns></returns>
        private async Task<TResponse> GetResponseAsync<TResponse>(HttpResponseMessage response)
        {
            TResponse resp = default(TResponse);
            await response.Content.ReadAsStreamAsync().ContinueWith(t =>
            {
                var dataObjects = t.Result;
                using (var reader = new StreamReader(dataObjects))
                {
                    resp = serializer.Deserialize<TResponse>(new JsonTextReader(reader));
                }
            });

            return resp;
        }



        #endregion

    }

    /// <summary>
    /// Для серилизации 
    /// </summary>
    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :  base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }
}
