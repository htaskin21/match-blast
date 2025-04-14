using System.Collections.Generic;
using Blocks;
using Cores;
using UnityEngine;

namespace Logic
{
    public class BlockMatcher : IBlockMatcher
    {
        private readonly Vector2Int[] _directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        private readonly int _minMatchCount;

        public BlockMatcher(int minMatchCount = 2)
        {
            _minMatchCount = minMatchCount;
        }

        public List<MatchableBlock> FindConnectedBlocks(MatchableBlock startBlock, GridSystem<Block> gridSystem)
        {
            var visited = new HashSet<Vector2Int>();
            var toVisit = new Stack<Vector2Int>();
            var connectedBlocks = new List<MatchableBlock>();

            var targetColor = startBlock.ColorType;

            toVisit.Push(startBlock.Position);

            while (toVisit.Count > 0)
            {
                var currentPos = toVisit.Pop();
                if (!gridSystem.CheckBounds(currentPos)) continue;

                if (visited.Contains(currentPos)) continue;

                var block = gridSystem.GetItemAt(currentPos) as MatchableBlock;
                if (block == null || block.ColorType != targetColor) continue;

                visited.Add(currentPos);
                connectedBlocks.Add(block);

                foreach (var dir in _directions)
                    toVisit.Push(currentPos + dir);
            }

            return connectedBlocks;
        }

        public Dictionary<Vector2Int, List<MatchableBlock>> GenerateMatchCache(GridSystem<Block> gridSystem)
        {
            var matchCache = new Dictionary<Vector2Int, List<MatchableBlock>>();
            var visited = new HashSet<Vector2Int>();

            for (var y = 0; y < gridSystem.GridSize.y; y++)
            {
                for (var x = 0; x < gridSystem.GridSize.x; x++)
                {
                    Vector2Int pos = new(x, y);
                    if (!gridSystem.CheckBounds(pos) || visited.Contains(pos) || gridSystem.IsEmpty(pos))
                        continue;

                    if (gridSystem.GetItemAt(pos) is not MatchableBlock startBlock)
                        continue;

                    var group = FindConnectedBlocks(startBlock, gridSystem);

                    if (group.Count < _minMatchCount)
                        continue;

                    foreach (var block in group)
                    {
                        if (visited.Contains(block.Position)) continue;
                        matchCache[block.Position] = group;
                        visited.Add(block.Position);
                    }
                }
            }

            return matchCache;
        }
    }
}