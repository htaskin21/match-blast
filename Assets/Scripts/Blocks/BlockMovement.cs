using DG.Tweening;
using UnityEngine;

namespace Blocks
{
    public class BlockMovement
    {
        private float _speed;
        private Ease _easeType;

        public void Move(GameObject block, Transform targetTransform)
        {
            block.transform.DOMove(targetTransform.position, _speed).SetEase(_easeType);
        }
    }
}