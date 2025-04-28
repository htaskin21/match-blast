using Blocks;
using Logic;
using UnityEngine;

namespace Managers
{
    // Manages the initialization of core game systems like grid, camera, and block pool.
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

            // Set up Pools
            _matchableBlockPool.Init(_levelManager.NumberOfColors);
            var poolSize = _levelManager.ColumnSize * _levelManager.RowSize * 2;
            _matchableBlockPool.CreatePool(poolSize);
            
            // Create Grid
            var gravityController = new GravityController();
            _gridManager.Init(_levelManager.ColumnSize,
                _levelManager.RowSize, _matchableBlockPool,
                gravityController);

            // Set Up Logics
            var blockMatcher = new BlockMatcher();
            var matchIconController = new MatchIconController(_levelManager.SmallCondition,
                _levelManager.MediumCondition, _levelManager.BigCondition);
            var boardController = new MatchController(_gridManager, blockMatcher, matchIconController);

            // Fill the Grids
            var blocks = _gridManager.FillGrids();
            boardController.RegisterBlocks(blocks);
        }
    }
}