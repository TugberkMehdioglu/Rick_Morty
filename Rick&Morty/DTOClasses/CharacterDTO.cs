using Rick_Morty.Entities;

namespace Rick_Morty.DTOClasses
{
    public class CharacterDTO
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
        public List<string> Episode { get; set; } = null!;
    }
}
