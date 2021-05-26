using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using pogoda.Models;

namespace pogoda.Services
{
    class DateService
    {
        const string API = "http://worldtimeapi.org/api/timezone/Europe/Warsaw";
        public static DateTime CurrentDate { get; private set; }

        public static async Task<DateTime> GetDate()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = await client.GetAsync(API);
            var resultArray = await responseMessage.Content.ReadAsStringAsync();
            var date = JsonConvert.DeserializeObject<Date>(resultArray);

            string dateString = date.datetime;
            DateTime currDate = Convert.ToDateTime(dateString);
            CurrentDate = currDate;

            return currDate;
        }
    }
}
