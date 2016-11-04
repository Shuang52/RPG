using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {

	List <GameObject> Tiles = new List<GameObject> ();

	//finds all walkable tiles using BFS
	public void walkableTiles(GameObject startTile, int x){

		Queue visited = new Queue(20);
		visited.Enqueue (startTile);
		startTile.GetComponent<TileClass> ().walk = true;
		startTile.GetComponent<TileClass> ().walkDirection = "start";
		Tiles.Add (startTile);
		while (visited.Count != 0) {
			GameObject currentTile = (GameObject) visited.Dequeue ();
			int count = currentTile.GetComponent<TileClass> ().walkSteps;
			if (count < x) {
				//right tile
				if (currentTile.GetComponent<TileClass> ().right != null) {
					GameObject right = currentTile.GetComponent<TileClass> ().right;
					if (right.GetComponent<TileClass> ().walk != true) {
						walkableTileVariables (right, currentTile, "right", count);
						Tiles.Add (right);
						visited.Enqueue (right);
					}
				}
				//left tile
				if (currentTile.GetComponent<TileClass> ().left != null) {
					GameObject left = currentTile.GetComponent<TileClass> ().left;
					if (left.GetComponent<TileClass> ().walk != true) {
						walkableTileVariables (left, currentTile, "left", count);
						Tiles.Add (left);
						visited.Enqueue (left);
					}
				}
				//down tile
				if (currentTile.GetComponent<TileClass> ().down != null) {
					GameObject down = currentTile.GetComponent<TileClass> ().down;
					if (down.GetComponent<TileClass> ().walk != true) {
						walkableTileVariables(down, currentTile, "down", count);
						Tiles.Add (down);
						visited.Enqueue (down);
					}
				}
				//up tile
				if (currentTile.GetComponent<TileClass> ().up != null) {
					GameObject up = currentTile.GetComponent<TileClass> ().up;
					if (up.GetComponent<TileClass> ().walk != true) {
						walkableTileVariables (up, currentTile, "up", count);
						Tiles.Add (up);
						visited.Enqueue (up);
					}
				}
			} 
		}
	}

	//helper function of walkPath that sets variables for walkable tiles
	public void walkableTileVariables(GameObject tile, GameObject current_Tile, string direction, int c){
		//sets walk, walksteps, ancestor, and walkDirection
		tile.GetComponent<TileClass> ().walk = true;
		tile.GetComponent<TileClass> ().walkSteps = c + 1;
		tile.GetComponent<TileClass> ().ancestor = current_Tile;
		tile.GetComponent<TileClass> ().walkDirection = direction;
	}

	//highlight walkable tile
	public void highlightTile(GameObject tile){
	}

	//resets tiles to original status
	public void deletePaths(){
		foreach(GameObject n in Tiles){
			n.GetComponent<TileClass>().walk = false;
			n.GetComponent<TileClass> ().walkSteps = 0;
			n.GetComponent<TileClass> ().ancestor = null;
			n.GetComponent<TileClass> ().walkDirection = "";
		}
		Tiles.Clear ();
		Destroy (this);
	}
}
