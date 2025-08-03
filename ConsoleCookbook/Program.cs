using ConsoleCookbook.Data;
using ConsoleCookbook.Services;
using Microsoft.EntityFrameworkCore;

namespace ConsoleCookbook
{
    class Program
    {
        private static PrzepisService _przepisService = null!;

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== KSIĄŻKA KUCHARSKA ===");
            Console.WriteLine();

            // Inicjalizacja bazy danych
            await InitializeDatabaseAsync();

            // Główna pętla aplikacji
            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await ShowAllPrzepisy();
                            break;
                        case "2":
                            await ShowPrzepisDetails();
                            break;
                        case "3":
                            await AddNewPrzepis();
                            break;
                        case "4":
                            await SearchPrzepisy();
                            break;
                        case "5":
                            await DeletePrzepis();
                            break;
                        case "0":
                            Console.WriteLine("Do widzenia!");
                            return;
                        default:
                            Console.WriteLine("Nieprawidłowy wybór!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                }

                Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static async Task InitializeDatabaseAsync()
        {
            using var context = new CookbookContext();
            
            try
            {
                Console.WriteLine("Inicjalizacja bazy danych...");
                await context.Database.EnsureCreatedAsync();
                _przepisService = new PrzepisService(context);
                Console.WriteLine("Baza danych została zainicjalizowana.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas inicjalizacji bazy danych: {ex.Message}");
                Console.WriteLine("Upewnij się, że MySQL jest uruchomiony i dostępny na localhost.");
                Environment.Exit(1);
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\n=== MENU GŁÓWNE ===");
            Console.WriteLine("1. Pokaż wszystkie przepisy");
            Console.WriteLine("2. Pokaż szczegóły przepisu");
            Console.WriteLine("3. Dodaj nowy przepis");
            Console.WriteLine("4. Szukaj przepisów");
            Console.WriteLine("5. Usuń przepis");
            Console.WriteLine("0. Wyjście");
            Console.Write("\nWybierz opcję: ");
        }

        private static async Task ShowAllPrzepisy()
        {
            using var context = new CookbookContext();
            var service = new PrzepisService(context);
            var przepisy = await service.GetAllPrzepisyAsync();

            Console.WriteLine("\n=== WSZYSTKIE PRZEPISY ===");
            if (!przepisy.Any())
            {
                Console.WriteLine("Brak przepisów w bazie danych.");
                return;
            }

            foreach (var przepis in przepisy)
            {
                Console.WriteLine($"{przepis.Id}. {przepis.Nazwa} (czas: {przepis.CzasPrzygotowania} min, dla {przepis.LiczbaOsob} osób)");
            }
        }

        private static async Task ShowPrzepisDetails()
        {
            Console.Write("Podaj ID przepisu: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Nieprawidłowe ID!");
                return;
            }

            using var context = new CookbookContext();
            var service = new PrzepisService(context);
            var przepis = await service.GetPrzepisAsync(id);

            if (przepis == null)
            {
                Console.WriteLine("Nie znaleziono przepisu o podanym ID.");
                return;
            }

            Console.WriteLine($"\n=== {przepis.Nazwa.ToUpper()} ===");
            Console.WriteLine($"Czas przygotowania: {przepis.CzasPrzygotowania} minut");
            Console.WriteLine($"Liczba osób: {przepis.LiczbaOsob}");
            Console.WriteLine($"Data dodania: {przepis.DataDodania:dd.MM.yyyy HH:mm}");

            Console.WriteLine("\nSKŁADNIKI:");
            foreach (var skladnik in przepis.Skladniki)
            {
                Console.WriteLine($"- {skladnik.Nazwa}: {skladnik.Ilosc}");
            }

            Console.WriteLine("\nINSTRUKCJE:");
            Console.WriteLine(przepis.Instrukcje);
        }

        private static async Task AddNewPrzepis()
        {
            Console.WriteLine("\n=== DODAWANIE NOWEGO PRZEPISU ===");

            Console.Write("Nazwa przepisu: ");
            var nazwa = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nazwa))
            {
                Console.WriteLine("Nazwa nie może być pusta!");
                return;
            }

            Console.Write("Czas przygotowania (w minutach): ");
            if (!int.TryParse(Console.ReadLine(), out int czas))
            {
                Console.WriteLine("Nieprawidłowy czas!");
                return;
            }

            Console.Write("Liczba osób: ");
            if (!int.TryParse(Console.ReadLine(), out int osoby))
            {
                Console.WriteLine("Nieprawidłowa liczba osób!");
                return;
            }

            Console.WriteLine("Instrukcje przygotowania (zakończ pustą linią):");
            var instrukcje = "";
            string linia;
            while (!string.IsNullOrEmpty(linia = Console.ReadLine()))
            {
                instrukcje += linia + "\n";
            }

            var skladniki = new List<(string nazwa, string ilosc)>();
            Console.WriteLine("Dodawanie składników (wpisz 'koniec' aby zakończyć):");

            while (true)
            {
                Console.Write("Nazwa składnika: ");
                var nazwaSkladnika = Console.ReadLine();
                if (nazwaSkladnika?.ToLower() == "koniec")
                    break;

                if (string.IsNullOrWhiteSpace(nazwaSkladnika))
                    continue;

                Console.Write("Ilość: ");
                var ilosc = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ilosc))
                    continue;

                skladniki.Add((nazwaSkladnika, ilosc));
            }

            using var context = new CookbookContext();
            var service = new PrzepisService(context);
            var nowyPrzepis = await service.AddPrzepisAsync(nazwa, instrukcje.TrimEnd(), czas, osoby, skladniki);

            Console.WriteLine($"Przepis '{nowyPrzepis.Nazwa}' został dodany z ID: {nowyPrzepis.Id}");
        }

        private static async Task SearchPrzepisy()
        {
            Console.Write("Wpisz frazę do wyszukania: ");
            var searchTerm = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Fraza nie może być pusta!");
                return;
            }

            using var context = new CookbookContext();
            var service = new PrzepisService(context);
            var wyniki = await service.SearchPrzepisyAsync(searchTerm);

            Console.WriteLine($"\n=== WYNIKI WYSZUKIWANIA: '{searchTerm}' ===");
            if (!wyniki.Any())
            {
                Console.WriteLine("Nie znaleziono żadnych przepisów.");
                return;
            }

            foreach (var przepis in wyniki)
            {
                Console.WriteLine($"{przepis.Id}. {przepis.Nazwa} (czas: {przepis.CzasPrzygotowania} min)");
            }
        }

        private static async Task DeletePrzepis()
        {
            Console.Write("Podaj ID przepisu do usunięcia: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Nieprawidłowe ID!");
                return;
            }

            using var context = new CookbookContext();
            var service = new PrzepisService(context);
            var success = await service.DeletePrzepisAsync(id);

            if (success)
                Console.WriteLine("Przepis został usunięty.");
            else
                Console.WriteLine("Nie znaleziono przepisu o podanym ID.");
        }
    }
}
