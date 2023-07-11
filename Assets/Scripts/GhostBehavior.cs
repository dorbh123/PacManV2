using UnityEngine;

public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float duration;

    protected GhostBehavior nextBehavior; // Reference to the next behavior to transition to

    private void Awake()
    {
        ghost = GetComponent<Ghost>();
        enabled = false;
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);

        // Set the next behavior to transition to after this behavior ends
        SetNextBehavior();
    }

    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke();

        // Transition to the next behavior if available
        if (nextBehavior != null)
        {
            nextBehavior.Enable();
        }
    }

    public virtual void SetTarget(Transform target)
    {
        // Implement target setting logic in derived classes
    }

    public virtual void SetNextBehavior()
    {
        // Implement behavior-specific logic to set the next behavior
    }
}
