using UnityEngine;

public abstract class Character : MonoBehaviour
{
  [Header("Components")]
  [SerializeField] protected Rigidbody2D rb;
  [SerializeField] protected Animator anim;

  /// <summary>Upward velocity magnitude use for jump speed/height </summary>
  [SerializeField] protected float jumpVelocity;

  /// <summary>Boolean if the character is touching the main ground gc</summary>
  protected bool isGrounded;

  protected string stateParam;

  /// <summary>
  /// Current state of the animation. 
  /// </summary>
  protected enum MovementState
  {
    idle, running, jumping, falling,
  }

  /// <summary>Current animation state</summary>
  protected MovementState? state;


  /// <summary>Parameter name in the animator tab</summary>
  protected string animParameter;

  protected virtual void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    state = MovementState.running;
    isGrounded = true;
    stateParam = "state"; // "state" by default
  }

  protected virtual void Update()
  {
    if (isJumping())
      state = MovementState.jumping;
    else if (isFalling())
      state = MovementState.falling;
    else if (isRunning())
      state = MovementState.running;
    else
      state = null;
    UpdateAnimationState();
  }

  /// <summary>Handles updating the animation state through the animator tab</summary>
  protected virtual void UpdateAnimationState()
  {
    if (state == null) return;
    anim.SetInteger(stateParam, (int)state);
  }

  /// <summary>
  /// Returns the bool of the Character being grounded yujhkj kjh fgdgh d g fddgf gf g
  /// </summary>
  /// <returns>isGrounded, bool value of character grounded status</returns>
  protected virtual bool isRunning()
  {
    return isGrounded; //TODO - Probably needs more, cant remember rn
  }

  /// <summary>
  /// Returns the bool of the Character jumping 
  /// This is independent from the animation (animation relies on this/math)
  /// </summary>
  /// <returns>Status of Character jumping</returns>
  protected bool isJumping()
  {
    return rb.velocity.y > 0.01f;
  }

  // 
  /// <summary>
  ///  If GameObject is falling. Is independent from animation
  /// </summary>
  /// <returns>
  ///  rb.velocity.y, bool state if the Character is falling
  /// </returns>
  protected virtual bool isFalling()
  {
    return rb.velocity.y < -0.1f;
  }

}
