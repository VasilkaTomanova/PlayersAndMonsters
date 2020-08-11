using PlayersAndMonsters.Models.BattleFields.Contracts;
using PlayersAndMonsters.Models.Players.Contracts;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace PlayersAndMonsters.Models.BattleFields
{
    public class BattleField : IBattleField
    {
        private const int defaultHealtIncreasemtnPoints = 40;
        private const int defaultIncreasmetnOfDamagePoints = 30;
        public void Fight(IPlayer attackPlayer, IPlayer enemyPlayer)
        {
            if (attackPlayer.IsDead || enemyPlayer.IsDead)
            {
                throw new ArgumentException("Player is dead!");
            }

            if (attackPlayer.GetType().Name == "Beginner")
            {
                IncreasePointsMethod(attackPlayer);
            }
            if (enemyPlayer.GetType().Name == "Beginner")
            {
                IncreasePointsMethod(enemyPlayer);
            }

            attackPlayer.Health += attackPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);
            enemyPlayer.Health += enemyPlayer.CardRepository.Cards.Sum(x => x.HealthPoints);


            while (true)
            {
                int totalAtackDamage = attackPlayer.CardRepository.Cards.Sum(x => x.DamagePoints);

                if (enemyPlayer.Health - totalAtackDamage < 0)
                {
                    enemyPlayer.Health = 0;
                }
                else
                {
                    enemyPlayer.Health -= totalAtackDamage;
                }

                if (enemyPlayer.IsDead)
                {
                    break;
                }

                int totalEnemyDamage = enemyPlayer.CardRepository.Cards.Sum(x => x.DamagePoints);

                if (attackPlayer.Health - totalEnemyDamage < 0)
                {
                    attackPlayer.Health = 0;
                }
                else
                {
                    attackPlayer.Health -= totalEnemyDamage;
                }

                if (attackPlayer.IsDead)
                {
                    break;
                }
            }

        }


        private static void IncreasePointsMethod(IPlayer player)
        {
            player.Health += defaultHealtIncreasemtnPoints;
            player.CardRepository.Cards.Select(x => x.DamagePoints += defaultIncreasmetnOfDamagePoints).ToList();

        }
    }
}
