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
        public string SearchTerms { get; set; }
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
        [BindProperty]
        public double? IMDBMin { get; set; }
        
        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum Rotten Tomatoes Rating
        /// </summary>
        [BindProperty]
        public double? RottenMin { get; set; }

        /// <summary>
        /// The maximum Rotten Tomatoes Rating
        /// </summary>
        [BindProperty]
        public double? RottenMax { get; set; }

        public void OnGet()
        {
            Movies = MovieDatabase.All;  
        }

        public void OnPost()
        {
            //Filterable Database
            Movies = MovieDatabase.All;
            
            //Search movie titles for the SearchTerms
            if (SearchTerms != null)
            {
                Movies = Movies.Where(
                movie => movie.Title != null && 
                movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase)
                );
               
            }
            
            //Filter by MPAA Rating
            if(MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(
                movie => movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating)
                );
            }
            
            //Filter by Genre
            if(Genres != null && Genres.Length != 0)
            {
                Movies = Movies.Where(
                    movie => movie.MajorGenre != null && Genres.Contains(movie.MajorGenre)
                    );
            }
            
            //Filter by IMDB Rating
            if(IMDBMin != null && IMDBMax != null)
            {
                Movies = Movies.Where(
                    movie => movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax
                    );
            }else if(IMDBMin == null && IMDBMax != null)
            {
                Movies = Movies.Where(
                    movie => movie.IMDBRating <= IMDBMax
                    );

            }
            else
            {
                Movies = Movies.Where(
                    movie => movie.IMDBRating >= IMDBMin
                    );
            }

            //Filter by Rotten Tomatoes
            if (RottenMin != null && RottenMax != null)
            {
                Movies = Movies.Where(
                    movie => movie.RottenTomatoesRating >= RottenMin && movie.RottenTomatoesRating <= RottenMax
                    );
            }
            else if (IMDBMin == null && IMDBMax != null)
            {
                Movies = Movies.Where(
                    movie => movie.RottenTomatoesRating <= RottenMax
                    );

            }
            else
            {
                Movies = Movies.Where(
                    movie => movie.RottenTomatoesRating >= RottenMin
                    );
            }
        }
    }
}
