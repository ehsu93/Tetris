namespace Tetris {
    public class ScoreBoard {
        
        int score;
        int scorePerRow;
        int totalRowsCleared;
        int toNextLevel;
        int levelThreshold;

        public string TotalRowsCleared {
            get  {return "Rows :" + this.totalRowsCleared.ToString(); }
        }

        public string Score {
            get {   return "Score :" + score.ToString(); }
        }

        public int ScoreThreshold {
            get {return ScoreThreshold; }
            set {levelThreshold = value; }
        }
        
        public ScoreBoard() {
            this.score = 0;
            this.toNextLevel = 0;
            this.scorePerRow = Constants.SCORE_PER_ROW;
            this.levelThreshold = Constants.LEVEL_THRESHOLD;
        }

        public bool UpdateScore(int level) {
            this.score += (this.scorePerRow * level) * Global.RowsCleared;
            this.toNextLevel += Global.RowsCleared;
            totalRowsCleared += Global.RowsCleared;
            Global.RowsCleared = 0;
            if(this.toNextLevel >= this.levelThreshold) {
                this.toNextLevel -= this.levelThreshold;
                return level < Constants.MAX_LEVEL ? true : false;
            }
            return false;
        }

        public void ResetScore() {
            score = 0;
        }

    }
}