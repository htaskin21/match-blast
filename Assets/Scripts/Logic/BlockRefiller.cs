using System;
using Blocks;
using Cores;
using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class BlockRefiller
    {
        private Sequence _refillSequence;

        public Sequence SpawnNewBlocks(GridSystem<Block> grid, MatchableBlockPool pool, Vector3 boardOrigin,
            Action<Block> onClickCallback)
        {
            _refillSequence?.Kill();
            _refillSequence = DOTween.Sequence();

            for (int x = 0; x < grid.GridSize.x; x++)
            {
                int spawnOffset = 0;

                for (int y = grid.GridSize.y - 1; y >= 0; y--)
                {
                    Vector2Int pos = new(x, y);
                    if (!grid.IsEmpty(pos))
                        continue;

                    int spawnY = grid.GridSize.y + spawnOffset;
                    var block = pool.GetRandomBlock();
                    block.SetPosition(boardOrigin, x, spawnY);
                    block.gameObject.SetActive(true);
                    block.BlockClicked += onClickCallback;

                    grid.PutItemAt(block, pos);

                    Vector3 targetWorldPos = boardOrigin + new Vector3(x, y);
                    _refillSequence.Join(block.BlockMovement.Move(block.gameObject, targetWorldPos)
                        .OnComplete(() => block.SetPosition(boardOrigin, pos.x, pos.y)));

                    spawnOffset++;
                }
            }

            return _refillSequence;
        }

        public void KillRefillSequence()
        {
            _refillSequence?.Kill();
        }
    }
}