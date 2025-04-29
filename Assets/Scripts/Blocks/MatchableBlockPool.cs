using Cores;
using UnityEngine;
using Random = System.Random;

namespace Blocks
{
    public class MatchableBlockPool : ObjectPool<MatchableBlock>
    {
        [SerializeField]
        private MatchableBlockIconSO[] _blockIconSO;

        private int _numberOfColors;
        private Random _random;

        public Vector2 BlockSize { get; private set; }

        public void Init(int numberOfColors)
        {
            _numberOfColors = numberOfColors;
            _random = new Random();
            BlockSize = _prefab.GetBlockSize();
        }

        public MatchableBlock GetRandomBlock()
        {
            var block = GetObject();
            var randomIconIndex = _random.Next(0, _numberOfColors);
            block.SetType(_blockIconSO[randomIconIndex]);
            return block;
        }
    }
}