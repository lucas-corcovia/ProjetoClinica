using Microsoft.EntityFrameworkCore;

namespace ProjetoClinica.App.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
