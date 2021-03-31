using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement Movement;
    public GameManager gameManager;

    // Update is called once per frame
    void OnCollisionEnter (Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            Movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
        
    }
}
