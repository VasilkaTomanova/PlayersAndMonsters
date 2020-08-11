using PlayersAndMonsters.Core.Contracts;
using PlayersAndMonsters.IO;
using PlayersAndMonsters.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayersAndMonsters.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private IManagerController controller;
     
        public Engine()
        {
            this.reader = new Reader();
            this.writer = new Writer();
            this.controller = new ManagerController();
        }
        public void Run()
        {

            while (true)
            {
                string input = this.reader.ReadLine();
                if(input == "Exit")
                {
                    break;
                }
                try
                {
                    string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                    string command = tokens[0];
                    string resultMessage = string.Empty;
                    switch (command)
                    {
                        //AddPlayer {player type} {player username} 
                        case "AddPlayer":
                            string playerType = tokens[1];
                            string playerUsername = tokens[2];
                            resultMessage = this.controller.AddPlayer(playerType,playerUsername);
                            break;

                        //AddCard {card type} {card name}
                        case "AddCard":
                            string cardType = tokens[1];
                            string cardName = tokens[2];
                            resultMessage = this.controller.AddCard(cardType, cardName);
                            break;

                        //AddPlayerCard {username} {card name} 
                        case "AddPlayerCard":
                            string username = tokens[1];
                             cardName = tokens[2];
                            resultMessage = this.controller.AddPlayerCard(username, cardName);
                            break;

                        //Fight {attack user} {enemy user}
                        case "Fight":
                            string attacker = tokens[1];
                            string enemy = tokens[2];
                            resultMessage = this.controller.Fight(attacker, enemy);
                            break;

                        case "Report":
                            resultMessage = this.controller.Report();
                            break;
                    }
                   this.writer.WriteLine(resultMessage);

                }
                catch (Exception exceptionMessage)
                {
                     this.writer.WriteLine(exceptionMessage.Message);
                }    

            }

        }
    }
}
