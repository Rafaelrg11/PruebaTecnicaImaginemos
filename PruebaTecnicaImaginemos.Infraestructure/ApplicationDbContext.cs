using MediatR;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaImaginemos.Application.Exceptions;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.Sale;
using PruebaTecnicaImaginemos.Domain.sale_detail;
using PruebaTecnicaImaginemos.Domain.Users;
using System.Data;

namespace PruebaTecnicaImaginemos.Infraestructure;

public sealed class ApplicationDbContext : DbContext , IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Products> Product { get; set; }
    public DbSet<Sales> Sale { get; set; }
    public DbSet<DetailSale> SaleDateal { get; set; }
    public DbSet<UserE> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEvetnsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception ocurred", ex);
        }
    }

    public void Add(Entity person)
    {
        base.Add(person);
    }

    private async Task PublishDomainEvetnsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvent();

                return domainEvents;
            });

        foreach (var item in domainEvents)

            await _publisher.Publish(item);
    }
}
