using Logic.Domain.Common;

namespace Logic.Persistence.Repositories;

public abstract class Repository<T> : IDisposable where T : AggregateRoot
{
    protected readonly ApplicationDbContext Context;

    public Repository(ApplicationDbContext context) => Context = context;

    public T? GetById(long id) => Context.Set<T>().Find(id);

    public int Save() => Context.SaveChanges();

    public void Dispose() => Context.Dispose();
}