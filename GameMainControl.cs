using UnityEngine;
using System.Collections;

public class GameMainControl : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Debug.Log ("Welcome to the game, you know the one that still needs a name, yeah that one.");
		//sets up player numbers
		GameObject.Find ("Player1").GetComponent<PlayerControl> ().playerNumber = 1;
		GameObject.Find ("Player2").GetComponent<PlayerControl> ().playerNumber = 2;

		//run scripts
		whichCharacterStarts();

	}

	// Update is called once per frame
	void Update () {

	}

	//determines randomly which character will start
	public void whichCharacterStarts(){
		int playerNumber;
		if (Random.value < 0.5f) {
			playerNumber = 1;
		} else {
			playerNumber = 2;
		}
		changeTurns (playerNumber);
	}

	//changes turns between characters
	public void changeTurns(int player){
		if (player == 1) {
			GameObject.Find ("Player2").GetComponent<PlayerControl> ().startOfTurn ();
			Debug.Log ("Player2's turn!");
		} else if (player == 2) {
			GameObject.Find ("Player1").GetComponent<PlayerControl> ().startOfTurn ();
			Debug.Log("Player1's turn");
		}
	}

	//ends game
	public void endGame(int player){
		Debug.Log ("Player" + player + " has lost.");
	}
}
