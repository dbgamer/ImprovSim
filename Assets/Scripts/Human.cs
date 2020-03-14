using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Animator ) )]
[RequireComponent( typeof( AudioSource ) )]
public class Human : MonoBehaviour
{
  public AudioData audioData;
  public bool useFem = true;

  private AudioSource audioSource = null;
  private Animator animator = null;
  private float minSpeed = 0.5f;
  private float maxSpeed = 1.5f;
  private float minInterest = 0.25f;
  private float maxInterest = 1.5f;
  private float madChance = 0.15f;
  private float cheerChance = 0.15f;

  private AudioClip clapSound = null;

  private void Start()
  {
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();

    clapSound = audioData.ClapSounds[ Random.Range( 0, audioData.ClapSounds.Length - 1 ) ];

    SetSpeed( Random.Range( minSpeed, maxSpeed ) );
    SetInterest( Random.Range( minInterest, maxInterest ) );
    SetSpirit( Random.Range( 0f, 1f ) < madChance );

    Hype();
  }

  private void Update()
  {
    if ( Input.GetKeyDown( KeyCode.Space ) )
    {
      Laugh();
    }
    if ( Input.GetKeyDown( KeyCode.Return ) )
    {
      Hype();
    }
  }

  private void Hype()
  {
    if ( Random.Range( 0f, 1f ) < cheerChance )
    {
      Cheer();
    }
    else
    {
      Applause();
    }
  }

  public void Clap()
  {
    audioSource.pitch = Random.Range( 0.8f, 1.15f );
    audioSource.volume = Random.Range( 0.5f, 1.15f );
    audioSource.clip = clapSound;
    audioSource.Play();
  }

  private void Laugh()
  {
    audioSource.pitch = Random.Range( 0.8f, 1.15f );
    audioSource.volume = Random.Range( 0.5f, 1.15f );
    var clipList = useFem ? audioData.FemLaughs : audioData.MascLaughs;
    audioSource.clip = clipList[ Random.Range( 0, clipList.Length - 1 ) ];
    audioSource.Play();
  }

  private void SetSpeed( float f ) => animator.SetFloat( "Speed", f );
  private void SetInterest( float f ) => animator.SetFloat( "Interest", f );
  private void SetSpirit( bool mad ) => animator.SetFloat( "Spirit", mad ? 0f : 1f );
  private void Applause() => animator.SetTrigger( "Applause" );
  private void Cheer() => animator.SetTrigger( "Cheer" );
}
