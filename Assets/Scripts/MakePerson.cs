using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePerson : MonoBehaviour
{
  public GameObject[] people;
  public Vector3 localOffset = Vector3.forward * 0.23f;

  public void Run( float chance )
  {
    var diceRoll = Random.Range( 0f, 1f );
    if ( diceRoll > chance )
    {
      return;
    }

    var prefab = people[ Random.Range( 0, people.Length ) ];
    var instance = Instantiate( prefab, transform );
    instance.transform.localPosition = localOffset;
  }
}
