using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	//variables
	private bool player_turn;
	private bool middle_of_character_turn = false;
	private int player_number;

	void Update(){
		// signals to character if it is selected when no one is in the middle of moving
		if (player_turn == true) {
			if (Input.GetMouseButtonDown (0)) {
				if (middle_of_character_turn == false) {
					if (GameObject.Find ("Control").GetComponent<Raycast> ().overACharacter == true) {
						characterSelected ();
					}
				}
			}
		}
	}

	//beginning of turn
	//sets all variables to initial state
	public void startOfTurn(){
		player_turn = true;
		middle_of_character_turn = false;
		Transform[] children = GetComponentsInChildren<Transform> ();
		foreach (Transform child in children) {
			if (child.gameObject.tag != "Player") {
				child.gameObject.GetComponent<CharacterMainControl> ().hasMoved = false;
			}
		}
	}

	//checks if player turn over
	public void checkDone(){
		bool done = true;
		Transform[] children = GetComponentsInChildren<Transform> ();
		foreach (Transform child in children) {
			if(child.gameObject.tag != "Player" ){
				if(child.gameObject.GetComponent<CharacterMainControl>().hasMoved == false){
					done = false;
				}
			}
		}
		if (done == true) {
			player_turn = false;
			GameObject.Find ("Control").GetComponent<GameMainControl> ().changeTurns(player_number);
		}
	}

	//determines which player this is
	public int playerNumber{
		get{ return player_number; }
		set{player_number = value; }
	}

	//determines which character is selected
	public void characterSelected(){
		GameObject selected_character = GameObject.Find ("Control").GetComponent<Raycast> ().selectedCharacter;
		Transform[] children = GetComponentsInChildren<Transform> ();
		foreach(Transform child in children){
			if (child.gameObject == selected_character) {
				if (child.gameObject.GetComponent<CharacterMainControl> ().hasMoved == false) {
					child.gameObject.GetComponent<CharacterMainControl> ().startCharacter ();
				}
			}
		}
	}

	//signals that one player is in the middle of turn, thus other units are not
	//free to move
	public bool middleOfCharacterTurn{
		get{ return middle_of_character_turn; }
		set{ middle_of_character_turn = value; }
	}


	//signals to GameControl that player has lost
	public void playerHasLost(){
		if (transform.childCount == 1) {
			GameObject.Find ("Control").GetComponent<GameMainControl> ().endGame (player_number);
		} 
	}

}
