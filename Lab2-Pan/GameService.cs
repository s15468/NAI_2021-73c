using Lab2_Pan.Cards;
using Lab2_Pan.Players;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan
{
    /// <summary>
    /// Public class which allowing to manage game
    /// </summary>
    public class GameService
    {
        private List<ICard> _stack;
        private bool isStartCardPlaced;

        private readonly PlayersCollection _players;
        private readonly RenderService _renderService;
        private readonly DeckService _deckService;


        /// <summary>
        /// Public constructor which initializing variable
        /// </summary>
        public GameService()
        {
            _stack = new List<ICard>();
            _players = new PlayersCollection();
            _renderService = new RenderService();
            _deckService = new DeckService();
        }

        /// <summary>
        /// Public method to prepare game for round
        /// </summary>
        public void PrepareGame()
        {
            addHumanPlayer();
            addAiPlayer();
            _deckService.GenerateDeck();
            _deckService.ShuffleDeck();
            giveCardsToPlayers();
        }

        /// <summary>
        /// Public method to start and make game
        /// </summary>
        public void StartGame()
        {
            IEnumerator<IPlayer> enumerator = _players.GetEnumerator();
            IPlayer currentPlayer = moveToNextPlayer(enumerator);

            do
            {
                if (!isStartCardPlaced && !isStartCardInHand(currentPlayer))
                {
                    currentPlayer = moveToNextPlayer(enumerator);
                    continue;
                }

                PlayerMove playerMove = currentPlayer.InvokeMove(currentPlayer.Cards, _stack, getAvailableMoves(currentPlayer.Cards));

                if (isMoveValid(playerMove))
                {
                    processMove(currentPlayer, playerMove);
                    currentPlayer = moveToNextPlayer(enumerator);
                }

            } while (_players.Any(x => x.Cards.Count() != 0));
        }

        /// <summary>
        /// Private method to move enumerator to next player
        /// </summary>
        /// <param name="enumerator">IEnumerator of IPlayer from PlayerCollection</param>
        /// <returns>Next player in queue</returns>
        private IPlayer moveToNextPlayer(IEnumerator<IPlayer> enumerator)
        {
                if (!enumerator.MoveNext())
                {
                    enumerator.Reset();
                    enumerator.MoveNext();
                }

                return enumerator.Current;
        }

        /// <summary>
        /// Private method to process player move
        /// </summary>
        /// <param name="currentPlayer">current player which do move</param>
        /// <param name="playerMove">Object representing player move</param>
        private void processMove(IPlayer currentPlayer, PlayerMove playerMove)
        {
            if (playerMove.MoveType == GameMove.DrawStack)
            {
                if (_stack.Count() > 3)
                {
                    var temp = _stack.TakeLast(3).ToList();
                    temp.ForEach(card => _stack.Remove(card));
                    temp.ForEach(card => currentPlayer.AddCard(card));
                }
                else
                {
                    var temp = _stack.TakeLast(_stack.Count - 1).ToList();
                    temp.ForEach(card => _stack.Remove(card));
                    temp.ForEach(card => currentPlayer.AddCard(card));
                }

                return;
            }

            IEnumerable<ICard> cardsToPut = (IEnumerable<ICard>)playerMove.Data;
            _stack.AddRange(cardsToPut);

            cardsToPut.ToList().ForEach(card => currentPlayer.RemoveCard(card));
        }

        /// <summary>
        /// Method to get available moves for current player
        /// </summary>
        /// <param name="playerCards">Current player cards</param>
        /// <returns>List of available moves</returns>
        private List<GameMove> getAvailableMoves(IEnumerable<ICard> playerCards)
        {
            List<GameMove> availableMoves = new();


            if (playerCards.Any(card => card.Figure == Figure.n9 && card.Suit == Suit.Heart)
                || (_stack.Count > 0 && playerCards.Any(card => (int)card.Figure >= (int)_stack.Last().Figure)))
            {
                availableMoves.Add(GameMove.PutCards);
            }

            if (_stack.Count > 1)
            {
                availableMoves.Add(GameMove.DrawStack);
            }

            return availableMoves;
        }

        /// <summary>
        /// Method to check if move is valid
        /// </summary>
        /// <param name="playerMove">Object representing PlayerMove</param>
        /// <returns>boolean as true if move is valid</returns>
        private bool isMoveValid(PlayerMove playerMove)
        {
            if (playerMove.MoveType == GameMove.DrawStack)
            {
                return true;
            }

            IEnumerable<ICard> cardsToVerify = (IEnumerable<ICard>)playerMove.Data;

            if (_stack.Count() == 0)
            {
                if (cardsToVerify.First().Figure != Figure.n9 || cardsToVerify.First().Suit != Suit.Heart)
                {
                    return false;
                }

                for (int i = 1; i < cardsToVerify.Count(); i++)
                {
                    if (cardsToVerify.ElementAt(i).Figure != cardsToVerify.First().Figure)
                    {
                        return false;
                    }
                }

                isStartCardPlaced = true;
            }
            else
            {
                ICard lastStackCard = _stack.Last();

                if ((int)cardsToVerify.First().Figure < (int)lastStackCard.Figure)
                {
                    return false;
                }

                if (cardsToVerify.First().Figure == lastStackCard.Figure)
                {
                    for (int i = 1; i < cardsToVerify.Count(); i++)
                    {
                        if (cardsToVerify.ElementAt(i).Figure != cardsToVerify.First().Figure)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Method to check if start card is in hand
        /// </summary>
        /// <param name="playerMove">Object representing PlayerMove</param>
        /// <returns>boolean as true if start card in hand</returns>
        private bool isStartCardInHand(IPlayer currentPlayer)
            => currentPlayer.Cards.Any(card => card.Figure.Equals(Figure.n9) && card.Suit.Equals(Suit.Heart));

        /// <summary>
        /// Method to add human player to PlayerCollection
        /// </summary>
        private void addHumanPlayer() => _players.Add(new HumanPlayer());

        /// <summary>
        /// Method to add ai player to PlayerCollection
        /// </summary>
        private void addAiPlayer() => _players.Add(new AIPlayer());

        /// <summary>
        /// Method to give each player cards
        /// </summary>
        private void giveCardsToPlayers()
        {
            List<ICard> deck = _deckService.Deck;

            do
            {
                foreach (var player in _players)
                {
                    ICard cardToGive = deck.First();
                    player.AddCard(cardToGive);
                    deck.Remove(cardToGive);
                }
            }
            while (deck.Count != 0);
        }
    }
}
