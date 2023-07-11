using UnityEngine;

public class GhostPinkyChase : GhostBehavior
{
   
    private void OnDisable()
    {
        if (ghost != null && ghost.scatter != null)
        {
            ghost.scatter.SetNextBehavior();
            ghost.scatter.Enable();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if (node != null && enabled && ghost != null && !ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            Vector3 targetPosition = CalculateTargetPosition();

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = ghost.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0);
                float distance = (targetPosition - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

    private Vector3 CalculateTargetPosition()
    {
        Vector3 pacmanPosition = ghost.target.position;
        Vector3 direction = pacmanPosition - ghost.transform.position;
        return pacmanPosition + (2 * direction);
    }

   
}
