using RecursiveIterator;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Shouldly;

namespace RecursiveIteratorTest
{
    public class ParentRecursionTest
    {
        [Fact]
        public void CanIterateParent()
        {
            var b1 = new Block(1);
            var b11 = b1.Add(11);
            var b12 = b1.Add(12);
            var b21 = b11.Add(111);

            var enumerable = new RecursiveList<Block>(b21, b => b.Parent).Select(b => b.Id).ToList();

            enumerable.ShouldBe(new List<int>() { 111, 11, 1 });
        }

        [Fact]
        public void CanIterateParent2()
        {
            var b1 = new Block(1);
            var b11 = b1.Add(11);
            var b12 = b1.Add(12);
            var b21 = b11.Add(111);

            var enumerable = new RecursiveList<Block>(b12, b => b.Parent).Select(b => b.Id).ToList();

            enumerable.ShouldBe(new List<int>() { 12, 1 });
        }
    }
}
