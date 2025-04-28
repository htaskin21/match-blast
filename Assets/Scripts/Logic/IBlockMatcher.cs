using System.Collections.Generic;
using Blocks;
using Cores;
using UnityEngine;

namespace Logic
{
    public interface IBlockMatcher
    {
        Dictionary<Vector2Int, List<MatchableBlock>> FindAllMatches(GridSystem<Block> gridSystem);
    }
}