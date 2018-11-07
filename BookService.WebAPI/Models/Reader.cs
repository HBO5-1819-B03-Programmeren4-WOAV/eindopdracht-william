using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookService.WebAPI.Models
{
    public class Reader : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Rating> Ratings { get; set; }

    }
}
