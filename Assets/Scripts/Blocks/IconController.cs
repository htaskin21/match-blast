using UnityEngine;

namespace Blocks
{
    // Manages the visual icons for a block based on its match state.
    public class IconController
    {
        private readonly SpriteRenderer _spriteRenderer;
        private MatchableBlockIconSO _blockIconSo;

        public IconController(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }
        
        /// <summary>
        /// Sets the icon ScriptableObject for the block.
        /// </summary>
        public void SetIconSO(MatchableBlockIconSO blockIconSo)
        {
            _blockIconSo = blockIconSo;
            _spriteRenderer.sprite = _blockIconSo.GetIcon(0);
        }

        /// <summary>
        /// Changes the block's icon based on the match size.
        /// </summary>
        public void ChangeIcon(int index)
        {
            _spriteRenderer.sprite = _blockIconSo.GetIcon(index);
        }
    }
}