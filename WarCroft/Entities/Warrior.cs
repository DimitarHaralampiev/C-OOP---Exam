using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities
{
    public class Warrior : Character, IAttacker
    {
        public Warrior(string name)
            : base(name, 100, 50, 40, null)
        {
        }

        public void Attack(Character character)
        {
            this.EnsureAlive();

            if (character.Name == this.Name)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterAttacksSelf);
            }

            this.Armor -= this.AbilityPoints;

            if (this.Armor == 0)
            {
                this.Health -= this.AbilityPoints;
            }
        }
    }
}
