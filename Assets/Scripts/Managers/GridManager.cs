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
        private Dictionary<Vector2Int, List<MatchableBlock>> _matchCache;

        public void Init(MatchableBlockPool matchableBlockPool, IBlockMatcher blockMatcher)
        {
            CreateGrid();
            _matchableBlockPool = matchableBlockPool;
            _blockMatcher = blockMatcher;
        }

        public void PopulateGrid()
        {
            for (var y = 0; y < _gridSize.y; y++)
            {
                for (var x = 0; x < _gridSize.x; x++)
                {
                    if (!IsEmpty(x, y)) continue;
                    var block = _matchableBlockPool.GetRandomBlock();
                    block.SetPosition(transform.position, x, y);
                    block.gameObject.SetActive(true);
                    block.BlockClicked += CheckMatch;
                    PutItemAt(block, x, y);
                }
            }

            _matchCache = _blockMatcher.GenerateMatchCache(this);
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
            }

            ApplyGravity();
        }

        private void ApplyGravity()
        {
            var fallSequence = DOTween.Sequence();

            for (int x = 0; x < _gridSize.x; x++)
            {
                int emptyCount = 0;

                for (int y = 0; y < _gridSize.y; y++)
                {
                    var currentPos = new Vector2Int(x, y);

                    if (IsEmpty(currentPos))
                    {
                        emptyCount++;
                    }
                    else if (emptyCount > 0)
                    {
                        var targetPos = new Vector2Int(x, y - emptyCount);

                        if (GetItemAt(currentPos) is not MatchableBlock block)
                            continue;

                        MoveItemTo(currentPos, targetPos);

                        var boardPos = transform.position + new Vector3(targetPos.x, targetPos.y);
                        var tween = block.BlockMovement.Move(block.gameObject, boardPos)
                            .OnComplete(() => block.SetPosition(transform.position, targetPos.x, targetPos.y));

                        fallSequence.Join(tween);
                    }
                }
            }

            fallSequence.OnComplete(() =>
            {
                _matchCache = _blockMatcher.GenerateMatchCache(this);
            });
        }
        
        private List<MatchableBlock> GetMatchedGroupIfAny(Vector2Int pos)
        {
            return _matchCache.TryGetValue(pos, out var group) ? group : new List<MatchableBlock>();
        }
    }
}