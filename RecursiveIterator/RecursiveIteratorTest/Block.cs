using System.Collections.Generic;

namespace RecursiveIteratorTest
{
    public class Block
    {
        public Block(int id)
        {
            Id = id;
            this.Children = new List<Block>();
        }
        public Block Parent { get; set; }
        public int Id { get; set; }
        public List<Block> Children { get; set; }

        public Block Add(int id)
        {
            var b = new Block(id);
            b.Parent = this;
            this.Children.Add(b);
            return b;
        }
    }
}
