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
            var group = _gridManager.GetMatchedGroupIfAny(Position);
            if (group.Count >= 2)
            {
                foreach (var block in group)
                {
                    _gridManager.RemoveItemAt(block.Position);
                    Destroy(block.gameObject);
                }
            }

        }
    }
}