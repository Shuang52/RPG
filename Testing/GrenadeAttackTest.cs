/*using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class GrenadeAttackTest: Photon.MonoBehaviour {

    private GrenadeAttack grenadeAttack;

    [SetUp]
    public void Setup()
    {
        GameObject go = new GameObject();
        go.AddComponent<GrenadeAttack>();
    }

	[Test]
	public void EditorTest()
	{
		//Arrange
		var gameObject = new GameObject();

		//Act
		//Try to rename the GameObject
		var newGameObjectName = "My game object";
		gameObject.name = newGameObjectName;

		//Assert
		//The object has a new name
		Assert.AreEqual(newGameObjectName, gameObject.name);
	}
}*/
