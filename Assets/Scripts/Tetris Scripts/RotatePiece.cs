using UnityEngine;

namespace Tetris {
    public class RotatePiece: Piece {
        public void RotateSelf() {
            transform.Rotate(0, 0, -90);
            if(!Board.IsValidPosition(transform))
                wallKick();
            
            if(!Board.IsValidPosition(transform)) {
                transform.Rotate(0, 0, 90);
            }
        }

        public new void Update() {
            base.Update();
            if (Input.GetKeyDown(Constants.ROTATE))         RotateSelf();
        }
    }
}