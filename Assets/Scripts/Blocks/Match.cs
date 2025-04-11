using System.Collections.Generic;

namespace Blocks
{
    public class Match
    {
        public List<MatchableBlock> MatchedBlocks { get; }

        public int Count => MatchedBlocks.Count;

        public Match()
        {
            MatchedBlocks = new List<MatchableBlock>();
        }

        public Match(MatchableBlock matchableBlock) : this()
        {
            AddToMatchedBlocks(matchableBlock);
        }

        public void AddToMatchedBlocks(MatchableBlock matchableBlock)
        {
            MatchedBlocks.Add(matchableBlock);
        }
        
        public override string ToString()
        {
            string s = "Match of type " + MatchedBlocks[0].ColorType + " : ";

            foreach(MatchableBlock m in MatchedBlocks)
                s += "(" + m.Position.x + ", " + m.Position.y + ") ";

            return s;
        }
    }
}
