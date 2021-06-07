using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris {
    public class Piece : MonoBehaviour {
        internal float lastFall = 0;

        float prevMove = 0;
        float moveDelay = 0.1f; 
    
        internal Dictionary<String, Vector3> directions = new Dictionary<String, Vector3>{
            {Constants.LEFT, new Vector3(-Constants.MOVE_SPEED, 0, 0)},
            {Constants.RIGHT, new Vector3(Constants.MOVE_SPEED, 0, 0)},
            {Constants.DOWN, new Vector3(0, -Constants.MOVE_SPEED, 0)}
        };

        int moveDown() {
            int rowsCleared = 0;
            transform.position += directions[Constants.DOWN];
            if(!Board.IsValidPosition(transform)) {
                rowsCleared = endFall();
            }
            this.lastFall = Time.time;
            return rowsCleared;
        }

        //Move to the left or right
        void moveHori(String dir) {
            transform.position += directions[dir];
            if(!Board.IsValidPosition(transform)) {
                transform.position -= directions[dir];
            }
        }

        //Move left/right/down
        public int Move(String dir) {
            if(Time.time - prevMove >= moveDelay) {
                prevMove = Time.time;
                if(dir == Constants.DOWN) {
                    return moveDown();
                }
                else {
                    moveHori(dir);
                    return 0;
                }
            }
            return 0;
        }

        //Check if any rows need to be deleted, set the flags to spawn new block, allow for block storing, and set how many rows have been cleared
        int endFall() { 
            tag = Constants.INACTIVE_PIECE;

            transform.position -= directions[Constants.DOWN];
            enabled = false;
            Global.SpawnNew = true;
            Board.UpdatePosition(transform);
            int rowsCleared = Board.deleteFullRows();
            Global.RowsCleared = rowsCleared;
            Global.Swapped = false;
            return rowsCleared > 0 ? rowsCleared : -1;
        }

        //Drop to the lowest valid position
        public void Drop() {
            while(Board.IsValidPosition(transform)) {
                transform.position += directions[Constants.DOWN];
            }
            endFall();
        }

        //Kick to the left or right while rotating
        internal void wallKick() {
            moveHori(Constants.RIGHT);
            moveHori(Constants.LEFT);
        }

        public void MoveIfTime() {
            if(Time.time - this.lastFall >= Global.FallRate) {
                moveDown();
            }
        }

        void storePiece() {
            if(!Global.Swapped) {
                tag = Constants.INACTIVE_PIECE;
                StoredPiece storeTo = GameObject.FindGameObjectWithTag(Constants.STORE_PIECE).GetComponent<StoredPiece>();
                PieceSpawner spawner = FindObjectOfType<PieceSpawner>();
                GameObject newPiece = storeTo.StowedPiece;
                storeTo.StowedPiece = gameObject;
                transform.rotation = Quaternion.identity;
                transform.position = storeTo.Location;    
                enabled = false;
                //Board.UpdatePosition(transform);
                if(newPiece == null)    
                    spawner.SpawnNext();
                else
                    spawner.SpawnPiece(newPiece);    
                Global.Swapped = true;
            }           
        }

        
        public void ActivateSelf() {
            tag = Constants.ACTIVE_PIECE;
            //Vector3 location = new Vector3(5f, 14f, 0f);//FindObjectOfType<PieceSpawner>().transform.position;
            //transform.position = location;
            enabled = true;
            if(!Board.IsValidPosition(transform)) {
                Global.GameOver = true;
                Global.SpawnNew = false;
                Destroy(gameObject);
            }

        }

        public void Start() {
            tag = Constants.ACTIVE_PIECE;
            //Vector3 location = new Vector3(5f, 14f, 0f);//FindObjectOfType<PieceSpawner>().transform.position;
            //transform.position = location;
            enabled = true;
            if(!Board.IsValidPosition(transform)) {
                Global.GameOver = true;
                Global.SpawnNew = false;
                Destroy(gameObject);
            }

        }

        public void Update() {
            MoveIfTime();
            if (Input.GetKey(Constants.LEFT_KEY))            Move(Constants.LEFT);
            else if (Input.GetKey(Constants.RIGHT_KEY))      Move(Constants.RIGHT);
            else if (Input.GetKey(Constants.DOWN_KEY))       Move(Constants.DOWN);
            else if (Input.GetKeyDown(Constants.DROP))           Drop();
            else if (Input.GetKeyDown(Constants.STORE))          storePiece();
        }
    }
}