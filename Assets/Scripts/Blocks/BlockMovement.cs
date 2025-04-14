using DG.Tweening;
using UnityEngine;

namespace Blocks
{
    public class BlockMovement
    {
        private readonly float _speed;
        private readonly Ease _easeType;

        public BlockMovement(float speed = 0.2f, Ease ease = Ease.Linear)
        {
            _speed = speed;
            _easeType = ease;
        }

        public Tween Move(GameObject block, Vector3 targetPos)
        {
            return block.transform.DOMove(targetPos, _speed).SetEase(_easeType);
        }
    }
}