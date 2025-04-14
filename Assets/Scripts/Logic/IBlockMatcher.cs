using System.Collections.Generic;
using Blocks;
using Cores;
using UnityEngine;

namespace Logic
{
    public interface IBlockMatcher
    {
        List<MatchableBlock> FindConnectedBlocks(MatchableBlock startBlock, GridSystem<Block> gridSystem);

        Dictionary<Vector2Int, List<MatchableBlock>> GenerateMatchCache(GridSystem<Block> gridSystem);
    }
}