using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        private ICollection<IBakedFood> foodOrder;

        private ICollection<IDrink> drinkOrder;

        private int capacity;

        private int numberOfPeople;

        public Table(decimal pricePerPerson, int capacity, int tableNumber)
        {
            this.PricePerPerson = pricePerPerson;
            this.Capacity = capacity;
            this.TableNumber = tableNumber;
            this.foodOrder = new List<IBakedFood>();
            this.drinkOrder = new List<IDrink>();
        }

        public int TableNumber { get; private set; }

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }

                this.capacity = value;
            }
        }

        public int NumberOfPeople
        {
            get => this.numberOfPeople;
            private set
            {
                if (this.Capacity < value)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }

                this.numberOfPeople = value;
            }
        }

        public decimal PricePerPerson { get; private set; }

        public bool IsReserved { get; private set; }

        public decimal Price 
        {
            get { return this.PricePerPerson * this.NumberOfPeople; }
        }

        public void Clear()
        {
            this.foodOrder.Clear();
            this.drinkOrder.Clear();

            this.NumberOfPeople = 0;

            this.IsReserved = false;
        }

        public decimal GetBill()
        {
            decimal sumDrink = this.drinkOrder.Sum( x => x.Price);
            decimal sumFood = this.foodOrder.Sum(x => x.Price);

            return sumDrink + sumFood;
        }

        public string GetFreeTableInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Table: {this.TableNumber}");
            sb.AppendLine($"Type: {this.GetType().Name}");
            sb.AppendLine($"Capacity: {this.Capacity}");
            sb.AppendLine($"Price per Person: {this.PricePerPerson}");

            return sb.ToString().TrimEnd();
        }

        public void OrderDrink(IDrink drink)
        {
            this.drinkOrder.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            this.foodOrder.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            this.NumberOfPeople = numberOfPeople;
            this.IsReserved = true;
        }
    }
}
