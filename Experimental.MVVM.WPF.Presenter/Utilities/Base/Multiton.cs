using System.Collections.Generic;

namespace Experimental.MVVM.WPF.Solver.Presenter.Utilities.Base
{
    public abstract class Multiton<Tkey, Uclass>
    {
        public Uclass? this[Tkey key]
        {
            get { return collection.GetValueOrDefault(key); }
            set { collection.TryAdd(key, value); }
        }

        public Multiton()
        {
            collection = new Dictionary<Tkey, Uclass>();
        }

        protected Dictionary<Tkey, Uclass> collection;
    }
}
