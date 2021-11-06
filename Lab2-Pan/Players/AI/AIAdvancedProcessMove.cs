using Accord.Fuzzy;
using Lab2_Pan.Cards;
using Lab2_Pan.Players.AI.Difficulty;
using Lab2_Pan.Players.AI.Fuzzy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Players.AI
{
    /// <summary>
    /// Method representing advanced logic of AI player
    /// </summary>
    public class AIAdvancedProcessMove : Fuzzifier
    {
        private List<ICard> _deck;
        private List<ICard> _aiCards;
        private List<ICard> _stackCards;
        private AIDifficulty _aiDifficulty;

        private readonly IPanDifficulty _pan;
        private readonly PanRules _panRules;
        private readonly DeckService _deckService;
        private readonly InferenceEngine _inferenceEngine;

        /// <summary>
        /// Custom constructor of current class
        /// </summary>
        /// <param name="aiDifficulty">Difficult of AI player</param>
        public AIAdvancedProcessMove(AIDifficulty aiDifficulty)
        {
            _aiCards = new List<ICard>();
            _deck = new List<ICard>();
            _stackCards = new List<ICard>();
            _deckService = new DeckService();
            _panRules = new PanRules();
            _inferenceEngine = new InferenceEngine();
            _pan = getPanDifficultyInstance(aiDifficulty);
        }

        /// <summary>
        /// Method to refresh AI cars require to process move
        /// </summary>
        /// <param name="aiCards">List of card of ai player</param>
        /// <param name="stackCards">List of car on stack</param>
        public void RefreshAIDeckAndStackCards(List<ICard> aiCards, List<ICard> stackCards)
        {
            _aiCards.Clear();
            _aiCards.AddRange(aiCards);

            _stackCards.Clear();
            _stackCards.AddRange(stackCards);

            _deck.Clear();
            _deck = _deckService.GetTempDeck();
        }

        /// <summary>
        /// Method to analyze and select main decision
        /// </summary>
        /// <returns>Returning enum representing decision</returns>
        public GameMove AnalyzeAndSelectMainDecision()
        {
            var lingVarList = createLingVarListForMainDecision();
            var db = _inferenceEngine.CreateDatabase(lingVarList);
            var rules = _panRules.GenerateRulesForMainDecision(db);
            var output = _inferenceEngine.StartFuzzyEngine(rules);

            return (GameMove)Enum.Parse(typeof(GameMove), output);
        }

        /// <summary>
        /// Method to analyze and put cards on stack
        /// </summary>
        /// <param name="availablePutCardsMoves">List of int how much card player can put</param>
        /// <returns>Returning List of Cards to put on stack</returns>
        public List<ICard> AnalyzeAndPutCards(List<int> availablePutCardsMoves)
        {
            var cardsToPut = new List<ICard>();
            var availableMoves = getAvailableMoves(availablePutCardsMoves);

            availableMoves.Reverse();

            if (isStartCardInHand())
            {
                cardsToPut.Add(getStartCard());
            }

            if (cardsToPut.Count == availableMoves.Max())
            {
                return cardsToPut;
            }

            _pan.GetCardsToPut(_aiCards, _stackCards, availableMoves, cardsToPut);

            return cardsToPut;
        }

        /// <summary>
        /// Method to get instance by AIDifficulty
        /// </summary>
        /// <param name="aiDifficulty"></param>
        /// <returns>Retuning instance of AI difficulty</returns>
        private IPanDifficulty getPanDifficultyInstance(AIDifficulty aiDifficulty)
        {
            switch (aiDifficulty)
            {
                case AIDifficulty.Easy:
                    return new PanEasyAI();

                case AIDifficulty.Normal:
                    return new PanNormalAI();

                case AIDifficulty.Hard:
                    return new PanHardAI();

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Method to get first card which starting game
        /// </summary>
        /// <returns>Returning ICard as starting game card</returns>
        private ICard getStartCard()
            => _aiCards.First(x => x.Figure == Figure.n9 && x.Suit == Suit.Heart);

        #region Logic for main decision

        /// <summary>
        /// Method to create list of LinguisticVariables with NumericInput to calculate main decision
        /// </summary>
        /// <returns>returning List of LinguisticVariables</returns>
        private List<LinguisticVariable> createLingVarListForMainDecision()
        {
            var fuzzySets = new PanFuzzySets();

            var lingVarList = new List<LinguisticVariable>
            {
                addFuzzySetsAsLinguisticVariableLabels(new CustomLinguisticVariable(PanLingVar.AIDifficulty, 0, 2), fuzzySets.AIDiffuculty()),
                addFuzzySetsAsLinguisticVariableLabels(new CustomLinguisticVariable(PanLingVar.NumberOfCardsInHand, 0, _deck.Count() - 1), fuzzySets.NumberOfCardsInHand(_deck.Count() - 1)),
                addFuzzySetsAsLinguisticVariableLabels(new CustomLinguisticVariable(PanLingVar.IsStartCardInHand, 0, 1), fuzzySets.IsStartCardInHand()),
                addFuzzySetsAsLinguisticVariableLabels(new CustomLinguisticVariable(PanLingVar.IsAnyCardFromHandShouldBeAlreadyOnStack, 0, 1), fuzzySets.IsAnyCardFromHandShouldBeAlreadyOnStack()),
                addFuzzySetsAsLinguisticVariableLabels(new CustomLinguisticVariable(PanLingVar.IsAnyFromRestCardShouldBeAlreadyOnStack, 0, 1), fuzzySets.IsAnyFromRestCardShouldBeAlreadyOnStack()),
                addFuzzySetsAsLinguisticVariableLabels(new CustomLinguisticVariable(PanLingVar.AnyPutMoveIsAvailable, 0, 1), fuzzySets.AnyPutMoveIsAvailable()),
                addFuzzySetsAsLinguisticVariableLabels(new CustomLinguisticVariable(PanLingVar.AnyAceOnStack, 0, 1), fuzzySets.AnyAceOnStack()),
                addFuzzySetsAsLinguisticVariableLabels(new CustomLinguisticVariable(PanLingVar.IsOnlyAceLeftInHand, 0, 1), fuzzySets.IsOnlyAceLeftInHand()),
                addFuzzySetsAsLinguisticVariableLabels(new CustomLinguisticVariable(PanLingVar.Decision, 0, 1), fuzzySets.Decisions()),
            };

            lingVarList.First(lingVar => lingVar.Name == PanLingVar.AIDifficulty.ToString()).
                NumericInput = (int)_aiDifficulty;
            lingVarList.First(lingVar => lingVar.Name == PanLingVar.NumberOfCardsInHand.ToString()).
                NumericInput = _aiCards.Count();
            lingVarList.First(lingVar => lingVar.Name == PanLingVar.IsStartCardInHand.ToString()).
                NumericInput = isStartCardInHand() ? 1 : 0;
            lingVarList.First(lingVar => lingVar.Name == PanLingVar.IsAnyCardFromHandShouldBeAlreadyOnStack.ToString()).
                NumericInput = isAnyCardFromHandShouldBeAlreadyOnStack() ? 1 : 0;
            lingVarList.First(lingVar => lingVar.Name == PanLingVar.IsAnyFromRestCardShouldBeAlreadyOnStack.ToString()).
                NumericInput = isAnyFromRestCardShouldBeAlreadyOnStack() ? 1 : 0;
            lingVarList.First(lingVar => lingVar.Name == PanLingVar.AnyPutMoveIsAvailable.ToString()).
                NumericInput = anyPutMoveIsAvailable() ? 1 : 0;
            lingVarList.First(lingVar => lingVar.Name == PanLingVar.AnyAceOnStack.ToString()).
                NumericInput = anyAceOnStack() ? 1 : 0;
            lingVarList.First(lingVar => lingVar.Name == PanLingVar.IsOnlyAceLeftInHand.ToString()).
                NumericInput = isOnlyAceLeftInHand() ? 0 : 1;

            return lingVarList;
        }

        /// <summary>
        /// Method to check if stard card in hand
        /// </summary>
        /// <returns>Returning boolean</returns>
        private bool isStartCardInHand()
        {
            return _aiCards.Any(card => card.Figure == Figure.n9 && card.Suit == Suit.Heart);
        }

        /// <summary>
        /// Method to check if any enemy player card should be on stack
        /// </summary>
        /// <returns>Returning boolean</returns>
        private bool isAnyFromRestCardShouldBeAlreadyOnStack()
        {
            var status = false;
            var restCards = _deck.Where(deckCard => _aiCards.Any(aiCard => aiCard != deckCard)).ToList();

            restCards.ForEach(card =>
            {
                for (int i = 0; i < _stackCards.Count - 1; i++)
                {
                    if ((int)card.Figure < (int)_stackCards[i].Figure)
                        status = true;
                }
            });

            return status;
        }

        /// <summary>
        /// Method to check if any put move is available
        /// </summary>
        /// <returns>Returning boolean</returns>
        private bool anyPutMoveIsAvailable()
        {
            return _aiCards.Any(aiCard => (int)aiCard.Figure >= (int)_stackCards.Last().Figure);
        }

        /// <summary>
        /// Method to check if any card from hand should be on stack already
        /// </summary>
        /// <returns>Returning boolean</returns>
        private bool isAnyCardFromHandShouldBeAlreadyOnStack()
        {
            var status = false;

            _aiCards.ForEach(card =>
            {
                if ((int)card.Figure < (int)_stackCards[_stackCards.Count - 1].Figure)
                    status = true;
            });

            return status;
        }

        /// <summary>
        /// Method to check if any ace is on stack
        /// </summary>
        /// <returns>Returning boolean</returns>
        private bool anyAceOnStack()
        {
            return _stackCards.Any(stackCard => stackCard.Figure == Figure.Ace);
        }

        /// <summary>
        /// Method to check if any ace left in hand
        /// </summary>
        /// <returns>Returning boolean</returns>
        private bool isOnlyAceLeftInHand()
        {
            return _aiCards.Any(aiCard => aiCard.Figure != Figure.Ace);
        }

        #endregion

        #region Logic for draw/put cards

        /// <summary>
        /// Method to get available moves for expected AI Difficulty
        /// </summary>
        /// <returns>List of int how much card can put</returns>
        private List<int> getAvailableMoves(List<int> availableCardsMoves)
        {
            int maxCardsForCurrentMove;

            switch (_aiDifficulty)
            {
                case AIDifficulty.Easy:
                    maxCardsForCurrentMove = availableCardsMoves.First();
                    break;
                case AIDifficulty.Normal:
                    maxCardsForCurrentMove = availableCardsMoves.Count() > 1 ? availableCardsMoves[availableCardsMoves.Count() - 2] : availableCardsMoves.Last();
                    break;
                case AIDifficulty.Hard:
                    maxCardsForCurrentMove = availableCardsMoves.Last();
                    break;
                default:
                    throw new NotImplementedException();
            }

            return availableCardsMoves.Where(x => x <= maxCardsForCurrentMove).ToList();
        }

        #endregion
    }
}
