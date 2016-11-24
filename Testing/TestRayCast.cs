using UnityEngine;
using System.Collections;

public class TestRayCast : MonoBehaviour {


	// Use this for initialization
	void Start () {
		gameObject.AddComponent<Raycast> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			//tests if mouse can detect character
			if (GetComponent<Raycast> ().overACharacter == true) {
				if (GetComponent<Raycast> ().selectedCharacter == gameObject) {
					if (GetComponent<Raycast> ().selectedTile.transform.localPosition.x == this.transform.position.x) {
						if (GetComponent<Raycast> ().selectedTile.transform.localPosition.y == this.transform.position.y) {
							Debug.Log (true);
						}
					}
				}
			}
			//tests coordinates
			Debug.Log(transform.position.x + " " + transform.position.y);
		}
	}
}
