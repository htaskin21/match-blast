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

        private Dictionary<Vector2Int, List<MatchableBlock>> _matchCache;

        public void Init(MatchableBlockPool matchableBlockPool, BlockMatcher blockMatcher)
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
                    block.gameObject.SetActive(true);
                    block.BlockClicked += CheckMatch;
                    block.Position = new Vector2Int(x, y);
                    PutItemAt(block, x, y);
                }
            }

            CacheAllMatches();
        }

        public void CacheAllMatches()
        {
            _matchCache = new Dictionary<Vector2Int, List<MatchableBlock>>();
            var visited = new HashSet<Vector2Int>();

            for (int y = 0; y < _gridSize.y; y++)
            {
                for (int x = 0; x < _gridSize.x; x++)
                {
                    Vector2Int currentPos = new(x, y);
                    if (!CheckBounds(currentPos) || visited.Contains(currentPos) || IsEmpty(currentPos))
                        continue;

                    if (GetItemAt(currentPos) is not MatchableBlock currentBlock)
                        continue;

                    var group = _blockMatcher.FindConnectedBlocks(currentBlock, this);

                    // Yeterli eşleşme yoksa işaretlemeye gerek yok
                    if (group.Count < 2)
                        continue;

                    // Sadece bir defa cache listesi oluştur
                    foreach (var block in group)
                    {
                        if (!visited.Contains(block.Position))
                        {
                            _matchCache[block.Position] = group; // Aynı referans, kopya yok
                            visited.Add(block.Position);
                        }
                    }
                }
            }
        }


        public List<MatchableBlock> GetMatchedGroupIfAny(Vector2Int pos)
        {
            if (_matchCache.TryGetValue(pos, out var group))
                return group;

            return new List<MatchableBlock>();
        }

        private void CheckMatch(Block clickedBlock)
        {
            var group = GetMatchedGroupIfAny(clickedBlock.Position);
            if (group.Count >= 2)
            {
                foreach (var block in group)
                {
                    block.BlockClicked -= CheckMatch;
                    RemoveItemAt(block.Position);
                    Destroy(block.gameObject);
                }
            }
        }
    }
}