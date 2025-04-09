using UnityEngine;

namespace Blocks
{
    public abstract class Block : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;

        public override string ToString()
        {
            return gameObject.name;
        }
    }
}