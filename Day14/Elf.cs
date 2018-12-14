using System.Collections.Generic;

namespace Day14
{
    public class Elf
    {
        public LinkedListNode<int> Position { get; set; }
        public int CurrentRecipe { get; set; }

        public Elf(LinkedListNode<int> position, int currentRecipe)
        {
            Position = position;
            CurrentRecipe = currentRecipe;
        }
    }
}