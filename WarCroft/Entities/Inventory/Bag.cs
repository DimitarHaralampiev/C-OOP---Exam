using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        private List<Item> bag;

        private int capacity;

        public Bag(int capacity)
        {
            this.Capacity = capacity;
            this.bag = new List<Item>();
            this.Items = new ReadOnlyCollection<Item>(bag);
        }

        public int Capacity
        {
            get => this.capacity;
            set
            {
                this.capacity = 100;
            }
        }

        public int Load { get => this.Items.Sum(x => x.Weight); }

        public IReadOnlyCollection<Item> Items { get; }

        public void AddItem(Item item)
        {
            if ((this.Load + item.Weight) > this.Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }

            this.bag.Add(item);
        }

        public Item GetItem(string name)
        {
            if (this.Items.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            if (!this.Items.Any(x => x.Name == name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ItemNotFoundInBag, name));
            }

            Item itemRes = this.Items.FirstOrDefault(x => x.GetType().Name == name);
            this.bag.Remove(itemRes);

            return itemRes;
        }
    }
}
