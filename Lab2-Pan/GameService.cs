using Lab2_Pan.Cards;
using Lab2_Pan.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan
{
    public class GameService
    {
        private List<ICard> _stack;
        private bool isStartCardPlaced;

        private readonly PlayersCollection _players;
        private readonly RenderService _renderService;
        private readonly DeckService _deckService;

        public GameService()
        {
            _stack = new List<ICard>();
            _players = new PlayersCollection();
            _renderService = new RenderService();
            _deckService = new DeckService();
        }

        public void PrepareGame()
        {
            addHumanPlayer();
            addAiPlayer(selectAiDifficulty());
            _deckService.GenerateDeck();
            _deckService.ShuffleDeck();
            giveCardsToPlayers();
        }

        public void StartGame()
        {
            do
            {
                _players.MoveToNextPlayer();
                IPlayer currentPlayer = _players.MoveToNextPlayer();

                if (!isStartCardPlaced && !isStartCardInHand(currentPlayer))
                {
                    continue;
                }

                var playerMove = currentPlayer.InvokeMove();

                if (isMoveValid(playerMove))
                {
                    enumerator.MoveNext();
                }

            } while (_players.Any(x => x.Cards.Any()));
        }

        private bool isMoveValid(int[] playerMove) => true;

        private bool isStartCardInHand(IPlayer currentPlayer)
            => currentPlayer.Cards.Any(card => card.Figure.Equals(Figure.n9) && card.Suit.Equals(Suit.Heart));

        private void addHumanPlayer() => _players.Add(new HumanPlayer());

        private void addAiPlayer(char difficulty) => _players.Add(new AiPlayer(int.Parse($"{difficulty}")));

        private char selectAiDifficulty()
        {
            char pressedKey;

            do
            {
                Console.Clear();
                _renderService.RenderAiDifficultyMenu();
                pressedKey = Console.ReadKey().KeyChar;
            }
            while (!isPlayerSelectCorrectAiLevel(pressedKey));

            return pressedKey;
        }

        private bool isPlayerSelectCorrectAiLevel(char pressedKey)
            => pressedKey.Equals('1') || pressedKey.Equals('2') || pressedKey.Equals('3');

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
