using UnityEngine;
using UnityEngine.EventSystems;

namespace Blocks
{
    public class MatchableBlock : Block, IPointerClickHandler
    {
        public MatchableBlockColorType ColorType { get; private set; }

        private IconController _iconController;
        private BlockMovement _blockMovement;
        
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
            Debug.Log($"{this.name} click tıklandı");

        }
    }
}