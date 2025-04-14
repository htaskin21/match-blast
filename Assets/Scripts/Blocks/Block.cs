using UnityEngine;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;

        public Vector2Int Position { get; private set; }
        public BlockColorType ColorType { get; protected set; }

        public void SetPosition(Vector3 boardPos, int x, int y)
        {
            transform.position = boardPos + new Vector3(x, y);
            Position = new Vector2Int(x, y);
        }

        public override string ToString()
        {
            return gameObject.name;
        }
    }
}