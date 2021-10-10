namespace Aquariums.Tests
{
    using System;
    using NUnit.Framework;

    public class AquariumsTests
    {

        [Test]
        public void CtorInitializeCorrectly()
        {
            Aquarium aquarium = new Aquarium("Has", 4);

            string name = aquarium.Name;
            int cap = aquarium.Capacity;

            Assert.AreEqual(aquarium.Name, name);
            Assert.AreEqual(aquarium.Capacity, cap);
        }

        [Test]
        public void TestName()
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(string.Empty, 1));
            Assert.Throws<ArgumentNullException>(() => new Aquarium(null, 1));
        }

        [Test]
        public void TestCapacity()
        {
            //Assert.Throws<ArgumentException>(() => new Aquarium("Ivan", 0));
            Assert.Throws<ArgumentException>(() => new Aquarium("Pesho", -3));
        }

        [Test]
        public void TestCount()
        {
            Aquarium aquarium = new Aquarium("sa", 1);
            aquarium.Add(new Fish("ad"));

            int count = 1;

            Assert.AreEqual(count, aquarium.Count);
        }

        [Test]
        public void TestAddPositive()
        {
            Aquarium aquarium = new Aquarium("ivan", 0);
            Assert.Throws<InvalidOperationException>(() => aquarium.Add(new Fish("sd")));
        }

        [Test]
        public void RemoveThrowException()
        {
            Aquarium aquarium = new Aquarium("s", 1);
            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish(null));
        }

        [Test]
        public void TestRemoveFish()
        {
            Aquarium aquarium = new Aquarium("d", 4);
            aquarium.Add(new Fish("s"));
            aquarium.Add(new Fish("45"));

            aquarium.RemoveFish("s");

            Assert.AreEqual(aquarium.Count, 1);
        }

        [Test]
        public void TestSellThrowFish()
        {
            Aquarium aquarium = new Aquarium("sa", 2); 
            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish(null));
        }

        [Test]
        public void TestSellFish()
        {
            Aquarium aquarium = new Aquarium("sa", 2);
            aquarium.Add(new Fish("sa"));

            Fish fish = aquarium.SellFish("sa");
            Assert.AreEqual(fish.Name, "sa");
            Assert.AreEqual(fish.Available, false);
        }

        [Test]
        public void TestReport()
        {
            Aquarium aquarium = new Aquarium("a", 1);
            aquarium.Add(new Fish("sa"));

            string message = $"Fish available at a: sa";
            Assert.AreEqual(message, aquarium.Report());
        }
    }
}
