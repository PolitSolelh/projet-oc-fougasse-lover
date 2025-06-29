namespace projet_oc_fougasse_lover.Models.Repository.Interface
{
    public interface IFougasse
    {
        Task<Fougasses> GetIdByAsync(int id);
        Task<List<Fougasses>> GetAllAsync();
        Task AddAsync(Fougasses fougasse);
        Task UpdateAsync(Fougasses fougasse);
        Task DeleteAsync(int id);
    }
}
