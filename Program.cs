﻿using System;
using Kroz.Classes;
using static System.Environment;
using static System.Console;
using System.Collections.Generic;

namespace Kroz
{
    class Program
    {
        static void Main(string[] args)
        {

            //using static Environment
            // Setup player and enemies

            WriteLine("New player, please enter your name.");
            string NewPlayerName = ReadLine();
            var Player = new Player(NewPlayerName);
            WriteLine("Welcome " + Player.GetName());
            var Goblin = new Enemy("Goblin", 50, 0);
            var Troll = new Enemy("Troll", 150, 0);
            var DarkWizard = new Enemy("Dark Wizard", 100, 100);
            var Wraith = new Enemy("Wraith", 150, 150);
            var currentEnemy = Goblin;
            var currentPlayer = Player;

            Random rand = new Random();

            int playerRoll, enemyRoll;

            // Create locations 



            Location Cell = new Location("Cell", "A dark gloomy room with a heavy wooden door.", true, null);
            Location GuardRoom = new Location("GuardRoom", "A Guardroom.", true, null);
            Location Room3 = new Location("Room3", "A room numbered 3.", false, Wraith);
            Location Room4 = new Location("Room4", "A room numbered 4.", false, Goblin);


            //var locationStore = new Dictionary<string, Location>
            //{
            //    {"Cell", Cell},
            //    {"GuardRoom", Cell},
            //    // todo: complete this
            //};

            //Cell.AddNorthRoom("GuardRoom");
            
            // Link locations

            //locationStore.TryGetValue("Cell" , out var location)
            //    {
            //    location.north = 
            //}

            Cell.north = GuardRoom;
            GuardRoom.south = Cell;

            GuardRoom.west = Room3;
            Room3.east = GuardRoom;

            GuardRoom.east = Room4;
            Room4.west = GuardRoom;


            // Create items

            Items Key = new Items("Key", "A large rusted key", "Door", "You have unlocked the door!", true, false);
            Items Door = new Items("Door", "A locked heavy oak door", "Key", "The door was unlocked!", false, true);
            Items Three = new Items("3", "Test item 3", null, "The door was unlocked!", true, true);
            Items Four = new Items("4", "Test item 4", null, "The door was unlocked!", true, true);


            // populate the locations with the items

            Cell.AddToLocation(Key);
            Cell.AddToLocation(Door);
            Cell.AddToLocation(Three);
            GuardRoom.AddToLocation(Four);

            // Initialise, set and describe initial location
            Location currentLocation;
            Location previousLocation;
            currentLocation = Cell;
            currentLocation.DescribeLocation(currentLocation);
            //currentLocation.ListLocationItems();

            int playerHealth()
            {
                return currentPlayer.GetHealth(currentPlayer);
            };

            int enemyHealth()
            {
                return currentEnemy.GetHealth(currentEnemy);
            };

            int RollDice(int maxValue)
            {
                return rand.Next(1, maxValue);
            };

            void Potion(int healthIncrease)
            {
                currentPlayer.Heal(currentPlayer, healthIncrease);
            }

            void Encounter()
            {
                void PhysicalAttack()
                {
                    WriteLine("Press a key to roll the dice and attack!");
                    WriteLine("If you roll higher than the enemy rolls, your strike is successful");
                    ReadKey();
                    WriteLine();
                    playerRoll = RollDice(6);
                    enemyRoll = RollDice(6);

                    // string playerChoices = "";
                    void RollResult()
                    {
                        WriteLine($"{currentPlayer.GetName()} has rolled {playerRoll}, the {currentLocation.GetLocationEnemy(currentLocation).GetName()} has rolled {enemyRoll}");
                    };

                    void HealthDisplay()
                    {
                        WriteLine($"The {currentPlayer.GetName()} now has {playerHealth()} HP");
                        WriteLine($"The {currentLocation.GetLocationEnemy(currentLocation).GetName()} now has {enemyHealth()} HP");
                    };

                    if (playerRoll > enemyRoll)
                    {
                        RollResult();
                        WriteLine($"{currentPlayer.GetName()} wins the roll, your attack was successful!");
                        WriteLine("Hit a key to roll a D20 for damage amount");
                        int playerDamageRoll = RollDice(20);
                        WriteLine($"{currentLocation.GetLocationEnemy(currentLocation).GetName()}'s health has been reduced by {playerDamageRoll} HP");
                        currentEnemy.TakeHealth(currentEnemy, playerDamageRoll);
                        HealthDisplay();
                    }

                    else if (playerRoll < enemyRoll)
                    {
                        RollResult();
                        WriteLine($"{currentLocation.GetLocationEnemy(currentLocation).GetName()} wins the roll, your attack was blocked and the {currentLocation.GetLocationEnemy(currentLocation).GetName()} strikes back!");
                        WriteLine($"The {currentLocation.GetLocationEnemy(currentLocation).GetName()} rolls a D12 for damage");
                        int enemyDamageRoll = RollDice(12);
                        WriteLine($"{currentPlayer.GetName()}'s health has been reduced by {enemyDamageRoll} HP");
                        currentPlayer.TakeHealth(currentPlayer, enemyDamageRoll);
                        HealthDisplay();
                    }

                    else
                    {
                        WriteLine("Your weapons clash against each other!");
                    }

                }
                WriteLine($"You encounter a {currentLocation.GetLocationEnemy(currentLocation).GetName()}");

                do // Encounter Loop
                {
                    WriteLine
                        (
                        "Pick an action" + NewLine +
                        "Strike - S" + NewLine +
                        "Inventory - I" + NewLine +
                        "Use - U" + NewLine +
                        "Escape - E"
                        );
                    string encounterCommand = ReadLine().ToLower();

                    switch (encounterCommand)
                    {
                        case "s":
                            PhysicalAttack();
                            break;
                        case "i":
                            break;
                        case "u":
                            break;
                        case "e":

                            break;
                        default:
                            WriteLine("Please choose a valid command!");
                            break;

                    }

                } while (enemyHealth() > 0 && playerHealth() > 0);

            }

            // Game loop

            while (true)
            { 
                if (!currentLocation.GetEnemyDefeated())
                {
                    currentEnemy = currentLocation.GetLocationEnemy(currentLocation);
                    WriteLine(currentEnemy.GetName());
                    WriteLine(currentLocation.GetLocationEnemy(currentLocation).GetName());
                    Encounter();
                    currentLocation.SetEnemyDefeated();
                }


                WriteLine("What would you like to do?\n" +
                    "L = Look" + NewLine +
                    "T = Take" + NewLine +
                    "U = Use" + NewLine +
                    "M = Move");
                string command = ReadLine();
                
                switch (command)
                {
                    case "Look":
                    case "look":
                    case "L":
                    case "l":
                        {
                            WriteLine("You search the room and find:");
                            currentLocation.ListLocationItems();
                            break;
                        }
                    case "Take":
                    case "take":
                    case "T":
                    case "t":
                        {   if (currentLocation.GetCount() >= 1)
                            {
                                WriteLine("Which item would you like to pick up?");
                                string itemChoice = ReadLine().ToLower();
                                Player.AddToInventory(currentLocation.Take(itemChoice));
                                break;
                            }
                            else
                            {
                                WriteLine("This room is empty of items!");
                                break;
                            }
                        }
                    case "Use":
                    case "use":
                    case "U":
                    case "u":
                        {   
                            if (Player.GetCount() > 0)
                            {   
                                Player.UseItem();
                                break;
                            }
                            else
                            {
                                WriteLine("Your inventory is empty!");
                                break;
                            }
                        }
                    case "Move":
                    case "move":
                    case "M":
                    case "m":
                        {
                            WriteLine("Which direction would you like to go?");
                            WriteLine("Exits: {0}{1}{2}{3}",
                                currentLocation.north == null ? "" : "North ",
                                currentLocation.east == null ? "" : "East ",
                                currentLocation.south == null ? "" : "South ",
                                currentLocation.west == null ? "" : "West");
                            string TravelDirection = ReadLine();
                            switch (TravelDirection)
                            {
                                case "North":
                                case "north":
                                case "N":
                                case "n":
                                    if (currentLocation.north != null)
                                        currentLocation = currentLocation.north;
                                    break;

                                case "East":
                                case "east":
                                case "E":
                                case "e":
                                    if (currentLocation.east != null)
                                        currentLocation = currentLocation.east;
                                    break;

                                case "South":
                                case "south":
                                case "S":
                                case "s":
                                    if (currentLocation.south != null)
                                        currentLocation = currentLocation.south;
                                    break;

                                case "West":
                                case "west":
                                case "W":
                                case "w":
                                    if (currentLocation.west != null)
                                        currentLocation = currentLocation.west;
                                    break;

                                case "Up":
                                case "up":
                                case "U":
                                case "u":
                                    if (currentLocation.up != null)
                                        currentLocation = currentLocation.up;
                                    break;

                                case "Down":
                                case "down":
                                case "D":
                                case "d":
                                    if (currentLocation.down != null)
                                        currentLocation = currentLocation.down;
                                    break;

                                default:
                                    WriteLine("Please choose a valid direction!");
                                    break;
                            }
                            currentLocation.DescribeLocation(currentLocation);
                            break;
                        }
                    default:
                        WriteLine("Please choose a valid action!");
                        break;
                }    
            }
        }
    }
}