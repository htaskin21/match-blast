using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Blocks
{
    public class MatchableBlock : Block, IPointerClickHandler
    {
        public Vector2Int Position;
        public GridManager _gridManager;
        
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
            var matchedBlocks = _gridManager.GetConnectedBlocks(this);
            if (matchedBlocks.Count >= 2)
            {
                _gridManager.ClearBlocks(matchedBlocks);
                Debug.Log($"{matchedBlocks.Count} blok patlatıldı.");
            }
            else
            {
                Debug.Log("Yeterli eşleşme yok.");
            }

        }
    }
}