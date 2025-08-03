using ConsoleCookbook.Data;
using ConsoleCookbook.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleCookbook.Services
{
    public class PrzepisService
    {
        private readonly CookbookContext _context;

        public PrzepisService(CookbookContext context)
        {
            _context = context;
        }

        public async Task<List<Przepis>> GetAllPrzepisyAsync()
        {
            return await _context.Przepisy
                .Include(p => p.Skladniki)
                .OrderBy(p => p.Nazwa)
                .ToListAsync();
        }

        public async Task<Przepis?> GetPrzepisAsync(int id)
        {
            return await _context.Przepisy
                .Include(p => p.Skladniki)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Przepis> AddPrzepisAsync(string nazwa, string instrukcje, int czasPrzygotowania, int liczbaOsob, List<(string nazwa, string ilosc)> skladniki)
        {
            var przepis = new Przepis
            {
                Nazwa = nazwa,
                Instrukcje = instrukcje,
                CzasPrzygotowania = czasPrzygotowania,
                LiczbaOsob = liczbaOsob,
                DataDodania = DateTime.Now
            };

            foreach (var (nazwaSkladnika, ilosc) in skladniki)
            {
                przepis.Skladniki.Add(new Skladnik
                {
                    Nazwa = nazwaSkladnika,
                    Ilosc = ilosc
                });
            }

            _context.Przepisy.Add(przepis);
            await _context.SaveChangesAsync();
            return przepis;
        }

        public async Task<bool> DeletePrzepisAsync(int id)
        {
            var przepis = await _context.Przepisy.FindAsync(id);
            if (przepis == null)
                return false;

            _context.Przepisy.Remove(przepis);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Przepis>> SearchPrzepisyAsync(string searchTerm)
        {
            return await _context.Przepisy
                .Include(p => p.Skladniki)
                .Where(p => p.Nazwa.Contains(searchTerm) || 
                           p.Skladniki.Any(s => s.Nazwa.Contains(searchTerm)))
                .OrderBy(p => p.Nazwa)
                .ToListAsync();
        }
    }
}
