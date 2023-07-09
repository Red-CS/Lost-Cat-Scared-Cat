using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
  protected Rigidbody2D rb;
  private Animator anim;

  /// <summary>Boolean if the character is touching the main ground</summary>
  private bool isGrounded;

  protected bool IsGrounded
  {
    get => this.isGrounded;
    set => this.isGrounded = value;
  }

  private bool isVisiblyJumping;

  protected bool IsVisiblyJumping
  {
    get => this.isVisiblyJumping;
    set => this.isVisiblyJumping = value;
  }

  private bool isVisiblyRunning;

  protected bool IsVisiblyRunning { get; set; }

  private bool hasInputJump;

  protected bool HasInputJump
  {
    get => this.hasInputJump;
    set => this.hasInputJump = value;
  }

  private static readonly int Running = Animator.StringToHash("IsRunning");
  private static readonly int Jumping = Animator.StringToHash("IsJumping");
  private static readonly int Falling = Animator.StringToHash("IsFalling");
  
  protected virtual void Awake()
  {
    this.isGrounded = true;
    this.hasInputJump = false;
    this.isVisiblyJumping = false;
  }
  
  protected virtual void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  /// <summary>
  /// Returns whether or not the character is running
  /// </summary>
  /// <returns>isGrounded, bool value of character grounded status</returns>
  protected virtual bool IsRunning()
  {
    return this.isGrounded && Mathf.Abs(this.rb.velocity.x) > 0.1f;
  }

  /// <summary>
  /// Returns the bool of the Character jumping 
  /// This is independent from the animation (animation relies on this/math)
  /// </summary>
  /// <returns>Status of Character jumping</returns>
  protected virtual bool IsJumping()
  {
    return rb.velocity.y > 0.01f;
  }

  /// <summary>
  ///  If GameObject is falling. Is independent from animation
  /// </summary>
  /// <returns>
  ///  rb.velocity.y, bool state if the Character is falling
  /// </returns>
  protected bool IsFalling()
  {
    return rb.velocity.y < -0.1f;
  }

  // protected abstract void HandleRun();
  // protected abstract void HandleJump();
  // protected abstract void HandleFall();

  protected void SetRunAnimationParam(bool value)
  {
    this.anim.SetBool(Running, value);
  }

  protected void SetJumpAnimationParam(bool value)
  {
    this.anim.SetBool(Jumping, value);
  }

  protected void SetFallAnimationParam(bool value)
  {
    this.anim.SetBool(Falling, value);
  }
  
  /// <summary>
  /// Called when this object collides with something
  /// </summary>
  private void OnCollisionEnter2D()
  {
    this.isGrounded = true;
  }

  /// <summary>
  /// Called when this object leaves a collider
  /// </summary>
  private void OnCollisionExit2D()
  {
    this.isGrounded = false;
  }
}
