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
        const string API = "http://api.timezonedb.com/v2.1/get-time-zone?format=json&key=XXG0IRO9WXI6&by=zone&zone=Europe/Warsaw";
        public static DateTime CurrentDate { get; set; } // private

        public static async Task<DateTime> GetDate()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = await client.GetAsync(API);
            var resultArray = await responseMessage.Content.ReadAsStringAsync();
            Date date = JsonConvert.DeserializeObject<Date>(resultArray);

            string dateString = date.formatted;
            DateTime currDate = Convert.ToDateTime(dateString);
            CurrentDate = currDate;

            return currDate;
        }
    }
}
