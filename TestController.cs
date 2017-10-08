using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace ApiCore.Controller
{
    public class TestController : Controller
    {
        private readonly IJSONService _restServ;
        private readonly string _urlService = "";

        public TestController(IJSONService serv)
        {
            _restServ = serv;
            _urlService = "url service";
        }

        //Пример использования GET запроса
        [Route("api/[controller]/Types")]
        [HttpGet]
        public async Task<List<SomeClassResponse>> Method1()
        {
            string url = _urlService + "api/types...";
            var resp = await _restServ.GetAsync<List<SomeClassResponse>>(url);
            return resp;
        }

     //Пример использования POST запроса
        [Route("api/[controller]/Types")]
        [HttpPost]
        public async Task<List<SomeClassResponse>> Method2([FromBody]SomeClassRequest value)
        {
            string url = _urlService + "api/types...";
            var result = await _restServ.PostAsync<SomeClassResponse, SomeClassRequest>(url, inc);
            return result;
        }
    }
}