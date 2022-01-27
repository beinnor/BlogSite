using BlogSite.Shared.Domain;

namespace BlogSite.Server.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        
        IGenericRepository<Post> Posts { get; }      
    }
}
