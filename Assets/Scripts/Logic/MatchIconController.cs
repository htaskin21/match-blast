using System.Collections.Generic;
using Blocks;
using Cores;
using UnityEngine;

namespace Logic
{
    // Controls block icons based on the size of the matched group.
    public class MatchIconController
    {
        private readonly int _smallCondition;
        private readonly int _mediumCondition;
        private readonly int _bigCondition;

        public MatchIconController(int smallCondition, int mediumCondition, int bigCondition)
        {
            _smallCondition = smallCondition;
            _mediumCondition = mediumCondition;
            _bigCondition = bigCondition;
        }

        /// <summary>
        /// Updates the icons of the blocks based on match group sizes.
        /// </summary>
        public void ChangeIcons(Dictionary<Vector2Int, List<MatchableBlock>> matchCache, GridSystem<Block> grid)
        {
            var processedBlocks = new HashSet<MatchableBlock>();
            var matchedBlocks = new HashSet<MatchableBlock>();

            foreach (var group in matchCache.Values)
            {
                if (group.Count == 0 || processedBlocks.Contains(group[0]))
                    continue;

                var uniqueGroup = new HashSet<MatchableBlock>(group);
                var count = uniqueGroup.Count;

                var iconIndex = GetIconIndexForMatchCount(count);

                foreach (var block in uniqueGroup)
                {
                    processedBlocks.Add(block);
                    matchedBlocks.Add(block);
                    block.IconController.ChangeIcon(iconIndex);
                }
            }

            for (var x = 0; x < grid.GridSize.x; x++)
            {
                for (var y = 0; y < grid.GridSize.y; y++)
                {
                    if (grid.GetItemAt(x, y) is MatchableBlock block && !matchedBlocks.Contains(block))
                    {
                        block.IconController.ChangeIcon(0);
                    }
                }
            }
        }

        /// <summary>
        /// Determines which icon index to use based on match count.
        /// </summary>
        private int GetIconIndexForMatchCount(int count)
        {
            if (count >= _smallCondition && count < _mediumCondition)
            {
                return 1;
            }

            if (count >= _mediumCondition && count < _bigCondition)
            {
                return 2;
            }

            return count >= _bigCondition ? 3 : 0;
        }
    }
}