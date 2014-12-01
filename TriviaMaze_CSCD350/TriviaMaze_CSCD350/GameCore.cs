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

    class GameCore {
        private Maze maze;
        private IDoor curDoor;

        public GameCore() {
            MazeBuilderDirector director = new MazeBuilderDirector();
            
            this.maze = director.Construct();
        }

        public IDoor GetcurDoor() {
            return this.curDoor;
        }

        //Change this at COMMENT
        public void SetcurDoor(IDoor door){
            this.curDoor = door;
            this.maze.GetCurRoom().SetEDoor(door);//#
            this.maze.GetCurRoom().SetWDoor(door);//#
            this.maze.GetCurRoom().SetNDoor(door);//#
            this.maze.GetCurRoom().SetSDoor(door);//#
        }

        public bool RightDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();

            if (dirEntered == 'n') {
                this.curDoor=this.maze.GetCurRoom().GetWDoor();
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

        public bool CenterDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();

            if (dirEntered == 'n') {
                return this.maze.MoveSouth();
            } else if (dirEntered == 'w') {
                return this.maze.MoveEast();
            } else if (dirEntered == 's') {
                return this.maze.MoveNorth();
            } else if (dirEntered == 'e') {
                return this.maze.MoveWest();
            }
            return false;
        }

        public bool LeftDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();

            if (dirEntered == 'n') {
                return this.maze.MoveEast();
            } else if (dirEntered == 'w') {
                return this.maze.MoveNorth();
            } else if (dirEntered == 's') {
                return this.maze.MoveWest();
            } else if (dirEntered == 'e') {
                return this.maze.MoveSouth();
            }
            return false;
        }

        public bool BackDoorClick() {
            char dirEntered = this.maze.GetCurRoom().GetEnteredFrom();

            if (dirEntered == 'n') {
                return this.maze.MoveNorth();
            } else if (dirEntered == 'w') {
                return this.maze.MoveWest();
            } else if (dirEntered == 's') {
                return this.maze.MoveSouth();
            } else if (dirEntered == 'e') {
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

        //should only ever be used for subscription purposes
        public Maze GetMaze() {
            return this.maze;
        }
    }
}
