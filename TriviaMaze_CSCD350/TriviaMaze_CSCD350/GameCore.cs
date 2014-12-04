﻿/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - GameCore, hooks our main window to the game
 * so main window isnt coupled with anything our game does.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Windows.Markup;

namespace TriviaMaze_CSCD350 {

    public class GameCore {
        private Maze maze;
        private IDoor curDoor;
        private String clickDirection;
        private MainWindow mainWindow;

        //=====================================================================
        
        public GameCore(MainWindow mainWindow) {
            GetDifficulty();
            this.mainWindow = mainWindow;
        }

        //=====================================================================

        private void GetDifficulty() {
            DifficultyInput difficultyInput = new DifficultyInput(this);
            difficultyInput.Show();
        }

        //=====================================================================

        public void InitializeMaze(int difficulty) {
            MazeBuilderDirector director = new MazeBuilderDirector();
            this.maze = director.Construct(difficulty);
            this.mainWindow.SubscribeToMaze();
        }

        //=====================================================================
        
        public IDoor GetCurDoor() {
            return this.curDoor;
        }

        //=====================================================================
        // Not a normal set, when the curdoor is set it also sets
        // the rooms door as well and to do that you need to find the entered
        // from direction and the direction you click.
        public void SetCurDoor(IDoor door){
            this.curDoor = door;

            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();


            if(this.clickDirection.Equals("right")){
                if (dirEntered == 'n') {
                    this.maze.GetCurRoom().SetWDoor(door);//#
                }
                else if (dirEntered == 'w') {
                    this.maze.GetCurRoom().SetSDoor(door);//#
                }
                else if (dirEntered == 's') {
                    this.maze.GetCurRoom().SetEDoor(door);//#
                }
                else if (dirEntered == 'e') {
                    this.maze.GetCurRoom().SetNDoor(door);//#
                }
                else {
                    Console.WriteLine("*Error* - setdoor");
                }
            }
            else if (this.clickDirection.Equals("center")) {
                if (dirEntered == 'n') {
                    this.maze.GetCurRoom().SetSDoor(door);//#
                }
                else if (dirEntered == 'w') {
                    this.maze.GetCurRoom().SetEDoor(door);//#
                }
                else if (dirEntered == 's') {
                    this.maze.GetCurRoom().SetNDoor(door);//#
                }
                else if (dirEntered == 'e') {
                    this.maze.GetCurRoom().SetWDoor(door);//#
                }
            }
            else if (this.clickDirection.Equals("left")) {
                if (dirEntered == 'n') {
                    this.maze.GetCurRoom().SetEDoor(door);//#
                }
                else if (dirEntered == 'w') {
                    this.maze.GetCurRoom().SetNDoor(door);//#
                }
                else if (dirEntered == 's') {
                    this.maze.GetCurRoom().SetWDoor(door);//#
                }
                else if (dirEntered == 'e') {
                    this.maze.GetCurRoom().SetSDoor(door);//#
                }
            }
            else if (this.clickDirection.Equals("back")) {
                if (dirEntered == 'n') {
                    this.maze.GetCurRoom().SetNDoor(door);//#
                }
                else if (dirEntered == 'w') {
                    this.maze.GetCurRoom().SetWDoor(door);//#
                }
                else if (dirEntered == 's') {
                    this.maze.GetCurRoom().SetSDoor(door);//#
                }
                else if (dirEntered == 'e') {
                    this.maze.GetCurRoom().SetEDoor(door);//#
                }
            }
            else {
                Console.WriteLine("*Error* - setdoor");
            }
        }

        //=====================================================================
        
        public bool RightDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();
            this.clickDirection = "right";

            if (dirEntered == 'n') {
                this.curDoor = this.maze.GetCurRoom().GetWDoor();
                return this.maze.MoveWest();
            } else if (dirEntered == 'w') {
                this.curDoor = this.maze.GetCurRoom().GetSDoor();
                return this.maze.MoveSouth();
            } else if (dirEntered == 's') {
                this.curDoor = this.maze.GetCurRoom().GetEDoor();
                return this.maze.MoveEast();
            } else if (dirEntered == 'e') {
                this.curDoor = this.maze.GetCurRoom().GetNDoor();
                return this.maze.MoveNorth();
            }
            return false;
        }

        //=====================================================================
        
        public bool CenterDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();
            this.clickDirection = "center";

            if (dirEntered == 'n') {
                this.curDoor = this.maze.GetCurRoom().GetSDoor();
                return this.maze.MoveSouth();
            } else if (dirEntered == 'w') {
                this.curDoor = this.maze.GetCurRoom().GetEDoor();
                return this.maze.MoveEast();
            } else if (dirEntered == 's') {
                this.curDoor = this.maze.GetCurRoom().GetNDoor();
                return this.maze.MoveNorth();
            } else if (dirEntered == 'e') {
                this.curDoor = this.maze.GetCurRoom().GetWDoor();
                return this.maze.MoveWest();
            }
            return false;
        }

        //=====================================================================
        
        public bool LeftDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();
            this.clickDirection = "left";

            if (dirEntered == 'n') {
                this.curDoor = this.maze.GetCurRoom().GetEDoor();
                return this.maze.MoveEast();
            } else if (dirEntered == 'w') {
                this.curDoor = this.maze.GetCurRoom().GetNDoor();
                return this.maze.MoveNorth();
            } else if (dirEntered == 's') {
                this.curDoor = this.maze.GetCurRoom().GetWDoor();
                return this.maze.MoveWest();
            } else if (dirEntered == 'e') {
                this.curDoor = this.maze.GetCurRoom().GetSDoor();
                return this.maze.MoveSouth();
            }
            return false;
        }

        //=====================================================================
        
        public bool BackDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();
            this.clickDirection = "back";

            if (dirEntered == 'n') {
                this.curDoor = this.maze.GetCurRoom().GetNDoor();
                return this.maze.MoveNorth();
            } else if (dirEntered == 'w') {
                this.curDoor = this.maze.GetCurRoom().GetWDoor();
                return this.maze.MoveWest();
            } else if (dirEntered == 's') {
                this.curDoor = this.maze.GetCurRoom().GetSDoor();
                return this.maze.MoveSouth();
            } else if (dirEntered == 'e') {
                this.curDoor = this.maze.GetCurRoom().GetEDoor();
                return this.maze.MoveEast();
            }
            return false;
        }

        //=====================================================================
        //help for serialization found at: http://msdn.microsoft.com/en-us/library/et91as27.aspx
        public bool LoadGame(String fileName) {
            String path = @"..\..\SaveFiles\" + fileName + ".bin";
            if (File.Exists(path)) {
                Stream fileStream = File.OpenRead(path);
                BinaryFormatter deserializer = new BinaryFormatter();
                this.maze = (Maze)deserializer.Deserialize(fileStream);
                fileStream.Close();
                return true;
            }
            return false;
        }

        //=====================================================================
        
        public void UpdateMazeView() {

            this.maze.Update();
        }

        //=====================================================================
        //help for serialization found at: http://msdn.microsoft.com/en-us/library/et91as27.aspx
        public void SaveGame(String fileName) {
            String path = @"..\..\SaveFiles\" + fileName + ".bin";

            Stream fileStream = File.Create(path);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(fileStream, this.maze);
            fileStream.Close();
        }

        //=====================================================================
        //should only ever be used for subscription purposes
        public Maze GetMaze() {
            return this.maze;
        }
    }
}
