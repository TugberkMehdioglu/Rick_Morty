using Rick_Morty.DTOClasses;
using Rick_Morty.Entities;

namespace Rick_Morty.Model
{
    public class EpisodeWrapper
    {
        public Info Info { get; set; } = null!;
        public List<EpisodeDTO> Results { get; set; } = null!;
    }
}
