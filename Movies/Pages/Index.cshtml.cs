using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        
        public IEnumerable<Movie> Movies { get; protected set; }

        /// <summary>
        /// The current search terms
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; } =  "";
        /// <summary>
        /// The filtered MPAARatings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty]
        public string[] Genres { get; set; }
        
        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        public double? IMDBMin { get; set; }
        
        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum Rotten Tomatoes Rating
        /// </summary>
        public double? RottenMin { get; set; }

        /// <summary>
        /// The maximum Rotten Tomatoes Rating
        /// </summary>
        public double? RottenMax { get; set; }

        public void OnGet(double? IMDBMin, double? IMDBMax, double? RottenMin, double? RottenMax)
        {
            this.RottenMin = RottenMin;
            this.RottenMax = RottenMax;
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            
            SearchTerms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            Genres = Request.Query["Genres"];

            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RottenMin, RottenMax);
            
        }
    }
}
