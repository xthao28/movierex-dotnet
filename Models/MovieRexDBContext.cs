using System;
using System.Xml;
using Microsoft.EntityFrameworkCore;
namespace MovieRex.Models
{
	public class MovieRexDBContext : DbContext
	{
		public MovieRexDBContext(DbContextOptions<MovieRexDBContext> options) : base(options) { }

		public DbSet<Account> Accounts { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieFavourite> MovieFavourites { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<MovieActor>().ToTable("MovieActor");
            modelBuilder.Entity<MovieGenre>().ToTable("MovieGenre");
            modelBuilder.Entity<MovieFavourite>().ToTable("MovieFavourite");            
        }
    }
}

