using UnityEngine;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;

        public BlockColorType ColorType { get; protected set; }
        
        public override string ToString()
        {
            return gameObject.name;
        }
    }
}