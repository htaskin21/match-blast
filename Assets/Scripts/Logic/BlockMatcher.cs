using System.Collections.Generic;
using Blocks;
using Managers;
using UnityEngine;

namespace Logic
{
    public class BlockMatcher
    {
        private readonly Vector2Int[] _directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        public List<MatchableBlock> FindConnectedBlocks(MatchableBlock startBlock, GridManager gridManager)
        {
            var visited = new HashSet<Vector2Int>();
            var toVisit = new Stack<Vector2Int>();
            var connectedBlocks = new List<MatchableBlock>();

            var targetColor = startBlock.ColorType;
            var startPos = startBlock.Position;

          
            toVisit.Push(startBlock.Position);

            while (toVisit.Count > 0)
            {
                var currentPos = toVisit.Pop();
                if (!gridManager.CheckBounds(currentPos)) continue;

                if (visited.Contains(currentPos)) continue;

                var block = gridManager.GetItemAt(currentPos) as MatchableBlock;
                if (block == null || block.ColorType != targetColor) continue;

                visited.Add(currentPos);
                connectedBlocks.Add(block);

                foreach (var dir in _directions)
                    toVisit.Push(currentPos + dir);
            }

            return connectedBlocks;
        }
    }
}
