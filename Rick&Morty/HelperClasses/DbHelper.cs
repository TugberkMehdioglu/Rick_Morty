using Rick_Morty.ContextClasses;
using Rick_Morty.DTOClasses;
using Rick_Morty.Entities;

namespace Rick_Morty.HelperClasses
{
    public class DbHelper
    {
        public async Task AddCharacters(List<Character> characters, MyContext context)
        {
            await context.Characters.AddRangeAsync(characters);
            await context.SaveChangesAsync();
        }

        public async Task AddEpisodesAndEpisodesCharacters(List<Episode> episodes, List<EpisodeCharacters> episodeCharacters, MyContext context)
        {
            await context.Episodes.AddRangeAsync(episodes);
            await context.EpisodeCharacters.AddRangeAsync(episodeCharacters);
            await context.SaveChangesAsync();
        }
    }
}
