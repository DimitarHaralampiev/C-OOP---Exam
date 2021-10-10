using AquaShop.Repositories.Contracts;
using System.Collections.Generic;
using AquaShop.Models.Decorations.Contracts;
using System.Linq;

namespace AquaShop.Models
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private IList<IDecoration> decorations;

        public DecorationRepository()
        {
            this.decorations = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models { get; }

        public void Add(IDecoration model) => this.decorations.Add(model);

        public IDecoration FindByType(string type) => this.decorations.FirstOrDefault(x => x.GetType().Name == type);

        public bool Remove(IDecoration model) => this.decorations.Remove(model);
    }
}
