using UnityEngine;
using System.Collections;

//raycast to determine mouse position

public class Raycast : MonoBehaviour {

	//variables
	private Vector3 targetPosition;
	private Vector3 mousePosition;
	private bool over_a_character = false;
	private GameObject selected_Tile;
	private GameObject selected_character;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		//sets up raycast
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldPoint.z = Camera.main.transform.position.z;
		Ray ray = new Ray(worldPoint, new Vector3(0,0,1));
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

		//returns if raycast hits collider
		if(hit){
			//if raycast hits player
			if(hit.transform.gameObject.tag == "Player1" || hit.transform.gameObject.tag == "Player2"){
				over_a_character = true;
				selected_character = hit.transform.gameObject;
				Ray ray2 = new Ray(hit.transform.position, new Vector3(0,0,1));
				RaycastHit2D hit2 = Physics2D.GetRayIntersection(ray2);
				if(hit2){
					selected_Tile = hit2.transform.parent.gameObject;
				}
			}
			//if raycast hits tile
			else{
				over_a_character = false;
				selected_Tile = hit.transform.parent.gameObject;
			}
		}

	}

	public bool overACharacter{
		get{ return over_a_character; } 
	}

	//returns selected character
	public GameObject selectedCharacter{
		get{ return selected_character; }
	}


	//returns currentTile
	public GameObject selectedTile{
		get{ return selected_Tile; } 
	}
}
