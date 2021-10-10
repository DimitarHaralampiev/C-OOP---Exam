using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Tables
{
    public class OutsideTable : Table
    {
        private const decimal InitialPricePerPerson = 3.20m;

        public OutsideTable(int capacity, int numberPeople)
            : base(InitialPricePerPerson, capacity, numberPeople)
        {
        }
    }
}
