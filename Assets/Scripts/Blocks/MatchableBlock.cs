using System;
using UnityEngine.EventSystems;

namespace Blocks
{
    public class MatchableBlock : Block, IPointerClickHandler
    {
        public IconController IconController { get; private set; }

        public BlockMovement BlockMovement { get; private set; }

        public event Action<Block> BlockClicked;

        public void Awake()
        {
            IconController = new IconController(_spriteRenderer);
            BlockMovement = new BlockMovement();
        }

        public void SetType(MatchableBlockIconSO iconSO)
        {
            ColorType = iconSO.ColorType;
            IconController.SetIconSO(iconSO);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            BlockClicked?.Invoke(this);
        }
    }
}