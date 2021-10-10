using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories;
using Easter.Utilities.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Easter.Core
{
    public class Controller : IController
    {
        private BunnyRepository bunnyRepository;
        private EggRepository eggRepository;

        public Controller()
        {
            this.bunnyRepository = new BunnyRepository();
            this.eggRepository = new EggRepository();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            if (bunnyType == nameof(HappyBunny))
            {
                bunnyRepository.Add(new HappyBunny(bunnyName));
            }
            else if (bunnyType == nameof(SleepyBunny))
            {
                bunnyRepository.Add(new SleepyBunny(bunnyName));
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            return $"Successfully added {bunnyType} named {bunnyName}.";
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IDye dye = new Dye(power);
            IBunny bunny = new Bunny(bunnyName, power);

            if (bunny.Name == bunnyName)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }

            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);

            this.eggRepository.Add(egg);

            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            foreach (var item in bunnyRepository.Models)
            {
                if (item.Energy >= 50)
                {
                    item.Work();
                }
               
                if (item.Energy == 0)
                {
                    this.bunnyRepository.Remove(item);
                    return string.Format(OutputMessages.EggIsDone, eggName);
                }
            }

            return string.Format(OutputMessages.EggIsNotDone, eggName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{eggRepository.Models.Count(x => x.IsDone())} eggs are done!");
            sb.AppendLine("Bunnies info:");

            foreach (var item in bunnyRepository.Models)
            {
                sb.AppendLine($"Name: {item.Name}");
                sb.AppendLine($"Energy: {item.Energy}");
                sb.AppendLine($"Dyes: {item.Dyes.Count(x => !x.IsFinished())} not finished");
            }

            return sb.ToString().Trim();
        }
    }
}
