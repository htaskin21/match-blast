using System.Collections.Generic;
using Blocks;
using DG.Tweening;
using Logic;
using UnityEngine;

namespace Managers
{
    public class MatchController
    {
        private readonly GridManager _gridManager;
        private readonly IBlockMatcher _blockMatcher;
        private readonly MatchIconController _matchIconController;
        private Dictionary<Vector2Int, List<MatchableBlock>> _matchCache;

        public MatchController(GridManager gridManager, IBlockMatcher blockMatcher,
            MatchIconController matchIconController)
        {
            _gridManager = gridManager;
            _blockMatcher = blockMatcher;
            _matchIconController = matchIconController;
        }

        public void RegisterBlocks(List<MatchableBlock> blocks)
        {
            SubscribeAllBlocks(blocks);
            UpdateMatchCache();
        }

        private void SubscribeAllBlocks(List<MatchableBlock> blocks)
        {
            foreach (var block in blocks)
            {
                block.BlockClicked += CheckMatch;
            }
        }

        private void CheckMatch(Block clickedBlock)
        {
            var group = GetMatchedGroupIfAny(clickedBlock.GridPosition);
            if (group.Count >= 2)
            {
                foreach (var block in group)
                {
                    block.BlockClicked -= CheckMatch;
                    _gridManager.RemoveBlock(block);
                }

                _gridManager.RefillBoard().OnComplete(() => { RegisterBlocks(_gridManager.DroppedBlocks); });
            }
        }

        private void UpdateMatchCache()
        {
            _matchCache = _blockMatcher.FindAllMatches(_gridManager);
            _matchIconController.ChangeIcons(_matchCache, _gridManager);
        }

        private List<MatchableBlock> GetMatchedGroupIfAny(Vector2Int pos)
        {
            return _matchCache.TryGetValue(pos, out var group) ? group : new List<MatchableBlock>();
        }
    }
}