using System;
using UnityEngine.EventSystems;

namespace Blocks
{
    public class MatchableBlock : Block, IPointerClickHandler
    {
        private IconController _iconController;

        public BlockMovement BlockMovement { get; private set; }

        public event Action<Block> BlockClicked;

        public void Awake()
        {
            _iconController = new IconController(_spriteRenderer);
            BlockMovement = new BlockMovement();
        }

        public void SetType(MatchableBlockIconSO iconSO)
        {
            ColorType = iconSO.ColorType;
            _iconController.SetIconSO(iconSO);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            BlockClicked?.Invoke(this);
        }
    }
}