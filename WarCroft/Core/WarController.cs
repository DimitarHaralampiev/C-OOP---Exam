using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		private IList<Character> characters;

		private Stack<Item> items;

		public WarController()
		{
			this.characters = new List<Character>();
			this.items = new Stack<Item>();
		}

		public string JoinParty(string[] args)
		{
			if (args[0] != nameof(Priest) && args[0] != nameof(Warrior))
			{
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, args[0]));
			}

			if (args[0] == nameof(Priest))
			{
				characters.Add(new Priest(args[1]));
			}
			else if(args[0] != nameof(Warrior))
			{
				characters.Add(new Warrior(args[1]));
			}

			return string.Format(SuccessMessages.JoinParty, args[1]);
		}

		public string AddItemToPool(string[] args)
		{
			if (args[0] != nameof(FirePotion) && args[0] != nameof(HealthPotion))
			{
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, args[1]));
			}

			if (args[0] == nameof(FirePotion))
			{
				FirePotion firePotion = new FirePotion();
				items.Push(firePotion);
			}
			else if(args[0] == nameof(HealthPotion))
			{
				HealthPotion healthPotion = new HealthPotion();
				items.Push(healthPotion);
			}

			return string.Format(SuccessMessages.AddItemToPool, args[1]);
		}

		public string PickUpItem(string[] args)
		{
			Item itemPool = items.Peek();
			IBag bag = default;

			bag.AddItem(itemPool);

			//itemPool = bag.Items.FirstOrDefault(x => x.Name == args[0]);

			if (characters.Any(x => x.Name == args[0]))
			{
				throw new ArgumentException(ExceptionMessages.CharacterNotInParty);
			}

			if (bag.Items.Count == 0)
			{
				throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
			}

			return string.Format(SuccessMessages.PickUpItem, args[0], args[1]);
		}

		public string UseItem(string[] args)
		{
			Character character = characters.FirstOrDefault(x => x.Name == args[0]);

			if (character == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, args[0]));
			}

			Item item = character.Bag.GetItem(args[1]);

			return string.Format(SuccessMessages.UsedItem, character.Name, character.GetType().Name);
		}

		public string GetStats()
		{
			StringBuilder sb = new StringBuilder();

			var character = characters
				.OrderByDescending(x => x.IsAlive)
				.ThenByDescending(x => x.Health);

			foreach (var item in character)
			{
				sb.AppendLine($"{item.Name} - HP: {item.Health}/{item.BaseHealth} AP: {item.Armor}/{item.BaseArmor} Status: {item.IsAlive}");
			}

			return sb
				.ToString()
				.TrimEnd();
		}

		public string Attack(string[] args)
		{
			Character attacker = characters.FirstOrDefault(x => x.Name == args[0]);
			Character receiver = characters.FirstOrDefault(x => x.Name == args[1]);

			if (attacker == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, args[0]));				
			}

			if (receiver == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, args[1]));
			}

			Warrior warrior = default;
			warrior.Attack(receiver);

			string output = string.Format(SuccessMessages.AttackCharacter, warrior.Name, receiver.Name, warrior.AbilityPoints,
				receiver.Name, receiver.Health, receiver.BaseHealth, receiver.Armor, receiver.BaseArmor);

			if (receiver.Health == 0)
			{
				string temp = string.Format(SuccessMessages.AttackKillsCharacter, receiver.Name);
				output = $"{output}\n{temp}";
			}

			return output;
		}

		public string Heal(string[] args)
		{
			Character heal = characters.FirstOrDefault(x => x.Name == args[0]);
			Character receive = characters.FirstOrDefault(x => x.Name == args[1]);

			if (heal == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, args[0]));
			}

			if (receive == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, args[1]));
			}

			return string.Format(SuccessMessages.HealCharacter, args[0], args[1], args[2], args[3], args[4]);
		}
	}
}
