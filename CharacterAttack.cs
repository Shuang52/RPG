using UnityEngine;
using System.Collections;

//Script responsible for attack


public class CharacterAttack : MonoBehaviour {

	private Vector3 mousePosition;
	private bool target_selected = false;
	private int attack_range;
	private int attack_damage;
	private GameObject selected_character;

	// Use this for initialization
	void Start () {
		GameObject current_tile = GameObject.Find ("Control").GetComponent<Raycast> ().selectedTile;
		attack_range = GetComponent<CharacterMainControl> ().attackRange;
		attack_damage = GetComponent<CharacterMainControl> ().attackDamage;
		if (current_tile.GetComponent<TileClass> ().tag == "Elevated") {
			attack_range+=2;
			Debug.Log (attack_range);
		}
	}

	// Update is called once per frame
	void Update () {
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition + Vector3.forward * 10f);
		turnCharacter ();

		if (Input.GetMouseButtonDown (0)) {
			bool over_a_character = GameObject.Find ("Control").GetComponent<Raycast> ().overACharacter;
			if (over_a_character == true) {
				detectWhichCharacter ();
				if (target_selected == true) {
					if(checkDistance() == true){
						attackCharacter ();
					}
				}
			}
		}

	}

	private void detectWhichCharacter(){
		selected_character = GameObject.Find ("Control").GetComponent<Raycast> ().selectedCharacter;
		if (selected_character == gameObject) {
			target_selected = false;
			transform.parent.gameObject.GetComponent<PlayerControl> ().middleOfCharacterTurn = false;
			Destroy (this);
		} else if (selected_character.tag != gameObject.tag) {
			target_selected = true;
		} else {
			target_selected = false;
		}
	} 

	private bool checkDistance(){
		int x = (int)selected_character.transform.position.x;
		int y = (int)selected_character.transform.position.y;
		bool within_range = true;

		Debug.Log (attack_range);
		if (Mathf.Abs (x - transform.position.x) > attack_range || Mathf.Abs (y - transform.position.y) > attack_range) {
			within_range = false;
		}

		return within_range;
	}

	private void attackCharacter(){
		Debug.Log(selected_character.name + " is under attack.");
		LayerMask layerMask = 1 << 8;
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right, 3, layerMask);
		if (hit) {
			attack_damage -= 2;
		}
		GameObject.Find (selected_character.name).GetComponent<CharacterMainControl> ().characterHealth -= attack_damage;
		GameObject.Find (selected_character.name).GetComponent<CharacterMainControl> ().characterDeath ();
		GetComponent<CharacterMainControl> ().endCharacter ();
		Destroy (this);
	}

	private void turnCharacter(){
		transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((mousePosition.y - transform.position.y), 
			(mousePosition.x - transform.position.x)) * Mathf.Rad2Deg - 5);
	}
}
