using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using Rick_Morty.ContextClasses;
using Rick_Morty.Entities;
using Rick_Morty.ViewModels;

namespace Rick_Morty.Controllers
{
    [Route("[Controller]/[Action]")]
    public class EpisodeController : Controller
    {
        private readonly MyContext _context;
        public EpisodeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("{search?}/{pageNumber?}/{pageSize?}")]
        public async Task<IActionResult> Index(string? search, int pageNumber = 1, int pageSize = 12)
        {
            int totalItemsCount;
            IQueryable<Episode> query;

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                totalItemsCount = await _context.Episodes!.Where(x => x.EpisodeCode.ToLower().Trim().Contains(search.ToLower().Trim())).CountAsync();
                query = _context.Episodes!.Where(x => x.EpisodeCode.ToLower().Trim().Contains(search.ToLower().Trim()));
            }
            else
            {
                totalItemsCount = await _context.Episodes!.CountAsync();
                query = _context.Episodes!.AsQueryable();
            }


            List<EpisodeViewModel> episodeViewModels = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new EpisodeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                AirDate = x.AirDate,
                EpisodeCode = x.EpisodeCode,
                Url = x.Url,
                Created = x.Created
            }).ToListAsync();

            ViewBag.totalItemsCount = totalItemsCount;
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalPagesCount = (int)Math.Ceiling((double)totalItemsCount / pageSize);

            return View(episodeViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> EpisodeDetail(int id)
        {
            Episode episode = (await _context.Episodes!.Where(x => x.Id == id).Include(x => x.EpisodeCharacters).ThenInclude(x=>x.Character).FirstOrDefaultAsync())!;

            EpisodeViewModel episodeViewModel = new()
            {
                Id = episode.Id,
                Name = episode.Name,
                AirDate = episode.AirDate,
                EpisodeCode = episode.EpisodeCode,
                Url = episode.Url,
                Created = episode.Created,
                EpisodeCharacters = new List<EpisodeCharactersViewModel>()
            };

            foreach (EpisodeCharacters item in episode.EpisodeCharacters)
            {
                episodeViewModel.EpisodeCharacters.Add(new()
                {
                    EpisodeId = item.EpisodeId,
                    Character = new CharacterViewModel()
                    {
                        Id = item.Character.Id,
                        Name = item.Character.Name,
                        Image = item.Character.Image
                    }
                });
            }

            return View(episodeViewModel);
        }
    }
}
