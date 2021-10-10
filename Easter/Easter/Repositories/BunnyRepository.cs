using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Easter.Models.Bunnies.Contracts
{
    public class BunnyRepository : IRepository<IBunny>
    {

        private readonly IList<IBunny> bunnies;
        private IReadOnlyCollection<IBunny> models;

        public BunnyRepository()
        {
            this.bunnies = new List<IBunny>();
        }

        public IReadOnlyCollection<IBunny> Models { get; }

        public void Add(IBunny bunny)
        {
            this.models = new ReadOnlyCollection<IBunny>(bunnies);

            bunnies.Add(bunny);
        }

        public bool Remove(IBunny bunny)
        {
            if (bunnies.Any(b => b.Name == bunny.Name))
            {
                bunnies.Remove(bunny);
                return true;
            }

            return false;
        }

        public IBunny FindByName(string name)
        {
            if (bunnies.Any(b => b.Name == name))
            {
                return bunnies.FirstOrDefault(b => b.Name == name);
            }

            return null;
        }
    }
}
