using UnityEngine;

namespace Tetris {
    public class FlipPiece : Piece {
        bool flipped = false;
        public void RotateSelf() {
            var flipAngle = flipped ? -90 : 90;
            transform.Rotate(0, 0, flipAngle);
            if(!Board.IsValidPosition(transform))
            wallKick();
            
            if(!Board.IsValidPosition(transform)) {
                transform.Rotate(0, 0, -flipAngle);
            }
        }

        new void Start() {
            base.Start();
        }

        new void Update() {
            base.Update();
            if (Input.GetKeyDown(Constants.ROTATE))         RotateSelf();

        }
    }
}