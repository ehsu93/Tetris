using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris {
    public class GameController: MonoBehaviour {
        int level;

        ScoreBoard scoreBoard;
        public Text scoreText;


        public Text levelText;


        public Text totalRowsText;
        

        int score = 0;
        PieceSpawner spawner;

        GameObject activePiece;

        string getLevel() {
            int result = this.level;
            return "Level: " + result.ToString();
        }
        void Start() {
            this.level = 1;

            this.levelText.text = getLevel();
            this.scoreBoard = new ScoreBoard();
            //spaw new piece
            spawner = FindObjectOfType<PieceSpawner>();
            spawner.SpawnNext();
            //scoreText.text = scoreBoard.Score.ToString();
            scoreText.text = scoreBoard.Score;
            totalRowsText.text = scoreBoard.TotalRowsCleared;

        }
 
        void Update() {
            if(Global.SpawnNew && !Global.GameOver) {
                spawner.SpawnNext();

                bool newLevel = scoreBoard.UpdateScore(this.level);
                if(newLevel) {
                    this.level += 1;
                    Global.FallRate -= Constants.INCREASE_SPEED;
                }
            }
            this.levelText.text = getLevel();
            scoreText.text = scoreBoard.Score;
            totalRowsText.text = Global.GameOver ? "GAME OVER" : scoreBoard.TotalRowsCleared;
        }
        
    }

}