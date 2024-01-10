using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rick_Morty.ContextClasses;
using Rick_Morty.Entities;
using Rick_Morty.Extensions;
using Rick_Morty.HelperClasses;
using Rick_Morty.ViewModels;

namespace Rick_Morty.Controllers
{
    [Route("[Controller]/[Action]")]
    public class CharacterController : Controller
    {
        private readonly MyContext _context;
        private readonly HttpClient _httpClient;

        public CharacterController(MyContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [Route("/")]
        [Route("/Home")]
        [HttpGet("{search?}/{pageNumber?}/{pageSize?}")]
        public async Task<IActionResult> Index(string? search, int pageNumber = 1, int pageSize = 12)
        {
            await CreateDbIfNotExist(_context, _httpClient);

            int totalItemsCount;
            IQueryable<Character> query;

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                totalItemsCount = await _context.Characters!.Where(x => x.Name.ToLower().Trim().Contains(search.ToLower().Trim())).CountAsync();
                query = _context.Characters!.Where(x => x.Name.ToLower().Trim().Contains(search.ToLower().Trim()));
            }
            else
            {
                totalItemsCount = await _context.Characters!.CountAsync();
                query = _context.Characters!.AsQueryable();
            }


            List<CharacterViewModel> characterViewModels = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => new CharacterViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                Species = x.Species,
                Type = x.Type,
                Gender = x.Gender,
                Image = x.Image,
                Url = x.Url,
                Created = x.Created
            }).ToListAsync();

            ViewBag.totalItemsCount = totalItemsCount;
            ViewBag.pageNumber = pageNumber;
            ViewBag.totalPagesCount = (int)Math.Ceiling((double)totalItemsCount / pageSize);

            return View(characterViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AddToFav(int id)
        {
            CharacterViewModel characterViewModel = (await _context.Characters!.Where(x => x.Id == id).Select(x => new CharacterViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image
            }).FirstOrDefaultAsync())!;

            List<CharacterViewModel>? fav = HttpContext.Session.GetSession<List<CharacterViewModel>>("fav");
            if (fav == null) fav = new List<CharacterViewModel>();

            if (fav.Any(x => x.Id == characterViewModel.Id))
            {
                TempData["fail"] = "Karakter zaten favori listenizde";
                return RedirectToAction(nameof(Index));
            }
            else if (fav.Count >= 10) 
            {
                TempData["fail"] = "Favori karakter ekleme sayısını aştınız. Başka bir karakteri favorilerden çıkarmalısınız";
                return RedirectToAction(nameof(Index));
            }
            else fav.Add(characterViewModel);

            HttpContext.Session.SetSession("fav", fav);

            TempData["success"] = "Karakter favoriye eklendi";
            return RedirectToAction(nameof(Index), "Character");
        }

        [HttpGet("{id}")]
        public IActionResult DeleteFromFav(int id)
        {
            List<CharacterViewModel> characterViewModelList = (HttpContext.Session.GetSession<List<CharacterViewModel>>("fav"))!;

            CharacterViewModel characterViewModel = (characterViewModelList.Find(x => x.Id == id))!;

            characterViewModelList.Remove(characterViewModel);

            if(characterViewModelList.Count <= 0)
            {
                HttpContext.Session.Remove("fav");
                TempData["fail"] = "Favori listeniz boş";
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.SetSession("fav", characterViewModelList);

            TempData["success"] = "Karakter favorilerden silindi";
            return RedirectToAction(nameof(FavoritePage));
        }

        public IActionResult FavoritePage()
        {
            List<CharacterViewModel>? fav = HttpContext.Session.GetSession<List<CharacterViewModel>>("fav");

            if(fav == null)
            {
                TempData["fail"] = "Favori sayfanız boş";
                return RedirectToAction(nameof(Index));
            }

            return View(fav);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CharacterDetail(int id)
        {
            Character character = (await _context.Characters!.Where(x => x.Id == id).Include(x => x.EpisodeCharacters).FirstOrDefaultAsync())!;

            CharacterViewModel characterViewModel = new()
            {
                Id = character.Id,
                Name = character.Name,
                Status = character.Status,
                Species = character.Species,
                Type = character.Type,
                Gender = character.Gender,
                Image = character.Image,
                Url = character.Url,
                Created = character.Created,
                EpisodeCharacters = new List<EpisodeCharactersViewModel>()
            };

            foreach (EpisodeCharacters item in character.EpisodeCharacters)
            {
                characterViewModel.EpisodeCharacters.Add(new()
                {
                    EpisodeId = item.EpisodeId,
                    CharacterId = item.CharacterId
                });
            }

            //Todo: Burdaki sayfadan episode'a aktarım olayını unutma

            return View(characterViewModel);
        }

        public async Task CreateDbIfNotExist(MyContext context, HttpClient client)
        {
            if (!context.Episodes.Any())
            {
                ApiHelper apiHelper = new();
                ConvertHelper convertHelper = new();
                DbHelper dbHelper = new();

                var (error, characterDTOs) = await apiHelper.GetCharacters(_httpClient);
                if (error != null)
                {
                    ViewBag.error = error;
                    return;
                }

                List<Character> characters = convertHelper.ConvertToCharacterEntity(characterDTOs!);

                await dbHelper.AddCharacters(characters, _context);


                var (warning, episodeDtos) = await apiHelper.GetEpisodes(_httpClient);
                if (warning != null)
                {
                    ViewBag.error = warning;
                    return;
                }

                var (episodes, episodeCharacters) = convertHelper.ConvertToEpisodeEntity(episodeDtos!);

                await dbHelper.AddEpisodesAndEpisodesCharacters(episodes, episodeCharacters, _context);
            }
        }
    }
}
