using ExamenProgreso3_SebastianLargo.Models_SL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProgreso3_SebastianLargo.Services
{
    public class SL_NarutoAPIService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://narutodb.xyz/api/";

        public SL_NarutoAPIService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<NarutoCharacter_SL>> GetCharactersAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}character");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse>(content);
                return result.Characters;
            }
            return new List<NarutoCharacter_SL>();
        }
        public class ApiResponse
        {
            public List<NarutoCharacter_SL> Characters { get; set; }
        }
    }
}
