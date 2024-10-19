namespace AuthExampleClient.Services.Interfaces
{
    public interface IReadService<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(string endpoint,string objectName);
        Task<TEntity> GetAsync(string endpoint,string id);

    }
}
