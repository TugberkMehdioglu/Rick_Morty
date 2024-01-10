using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Rick_Morty.Entities
{
    //Only episode and character were wanted in the task, other entities are not taken
    //Property names are same of the names on API
    //API: https://rickandmortyapi.com/documentation/#episode-schema
    public class Episode
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [JsonPropertyName("air_date")]
        public string AirDate { get; set; } = null!;
        public string EpisodeCode { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Created { get; set; } = null!;


        //Navigation Properties
        [JsonPropertyName("characters")]
        public ICollection<EpisodeCharacters> EpisodeCharacters { get; set; } = null!;
    }
}
