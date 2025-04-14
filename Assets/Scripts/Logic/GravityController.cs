using System.Collections.Generic;
using Blocks;
using Cores;
using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class GravityController
    {
        private Sequence _fallSequence;
        private readonly List<Tween> _individualTweens = new();

        public Sequence ApplyGravity(GridSystem<Block> grid, Vector3 boardOrigin)
        {
            KillActiveTweens();
            _fallSequence = DOTween.Sequence();

            for (var x = 0; x < grid.GridSize.x; x++)
            {
                var emptyCount = 0;

                for (var y = 0; y < grid.GridSize.y; y++)
                {
                    var currentPos = new Vector2Int(x, y);

                    if (grid.IsEmpty(currentPos))
                    {
                        emptyCount++;
                    }
                    else if (emptyCount > 0)
                    {
                        var targetPos = new Vector2Int(x, y - emptyCount);

                        if (grid.GetItemAt(currentPos) is not MatchableBlock block)
                            continue;

                        grid.MoveItemTo(currentPos, targetPos);

                        var worldTarget = boardOrigin + new Vector3(targetPos.x, targetPos.y);
                        var moveTween = block.BlockMovement.Move(block.gameObject, worldTarget)
                            .OnComplete(() => block.SetPosition(boardOrigin, targetPos.x, targetPos.y));

                        _individualTweens.Add(moveTween);
                        _fallSequence.Join(moveTween);
                    }
                }
            }

            return _fallSequence;
        }

        public void KillActiveTweens()
        {
            _fallSequence?.Kill();

            foreach (var tween in _individualTweens)
            {
                tween?.Kill();
            }

            _individualTweens.Clear();
        }
    }
}