using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Board Size")]
        [SerializeField]
        private int _rowSize;

        public int RowSize => _rowSize;
        
        [SerializeField]
        private int _columnSize;

        public int ColumnSize => _columnSize;

        [Header("Matchable Block Color Amount")]
        [SerializeField]
        private int _numberOfColors;

        public int NumberOfColors => _numberOfColors;

        [Header("Match Conditions")]
        [SerializeField]
        private int _smallCondition;

        public int SmallCondition => _smallCondition;

        [SerializeField]
        private int _mediumCondition;

        public int MediumCondition => _mediumCondition;

        [SerializeField]
        private int _bigCondition;

        public int BigCondition => _bigCondition;
    }
}
