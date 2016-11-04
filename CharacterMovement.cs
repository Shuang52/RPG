using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

//Script responsible for moving sprites

public class CharacterMovement : MonoBehaviour {

	//variables
	bool stillMoving = false;
	bool charaSelect = false;
	bool turn = false;
	bool nextStep = false;
	int index = 0;
	float angle;
	private Vector3 targetPosition;
	private GameObject start_tile;
	private GameObject destination_tile;
	List <GameObject> walkPath = new List <GameObject>(); 
	private GameObject nBlock;

	void Start(){
		start_tile = GameObject.Find ("Control").GetComponent<Raycast> ().selectedTile;
		gameObject.AddComponent<PathFinder> ();
		int speed = GetComponent<CharacterMainControl> ().movementSpeed;
		GetComponent<PathFinder> ().walkableTiles (start_tile, speed);
	}
	// Update is called once per frame
	void Update () {
		if(stillMoving == false){
			if (Input.GetMouseButtonDown (0)) {
				bool over_a_character = GameObject.Find("Control").GetComponent<Raycast> ().overACharacter;
				if (over_a_character == true) {
					if (start_tile == GameObject.Find("Control").GetComponent<Raycast> ().selectedTile) {
						stillMoving = false;
						index = 0;
						walkPath.Clear ();
						GetComponent<PathFinder> ().deletePaths ();
						GetComponent<CharacterMainControl> ().startAttack ();
					} else {
						Debug.Log ("Cannot walk here");
					}
				}
				else{
					destination_tile = GameObject.Find("Control").GetComponent<Raycast>().selectedTile;
					if(destination_tile.GetComponent<TileClass>().walk == true){
						Path(start_tile, destination_tile, walkPath);
						nextStep = true;
					}
					else{
						Debug.Log("You Cannot walk here");
					}
				}
			}
		}
		if (nextStep == true) {
			nBlock = nextBlock (walkPath, index);
			targetPosition.x = nBlock.GetComponent<TileClass> ().xCoordinate;
			targetPosition.y = nBlock.GetComponent<TileClass> ().yCoordinate - 19;
			targetPosition.z = transform.position.z;
			if (nBlock.GetComponent<TileClass> ().walkDirection != "start") {
				angle = turnAngle (nBlock);
				turn = true;
			} 
			stillMoving = true;
			nextStep = false;
		}
		if (stillMoving == true) {
			if (turn == true) {
				turnChara (angle);
			} else {
				if (Vector3.Distance (transform.position, targetPosition) > 0.01) {
					moveChara ();
				} else {
					if (nBlock != destination_tile) {
						nextStep = true;
					} else {
						stillMoving = false;
						index = 0;
						walkPath.Clear ();
						GetComponent<PathFinder> ().deletePaths ();
						GetComponent<CharacterMainControl> ().endCharacter ();
						Destroy (this);
					}
				}
			}
		}

	}

	//finds which path to walk on and places in list
	private void Path(GameObject s, GameObject e, List<GameObject> x){
		GameObject start = s;
		GameObject end = e;
		if (start != end) {
			//something wrong here
			Path (start, end.GetComponent<TileClass> ().ancestor, x);
			x.Add (end);
		} else {
			x.Add (end);
		}
	}

	//finds next block to walk to
	private GameObject nextBlock(List<GameObject> x, int i){
		GameObject block = x [x.Count -1];
		int count = i;
		while (count < x.Count-1) {
			string cDirection = x[count].GetComponent<TileClass>().walkDirection;
			string pDirection = x[count+1].GetComponent<TileClass>().walkDirection;
			if (cDirection != pDirection) {
				block = x [count];
				break;
			}
			count++;
		}
		index = count+1;
		return block;
	}

	//determines angle to turn sprite
	private float turnAngle(GameObject n){
		string direction = n.GetComponent<TileClass> ().walkDirection;
		float x = transform.rotation.z;

		switch (direction) {
		case "right":
			x = 0;
			break;
		case "left":
			x = 180;
			break;
		case "down":
			x = 270;
			break;
		case "up":
			x = 90;
			break;
		}

		return x;
	}

	//turns sprite
	private void turnChara(float targetAngle){
		float turnSpeed = 5;
		if (Mathf.Abs(Quaternion.Angle(transform.rotation, Quaternion.Euler (0, 0, targetAngle))) > 1) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);
		} else {
			turn = false;
		}
	}

	//moves sprite
	private void moveChara(){
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, Time.deltaTime * 2);
	}

}
