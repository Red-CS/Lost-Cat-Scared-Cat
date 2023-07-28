using UnityEngine;

public abstract class Fox : Character
{
  /// Default Fox velocity
  [SerializeField] private float runSpeed = -3f;
  protected float RunSpeed => runSpeed;

  /// Fox destruction point
  private readonly float deadZone = -4.5f;

  /// True if the Fox has initiated its `Attack()` method
  // protected bool hasAttacked = false;
  protected bool HasAttacked { get; set; }
  /// The amount of space between the Fox and Player to initiate Fox attack
  private readonly float spaceBeforeAttack = 1.5f;
  // REVIEW - I could change this into an array to accommodate for sitting foxes
  // Each value in the array would dictate the spacing before each action of a sitting fox

  protected void Start()
  {
    // Flip sprite horizontally
    Transform rbTransform = this.rb.transform;
    Vector3 localScale = rbTransform.localScale;
    localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
    rbTransform.localScale = localScale;
  }

  protected virtual void Update()
  {
    // Destroy on off-screen
    if (IsOffscreen(transform.position.x, deadZone)) 
    {
      Destroy(gameObject);
      return;
    }

    // Update animation
    SetAnimationParams();
  }

  protected void FixedUpdate()
  {
    HandleMovement();
    
    // Distance between Fox and Player's position && isRunning
    float distanceFromPlayer = Mathf.Abs(Player.PlayerXPos - transform.position.x);
    if (HasAttacked || (!IsInPosition(distanceFromPlayer, this.spaceBeforeAttack)) 
        ) return;
    
    Attack();
  }

  protected abstract void HandleMovement();
  
  /// A Fox's attack sequence
  protected abstract void Attack();

  /// Returns true if the Fox is outside the dead zone
  private static bool IsOffscreen(float currPos, float deadZone)
  {
    return currPos < deadZone;
  }

  /// Returns true if the Fox is at the correct distance to Attack
  private static bool IsInPosition(float distance, float spacing)
  {
    return distance < spacing;
  }
  
  protected abstract void SetAnimationParams();

}