namespace AuthExampleClient.Services.Interfaces
{
    public interface IReadService<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(string endpoint);
        Task<TEntity> GetAsync(string endpoint);

    }
}
