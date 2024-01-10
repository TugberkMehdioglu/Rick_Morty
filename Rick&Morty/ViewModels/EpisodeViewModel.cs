using System.Text.Json.Serialization;

namespace Rick_Morty.ViewModels
{
    public class EpisodeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string AirDate { get; set; } = null!;
        public string EpisodeCode { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Created { get; set; } = null!;


        //Navigation Properties
        public ICollection<EpisodeCharactersViewModel>? EpisodeCharacters { get; set; }
    }
}
