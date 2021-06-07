using System;
using System.Collections;
using UnityEngine;

namespace Tetris {
    public class Board: MonoBehaviour {
        public static int width = (int) Constants.WIDTH;
        public static int height = (int) Constants.HEIGHT;  
        static Transform[,] grid = new Transform[width, height];


        //Check if position is within left border
        public static bool LeftPosValid(Vector2 pos) {
            return (int) pos.x >= 0;
        }
        
        //Check if position is within right border
        public static bool RightPosValid(Vector2 pos) {
            return (int) pos.x < width;
        }

        //Check if position is inside the board
        public static bool InsideBoard(Vector2 pos) {
            bool result =   LeftPosValid(pos) &&
                            RightPosValid(pos) &&
                            (int) pos.y >= 0;
            return result;
        }

        //Get x and y of a vector as integer values
        static Transform getAtPos(Vector2 vector) {
            return grid[(int) vector.x, (int) vector.y];
        }

        //Checks if a piece is at a valid position
        public static bool IsValidPosition(Transform transform) {
            foreach(Transform child in transform) {
                Vector2 v = roundVec2(child.position);

                if(!InsideBoard(v)) 
                    return false;

                Transform t = getAtPos(v);
                if (t != null && t.parent != transform)
                    return false;
            }
            return true;
        }


        static Vector2 roundVec2(Vector2 v)
        {
            return new Vector2(Mathf.Round(v.x),
                            Mathf.Round(v.y));
        }

        //Shift rows down after deletion
        static void shiftRow(int y) {
            for (int x = 0; x < width; x++) {
                if (grid[x, y] != null) {
                    grid[x, y-1] = grid[x,y];
                    grid[x, y] = null;

                    grid[x, y-1].position += new Vector3(0, -1, 0);
                }
            }
        }

        //Saves the position of a piece into the board
        public static void UpdatePosition(Transform transform) {
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if(grid[x, y] != null) {
                        if(grid[x, y].parent == transform)
                            grid[x, y] = null;
                    }
                }
            }
            foreach (Transform child in transform) {
                try {
                    Vector2 v = roundVec2(child.position);
                    grid[(int) v.x, (int) v.y] = child;
                }
                catch {}
            }
        }

        //Shifts all the rows above a row
        static void shiftRowsAbove(int row) {
            for (int y = row; y < height; y++) {
                shiftRow(y);
            }
        }

        //Checks if a row is full
        static bool rowFull(int y) {
            for (int x = 0; x < width; x++) {
                if(grid[x, y] == null) return false;
            }
            return true;
        }


        //Deletes all full rows in the board
        public static int deleteFullRows() {
            int rows = 0;
            float lastTime = Time.time;
            for(int y = height-1; y >= 0; y--) {
                if (rowFull(y))
                {
                    DeleteRow(y);
                    rows++;
                    lastTime = Time.time;
                }
            }
            return rows;
        }

        //Delete a row and shift all rows above this row
        public static void DeleteRow(int y) {
            for (int x = 0; x < width; x++) {
                try {
                    Destroy(grid[x, y].gameObject);
                }
                catch {
                    Console.WriteLine(String.Format("Destroy failed at {0}, {1}", x, y));
                }
                grid[x, y] = null;
            }
            shiftRowsAbove(y+1);
        }

        //Delete all pieces in the board
        public static void DeleteAll() {
            for(int y = 0; y < height; y++) {
                DeleteRow(y);
            }
        }


    }

}