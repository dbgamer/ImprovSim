using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Data/Providers/Simple String Provider")]
public class SimpleStringProvider : AbstractDataProvider<string>
{
  [SerializeField]
  private string[] stringCollection = new string[] { };

  public override string Get()
  {
    if ( stringCollection.Length == 0 )
    {
      return string.Empty;
    }

    return stringCollection[ Random.Range( 0, stringCollection.Length ) ];
  }
}
