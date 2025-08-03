using Microsoft.EntityFrameworkCore;
using ConsoleCookbook.Models;

namespace ConsoleCookbook.Data
{
    public class CookbookContext : DbContext
    {
        public DbSet<Przepis> Przepisy { get; set; }
        public DbSet<Skladnik> Skladniki { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=cookbook;Uid=root;Pwd=;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracja relacji jeden do wielu
            modelBuilder.Entity<Skladnik>()
                .HasOne(s => s.Przepis)
                .WithMany(p => p.Skladniki)
                .HasForeignKey(s => s.PrzepisId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seeding danych - 5 przykładowych przepisów
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Przepisy
            modelBuilder.Entity<Przepis>().HasData(
                new Przepis { Id = 1, Nazwa = "Spaghetti Carbonara", Instrukcje = "1. Ugotuj makaron al dente\n2. Podsmaż boczek\n3. Wymieszaj jajka z parmezanem\n4. Połącz wszystko razem", CzasPrzygotowania = 20, LiczbaOsob = 4, DataDodania = DateTime.Now },
                new Przepis { Id = 2, Nazwa = "Rosół z kurczaka", Instrukcje = "1. Włóż kurczaka do garnka z wodą\n2. Dodaj warzywa\n3. Gotuj 2 godziny na wolnym ogniu\n4. Przecedź bulion", CzasPrzygotowania = 150, LiczbaOsob = 6, DataDodania = DateTime.Now },
                new Przepis { Id = 3, Nazwa = "Naleśniki", Instrukcje = "1. Wymieszaj mąkę z mlekiem\n2. Dodaj jajka i szczyptę soli\n3. Smaż na patelni z obu stron", CzasPrzygotowania = 30, LiczbaOsob = 4, DataDodania = DateTime.Now },
                new Przepis { Id = 4, Nazwa = "Kotlet schabowy", Instrukcje = "1. Rozbij mięso\n2. Obtocz w mące, jajku i bułce tartej\n3. Smaż na złoty kolor", CzasPrzygotowania = 25, LiczbaOsob = 4, DataDodania = DateTime.Now },
                new Przepis { Id = 5, Nazwa = "Sernik na zimno", Instrukcje = "1. Rozkrusz herbatniki\n2. Wymieszaj ser z cukrem i śmietaną\n3. Ułóż warstwami\n4. Schłodź w lodówce", CzasPrzygotowania = 30, LiczbaOsob = 8, DataDodania = DateTime.Now }
            );

            // Składniki
            modelBuilder.Entity<Skladnik>().HasData(
                // Spaghetti Carbonara
                new Skladnik { Id = 1, Nazwa = "Spaghetti", Ilosc = "500g", PrzepisId = 1 },
                new Skladnik { Id = 2, Nazwa = "Boczek", Ilosc = "200g", PrzepisId = 1 },
                new Skladnik { Id = 3, Nazwa = "Jajka", Ilosc = "4 sztuki", PrzepisId = 1 },
                new Skladnik { Id = 4, Nazwa = "Parmezan", Ilosc = "100g", PrzepisId = 1 },

                // Rosół z kurczaka
                new Skladnik { Id = 5, Nazwa = "Kurczak", Ilosc = "1 cały", PrzepisId = 2 },
                new Skladnik { Id = 6, Nazwa = "Marchewka", Ilosc = "3 sztuki", PrzepisId = 2 },
                new Skladnik { Id = 7, Nazwa = "Pietruszka", Ilosc = "2 sztuki", PrzepisId = 2 },
                new Skladnik { Id = 8, Nazwa = "Cebula", Ilosc = "1 sztuka", PrzepisId = 2 },

                // Naleśniki
                new Skladnik { Id = 9, Nazwa = "Mąka", Ilosc = "2 szklanki", PrzepisId = 3 },
                new Skladnik { Id = 10, Nazwa = "Mleko", Ilosc = "500ml", PrzepisId = 3 },
                new Skladnik { Id = 11, Nazwa = "Jajka", Ilosc = "3 sztuki", PrzepisId = 3 },
                new Skladnik { Id = 12, Nazwa = "Sól", Ilosc = "szczypta", PrzepisId = 3 },

                // Kotlet schabowy
                new Skladnik { Id = 13, Nazwa = "Schab", Ilosc = "800g", PrzepisId = 4 },
                new Skladnik { Id = 14, Nazwa = "Mąka", Ilosc = "100g", PrzepisId = 4 },
                new Skladnik { Id = 15, Nazwa = "Jajka", Ilosc = "2 sztuki", PrzepisId = 4 },
                new Skladnik { Id = 16, Nazwa = "Bułka tarta", Ilosc = "200g", PrzepisId = 4 },

                // Sernik na zimno
                new Skladnik { Id = 17, Nazwa = "Herbatniki", Ilosc = "200g", PrzepisId = 5 },
                new Skladnik { Id = 18, Nazwa = "Ser twarogowy", Ilosc = "1kg", PrzepisId = 5 },
                new Skladnik { Id = 19, Nazwa = "Cukier puder", Ilosc = "200g", PrzepisId = 5 },
                new Skladnik { Id = 20, Nazwa = "Śmietana 30%", Ilosc = "400ml", PrzepisId = 5 }
            );
        }
    }
}
