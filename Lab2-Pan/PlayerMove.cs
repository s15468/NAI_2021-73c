namespace Lab2_Pan
{
    /// <summary>
    /// Public class representing player move
    /// </summary>
    public class PlayerMove
    {
        /// <summary>
        /// Public property representing expected game move
        /// </summary>
        public GameMove MoveType { get; set; }

        /// <summary>
        /// Public property representing object as data required to make move
        /// </summary>
        public object Data { get; set; }
    }
}
