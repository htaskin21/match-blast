namespace Blocks
{
    public class MatchableBlock : Block
    {
        public MatchableBlockColorType ColorType { get; private set; }

        private IconController _iconController;
        private BlockMovement _blockMovement;
        
        public void Init()
        {
            _iconController = new IconController(_spriteRenderer);
        }

        public void SetType(MatchableBlockIconSO iconSO)
        {
            ColorType = iconSO.ColorType;
            _iconController.SetIconSO(iconSO);
        }
    }
}