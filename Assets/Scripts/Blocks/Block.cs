using UnityEngine;

namespace Blocks
{
    // Base class for all block types on the board with basic properties.
    public abstract class Block : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;

        [SerializeField]
        protected BoxCollider2D _boxCollider2D;

        public Vector2Int GridPosition { get; private set; }
        public BlockColorType ColorType { get; protected set; }

        public virtual void SetGridPosition(int x, int y)
        {
            GridPosition = new Vector2Int(x, y);
        }

        public virtual void SetWorldPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        public Vector2 GetBlockSize()
        {
            return _boxCollider2D.size;
        }

        public void SetSortingOrder(int rowNo)
        {
            _spriteRenderer.sortingOrder = rowNo;
        }

        public override string ToString()
        {
            return gameObject.name;
        }
    }
}