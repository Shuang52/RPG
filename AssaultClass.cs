using UnityEngine;
using System.Collections;

public class AssaultClass : BaseCharacterClass
{
	public void Assault()
	{
		ClassName = "Assault";
		ClassDescription = "High risk, high reward. Thrives in close-quarters combat with abilities that punish solo targets.";
		TalentName = "Close Combat";
		TalentDescription = "Adjacent enemies take +2 damage from basic attacks";
		Ability1Name = "Executioner";
		Ability1Description = "Increase damage dealt to damaged targets by 1";
		Ability2Name = "Double-Edged";
		Ability2Description = "Increase damage dealt to adjacent targets by 1, but no longer does damage to targets in cover";
		Ability3Name = "Dominance";
		Ability3Description = "Restore 3 health per kill";
		Weapon1Name = "Auto Shotgun";
		Weapon1Description = "Increased range";
		Weapon2Name = "Double-Barreled Shotgun";
		Weapon2Description = "Can only attack every other friendly phase";

		Health = 8;
		Shield = 0;
		DamageMitigation = 0;
		MovementSpeed = 3;
		SightRange = 5;
		Weapon1Range = 3;
		Weapon1Damage = 3;
		Weapon2Range = 2;
		Weapon2Damage = 4;

	}
}
