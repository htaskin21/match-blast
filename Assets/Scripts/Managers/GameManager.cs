using Blocks;
using Logic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private LevelManager _levelManager;

        [SerializeField]
        private GridManager _gridManager;

        [SerializeField]
        private MatchableBlockPool _matchableBlockPool;

        private void Start()
        {
            _matchableBlockPool.Init(_levelManager.NumberOfColors);
            var poolSize = _levelManager.ColumnSize * _levelManager.RowSize * 2;
            _matchableBlockPool.CreatePool(poolSize);

            var blockMatcher = new BlockMatcher();
            var gravityController = new GravityController();
            var blockRefiller = new BlockRefiller();
            var matchIconController = new MatchIconController(_levelManager.SmallCondition,
                _levelManager.MediumCondition, _levelManager.BigCondition);
            _gridManager.Init(_levelManager.ColumnSize,
                _levelManager.RowSize, _matchableBlockPool, blockMatcher,
                gravityController, blockRefiller, matchIconController);
            _gridManager.PopulateGrid();
        }
    }
}