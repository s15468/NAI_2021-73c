namespace Lab2_Pan.Players
{
    public sealed class AiPlayer : Player
    {
        private int aiLevel;

        public AiPlayer(int difficulty)
        {
            aiLevel = difficulty;
        }

        public override int[] InvokeMove()
        {
            throw new System.NotImplementedException();
        }
    }
}
