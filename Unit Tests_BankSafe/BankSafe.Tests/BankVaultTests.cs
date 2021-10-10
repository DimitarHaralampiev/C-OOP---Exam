using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private BankVault bankFault;

        [SetUp]
        public void Setup()
        {
            this.bankFault = new BankVault();
        }

        [Test]
        public void CtorInitializesCollectionOfItems()
        {
            Assert.That(bankFault.VaultCells, Is.Not.Null);
        }

        [Test]
        public void  CountDictionaryTwelve()
        {
            Assert.AreEqual(bankFault.VaultCells.Count, 12);
        }

        [Test]
        public void AddItemThrowCell()
        {
            Item item = new Item("Ivan", "1");

            Assert.Throws<ArgumentException>(() => bankFault.AddItem("?5", item));
        }

        [Test]
        public void ThrowExeptionWhenCellIsAlreadyTaken()
        {
            Item itemOne = new Item("Pesho", "1");
            Item itemTwo = new Item("Gosho", "2");

            bankFault.AddItem("A1", itemOne);

            Assert.Throws<ArgumentException>(() => bankFault.AddItem("A1", itemTwo));
        }

        [Test]
        public void ThrowExeptionWhenItemAlreadyExist()
        {
            Item item = new Item("Pesho", "1");

            bankFault.AddItem("A1", item);

            Item newItem = new Item("Gosho", "1");

            Assert.Throws<InvalidOperationException>(() => bankFault.AddItem("A2", newItem));
        }

        [Test]
        public void AddItemMethodReturnProperValue()
        {
            Item item = new Item("Pesho", "1");

            Assert.AreEqual(bankFault.AddItem("A1", item), "Item:1 saved successfully!");
        }

        [Test]
        public void ThrowExeptionWheItemToRemoveDoesntExist()
        {
            Item item = new Item("Pesho", "1");

            Assert.Throws<ArgumentException>(() => bankFault.RemoveItem("A7", item));
        }

        [Test]
        public void RemoveMethodRemovesItem()
        {
            Item item = new Item("Pesho", "1");

            bankFault.AddItem("A1", item);

            Item item1 = new Item("Gosho", "1");

            Assert.Throws<ArgumentException>(() => bankFault.RemoveItem("A2", item1));
        }

    }
}