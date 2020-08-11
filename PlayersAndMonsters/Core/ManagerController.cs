namespace PlayersAndMonsters.Core
{
    using System;
    using System.Text;
    using Contracts;
    using PlayersAndMonsters.Common;
    using PlayersAndMonsters.Core.Factories;
    using PlayersAndMonsters.Core.Factories.Contracts;
    using PlayersAndMonsters.Models.BattleFields;
    using PlayersAndMonsters.Models.BattleFields.Contracts;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using PlayersAndMonsters.Models.Players.Contracts;
    using PlayersAndMonsters.Repositories;
    using PlayersAndMonsters.Repositories.Contracts;

    public class ManagerController : IManagerController
    {
        private readonly IPlayerRepository playerRepository; 
        private readonly ICardRepository cardRepository; 

        private readonly IBattleField battleField;
        private readonly IPlayerFactory playerFactory;
        private readonly ICardFactory cardFactory;
        public ManagerController()
        {
            this.playerRepository = new PlayerRepository();
            this.cardRepository = new CardRepository();
            this.battleField = new BattleField();
            this.playerFactory = new PlayerFactory();
            this.cardFactory = new CardFactory();
        }

        public string AddPlayer(string type, string username)
        {
            IPlayer player = this.playerFactory.CreatePlayer(type, username);
            this.playerRepository.Add(player);
            string msg = string.Format(ConstantMessages.SuccessfullyAddedPlayer, type, username);
            return msg;
        }

        public string AddCard(string type, string name)
        {
            ICard card = this.cardFactory.CreateCard(type, name);
            this.cardRepository.Add(card);
            string msg = string.Format(ConstantMessages.SuccessfullyAddedCard, type, name);
            return msg;
        }

        public string AddPlayerCard(string username, string cardName)
        {
            IPlayer player = this.playerRepository.Find(username);
            ICard card = this.cardRepository.Find(cardName);
            player.CardRepository.Add(card);
            string msg = string.Format(ConstantMessages.SuccessfullyAddedPlayerWithCards, cardName, username);
            return msg;
        }

        public string Fight(string attackUser, string enemyUser)
        {
            IPlayer attacker = this.playerRepository.Find(attackUser);
            IPlayer enemy = this.playerRepository.Find(enemyUser);
            this.battleField.Fight(attacker, enemy);
            string msg = string.Format(ConstantMessages.FightInfo, attacker.Health, enemy.Health);
            return msg;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IPlayer player in this.playerRepository.Players)
            {
                string msg = string.Format(ConstantMessages.PlayerReportInfo, player.Username, player.Health, player.CardRepository.Count);
                sb.AppendLine(msg);
                foreach (ICard card in player.CardRepository.Cards)
                {
                    string msg2 = string.Format(ConstantMessages.CardReportInfo, card.Name, card.DamagePoints);
                    sb.AppendLine(msg2);
                }
                sb.AppendLine(ConstantMessages.DefaultReportSeparator);
            }

            return sb.ToString().TrimEnd();
        }
    }
}

