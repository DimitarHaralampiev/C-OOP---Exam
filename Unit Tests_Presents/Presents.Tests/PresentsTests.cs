namespace Presents.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;

        [SetUp]

        public void SetUp()
        {
            bag = new Bag();
        }

        [Test]
        public void CtorInitializeCollectionOfPressent()
        {
            Assert.That(bag, Is.Not.Null);
        }

        [Test]
        public void CreateThrowsAnExceptionWhenPresetIsNull()
        {
            Present present = null;

            Assert.Throws<ArgumentNullException>(() => bag.Create(present));
        }

        [Test]
        public void CreateThrowsAnExceptionWhenPresentIsAlreadyInTheBag()
        {
            Present present = new Present("Truck", 5);

            bag.Create(present);

            Assert.Throws<InvalidOperationException>(() => bag.Create(present));
        }

        [Test]
        public void CreateAddsPresentsInTheBag()
        {
            Present present = new Present("Truck", 68);

            bag.Create(present);

            Assert.That(present, Is.EqualTo(bag.GetPresent(present.Name)));
        }

        [Test]
        public void RemoveMethodRemovesPresentsFromTheBag()
        {
            Present present = new Present("Truck", 34);

            bag.Create(present);

            bag.Remove(present);

            Assert.That(bag.GetPresent(present.Name), Is.Null);
        }

        [Test]
        public void GetPresentWithLeastMagicWorks()
        {
            Present present = new Present("Truck", 44);
            Present present1 = new Present("Buc", 5);
            Present present2 = new Present("Car", 89);

            bag.Create(present);
            bag.Create(present1);
            bag.Create(present2);

            Assert.That(present1, Is.EqualTo(bag.GetPresentWithLeastMagic()));
        }
    }
}
