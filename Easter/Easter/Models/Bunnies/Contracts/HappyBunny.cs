﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Easter.Models.Bunnies.Contracts
{
    public class HappyBunny : Bunny
    {
        public HappyBunny(string name)
            : base(name, 100)
        {
        }
    }
}
