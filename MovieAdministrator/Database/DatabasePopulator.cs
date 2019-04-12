using MovieAdministrator.Models;
using System;
using System.Linq;

namespace MovieAdministrator.Database
{
    public static class DatabasePopulator
    {
        public static void Populate(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            if (context.Actors.Any())
            {
                return;
            }
            PopulateActors(context);
            PopulateGenre(context);
            PopulateMovies(context);
            PopulateCast(context);
            Console.WriteLine("Database Loaded sucessfully");
        }

        private static void PopulateActors(DatabaseContext context)
        {
            Actor[] actors = new Actor[]
            {
                new Actor{FirstName="Leonardo",LastName="DiCaprio",Gender=Actor.GenderOptions.Male, Age=44},
                new Actor{FirstName="Jonah",LastName="Hill",Gender=Actor.GenderOptions.Male, Age=35},
                new Actor{FirstName="Margot",LastName="Robbie",Gender=Actor.GenderOptions.Female, Age=28},
                new Actor{FirstName="Matthew",LastName="McConaughey",Gender=Actor.GenderOptions.Male, Age=49},
            };
            foreach (Actor act in actors)
            {
                context.Actors.Add(act);
            }
            context.SaveChanges();
        }

        private static void PopulateGenre(DatabaseContext context)
        {
            Genre[] genres = new Genre[] {
                new Genre{Name="Comedy"},
                new Genre{Name="Action"},
                new Genre{Name="Thriller"},
                new Genre{Name="Horror"}
            };
            foreach (Genre genre in genres)
            {
                context.Genres.Add(genre);
            }
            context.SaveChanges();
        }

        private static void PopulateMovies(DatabaseContext context)
        {
            Movie[] movies = new Movie[] {
                new Movie{Title="The Wolf Of Wall Street", ReleaseDate=DateTime.Parse("2013-12-17"),
                    GenreID=context.Genres.Single( s => s.Name == "Comedy").ID }
            };

            foreach (Movie mov in movies)
            {
                context.Movies.Add(mov);
            }
            context.SaveChanges();
        }

        private static void PopulateCast(DatabaseContext context)
        {
            Cast[] casts = new Cast[] {
                new Cast { ActorID=context.Actors.Single( s => s.LastName == "DiCaprio").ID,
                    MovieID=context.Movies.Single( s => s.Title == "The Wolf Of Wall Street").ID },
                new Cast { ActorID=context.Actors.Single( s => s.LastName == "Hill").ID,
                    MovieID=context.Movies.Single( s => s.Title == "The Wolf Of Wall Street").ID },
                new Cast { ActorID=context.Actors.Single( s => s.LastName == "Robbie").ID,
                    MovieID=context.Movies.Single( s => s.Title == "The Wolf Of Wall Street").ID },
                new Cast { ActorID=context.Actors.Single( s => s.LastName == "McConaughey").ID,
                    MovieID=context.Movies.Single( s => s.Title == "The Wolf Of Wall Street").ID }
            };

            foreach (Cast cast in casts)
            {
                context.Casts.Add(cast);
            }
            context.SaveChanges();
        }
    }
}
