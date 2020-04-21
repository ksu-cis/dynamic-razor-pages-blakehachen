using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = new List<Movie>();

        private static string[] genres;

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        static MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }

            HashSet<string> genreSet = new HashSet<string>();
            foreach(Movie movie in movies)
            {
                if(movie.MajorGenre != null)
                {
                    genreSet.Add(movie.MajorGenre);
                }
            }
            genres = genreSet.ToArray();
        }

        /// <summary>
        /// Gets all the movies in the database
        /// </summary>
        public static IEnumerable<Movie> All { get { return movies; } }

        /// <summary>
        /// Searches the database for mathcing movies
        /// </summary>
        /// <param name="terms">terms to search</param>
        /// <returns>collection of movies found from search terms</returns>
        public static IEnumerable<Movie> Search(string terms)
        {
            List<Movie> results = new List<Movie>();

            if (terms == null) return All;

            foreach(Movie movie in All)
            {
                if(movie.Title != null && movie.Title.Contains(terms, StringComparison.InvariantCultureIgnoreCase))
                {
                    results.Add(movie);
                }
            }

            return results;
        }

        /// <summary>
        /// Filters database by MPAA Rating
        /// </summary>
        /// <param name="movies">movie database</param>
        /// <param name="ratings">available ratings to narrow search criteria</param>
        /// <returns>list of movies fitting MPAA based search criteria</returns>
        public static IEnumerable<Movie> FilterByMPAARating(IEnumerable<Movie> movies, IEnumerable<string> ratings)
        {
            //TODO: Filter the list
            if (ratings == null || ratings.Count() == 0) return movies;
            List<Movie> results = new List<Movie>();
            foreach(Movie movie in movies)
            {
                if(movie.MPAARating != null && ratings.Contains(movie.MPAARating))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        /// <summary>
        /// Filters database by genre
        /// </summary>
        /// <param name="movies">movie database</param>
        /// <param name="genres">availabe genres to narrow search criteria</param>
        /// <returns>list of movies fitting genre based search criteria</returns>
        public static IEnumerable<Movie> FilterByGenre(IEnumerable<Movie> movies, IEnumerable<string> genres)
        {
            //TODO: Filter the list
            if (genres == null || genres.Count() == 0) return movies;
            List<Movie> results = new List<Movie>();
            foreach (Movie movie in movies)
            {
                if (movie.MPAARating != null && genres.Contains(movie.MajorGenre))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        /// <summary>
        /// Filters database by IMDB rating
        /// </summary>
        /// <param name="movies">movie database</param>
        /// <param name="min">minimum imdb rating</param>
        /// <param name="max">maximum imdb rating</param>
        /// <returns>list of movies fitting imdb based search criteria</returns>
        public static IEnumerable<Movie> FilterByIMDBRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;
            var results = new List<Movie>();
            
            if(min == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.IMDBRating <= max) results.Add(movie);
                }
                return results;
            }

            if(max == null)
            {
                foreach(Movie movie in movies)
                {
                    if (movie.IMDBRating >= min) results.Add(movie);
                }
                return results;
            }
            foreach (Movie movie in movies)
            {
                if(movie.IMDBRating != null && movie.IMDBRating >= min && movie.IMDBRating <= max)
                {
                    results.Add(movie);
                }

            }
            return results;
        }

        public static IEnumerable<Movie> FilterByRottenTomatoesRating(IEnumerable<Movie> movies, double? min, double? max)
        {
            if (min == null && max == null) return movies;
            var results = new List<Movie>();

            if (min == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating <= max) results.Add(movie);
                }
                return results;
            }

            if (max == null)
            {
                foreach (Movie movie in movies)
                {
                    if (movie.RottenTomatoesRating >= min) results.Add(movie);
                }
                return results;
            }
            foreach (Movie movie in movies)
            {
                if (movie.RottenTomatoesRating != null && movie.RottenTomatoesRating >= min && movie.RottenTomatoesRating <= max)
                {
                    results.Add(movie);
                }

            }
            return results;
        }

        /// <summary>
        /// array of MPAA Ratings
        /// </summary>
        public static string[] MPAARatings
        {
            get => new string[]
            {
                "G",
                "PG",
                "PG-13",
                "R",
                "NC-17"
            };
        }

        /// <summary>
        /// Gets the movie genres represented in the database
        /// </summary>
        public static string[] Genres => genres;
    }
}
