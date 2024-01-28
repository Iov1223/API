using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace API
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiKey = "0c5916fd146e546373e832ff80334aa9";
            string city = "Yekaterinburg";
            string weatherAPIUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(weatherAPIUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(responseBody);

                    double temperature = (double)json["main"]["temp"];

                    string precipitation = (string)json["weather"][0]["main"]; 

                    double windSpeed = (double)json["wind"]["speed"];

                    double celsiusTemperature = temperature - 273.15;

                    Console.WriteLine("Погода в {0}:", city);
                    Console.WriteLine("Температура: {0:F1}°C", celsiusTemperature);
                    Console.WriteLine("Осадки: {0}", precipitation);
                    Console.WriteLine("Сила ветра: {0} м/с", windSpeed);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка при выполнении HTTP запроса: {ex.Message}");
            }
        }
    }
}
