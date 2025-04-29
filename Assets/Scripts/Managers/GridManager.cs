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
        private Vector2Int _offScreenOffSet;

        private MatchableBlockPool _matchableBlockPool;
        private GravityController _gravityController;
        private Sequence _dropBlocksSequence;
        private Sequence _refillBoardSequence;
        private Vector2 _blockSize;

        public List<MatchableBlock> DroppedBlocks { get; private set; }

        public void Init(int columnSize, int rowSize, MatchableBlockPool matchableBlockPool,
            GravityController gravityController)
        {
            GridSize = new Vector2Int(columnSize, rowSize);
            CreateGrid();
            _matchableBlockPool = matchableBlockPool;
            _gravityController = gravityController;
            _blockSize = _matchableBlockPool.BlockSize;
            DroppedBlocks = new List<MatchableBlock>();
        }


        /// <summary>
        /// Fills the entire grid with random blocks.
        /// </summary>
        /// <returns></returns>
        public List<MatchableBlock> FillGrids()
        {
            var blocks = new List<MatchableBlock>(GridSize.x * GridSize.y);
            for (var y = 0; y < GridSize.y; y++)
            {
                for (var x = 0; x < GridSize.x; x++)
                {
                    if (!IsEmpty(x, y)) continue;
                    var block = _matchableBlockPool.GetRandomBlock();

                    block.SetGridPosition(x, y);
                    var blockWorldPos = transform.position + new Vector3(x * _blockSize.x, y * _blockSize.y, 0);
                    block.SetWorldPosition(blockWorldPos);
                    block.SetSortingOrder(y);

                    block.gameObject.SetActive(true);
                    PutItemAt(block, x, y);
                    blocks.Add(block);
                }
            }

            return blocks;
        }

        /// <summary>
        /// Spawns blocks for all empty grid positions from off-screen.
        /// </summary>
        private Sequence SpawnBlocksForEmptyGrids()
        {
            _dropBlocksSequence?.Kill();
            _dropBlocksSequence = DOTween.Sequence();
            DroppedBlocks.Clear();
            for (var x = 0; x < GridSize.x; x++)
            {
                var spawnOffset = _offScreenOffSet.y;

                for (var y = GridSize.y - 1; y >= 0; y--)
                {
                    Vector2Int pos = new(x, y);
                    if (!IsEmpty(pos))
                        continue;

                    var spawnY = GridSize.y + spawnOffset;
                    var block = _matchableBlockPool.GetRandomBlock();

                    var blockWorldPos = transform.position + new Vector3(x * _blockSize.x, spawnY * _blockSize.y, 0);
                    block.SetWorldPosition(blockWorldPos);

                    block.gameObject.SetActive(true);
                    PutItemAt(block, pos);
                    DroppedBlocks.Add(block);

                    var targetWorldPos = transform.position + new Vector3(x * _blockSize.x, y * _blockSize.y, 0);
                    _dropBlocksSequence.Join(block.BlockMovement.Move(block.gameObject, targetWorldPos)
                        .OnComplete(() =>
                        {
                            block.SetGridPosition(pos.x, pos.y);
                            block.SetWorldPosition(targetWorldPos);
                            block.SetSortingOrder(pos.y);
                        }));

                    spawnOffset++;
                }
            }

            return _dropBlocksSequence;
        }

        public void RemoveBlock(MatchableBlock block)
        {
            RemoveItemAt(block.GridPosition);
            _matchableBlockPool.ReturnToPool(block);
        }

        /// <summary>
        /// Refills the board by applying gravity and spawning new blocks.
        /// </summary>
        /// <returns></returns>
        public Sequence RefillBoard()
        {
            var gravitySequence = _gravityController.ApplyGravity(this, transform.position);
            var spawnSequence = SpawnBlocksForEmptyGrids();

            _refillBoardSequence?.Kill();
            _refillBoardSequence = DOTween.Sequence();

            return _refillBoardSequence
                .Append(gravitySequence)
                .Append(spawnSequence);
        }

        private void OnDestroy()
        {
            _gravityController.KillActiveTweens();
            _dropBlocksSequence?.Kill();
            _refillBoardSequence?.Kill();
        }
    }
}