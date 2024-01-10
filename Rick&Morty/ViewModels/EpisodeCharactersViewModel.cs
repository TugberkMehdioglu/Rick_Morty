namespace Rick_Morty.ViewModels
{
    public class EpisodeCharactersViewModel
    {
        public int EpisodeId { get; set; }
        public int CharacterId { get; set; }


        //Navigation Properties
        public CharacterViewModel Character { get; set; } = null!;
        public EpisodeViewModel Episode { get; set; } = null!;
    }
}
