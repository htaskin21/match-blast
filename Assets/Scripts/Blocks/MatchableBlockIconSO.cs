using UnityEngine;

namespace Blocks
{
    [CreateAssetMenu(fileName = "IconSO", menuName = "ScriptableObjects/MatchableBlockIconSO", order = 1)]
    public class MatchableBlockIconSO : ScriptableObject
    {
        [SerializeField]
        private BlockColorType _colorType;

        public BlockColorType ColorType => _colorType;

        [SerializeField]
        private Sprite[] _icons;

        public Sprite GetIcon(int index)
        {
            if (index >= _icons.Length)
            {
                index = _icons.Length - 1;
            }

            return _icons[index];
        }
    }
}