using UnityEngine;

[RequireComponent( typeof( CharacterController ) )]
public class FPSWalkerEnhanced : MonoBehaviour
{
  public Theater theater;

  CharacterController characterController;

  public float speed = 6.0f;
  public float gravity = 20.0f;

  private Vector3 moveDirection = Vector3.zero;

  void Start()
  {
    characterController = GetComponent<CharacterController>();
  }

  void Update()
  {
    if ( !theater.Started )
    {
      return;
    }

    if ( characterController.isGrounded )
    {
      // We are grounded, so recalculate
      // move direction directly from axes

      var x = Input.GetAxis( "Horizontal" ) * this.transform.right * speed;
      var z = Input.GetAxis( "Vertical" ) * this.transform.forward * speed;

      moveDirection = x + z;
    }

    // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
    // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
    // as an acceleration (ms^-2)
    moveDirection.y -= gravity * Time.deltaTime;

    // Move the controller
    characterController.Move( moveDirection * Time.deltaTime );
  }
}