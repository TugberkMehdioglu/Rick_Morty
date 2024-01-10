namespace Rick_Morty.Entities
{
    public class EpisodeCharacters
    {
        public int EpisodeId { get; set; }
        public int CharacterId { get; set; }

        //Navigation Properties
        public Episode Episode { get; set; } = null!;
        public Character Character { get; set; } = null!;
    }
}
