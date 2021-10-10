using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private readonly IList<IEgg> eggs;
        private IReadOnlyCollection<IEgg> modelEgg;

        public EggRepository()
        {
            this.modelEgg = new List<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models { get; }

        public void Add(IEgg model)
        {
            this.modelEgg = new ReadOnlyCollection<IEgg>(eggs);

            this.eggs.Add(model);
        }

        public IEgg FindByName(string name)
        {
            if (this.eggs.Any(e => e.Name == name))
            {
                return this.eggs.FirstOrDefault(e => e.Name == name);
            }

            return null;
        }

        public bool Remove(IEgg model) => this.eggs.Remove(model);
    }
}
