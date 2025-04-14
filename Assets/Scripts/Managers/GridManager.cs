using System.Collections.Generic;
using Blocks;
using Cores;
using DG.Tweening;
using Logic;
using UnityEngine;

namespace Managers
{
    public class GridManager : GridSystem<Block>
    {
        [SerializeField]
        private Vector2 _offScreenOffSet;

        private MatchableBlockPool _matchableBlockPool;
        private IBlockMatcher _blockMatcher;
        private GravityController _gravityController;
        private BlockRefiller _blockRefiller;
        private MatchIconController _matchIconController;
        private Dictionary<Vector2Int, List<MatchableBlock>> _matchCache;

        public void Init(int columnSize, int rowSize, MatchableBlockPool matchableBlockPool, IBlockMatcher blockMatcher,
            GravityController gravityController, BlockRefiller blockRefiller,MatchIconController matchIconController)
        {
            GridSize = new Vector2Int(columnSize, rowSize);
            CreateGrid();
            _matchableBlockPool = matchableBlockPool;
            _blockMatcher = blockMatcher;
            _gravityController = gravityController;
            _blockRefiller = blockRefiller;
            _matchIconController = matchIconController;
        }

        public void PopulateGrid()
        {
            for (var y = 0; y < GridSize.y; y++)
            {
                for (var x = 0; x < GridSize.x; x++)
                {
                    if (!IsEmpty(x, y)) continue;
                    var block = _matchableBlockPool.GetRandomBlock();
                    block.SetPosition(transform.position, x, y);
                    block.gameObject.SetActive(true);
                    block.BlockClicked += CheckMatch;
                    PutItemAt(block, x, y);
                }
            }

            UpdateMatchCache();
        }

        private void CheckMatch(Block clickedBlock)
        {
            var group = GetMatchedGroupIfAny(clickedBlock.Position);
            if (group.Count >= 2)
            {
                foreach (var block in group)
                {
                    block.BlockClicked -= CheckMatch;
                    RemoveItemAt(block.Position);
                    _matchableBlockPool.ReturnToPool(block);
                }

                _gravityController.ApplyGravity(this, transform.position)
                    .OnComplete(RefillAfterGravity);
            }
        }

        private void RefillAfterGravity()
        {
            _blockRefiller.SpawnNewBlocks(this, _matchableBlockPool, transform.position, CheckMatch)
                .OnComplete(UpdateMatchCache);
        }

        private void UpdateMatchCache()
        {
            _matchCache = _blockMatcher.GenerateMatchCache(this);
            _matchIconController.ChangeIcons(_matchCache);
        }

        private List<MatchableBlock> GetMatchedGroupIfAny(Vector2Int pos)
        {
            return _matchCache.TryGetValue(pos, out var group) ? group : new List<MatchableBlock>();
        }

        private void OnDestroy()
        {
            _gravityController.KillActiveTweens();
            _blockRefiller.KillRefillSequence();
        }
    }
}