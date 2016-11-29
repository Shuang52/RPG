using UnityEngine;
using System.Collections;

public class GrenadeAttack : Photon.MonoBehaviour {

	//basic attack variables
	private Vector3 mousePosition;
	private bool target_selected = false;
	private int attack_range;
	private int attack_damage;
	private int current_x;
	private int current_y;
	private GameObject selected_character;
	private GameObject selected_tile;

	//AOE variables
	private int aoe_range = 1;
	private int leftx;
	private int rightx;
	private int lefty;
	private int righty;
	private int target_x_position;
	private int target_y_position;
	private GameObject[] player1;
	private GameObject[] player2;

	// Use this for initialization
	void Start () {
		GameObject current_tile = GameObject.Find("Control").GetComponent<Raycast>().selectedTile;
		current_x = current_tile.GetComponent<TileClass>().xCoordinate;
		current_y = current_tile.GetComponent<TileClass>().yCoordinate;
		attack_range = GetComponent<CharacterMainControl>().attackRange;
		attack_damage = GetComponent<CharacterMainControl>().attackDamage;
		if (current_tile.GetComponent<TileClass>().tag == "Elevated")
		{
			attack_range += 2;
		}
		selectAllCharacters();
	}

	// Update is called once per frame
	void Update () {
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition + Vector3.forward * 10f);
		turnCharacter ();

		if (Input.GetMouseButtonDown (0)) {
			bool over_a_character = GameObject.Find ("Control").GetComponent<Raycast> ().overACharacter;
			selected_tile = GameObject.Find ("Control").GetComponent<Raycast> ().selectedTile;
			target_x_position = selected_tile.GetComponent<TileClass>().xCoordinate;
			target_y_position = selected_tile.GetComponent<TileClass> ().yCoordinate;
			if (over_a_character == true) {
				detectWhichCharacter ();
				if (target_selected == true) {
					startAttack ();
				}
			} else {
				startAttack ();
			}
		}

	}

	private void instantiateGrenade(int x, int y){
		PhotonNetwork.Instantiate ("Grenade", new Vector3 (x, y-19, transform.position.z), Quaternion.identity, 0);
	}

	private void startAttack(){
		if(checkDistance(target_x_position, target_y_position, current_x, current_y, attack_range) == true){
			instantiateGrenade (target_x_position, target_y_position);
			attackCharacter(target_x_position, target_y_position);
			checkVictims (target_x_position, target_y_position);
		}
	}

	//help identify which character is in range of AOE effect
	private void selectAllCharacters() {
		player1 = GameObject.FindGameObjectsWithTag("Player1");
		player2 = GameObject.FindGameObjectsWithTag("Player2");
	}

	private void detectWhichCharacter() {
		selected_character = GameObject.Find("Control").GetComponent<Raycast>().selectedCharacter;
		if (selected_character == gameObject)
		{
			target_selected = false;
			transform.parent.gameObject.GetComponent<PlayerControl>().middleOfCharacterTurn = false;
			Destroy(this);
		}
		else if (selected_character.tag != gameObject.tag)
		{
			target_selected = true;
		}
		else
		{
			target_selected = false;
		}
	}

	//check distance between any two gameobjects under some range
	private bool checkDistance(int x, int y, int a, int b, int z) {
		bool within_range = true;

		if (Mathf.Abs(x - a) > z || Mathf.Abs(y - b) > z)
		{
			within_range = false;
		}

		return within_range;
	}

	//attackCharacter(int x, int y)
	//initiates attack on enemy character
	[PunRPC]
	private void attackCharacter(int x, int y)
	{
		LayerMask layerMask = 1 << 8;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 3, layerMask);
		if (hit)
		{
			if (Mathf.Abs(x - hit.transform.gameObject.GetComponent<TileClass>().xCoordinate) <= 1 && Mathf.Abs(y - hit.transform.gameObject.GetComponent<TileClass>().yCoordinate) <= 1)
			{
				attack_damage -= 2;
			}
		}
		GameObject.Find(selected_character.name).GetComponent<CharacterMainControl>().photonView.RPC("updateHealth", PhotonTargets.AllBuffered, -attack_damage);
		GameObject.Find(selected_character.name).GetComponent<CharacterMainControl>().photonView.RPC("characterDeath", PhotonTargets.AllBuffered, null);
		//GetComponent<CharacterMainControl>().endCharacter();
		//Destroy(this);
	}

	//damage calculations on characters affected by AOE
	[PunRPC]
	private void AOEimpact(int x, int y, int i, GameObject[] player) {
		GameObject.Find(player[i].name).GetComponent<CharacterMainControl>().photonView.RPC("updateHealth", PhotonTargets.AllBuffered, -attack_damage);
		GameObject.Find(player[i].name).GetComponent<CharacterMainControl>().photonView.RPC("characterDeath", PhotonTargets.AllBuffered, null);
	}

	//manually check every character if within range of AOE effect
	private void checkVictims(int x, int y) {
		leftx = x - aoe_range;
		lefty = y - aoe_range;
		rightx = x + aoe_range;
		righty = y + aoe_range;
		checkCharDistance(x, y, player1);
		checkCharDistance(x, y, player2);
		GetComponent<CharacterMainControl>().endCharacter();
		Destroy(this);
	}

	//check distance from each character to the 8 tiles
	private void checkCharDistance(int x, int y, GameObject[] player) {
		for (int i = 0; i != player.Length; i++) {
			int coordx = (int) player[i].transform.position.x;
			int coordy = (int) player[i].transform.position.y;

			//checks the 8 tiles around the central point
			if (checkDistance(coordx, coordy, leftx, lefty, 0) == true) { AOEimpact(coordx, coordy, i, player); }
			else if (checkDistance(coordx, coordy, leftx, y, 0) == true) { AOEimpact(coordx, coordy, i, player); }
			else if (checkDistance(coordx, coordy, leftx, righty, 0) == true) { AOEimpact(coordx, coordy, i, player); }
			else if (checkDistance(coordx, coordy, x, lefty, 0) == true) { AOEimpact(coordx, coordy, i, player); }
			else if (checkDistance(coordx, coordy, x, righty, 0) == true) { AOEimpact(coordx, coordy, i, player); }
			else if (checkDistance(coordx, coordy, rightx, lefty, 0) == true) { AOEimpact(coordx, coordy, i, player); }
			else if (checkDistance(coordx, coordy, rightx, y, 0) == true) { AOEimpact(coordx, coordy, i, player); }
			else if (checkDistance(coordx, coordy, righty, righty, 0) == true) { AOEimpact(coordx, coordy, i, player); }
		}
	}

	private void turnCharacter() {
		transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePosition.y - transform.position.y),
			(mousePosition.x - transform.position.x)) * Mathf.Rad2Deg - 5);
	}

    public int currentX
    {
        get { return current_x; }
    }

    public int currentY
    {
        get { return current_y; }
    }

    public int currentRange
    {
        get { return attack_range; }
    }

    public int targetX
    {
        get { return target_x_position; }
    }

    public int targetY
    {
        get { return target_y_position; }
    }

    public bool targetSelect
    {
        get { return target_selected; }
    }

    public int leftX
    {
        get { return leftx; }
    }

    public int leftY
    {
        get { return lefty; }
    }

    public int rightX
    {
        get { return rightx; }
    }

    public int rightY
    {
        get { return righty; }
    }

    public GameObject[] Player1
    {
        get { return player1; }
    }

    public GameObject[] Player2
    {
        get { return player2; }
    }
}
