using Blocks;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int _numberOfColors;

        [SerializeField]
        private GridManager _gridManager;

        [SerializeField]
        private MatchableBlockPool _matchableBlockPool;

        private void Start()
        {
            var poolSize = _gridManager.GridSize.x * _gridManager.GridSize.y * 2;
            _matchableBlockPool.Init(_numberOfColors);
            _matchableBlockPool.CreatePool(poolSize);

            _gridManager.Init(_matchableBlockPool);
            _gridManager.PopulateGrid();
        }
    }
}