using BookService.WebAPI.Models;
using BookService.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerCrudBase<Rating, RatingRepository>
    {
        public RatingsController(RatingRepository ratingRepository) : base(ratingRepository)
        {

        }
        // GET: api/Ratings
        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var result = await repository.GetAllInclusive();

            List<Rating> ratings = new List<Rating>();
            foreach(Rating rating in result)
            {
                rating.Reader.Ratings = null;
                rating.Book.Ratings = null;
                ratings.Add(rating);
            }

            return Ok(JsonConvert.SerializeObject(ratings));
        }
    }
}