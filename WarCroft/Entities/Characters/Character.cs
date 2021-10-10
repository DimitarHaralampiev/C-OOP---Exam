using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
		private readonly double abilityPoints;

		private readonly double baseHealth;

		private readonly double baseArmor;

		private double health;

		private double armor;

		private string name;

		private Bag bag;

		public Character(string name, double health, double armor, double abilityPoints, Bag bag)
		{
			this.Name = name;
			this.Health = health;
			this.Armor = armor;
			this.AbilityPoints = abilityPoints;
			this.Bag = bag;
		}

		public double BaseHealth { get; protected set; }

		public double BaseArmor { get; protected set; }

		public double Armor 
		{
			get => this.armor;
			protected set
			{
				if (value < 0)
				{
					this.armor = 0;
				}

				this.armor = value;
			}
		}

		public double AbilityPoints { get; }

		public Bag Bag { get; }

		public string Name

		{
			get => this.name;
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
				}

				this.name = value;
			}
		}

		public double Health 
		{
			get => this.health;
			set
			{
				if (value > this.baseHealth)
				{
					this.health = this.baseHealth;
				}
				else if(value < 0)
				{
					this.health = 0;
				}

				this.health = value;
			}
		}

		public bool IsAlive { get; set; } = true;

		protected void EnsureAlive()
		{
			if (!this.IsAlive)
			{
				throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
			}
		}

		public void TakeDamage(double hitPoints)
		{
			this.EnsureAlive();
			double healthReduce = hitPoints - this.Armor;
			this.Armor -= hitPoints;

			if (healthReduce > 0)
			{
				this.Health -= healthReduce;
			}

			if (this.Health == 0)
			{
				this.IsAlive = false;
			}
		}

		public void UseItem(Item item)
		{
			this.EnsureAlive();

			item.AffectCharacter(this);
		}
	}
}