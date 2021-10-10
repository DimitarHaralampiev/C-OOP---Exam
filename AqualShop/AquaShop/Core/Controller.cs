using AquaShop.Core.Contracts;
using AquaShop.Models;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private DecorationRepository decorationRepository;

        private List<IAquarium> aquarium;

        public Controller()
        {
            this.decorationRepository = new DecorationRepository();
            this.aquarium = new List<IAquarium>();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType != nameof(FreshwaterAquarium) && aquariumType != nameof(SaltwaterAquarium))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }

            IAquarium aquariums = default;
            if (aquariumType == nameof(FreshwaterAquarium))
            {
                aquariums = new FreshwaterAquarium(aquariumName);
            }
            else
            {
                aquariums = new SaltwaterAquarium(aquariumName);
            }

            this.aquarium.Add(aquariums);

            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType != nameof(Ornament) && decorationType != nameof(Plant))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            IDecoration decoration;
            if (decorationType == nameof(Ornament))
            {
                decoration = new Ornament();
            }
            else
            {
                decoration = new Plant();
            }

            this.decorationRepository.Add(decoration);

            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            if (fishType != nameof(FreshwaterFish) && fishType != nameof(SaltwaterFish))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            IFish fish;
            IAquarium aquariums = this.aquarium.FirstOrDefault(x => x.Name == aquariumName);

            if (fishType != nameof(FreshwaterFish))
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);

                if (aquariums.GetType().Name == nameof(FreshwaterFish))
                {
                    return OutputMessages.UnsuitableWater;
                }
            }
            else
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);

                if (aquariums.GetType().Name == nameof(SaltwaterFish))
                {
                    return OutputMessages.UnsuitableWater;
                }
            }

            aquariums.AddFish(fish);

            return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aq = this.aquarium.FirstOrDefault(x => x.Name == aquariumName);
            decimal sumOfDecoration = aq.Decorations.Sum(p => p.Price);
            decimal sumOfFish = aq.Fish.Sum(f => f.Price);

            decimal totalSum = sumOfDecoration + sumOfFish;

            return string.Format(OutputMessages.AquariumValue, aquariumName, totalSum);
        }

        public string FeedFish(string aquariumName)
        {
            this.aquarium.FirstOrDefault(x => x.Name == aquariumName).Feed();

            return string.Format(OutputMessages.FishFed, this.aquarium.Count);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decoration = this.decorationRepository.FindByType(decorationType);

            if (decoration == null)
            {
                return string.Format(ExceptionMessages.InexistentDecoration, decorationType);
            }

            IAquarium aquariums = this.aquarium.FirstOrDefault(x => x.Name == aquariumName);
            aquariums.AddDecoration(decoration);

            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var aquariums in aquarium)
            {
                aquariums.GetInfo();
            }

            return sb
                .ToString()
                .TrimEnd();
        }
    }
}
