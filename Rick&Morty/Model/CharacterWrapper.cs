using Rick_Morty.DTOClasses;
using Rick_Morty.Entities;

namespace Rick_Morty.Model
{
    public class CharacterWrapper
    {
        public Info Info { get; set; } = null!;
        public List<CharacterDTO> Results { get; set; } = null!;
    }
}
