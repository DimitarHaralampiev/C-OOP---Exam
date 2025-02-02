﻿using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    public class Priest : Character, IHealer
    {
        private IBag bag;

        public Priest(string name)
            : base(name, 50, 25, 40, null)
        {
            this.bag = new Backpack();
        }

        public void Heal(Character character)
        {
            this.EnsureAlive();

            this.Health += this.AbilityPoints;
        }
    }
}
