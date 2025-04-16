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
        private CameraController _cameraController;

        [SerializeField]
        private GridManager _gridManager;

        [SerializeField]
        private MatchableBlockPool _matchableBlockPool;

        private void Start()
        {
            _cameraController.Setup(_levelManager.RowSize, _levelManager.ColumnSize);

            _matchableBlockPool.Init(_levelManager.NumberOfColors);
            var poolSize = _levelManager.ColumnSize * _levelManager.RowSize * 2;
            _matchableBlockPool.CreatePool(poolSize);


            var gravityController = new GravityController();
            _gridManager.Init(_levelManager.ColumnSize,
                _levelManager.RowSize, _matchableBlockPool,
                gravityController);

            var blockMatcher = new BlockMatcher();
            var matchIconController = new MatchIconController(_levelManager.SmallCondition,
                _levelManager.MediumCondition, _levelManager.BigCondition);
            var boardController = new MatchController(_gridManager, blockMatcher, matchIconController);

            var blocks = _gridManager.FillGrids();
            boardController.RegisterBlocks(blocks);
        }
    }
}