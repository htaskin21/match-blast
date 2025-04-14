using System;
using UnityEngine.EventSystems;

namespace Blocks
{
    public class MatchableBlock : Block, IPointerClickHandler
    {
        private IconController _iconController;
        private BlockMovement _blockMovement;

        public event Action<Block> BlockClicked; 
        
        public void Awake()
        {
            _iconController = new IconController(_spriteRenderer);
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