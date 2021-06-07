using UnityEngine;

    namespace Tetris {
    public class StoredPiece: MonoBehaviour {
        GameObject storedPiece;
        Vector3 location;

        public GameObject StowedPiece {
            get {   return storedPiece;     }
            set {   
                    if(storedPiece != null) Destroy(storedPiece);
                    storedPiece = value;    
                }
        }

        public Vector3 Location {
            get {   return location;    }
        }

        void Start() {
            location = transform.position;
        }
    }

}