using BlogSite.Server.Data;
using BlogSite.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        
        private IGenericRepository<Post> _posts;       

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public IGenericRepository<Post> Posts
            => _posts ??= new GenericRepository<Post>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            var user = httpContext.User.Identity.Name;
            if (user == null)
            {
                user = "unknown";
            }
            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                            q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseModel)entry.Entity).DateUpdated = DateTime.Now;                
                if (entry.State == EntityState.Added)
                {
                    ((BaseModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseModel)entry.Entity).Owner = user;
                    //((BaseModel)entry.Entity).Owner = "stringfromcode";
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
