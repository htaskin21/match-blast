using UnityEngine;

namespace Blocks
{
    public class IconController
    {
        private readonly SpriteRenderer _spriteRenderer;
        private MatchableBlockIconSO _blockIconSo;

        public IconController(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }
        
        public void SetIconSO(MatchableBlockIconSO blockIconSo)
        {
            _blockIconSo = blockIconSo;
            _spriteRenderer.sprite = _blockIconSo.GetIcon(0);
        }

        public void ChangeIcon(int index)
        {
            _spriteRenderer.sprite = _blockIconSo.GetIcon(index);
        }
    }
}