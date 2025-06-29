using Microsoft.EntityFrameworkCore;
using projet_oc_fougasse_lover.Data;
using projet_oc_fougasse_lover.Models.Repository.Interface;

namespace projet_oc_fougasse_lover.Models.Repository
{
    public class FougasseRepository : IFougasse
    {
        private readonly ApplicationDbContext _context;

        public FougasseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Fougasses> GetIdByAsync(int id)
        {
            return await _context.Fougasses.FindAsync(id);
        }
        public async Task<List<Fougasses>> GetAllAsync()
        {
            return await _context.Fougasses.ToListAsync();
        }
        public async Task AddAsync(Fougasses fougasse)
        {
            _context.Fougasses.Add(fougasse);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Fougasses fougasse)
        {
            _context.Fougasses.Update(fougasse);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var fougasse = await GetIdByAsync(id);
            if (fougasse != null)
            {
                _context.Fougasses.Remove(fougasse);
                await _context.SaveChangesAsync();
            }
        }
    }
}
