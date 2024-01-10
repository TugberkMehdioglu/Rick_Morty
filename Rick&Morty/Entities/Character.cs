using Newtonsoft.Json;

namespace Rick_Morty.Entities
{
    //Only episode and character were wanted in the task, other entities are not taken
    //Property names are same of the names on API
    //API: https://rickandmortyapi.com/documentation/#character-schema
    [Serializable]
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Species { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Created { get; set; } = null!;


        //Navigation Properties
        public ICollection<EpisodeCharacters> EpisodeCharacters { get; set; } = null!;
    }
}
