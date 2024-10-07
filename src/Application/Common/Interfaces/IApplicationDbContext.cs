using SampleProject.Domain.Entities;

namespace SampleProject.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Property> Properties { get; }
    DbSet<Amenity> Amenities { get; }
    DbSet<ExternalFeature> ExternalFeatures { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
