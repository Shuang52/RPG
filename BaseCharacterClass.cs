using UnityEngine;
using System.Collections;

public class BaseCharacterClass : MonoBehaviour {

	// strings
	private string class_name;
	private string class_desc;
	private string talent_name;
	private string talent_desc;
	private string ab1_name;
	private string ab1_desc;
	private string ab2_name;
	private string ab2_desc;
	private string ab3_name;
	private string ab3_desc;
	private string wpn1_name;
	private string wpn1_desc;
	private string wpn2_name;
	private string wpn2_desc;

	//stats
	private int health;
	private int shield;
	private int dmg_mitig;
	private int move_spd;
	private int sight_rng;
	private int wpn1_rng;
	private int wpn1_dmg;
	private int wpn2_rng;
	private int wpn2_dmg;


	// public getters/setters
	public string ClassName
	{
		get { return class_name; }
		set { class_name = value; }
	}
	public string ClassDescription
	{
		get { return class_desc; }
		set { class_desc = value; }
	}
	public string TalentName
	{
		get { return talent_name; }
		set { talent_name = value; }
	}
	public string TalentDescription
	{
		get { return talent_desc; }
		set { talent_desc = value; }
	}
	public string Ability1Name
	{
		get { return ab1_name; }
		set { ab1_name = value; }
	}
	public string Ability1Description
	{
		get { return ab1_desc; }
		set { ab1_desc = value; }
	}
	public string Ability2Name
	{
		get { return ab2_name; }
		set { ab2_name = value; }
	}
	public string Ability2Description
	{
		get { return ab2_desc; }
		set { ab2_desc = value; }
	}
	public string Ability3Name
	{
		get { return ab3_name; }
		set { ab3_name = value; }
	}
	public string Ability3Description
	{
		get { return ab3_desc; }
		set { ab3_desc = value; }
	}
	public string Weapon1Name
	{
		get { return wpn1_name; }
		set { wpn1_name = value; }
	}
	public string Weapon1Description
	{
		get { return wpn1_desc; }
		set { wpn1_desc = value; }
	}
	public string Weapon2Name
	{
		get { return wpn2_name; }
		set { wpn2_name = value; }
	}
	public string Weapon2Description
	{
		get { return wpn2_desc; }
		set { wpn2_desc = value; }
	}


	public int Health
	{
		get { return health; }
		set { health = value; }
	}
	public int Shield
	{
		get { return shield; }
		set { shield = value; }
	}
	public int DamageMitigation
	{
		get { return dmg_mitig; }
		set { dmg_mitig = value; }
	}
	public int MovementSpeed
	{
		get { return move_spd; }
		set { move_spd = value; }
	}
	public int SightRange
	{
		get { return sight_rng; }
		set { sight_rng = value; }
	}
	public int Weapon1Range
	{
		get { return wpn1_rng; }
		set { wpn1_rng = value; }
	}
	public int Weapon1Damage
	{
		get { return wpn1_dmg; }
		set { wpn1_dmg = value; }
	}
	public int Weapon2Range
	{
		get { return wpn2_rng; }
		set { wpn2_rng = value; }
	}
	public int Weapon2Damage
	{
		get { return wpn2_dmg; }
		set { wpn2_dmg = value; }
	}
}
