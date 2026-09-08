using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
        {
            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }
            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Price = 7.99M,
                    Rating = "R"
                },
                new Movie
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Price = 8.99M,
                    Rating = "R"
                },
                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Price = 9.99M,
                    Rating = "R"
                },
                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "R"
                },
                new Movie
                {
                    Title = "KPop Demon Hunters",
                    ReleaseDate = DateTime.Parse("2025-6-20"),
                    Genre = "Animation",
                    Price = 9.99M,
                    Rating = "PG"
                },

                new Movie
                {
                    Title = "Spider-Man: No Way Home",
                    ReleaseDate = DateTime.Parse("2021-12-17"),
                    Genre = "Action",
                    Price = 9.99M,
                    Rating = "PG-13"
                },

                new Movie
                {
                    Title = "Dead Poets Society",
                    ReleaseDate = DateTime.Parse("1989-6-2"),
                    Genre = "Drama",
                    Price = 7.99M,
                    Rating = "PG"
                }
            );
            context.SaveChanges();
        }
    }
}