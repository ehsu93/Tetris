using UnityEngine;
using System.Collections.Generic;

namespace Tetris {
    public class Ghost : Piece {
        GameObject toFollow;

        void followActivePiece() {
            if(toFollow != null) {
                transform.position = toFollow.transform.position;
                transform.rotation = toFollow.transform.rotation;
            }
        }

        new void Drop() {
            while(Board.IsValidPosition(transform)) {
                transform.position += directions[Constants.DOWN];
            }
            transform.position -= directions[Constants.DOWN];
        }

        public new void Start() {
            toFollow = GameObject.FindGameObjectWithTag(Constants.ACTIVE_PIECE);
            tag = Constants.ACTIVE_GHOST;
            followActivePiece();
            Drop();
        }

        public new void Update() {
            followActivePiece();
            Drop();
        }
    }
}