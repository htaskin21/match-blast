using DG.Tweening;
using UnityEngine;

namespace Blocks
{
    // Controls the movement animation of a block using DOTween.
    public class BlockMovement
    {
        private readonly float _speed;
        private readonly Ease _easeType;

        private Tween _activeTween;

        public BlockMovement(float speed = 0.2f, Ease ease = Ease.Linear)
        {
            _speed = speed;
            _easeType = ease;
        }

        public Tween Move(GameObject block, Vector3 targetPos)
        {
            _activeTween?.Kill();

            _activeTween = block.transform.DOMove(targetPos, _speed)
                .SetEase(_easeType);

            return _activeTween;
        }
    }
}