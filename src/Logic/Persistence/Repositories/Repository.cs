﻿using Logic.Domain;
using Logic.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Logic.Persistence.Repositories;

public abstract class Repository<T> : IDisposable where T : AggregateRoot
{
    protected readonly ApplicationDbContext Context;

    public Repository(ApplicationDbContext context) => Context = context;

    public virtual T? GetById(long id) => Context.Set<T>().Find(id);

    public int Save()
    {
        // var snacks = Context.ChangeTracker.Entries()
        //     .Where(x => x.Entity is Snack);
        //
        // foreach (var snack in snacks)
        // {
        //     if (IsReferenceData(snack))
        //         snack.State = EntityState.Unchanged;
        // }
        //
        // var snackMachines = Context.ChangeTracker.Entries()
        //     .Where(x => x.Entity is SnackMachine);
        
        // Context.ChangeTracker.DetectChanges();
        //
        var slots = Context.ChangeTracker.Entries()
            .Where(x => x.Entity is Slot);
        
        foreach (var slot in slots)
        {
            var entity = slot.Entity as Slot;
            var snackPile = entity?.SnackPile;
        
            if (snackPile is null)
                throw new InvalidOperationException();
        }
        
        return Context.SaveChanges();
    }

    private static bool IsReferenceData(EntityEntry entity)
    {
        var snack = entity.Entity as Snack;
        return snack == Snack.None || 
               snack == Snack.Chocolate || 
               snack == Snack.Soda || 
               snack == Snack.Gum;
    }

    public void Dispose() => Context.Dispose();
}