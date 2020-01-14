﻿using System;
using Kroz.Classes;

namespace Kroz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("New player, please enter your name.");
            string NewPlayerName = Console.ReadLine();
            Player Player = new Player(NewPlayerName);
            Console.WriteLine("Welcome " + Player.PlayerName);

            Location Cell = new Location("Cell", "A dark gloomy room with a heavy wooden door." );
            //Console.WriteLine("Room " + EscapeRoom.LocationName + " has been created");
            Location GuardRoom = new Location("GuardRoom", "A Guardroom.");
            Location Room3 = new Location("Room3", "A room numbered 3.");
            Location Room4 = new Location("Room4", "A room numbered 4.");

            // Link Locations
            Cell.North = GuardRoom;
            GuardRoom.South = Cell;

            GuardRoom.West = Room3;
            Room3.East = GuardRoom;

            GuardRoom.East = Room4;
            Room4.West = GuardRoom;



            // Create items

            Items Key = new Items("Key", "A large rusted key", "Door", "You have unlocked the door!", true, false);
            Items Door = new Items("Door", "A locked heavy oak door", "Key", "The door was unlocked!", false, true);
            Items Three = new Items("3", "Test item 3", null, "The door was unlocked!", true, true);
            Items Four = new Items("4", "Test item 4", null, "The door was unlocked!", true, true);


            //Console.WriteLine("Item " + Key.ItemName + " has been created");


            // populate the locations with the items

            Cell.AddToLocation(Key);
            Cell.AddToLocation(Door);
            Cell.AddToLocation(Three);
            Cell.AddToLocation(Four);

            //Console.WriteLine("Your current location contains " + currentLocation.GetCount() + " items."); 

            // Initialise, set and describe initial location
            Location currentLocation; 
            currentLocation = Cell;
            currentLocation.DescribeLocation(currentLocation);
            //currentLocation.ListLocationItems();

            while (true)
            { 
                Console.WriteLine("What would you like to do?\n" +
                    "L = Look\n" +
                    "T = Take\n" +
                    "U = Use\n" +
                    "M = Move");
                string command = Console.ReadLine();
                
                switch (command)
                {
                    case "Look":
                    case "look":
                    case "L":
                    case "l":
                        {
                            Console.WriteLine("You search the room and find:");
                            currentLocation.ListLocationItems();
                            break;
                        }
                    case "Take":
                    case "take":
                    case "T":
                    case "t":
                        {   if (currentLocation.GetCount() >= 1)
                            {
                                Console.WriteLine("Which item would you like to pick up?");
                                string itemChoice = Console.ReadLine().ToLower();
                                Player.AddToInventory(currentLocation.Take(itemChoice));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("This room is empty of items!");
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
                                Console.WriteLine("Your inventory is empty!");
                                break;
                            }
                        }
                    case "Move":
                    case "move":
                    case "M":
                    case "m":
                        {
                            Console.WriteLine("Which direction would you like to go?");
                            Console.WriteLine("Exits: {0}{1}{2}{3}",
                                currentLocation.North == null ? "" : "North ",
                                currentLocation.East == null ? "" : "East ",
                                currentLocation.South == null ? "" : "South ",
                                currentLocation.West == null ? "" : "West");
                            string TravelDirection = Console.ReadLine();
                            switch (TravelDirection)
                            {
                                case "North":
                                case "north":
                                case "N":
                                case "n":
                                    if (currentLocation.North != null)
                                        currentLocation = currentLocation.North;
                                    break;

                                case "East":
                                case "east":
                                case "E":
                                case "e":
                                    if (currentLocation.East != null)
                                        currentLocation = currentLocation.East;
                                    break;

                                case "South":
                                case "south":
                                case "S":
                                case "s":
                                    if (currentLocation.South != null)
                                        currentLocation = currentLocation.South;
                                    break;

                                case "West":
                                case "west":
                                case "W":
                                case "w":
                                    if (currentLocation.West != null)
                                        currentLocation = currentLocation.West;
                                    break;

                                case "Up":
                                case "up":
                                case "U":
                                case "u":
                                    if (currentLocation.Up != null)
                                        currentLocation = currentLocation.Up;
                                    break;

                                case "Down":
                                case "down":
                                case "D":
                                case "d":
                                    if (currentLocation.Down != null)
                                        currentLocation = currentLocation.Down;
                                    break;

                                default:
                                    Console.WriteLine("Please choose a valid direction!");
                                    break;
                            }
                            currentLocation.DescribeLocation(currentLocation);
                            break;
                        }
                    default:
                        Console.WriteLine("Please choose a valid action!");
                        break;
                }    
            }
        }
    }
}