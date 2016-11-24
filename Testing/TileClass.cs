using UnityEngine;
using System.Collections;

public class TileClass : MonoBehaviour {

	//private variables
	private int x;
	private int y;
	private int steps;
	private GameObject is_up;
	private GameObject is_down;
	private GameObject is_right;
	private GameObject is_left;
	private GameObject predecessor;
	private string tile_tag;
	private string stairs_tag;
	private string direction;
	private bool walkable = false;

	//x coordinate
	public int xCoordinate{
		get{ return x; }
		set{ x = value; }
	}

	//y coordinate
	public int yCoordinate{
		get{ return y; }
		set{ y = value; }
	}

	//tile above 
	public GameObject up{
		get{ return is_up; }
		set{ is_up = value; }
	}

	//tile below
	public GameObject down{
		get{ return is_down; }
		set{ is_down = value; }
	}

	//tile to the right
	public GameObject right{
		get { return is_right; }
		set { is_right = value; }

	}

	//tile to the left
	public GameObject left{
		get { return is_left; }
		set{ is_left = value; }
	}

	//direction of stairs (if tile is stairs)
	public string stairsDirection{
		get{ return stairs_tag; } 
		set{ stairs_tag = value; } 
	}

	//determines if tile is reachable
	public bool walk{
		get{ return walkable; }
		set{ walkable = value; }
	}

	//determines steps from base tile
	public int walkSteps{
		get{ return steps; }
		set{ steps = value; }
	}

	//determines predecessor of tile
	public GameObject ancestor{
		get{ return predecessor; }
		set{ predecessor = value; }
	}

	//determines direction needed to get to tile
	public string walkDirection{
		get{ return direction; }
		set{ direction = value; }
	}
}
