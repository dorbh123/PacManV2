using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    //public override void SetTarget(Transform target)
    //{
    //    base.SetTarget(target);

    //    // Implement additional target-specific logic for each ghost behavior
    //    // For example, in GhostChase:
    //    // this.ghost.movement.SetTarget(target);
    //}
    public override void SetNextBehavior()
    {
        // Set the next behavior to transition to after chase behavior
        if (ghost.scatter != null)
        {
            nextBehavior = ghost.scatter;
        }
    }
    private void OnDisable()
    {
        ghost.scatter.Enable();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frightened.enabled) //chk if the ghost is in the right behavior
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            
            foreach (Vector2 availableDirection in node.availableDirections) // Find the available direction that moves closet to pacman
            {
                
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0);
                float distance = (ghost.target.position - newPosition).sqrMagnitude; // dont use magnitude not good preformnce

                if (distance < minDistance) 
                {
                    direction = availableDirection; 
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }
}
