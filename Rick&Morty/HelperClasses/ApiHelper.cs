using Rick_Morty.DTOClasses;
using Rick_Morty.Entities;
using Rick_Morty.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rick_Morty.HelperClasses
{
    public class ApiHelper
    {
        public async Task<(string?, List<CharacterDTO>?)> GetCharacters(HttpClient client) 
        {
            string apiUrl = "https://rickandmortyapi.com/api/character";

            HttpResponseMessage getResponse = await client.GetAsync(apiUrl);
            string result = await getResponse.Content.ReadAsStringAsync();

            List<CharacterDTO> dtoList = new();

            if (!getResponse.IsSuccessStatusCode)
            {
                return (result, null);
            }
            else
            {
                CharacterWrapper? characterWrapper = System.Text.Json.JsonSerializer.Deserialize<CharacterWrapper>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if(characterWrapper == null ) return ("Dönüşüm başarısız", null);
                else
                {
                    dtoList.AddRange(characterWrapper.Results);

                    for (int i = 2; i <= characterWrapper.Info.Pages; i++)
                    {
                        var response = await client.GetAsync($"{apiUrl}?page={i}");
                        string result2 = await response.Content.ReadAsStringAsync();

                        CharacterWrapper? wrapper = System.Text.Json.JsonSerializer.Deserialize<CharacterWrapper>(result2, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (wrapper == null) return ("Dönüşüm başarısız", null);

                        dtoList.AddRange(wrapper.Results);
                    }

                    return (null, dtoList);
                }
                
            }
        }

        public async Task<(string?, List<EpisodeDTO>?)> GetEpisodes(HttpClient client)
        {
            string apiUrl = "https://rickandmortyapi.com/api/episode";

            HttpResponseMessage getResponse = await client.GetAsync(apiUrl);
            string result = await getResponse.Content.ReadAsStringAsync();

            List<EpisodeDTO> dtoList = new();

            if (!getResponse.IsSuccessStatusCode)
            {
                return (result, null);
            }
            else
            {
                EpisodeWrapper? episodeWrapper = JsonSerializer.Deserialize<EpisodeWrapper>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (episodeWrapper == null) return ("Dönüşüm başarısız", null);
                else
                {
                    dtoList.AddRange(episodeWrapper.Results);

                    for (int i = 2; i <= episodeWrapper.Info.Pages; i++)
                    {
                        var response = await client.GetAsync($"{apiUrl}?page={i}");
                        string result2 = await response.Content.ReadAsStringAsync();

                        EpisodeWrapper? wrapper = JsonSerializer.Deserialize<EpisodeWrapper>(result2, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (wrapper == null) return ("Dönüşüm başarısız", null);

                        dtoList.AddRange(wrapper.Results);
                    }

                    return (null, dtoList);
                }
            }
        }
    }
}
