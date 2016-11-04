using UnityEngine;
using System.Collections;

//This script will be the main control system for a partiicular character,
//determining when to attack, walk, etc


public class CharacterMainControl : MonoBehaviour {

	//variables 
	private int movement_speed;
	private int attack_range;
	private int attack_damage;
	private int health;
	private bool has_moved = true;
	private bool enemy_selected = false;
	private bool start_attack;

	// Use this for initialization
	void Start () {
		//later on add logic to determine which character class it is
		//for now assault class will do

		//gets character speed
		GameObject.Find("Control").GetComponent<AssaultClass>().Assault();
		movement_speed = GameObject.Find("Control").GetComponent<AssaultClass>().MovementSpeed;
		attack_range = GameObject.Find ("Control").GetComponent<AssaultClass> ().Weapon1Range;
		attack_damage = GameObject.Find ("Control").GetComponent<AssaultClass> ().Weapon1Damage;

		//gets initial health
		health = GameObject.Find("Control").GetComponent<AssaultClass>().Health;

	}

	public void startCharacter(){
		bool middle_of_turn = transform.parent.gameObject.GetComponent<PlayerControl> ().middleOfCharacterTurn = true;
		gameObject.AddComponent<CharacterMovement> ();

	}

	public void startAttack(){
		Destroy (GetComponent<CharacterMovement>());
		gameObject.AddComponent<CharacterAttack> ();
	}

	public void endCharacter(){
		has_moved = true;
		transform.parent.gameObject.GetComponent<PlayerControl> ().middleOfCharacterTurn = false;
		transform.parent.gameObject.GetComponent<PlayerControl> ().checkDone ();
	}

	public void characterDeath(){
		Debug.Log (health);
		if (health < 0) {
			Debug.Log (gameObject.name + "has died D:");
			transform.parent.gameObject.GetComponent<PlayerControl> ().playerHasLost ();
			Destroy (gameObject);
		}
	}

	public int characterHealth{
		get{ return health; }
		set{ health = value; }
	}
	//indicates that character has moved
	public bool hasMoved{
		get{ return has_moved; }
		set{ has_moved = value; }
	}

	public int movementSpeed{
		get{ return movement_speed; }
		set{ movement_speed = value; }
	}

	public int attackRange{
		get{ return attack_range; }
		set{ attack_range = value; } 
	}

	public int attackDamage{
		get{ return attack_damage; }
		set { attack_damage = value; }

	}

}
