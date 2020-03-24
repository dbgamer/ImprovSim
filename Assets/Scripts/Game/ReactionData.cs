using UnityEngine;

[CreateAssetMenu( fileName = "New Reaction Data", menuName = "Data/Reaction" )]
public class ReactionData : ScriptableObject
{
  public float minSpeed = 0.5f;
  public float maxSpeed = 1.5f;
  public float minInterest = 0.25f;
  public float maxInterest = 1.5f;
  public float maxSpirit = 1f;
  public float cheerChance = 0.15f;
  public float maxDelayForApplause = 0.4f;
  public float maxDelayForLaugh = 0.5f;
  public float distractionChance = 0.1f;
}
