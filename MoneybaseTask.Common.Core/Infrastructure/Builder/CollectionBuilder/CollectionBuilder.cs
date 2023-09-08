// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CollectionBuilder.cs">
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using MoneybaseTask.Common.Core.Common.ErrorObject;
using MoneybaseTask.Common.Core.Infrastructure.Persistence;

namespace MoneybaseTask.Common.Core.Infrastructure.Builder.CollectionBuilder;

public abstract class CollectionBuilder<TCollection> : ICollectionBuilder<TCollection> where TCollection : class, IBaseMongoEntity
{
    protected TCollection Collection;
    protected List<ErrorItem> Errors;

    public TCollection Build()
    {
        var collection = this.Collection;
        Reset();

        return collection;
    }

    protected abstract void Reset();
        
    protected void ResetErrors()
    {
        this.Errors = new List<ErrorItem>();
    }
        
    public List<ErrorItem> GetErrors()
    {
        var errors = this.Errors;
        ResetErrors();

        return errors;        
    }
}