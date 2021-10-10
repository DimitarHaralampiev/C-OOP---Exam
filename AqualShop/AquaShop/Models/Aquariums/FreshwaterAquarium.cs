using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models
{
    public class FreshwaterAquarium : Aquarium
    {
        public FreshwaterAquarium(string name)
            : base(name, 50)
        {
        }
    }
}
