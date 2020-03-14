using UnityEngine;

public class Theater : MonoBehaviour
{
  public GUISkin skin;
  public Texture logo;
  public GameObject lights;
  public ReactionData reactionData;
  public bool Started
  {
    get; private set;
  }

  private float crowdSize = 0.75f;
  private MakePerson[] people;

  private void Start()
  {
    people = GetComponentsInChildren<MakePerson>();
    lights.SetActive( false );
  }

  private void OnGUI()
  {
    if ( Started )
    {
      GUI.Label( new Rect( 10, Screen.height - 25, Screen.width - 10, 25 ), "Mouse to Look. WSAD / Arrow Keys to move. [Spacebar] for Laughs. [Enter] for hype." );
    }
    else
    {
      Vector2 midPoint = new Vector2( Screen.width / 2f, Screen.height / 2f );

      GUI.skin = skin;
      GUI.DrawTexture( new Rect( midPoint.x - 250, midPoint.y - 300, 500, 250 ), logo, ScaleMode.StretchToFill, true );
      crowdSize = GUI.HorizontalSlider( new Rect( midPoint.x - 250, midPoint.y, 500, 0 ), crowdSize, 0f, 1f );
      GUI.Label( new Rect( midPoint.x - 250, midPoint.y + 10, 500, 50 ), "Crowd Size" );
      if ( GUI.Button( new Rect( midPoint.x - 75, midPoint.y + 50, 150, 50 ), "Show Start!" ) )
      {
        Run();
      }
    }
  }

  public void Run()
  {
    Started = true;

    foreach ( var person in people )
    {
      person.Run( crowdSize );
    }

    lights.SetActive( true );
  }
}
