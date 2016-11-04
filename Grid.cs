using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject[,] tiles = new GameObject[30, 20];
		Transform[] children = GetComponentsInChildren<Transform>();
		foreach (Transform child in children) {
			if (child.gameObject.name == "Tile" || child.gameObject.name == "Cover" || child.gameObject.name == "Stairs_ud" || child.gameObject.name == "Stairs_rl") {
				int x = (int)child.transform.localPosition.x;
				int y = (int)child.transform.localPosition.y + 20;
				string tag = child.transform.parent.gameObject.tag;
				tiles [x, y] = child.gameObject;

				//adds tile class
				child.gameObject.AddComponent<TileClass> ();
				child.gameObject.GetComponent<TileClass> ().xCoordinate = x;
				child.gameObject.GetComponent<TileClass> ().yCoordinate = y;
				child.gameObject.GetComponent<TileClass> ().tag = tag;
			}

			if (child.gameObject.name == "Stairs_ud") {
				child.gameObject.GetComponent<TileClass> ().stairsDirection = "upDown";
			}

			if (child.gameObject.name == "Stairs_rl") {
				child.gameObject.GetComponent<TileClass> ().stairsDirection = "rightLeft"; 
			}

		}

		for (int i = 0; i < 30; i++) {
			for (int j = 0; j < 20; j++) {

				//sets up variables
				GameObject currentTile = tiles [i, j];
				string currentTag = currentTile.GetComponent<TileClass> ().tag;
				string currentStairs = currentTile.GetComponent<TileClass> ().stairsDirection;

				//links left tile
				if (i > 0) {
					//left tile and variables
					GameObject leftTile = tiles [i-1, j];
					string leftTag = leftTile.GetComponent<TileClass>().tag;

					if (currentTag == leftTag) {
						currentTile.GetComponent<TileClass> ().left = leftTile;
					}
					else if (currentTag == "Stairs" || leftTag == "Stairs") {
						if(currentStairs  == "rightLeft" || leftTile.GetComponent<TileClass>().stairsDirection == "rightLeft"){
							currentTile.GetComponent<TileClass>().left = leftTile;
						}
					}
				}

				//links right tile
				if (i < 29) {
					//right tile and variables
					GameObject rightTile = tiles [i + 1, j];
					string rightTag = rightTile.GetComponent<TileClass> ().tag;
					if (currentTag == rightTag) {
						currentTile.GetComponent<TileClass> ().right = rightTile;
					}
					else if (currentTag == "Stairs" || rightTag == "Stairs") {
						if (currentStairs == "rightLeft" || rightTile.GetComponent<TileClass> ().stairsDirection == "rightLeft") {
							currentTile.GetComponent<TileClass> ().right = rightTile;
						}
					}
				}

				//links down tile
				if (j > 0) {
					//down tile and variables
					GameObject downTile = tiles [i, j - 1];
					string downTag = downTile.GetComponent<TileClass> ().tag;
					if (currentTag == downTag) {
						currentTile.GetComponent<TileClass> ().down = downTile;
					} 
					else if (currentTag == "Stairs" || downTag == "Stairs") {
						if (currentStairs == "upDown" || downTile.GetComponent<TileClass> ().stairsDirection == "upDown") {
							currentTile.GetComponent<TileClass> ().down = downTile;
						}
					}
				}

				//links up tile
				if (j < 19) {
					//up tile and variables
					GameObject upTile = tiles [i, j + 1];
					string upTag = upTile.GetComponent<TileClass> ().tag;
					if (currentTag == upTag) {
						currentTile.GetComponent<TileClass> ().up = upTile;
					} 
					else if (currentTag == "Stairs" || upTag == "Stairs") {
						if (currentStairs == "upDown" || upTile.GetComponent<TileClass> ().stairsDirection == "upDown") {
							currentTile.GetComponent<TileClass> ().up = upTile;
						}
					}

				}
			}
		}

	}


}
