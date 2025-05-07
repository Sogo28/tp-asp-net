using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Pizzéria.Models
{
    public class PizzaEhod
    {
        [Key]
        public int IdEhod { get; set; }
        public string? NomEhod { get; set; }
        public string? DescriptionEhod { get; set; }
    }

    class PizzaEhodDB : DbContext
    {
        public PizzaEhodDB(DbContextOptions<PizzaEhodDB> options) : base(options) { }
        public DbSet<PizzaEhod> Pizzas { get; set; } = null!;
    }
}
