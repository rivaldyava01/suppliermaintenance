using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SupplierMaintenance.Domain.Entities;

namespace SupplierMaintenance.Infrastructure.Persistence.SqlServer;

public class SqlServerDbContextInitializer
{
    private readonly ILogger<SqlServerDbContextInitializer> _logger;
    private readonly SqlServerDbContext _context;

    public SqlServerDbContextInitializer(ILogger<SqlServerDbContextInitializer> logger, SqlServerDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var isMigrationNeeded = (await _context.Database.GetPendingMigrationsAsync()).Any();

            if (isMigrationNeeded)
            {
                _logger.LogInformation("Applying database migration...");

                await _context.Database.MigrateAsync();
            }
            else
            {
                _logger.LogInformation("Database is up to date. No database migration required.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");

            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");

            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        #region Seeding data Provinces

        var anyProvincesExists = await _context.Provinces.AnyAsync();

        if (!anyProvincesExists)
        {
            _logger.LogInformation("Seeding data Provinces...");


            var DkiJakarta = new Province
            {
                Id = Guid.NewGuid(),
                Name = "DKI Jakarta"
            };

            await _context.Provinces.AddAsync(DkiJakarta);

            var JawaBarat = new Province
            {
                Id = Guid.NewGuid(),
                Name = "Jawa Barat"
            };

            await _context.Provinces.AddAsync(JawaBarat);

            var JawaTengah = new Province
            {
                Id = Guid.NewGuid(),
                Name = "Jawa Tengah"
            };

            await _context.Provinces.AddAsync(JawaTengah);


        }

        #endregion Seeding data Provinces

        await _context.SaveChangesAsync();

        #region Seeding data Cities

        var anyCitiesExists = await _context.Cities.AnyAsync();
        var jakarta = _context.Provinces.Where(x => x.Name.Contains("DKI Jakarta")).SingleOrDefault();
        var jabar = _context.Provinces.Where(x => x.Name.Contains("Jawa Barat")).SingleOrDefault();
        var jateng = _context.Provinces.Where(x => x.Name.Contains("Jawa Tengah")).SingleOrDefault();

        if (!anyCitiesExists)
        {
            _logger.LogInformation("Seeding data Cities...");

          
                var jakartaBarat = new City
                {
                    Id = Guid.NewGuid(),
                    Name = "Jakarta Barat",
                    ProvinceId = jakarta.Id,
                };

                await _context.Cities.AddAsync(jakartaBarat);

            var jakartaPusat = new City
            {
                Id = Guid.NewGuid(),
                Name = "Jakarta Pusat",
                ProvinceId = jakarta.Id
            };

            await _context.Cities.AddAsync(jakartaPusat);

            var jakartaSelatan = new City
            {
                Id = Guid.NewGuid(),
                Name = "Jakarta Selatan",
                ProvinceId = jakarta.Id
            };

            await _context.Cities.AddAsync(jakartaSelatan);

            var bandung = new City
            {
                Id = Guid.NewGuid(),
                Name = "Bandung",
                ProvinceId = jabar.Id
            };

            await _context.Cities.AddAsync(bandung);

            var bekasi = new City
            {
                Id = Guid.NewGuid(),
                Name = "Bekasi",
                ProvinceId = jabar.Id
            };

            await _context.Cities.AddAsync(bekasi);

            var bogor = new City
            {
                Id = Guid.NewGuid(),
                Name = "Bogor",
                ProvinceId = jabar.Id
            };

            await _context.Cities.AddAsync(bogor);

            var semarang = new City
            {
                Id = Guid.NewGuid(),
                Name = "Semarang",
                ProvinceId = jateng.Id
            };

            await _context.Cities.AddAsync(semarang);

            var solo = new City
            {
                Id = Guid.NewGuid(),
                Name = "Solo",
                ProvinceId = jateng.Id
            };

            await _context.Cities.AddAsync(solo);
        }

        #endregion Seeding data Cities

        await _context.SaveChangesAsync();
    }
}
