using System.Collections.Generic;
using Blocks;
using Cores;
using Logic;
using UnityEngine;

namespace Managers
{
    public class GridManager : GridSystem<Block>
    {
        [SerializeField]
        private Vector2 _offScreenOffSet;

        private MatchableBlockPool _matchableBlockPool;
        private BlockMatcher _blockMatcher;

        public void Init(MatchableBlockPool matchableBlockPool,BlockMatcher blockMatcher)
        {
            CreateGrid();
            _matchableBlockPool = matchableBlockPool;
            _blockMatcher = blockMatcher;
        }

        public void PopulateGrid()
        {
            for (var y = 0; y < _gridSize.y; y++)
            {
                for (var x = 0; x < _gridSize.x; x++)
                {
                    if (!IsEmpty(x, y)) continue;
                    var block = _matchableBlockPool.GetRandomBlock();
                    block.transform.position = transform.position + new Vector3(x, y);
                    block._gridManager = this;
                    block.gameObject.SetActive(true);
                    block.Position = new Vector2Int(x, y);
                    PutItemAt(block, x, y);
                }
            }
        }

        public List<MatchableBlock> GetConnectedBlocks(MatchableBlock block)
        {
            return _blockMatcher.FindConnectedBlocks(block, this);
        }

        public void ClearBlocks(List<MatchableBlock> blocks)
        {
            foreach (var block in blocks)
            {
                RemoveItemAt(block.Position);
                Destroy(block.gameObject);
            }
        }
    }
}