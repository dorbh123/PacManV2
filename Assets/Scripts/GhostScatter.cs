using UnityEngine;

public class GhostScatter : GhostBehavior
{
   
    public override void SetNextBehavior()
    {
        // Transition to the appropriate chase behavior based on the specific ghost
        if (ghost == null)
            return;

        if (ghost.chase != null)
            nextBehavior = ghost.chase;
        else if (ghost.pinkyChase != null)
            nextBehavior = ghost.pinkyChase;
        else if (ghost.inkyChase != null)
            nextBehavior = ghost.inkyChase;
    }



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
            int index = Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections[index] == -ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++;
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }
            ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
