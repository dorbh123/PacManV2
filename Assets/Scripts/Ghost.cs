using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public GhostPinkyChase pinkyChase { get; private set; }
    public GhostInkyChase inkyChase { get; private set; }

    public GhostBehavior initialBehavior;
    public GameManager gameManager; // Reference to the GameManager script

    public Transform target;

    public int points = 200;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();
        pinkyChase = GetComponent<GhostPinkyChase>();
        inkyChase = GetComponent<GhostInkyChase>();
    }

    private void Start()
    {
        ResetState();
        SetTarget();
        
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();
        frightened?.Disable();
        scatter?.Enable();

        if (chase != null)
        {
            chase.Disable();
        }

        if (pinkyChase != null)
        {
            pinkyChase.Disable();
        }

        if (inkyChase != null)
        {
            inkyChase.Disable();
        }

        if (home != initialBehavior)
        {
            home?.Disable();
        }

        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }


    private void SetTarget()
    {
        if (target != null)
        {
            pinkyChase?.SetTarget(target);
            inkyChase?.SetTarget(target);
        }
    }


    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pacman"))
        {
            if (frightened.enabled)
            {
                gameManager?.GhostEatten(this); // Use the reference to the GameManager to call the method
            }
            else
            {
                gameManager?.PacmanEatten(); // Use the reference to the GameManager to call the method
            }
        }
    }
}
