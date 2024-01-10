using Rick_Morty.DTOClasses;
using Rick_Morty.Entities;

namespace Rick_Morty.HelperClasses
{
    public class ConvertHelper
    {
        public List<Character> ConvertToCharacterEntity(List<CharacterDTO> characterDTOs)
        {
            List<Character> characters = new();

            foreach (CharacterDTO item in characterDTOs)
            {
                characters.Add(new Character()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status,
                    Species = item.Species,
                    Type = item.Type,
                    Gender = item.Gender,
                    Image = item.Image,
                    Url = item.Url,
                    Created = item.Created
                });
            }

            return characters;
        }

        public (List<Episode>, List<EpisodeCharacters>) ConvertToEpisodeEntity(List<EpisodeDTO> episodeDTOs)
        {
            List<Episode> episodes = new();
            List<EpisodeCharacters> episodeCharacters = new();

            foreach (EpisodeDTO item in episodeDTOs)
            {
                episodes.Add(new()
                {
                    Id = item.Id,
                    Name = item.Name,
                    AirDate = item.AirDate,
                    EpisodeCode = item.EpisodeCode,
                    Url = item.Url,
                    Created = item.Created
                });

                foreach (string element in item.Characters)
                {
                    string[] parts = element.Split('/');
                    string characterId = parts.Last();

                    episodeCharacters.Add(new()
                    {
                        CharacterId = Convert.ToInt32(characterId),
                        EpisodeId = item.Id
                    });
                }

            }

            return (episodes, episodeCharacters);
        }
    }
}
