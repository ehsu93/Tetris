using UnityEngine;

namespace Tetris {
    public static class Constants {
        public static float WIDTH = 10f;
        public static float HEIGHT = 20f;
        public static int SCORE_PER_ROW = 100;
        public static string LEFT = "left";
        public static string RIGHT = "right";
        public static string DOWN = "down";

        public static KeyCode UP_KEY = KeyCode.UpArrow;
        public static KeyCode LEFT_KEY = KeyCode.LeftArrow;
        public static KeyCode RIGHT_KEY = KeyCode.RightArrow;
        public static KeyCode ROTATE = KeyCode.UpArrow;
        public static KeyCode DOWN_KEY = KeyCode.DownArrow;
        public static KeyCode DROP = KeyCode.Space;
        public static KeyCode STORE = KeyCode.LeftShift;

        public static float MOVE_SPEED = 1f;
        public static float INCREASE_SPEED = 0.0667f;
        public static int LEVEL_THRESHOLD = 10;
        public static int NEXT_PIECES = 5;
        public static int MAX_LEVEL = 15;

        //Tags 
        public static string ACTIVE_PIECE = "Active Piece";
        public static string INACTIVE_PIECE = "Inactive Piece";
        public static string ACTIVE_GHOST = "Active Ghost";
        public static string INACTIVE_GHOST = "Inactive Ghost";
        public static string DISPLAY_PIECE = "Display Piece";
        public static string STORE_PIECE = "Store Piece";
    }
}

