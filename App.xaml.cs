using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace Muse_Travel_Manager;

public partial class App : Application
{
    public static ApplicationDbContext DbContext { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=db.sqlite")
            .Options;

        DbContext = new ApplicationDbContext(options);
        //DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();

        base.OnStartup(e);
    }
}
