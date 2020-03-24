using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Pillars : MonoBehaviour
{
  //public VoSynUnityFree mVsu = null;
  public bool manageEffects = true;
  public AudioSource audioSource = null;
  public SimpleStringProvider wordBank;
  public Text wordDisplay = null;

  private Coroutine routine = null;

  private void OnEnable()
  {
    if ( routine != null )
    {
      StopCoroutine( routine );
      routine = null;
    }
  }

  public bool fem = false;
  public void Update()
  {
    if ( Input.GetMouseButtonUp( 0 ) )
    {
      Play();
    }
  }

  public void Play()
  {
    if ( routine != null )
    {
      return;
    }

    var raycast = Camera.main.ViewportPointToRay( new Vector3( 0.5f, 0.5f, 0 ) );
    if ( Physics.Raycast( raycast, out RaycastHit hitInfo ) )
    {
      var humanInfo = hitInfo.collider.GetComponent<Human>();
      if ( humanInfo != null )
      {
        routine = StartCoroutine( SynthesizeAndPlay( humanInfo.useFem ? 2 : 1 ) );
      }
    }
  }

  private IEnumerator SynthesizeAndPlay( int presetIndex )
  {
    var word = wordBank.Get();
    if ( wordDisplay != null )
    {
      wordDisplay.gameObject.SetActive( true );
      wordDisplay.text = $"[{word}]";
    }

    /*var preset = mVsu.GetFactoryPresetNames()[ presetIndex ];
    var clip = mVsu.SynthesizeTextToAudioClip( word, preset, manageEffects );

    // If there is no clip generated, throw error and break coroutine
    if ( clip == null )
    {
      Debug.LogError( "Voiceful ERROR: clip wasn't generated!" );
    }
    else
    {
      // Play the audio clip through the plugin AudioSource
      audioSource.clip = clip;
      audioSource.Play();
      yield return new WaitForSeconds( audioSource.clip.length );
    }*/

    yield return new WaitForSeconds( 0.5f );

    wordDisplay?.gameObject.SetActive( false );

    routine = null;
  }
}