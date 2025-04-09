using Blocks;
using Cores;
using UnityEngine;

namespace Managers
{
    public class GridManager : GridSystem<Block>
    {
        private MatchableBlockPool _matchableBlockPool;

        public void Init(MatchableBlockPool matchableBlockPool)
        {
            CreateGrid();
            _matchableBlockPool = matchableBlockPool;
        }

        public void PopulateGrid()
        {
            for (var y = 0; y < _gridSize.y; y++)
            {
                for (var x = 0; x < _gridSize.x; x++)
                {
                    var block = _matchableBlockPool.GetRandomBlock();
                    block.transform.position = transform.position + new Vector3(x, y);
                    block.gameObject.SetActive(true);
                    PutItemAt(block, x,y);
                }
            }
        }
    }
}
