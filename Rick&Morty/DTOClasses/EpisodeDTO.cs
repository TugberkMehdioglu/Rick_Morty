using Rick_Morty.Entities;
using System.Text.Json.Serialization;

namespace Rick_Morty.DTOClasses
{
    public class EpisodeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [JsonPropertyName("air_date")]
        public string AirDate { get; set; } = null!;

        [JsonPropertyName("episode")]
        public string EpisodeCode { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Created { get; set; } = null!;
        public List<string> Characters { get; set; } = null!;
    }
}
