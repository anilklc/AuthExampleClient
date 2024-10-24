using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.Services.Interfaces
{
    public interface IWriteService<TCreate,TUpdate>
    {
        Task<HttpResponseMessage> CreateAsync(string endpoint,TCreate entity);
        Task<bool> DeleteAsync(string endpoint,string id);
        Task<HttpResponseMessage> UpdateAsync(string endpoint,TUpdate entity);
    }
}
