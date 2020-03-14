using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Animator ) )]
[RequireComponent( typeof( AudioSource ) )]
public class Human : MonoBehaviour
{
  public AudioData audioData;
  public ReactionData reactionData;
  public bool useFem = true;

  private AudioSource audioSource = null;
  private Animator animator = null;
  private AudioClip selectedClap = null;
  private Coroutine pendingBehavior = null;

  private float EnergyMultiplier
  {
    get
    {
      if ( Input.GetKey( KeyCode.Alpha1 ) || Input.GetKey( KeyCode.Keypad1 ) )
      {
        return 0.35f;
      }
      if ( Input.GetKey( KeyCode.Alpha2 ) || Input.GetKey( KeyCode.Keypad2 ) )
      {
        return 0.5f;
      }
      if ( Input.GetKey( KeyCode.Alpha3 ) || Input.GetKey( KeyCode.Keypad3 ) )
      {
        return 0.75f;
      }
      if ( Input.GetKey( KeyCode.Alpha4 ) || Input.GetKey( KeyCode.Keypad4 ) )
      {
        return 2f;
      }
      return 1f;
    }
  }

  private void Start()
  {
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
    selectedClap = audioData.ClapSounds[ Random.Range( 0, audioData.ClapSounds.Length - 1 ) ];
    SetupAnimator();
    StartBehavior( 0, Hype );
  }

  private void SetupAnimator()
  {
    SetSpeed( Random.Range( reactionData.minSpeed, reactionData.maxSpeed ) );
    SetInterest( Random.Range( reactionData.minInterest, reactionData.maxInterest ) );
    SetSpirit( Random.Range( 0f, reactionData.maxSpirit ) );
  }

  private void Update()
  {
    if ( Input.GetKeyDown( KeyCode.Space ) )
    {
      StartBehavior( Random.Range( 0f, reactionData.maxDelayForLaugh ), Laugh );
    }
    if ( Input.GetKeyDown( KeyCode.Return ) )
    {
      StartBehavior( Random.Range( 0f, reactionData.maxDelayForApplause ), Hype );
    }
  }

  public void Clap()
  {
    audioSource.pitch = Random.Range( 0.8f, 1.15f );
    audioSource.volume = Random.Range( 0.5f, 1.15f * EnergyMultiplier );
    audioSource.clip = selectedClap;
    audioSource.Play();
  }

  private void StartBehavior( float delay, System.Action run )
  {
    float distractionChance = reactionData.distractionChance / EnergyMultiplier;
    if ( distractionChance > Mathf.Epsilon && Random.Range( 0f, 1f ) < distractionChance )
    {
      return;
    }

    if ( pendingBehavior != null )
    {
      StopCoroutine( pendingBehavior );
    }

    if ( delay <= Mathf.Epsilon )
    {
      run.Invoke();
    }
    else
    {
      pendingBehavior = StartCoroutine( StartBehaviorWithDelay( delay, run ) );
    }
  }

  private IEnumerator StartBehaviorWithDelay( float delay, System.Action run )
  {
    yield return new WaitForSeconds( delay );
    run.Invoke();
  }

  private void Hype()
  {
    if ( EnergyMultiplier >= 1 && Random.Range( 0f, 1f ) < reactionData.cheerChance )
    {
      Cheer();
    }
    else
    {
      Applause();
    }
  }

  private void Laugh()
  {
    audioSource.pitch = Random.Range( 0.8f, 1.15f );
    audioSource.volume = Random.Range( 0.5f, 1.15f * EnergyMultiplier );
    var clipList = useFem ? audioData.FemLaughs : audioData.MascLaughs;
    audioSource.clip = clipList[ Random.Range( 0, clipList.Length - 1 ) ];
    audioSource.Play();
  }

  private void SetSpeed( float f ) => animator.SetFloat( "Speed", f );
  private void SetInterest( float f ) => animator.SetFloat( "Interest", f );
  private void SetSpirit( float f ) => animator.SetFloat( "Spirit", f );
  private void Applause() => animator.SetTrigger( "Applause" );
  private void Cheer() => animator.SetTrigger( "Cheer" );
}
