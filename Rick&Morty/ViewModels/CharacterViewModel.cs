namespace Rick_Morty.ViewModels
{
    public class CharacterViewModel
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
        public ICollection<EpisodeCharactersViewModel>? EpisodeCharacters { get; set; }
    }
}
