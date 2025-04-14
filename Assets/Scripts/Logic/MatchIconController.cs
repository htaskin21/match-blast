using System.Collections.Generic;
using Blocks;
using UnityEngine;

namespace Logic
{
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

        public void ChangeIcons(Dictionary<Vector2Int, List<MatchableBlock>> matchCache)
        {
            var processedBlocks = new HashSet<MatchableBlock>();

            foreach (var group in matchCache.Values)
            {
                if (group.Count == 0)
                    continue;

                if (processedBlocks.Contains(group[0]))
                    continue;

                var uniqueGroup = new HashSet<MatchableBlock>(group);
                var count = uniqueGroup.Count;

                foreach (var block in uniqueGroup)
                {
                    processedBlocks.Add(block);
                }

                var iconIndex = GetIconIndexForMatchCount(count);
                foreach (var block in uniqueGroup)
                {
                    block.IconController.ChangeIcon(iconIndex);
                }
            }
        }

        private int GetIconIndexForMatchCount(int count)
        {
            if (count > _smallCondition && count <= _mediumCondition)
            {
                return 1;
            }

            if (count > _mediumCondition && count <= _bigCondition)
            {
                return 2;
            }

            return count > _bigCondition ? 3 : 0;
        }
    }
}