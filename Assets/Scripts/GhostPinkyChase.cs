using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPinkyChase : GhostBehavior
{
    private void OnDisable()
    {
        if (this.ghost.pinkyChase != null)
        {
            this.ghost.pinkyChase.Disable();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frightened.enabled) // Check if the ghost is in the right behavior
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            // Calculate the target position for Pinky
            Vector3 targetPosition = this.ghost.target.position + (this.ghost.target.right * 4f);

            foreach (Vector2 availableDirection in node.availableDirections) // Find the available direction that moves closest to the target
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0);
                float distance = (targetPosition - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}
