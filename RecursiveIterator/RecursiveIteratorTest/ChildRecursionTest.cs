using RecursiveIterator;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Shouldly;

namespace RecursiveIteratorTest
{
    public class ChildRecursionTest
    {
        [Fact]
        public void CanIterateChildren()
        {
            var b1 = new Block(1);
            var b11 = b1.Add(11);
            var b12 = b1.Add(12);
            var b21 = b11.Add(111);

            var enumerable = new RecursiveList<Block>(b1, b => b.Children).Select(b => b.Id).ToList();

            enumerable.ShouldBe(new List<int>() { 1, 11, 12, 111 });
        }

        [Fact]
        public void CanIterateChildren2()
        {
            var b1 = new Block(1);
            var b11 = b1.Add(11);
            var b12 = b1.Add(12);
            var b21 = b11.Add(111);

            var enumerable = new RecursiveList<Block>(b11, b => b.Children).Select(b => b.Id).ToList();

            enumerable.ShouldBe(new List<int>() { 11, 111 });
        }

    }
}
