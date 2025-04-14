using UnityEngine;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;
       
        public Vector2Int Position;
        public BlockColorType ColorType { get; protected set; }
        
        public override string ToString()
        {
            return gameObject.name;
        }
    }
}