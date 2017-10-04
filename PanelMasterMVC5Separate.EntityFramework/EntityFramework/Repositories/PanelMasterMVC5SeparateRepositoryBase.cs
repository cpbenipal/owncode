using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace PanelMasterMVC5Separate.EntityFramework.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class PanelMasterMVC5SeparateRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<PanelMasterMVC5SeparateDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected PanelMasterMVC5SeparateRepositoryBase(IDbContextProvider<PanelMasterMVC5SeparateDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add your common methods for all repositories
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="PanelMasterMVC5SeparateRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class PanelMasterMVC5SeparateRepositoryBase<TEntity> : PanelMasterMVC5SeparateRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected PanelMasterMVC5SeparateRepositoryBase(IDbContextProvider<PanelMasterMVC5SeparateDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)!!!
    }
}
