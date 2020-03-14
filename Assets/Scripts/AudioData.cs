using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Audio Data", menuName = "Data/Audio" )]
public class AudioData : ScriptableObject
{
  public AudioClip[] ClapSounds;
  public AudioClip[] FemLaughs;
  public AudioClip[] MascLaughs;
}
