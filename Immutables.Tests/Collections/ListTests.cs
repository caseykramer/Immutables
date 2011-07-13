using System;
using System.Linq;
using NUnit.Framework;
using Immutables.Collections;

namespace Immutables.Tests.Collections
{
    [TestFixture]
    public class ListTests
    {
        [Test]
        public void Can_Create_A_List_Of_Ints_And_Retrieve_Items_By_Index()
        {
            var ints = new List<int>(1, 2, 3, 4);
            Assert.That(ints[0], Is.EqualTo(1));
            Assert.That(ints[1], Is.EqualTo(2));
            Assert.That(ints[2], Is.EqualTo(3));
            Assert.That(ints[3], Is.EqualTo(4));
        }

        [Test]
        public void Can_Get_The_Length_Of_The_List()
        {
            var ints = new List<int>(1, 2, 3, 4, 5);
            Assert.That(ints.Length, Is.EqualTo(5));
        }

        [Test]
        public void Head_Of_The_List_Returns_The_First_Item()
        {
            var ints = new List<int>(1, 2, 3, 4, 5);
            Assert.That(ints.Head, Is.EqualTo(1));
        }

        [Test]
        public void Tail_Returns_All_Items_Except_For_The_Head()
        {
            var ints = new List<int>(1, 2, 3, 4, 5, 6);
            var tail = ints.Tail;
            Assert.That(tail.Length, Is.EqualTo(ints.Length - 1));
            Assert.That(tail[0], Is.EqualTo(2));
            Assert.That(tail[1], Is.EqualTo(3));
            Assert.That(tail[2], Is.EqualTo(4));
            Assert.That(tail[3], Is.EqualTo(5));
            Assert.That(tail[4], Is.EqualTo(6));
        }

        [Test]
        public void Can_Crete_A_New_List_Using_plus()
        {
            var ints1 = new List<int>(1, 2, 3, 4);
            var ints2 = new List<int>(5, 6);
            var ints3 = ints1 + ints2;

            Assert.That(ints3.Length, Is.EqualTo(6));
            Assert.That(ints3[0], Is.EqualTo(1));
            Assert.That(ints3[1], Is.EqualTo(2));
            Assert.That(ints3[2], Is.EqualTo(3));
            Assert.That(ints3[3], Is.EqualTo(4));
            Assert.That(ints3[4], Is.EqualTo(5));
            Assert.That(ints3[5], Is.EqualTo(6));
        }

        [Test]
        public void Can_Create_A_New_List_From_An_Existing_List_And_A_Single_Item_Using_plus()
        {
            var ints1 = new List<int>(1, 2, 3, 4);
            var ints2 = ints1 + 5;
            Assert.That(ints2.Length, Is.EqualTo(5));
            Assert.That(ints2[0], Is.EqualTo(1));
            Assert.That(ints2[1], Is.EqualTo(2));
            Assert.That(ints2[2], Is.EqualTo(3));
            Assert.That(ints2[3], Is.EqualTo(4));
            Assert.That(ints2[4], Is.EqualTo(5));

            var ints3 = ints1 + ints2.Head;
            Assert.That(ints3.Length, Is.EqualTo(5));
            Assert.That(ints3[0], Is.EqualTo(1));
            Assert.That(ints3[1], Is.EqualTo(2));
            Assert.That(ints3[2], Is.EqualTo(3));
            Assert.That(ints3[3], Is.EqualTo(4));
            Assert.That(ints3[4], Is.EqualTo(1));
        }

        [Test]
        public void Can_Sum_A_List_Of_Ints_Using_FoldLeft()
        {
            var ints = new List<int>(1, 2, 3, 4);
            var result = ints.FoldLeft(0, (acc, i) => acc + i);
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void Can_Sum_A_List_Of_Ints_Using_ReduceLeft()
        {
            var ints = new List<int>(1, 2, 3, 4);
            var result = ints.ReduceLeft((acc, i) => acc + i);
            Assert.That(result, Is.EqualTo(10));
        }
    }
}
