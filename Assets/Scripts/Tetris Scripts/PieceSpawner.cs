using UnityEngine;
using System.Collections.Generic;

namespace Tetris {
    public class PieceSpawner : MonoBehaviour {
        public GameObject[] Pieces;
        public GameObject[] Ghosts;
        internal Queue<GameObject> nextPieces;   
        Queue<GameObject> nextGhosts;

        //Spawn a specific piece.
        public void SpawnPiece(GameObject piece) {
            GameObject toSpawn = piece;
            clearGhost();
            int index = 0;
            for(int i = 0; i < Pieces.Length; i++) {
                if(piece.name.Contains(Pieces[i].name)) {
                    toSpawn = Pieces[i];
                    index = i;
                    break;
                }
            }
            GameObject newPiece = Instantiate(  toSpawn,
                                                transform.position,
                                                Quaternion.identity);
            newPiece.GetComponent<Piece>().ActivateSelf();

            GameObject newGhost = Instantiate(  Ghosts[index],
                                                transform.position,
                                                Quaternion.identity);
        }

        //Clear previous ghost so there's no overlap
        void clearGhost() {
            GameObject toDestroy = GameObject.FindGameObjectWithTag(Constants.ACTIVE_GHOST);
            if(toDestroy != null) {
                toDestroy.tag = Constants.INACTIVE_GHOST;
                Destroy(toDestroy);
            }
        }

        //
        void setPreview() {
            //store a piece from the top of the queue into StoredPiece (display piece)
            StoredPiece preview = GameObject.FindGameObjectWithTag(Constants.DISPLAY_PIECE).GetComponent<StoredPiece>();
            GameObject nextPiece = Instantiate (    nextPieces.Dequeue(),
                                                    preview.transform.position,
                                                    Quaternion.identity);
            nextPiece.GetComponent<Piece>().enabled = false;
            nextPiece.GetComponent<Piece>().tag = Constants.DISPLAY_PIECE;
            preview.StowedPiece = nextPiece;
            if(nextPieces.Count <= 3) {
                populateNextPieces();
            }
            
        }

        //
        public void SpawnNext() {
            clearGhost();
            GameObject newPiece = GameObject.FindGameObjectWithTag(Constants.DISPLAY_PIECE).GetComponent<StoredPiece>().StowedPiece;
            SpawnPiece(newPiece);
            setPreview();
            

            Global.SpawnNew = false;
        }

        //Generates a random seequence from the range zero to Pieces.Length-1
        int[] randomSequence() {
            int[] result = new int[Pieces.Length];
            List<int> possNumbers = new List<int>();
            
            for(int i = 0; i < Pieces.Length; i++) {
                possNumbers.Add(i);
            }
            for(int i = 0; i < Pieces.Length; i++) {
                int nextInd = Random.Range(0, possNumbers.Count);
                int nextNo = possNumbers[nextInd];
                result[i] = nextNo;
                possNumbers.RemoveAt(nextInd);
            }
            return result;
        }

        public GameObject[] GetNextPieces() {
            return nextPieces.ToArray();
        }

        //Takes the generated random sequence and uses it to 
        void populateNextPieces() {
            int[] sequence = randomSequence();
            for(int i = 0; i < sequence.Length; i++) {
                nextPieces.Enqueue(Pieces[sequence[i]]);
                nextGhosts.Enqueue(Ghosts[sequence[i]]);
            }
        }

        public void Start() {
            nextPieces = new Queue<GameObject>();
            nextGhosts = new Queue<GameObject>();
            populateNextPieces();
            setPreview();
        }
    } 
}