using UnityEngine;
using System.Collections;

public class TestPathFinder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//set up grid to test PathFinder
		GameObject[,] tiles = new GameObject[5, 5];
		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 5; j++){
				tiles [i, j] = new GameObject ();
				tiles [i, j].AddComponent<TileClass> ();
			}
		}

		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (i > 0) {
					tiles [i, j].GetComponent<TileClass> ().left = tiles [i -1, j];
				}
				if (i < 4) {
					tiles [i, j].GetComponent<TileClass> ().right = tiles [i + 1, j];
				}
				if (j > 0) {
					tiles [i, j].GetComponent<TileClass> ().up = tiles [i, j - 1];
				}
				if (j < 4) {
					tiles [i, j].GetComponent<TileClass> ().down = tiles [i, j + 1];
				}
			}
		}
		gameObject.AddComponent<PathFinder> ();
		GetComponent<PathFinder> ().walkableTiles (tiles [2, 2], 2);

		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				Debug.Log (tiles [i, j].GetComponent<TileClass> ().walkSteps + " ");
			}
			Debug.Log ("\n");
		}

		GetComponent<PathFinder> ().deletePaths ();


	}
}
