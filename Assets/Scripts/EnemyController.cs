using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private Rigidbody enemyRigidbody;
    [SerializeField] private float enemySpeed;
    
    public Transform CurrentBall { get; set; }
    
    void Update()
    {
        Vector3 velocity= Vector3.zero;
        if (manager.GameState == GameStates.BallMoves)
        {
            if (CurrentBall.position.x > transform.position.x)
            {
                velocity = new Vector3(enemySpeed, velocity.y, velocity.z);
            }
            else if (CurrentBall.position.x < transform.position.x)
            {
                velocity = new Vector3(-enemySpeed, velocity.y, velocity.z);
            }
        }
        enemyRigidbody.velocity = velocity;
    }

    public void IncreaseSpeed(float additionSpeed)
    {
        enemySpeed += additionSpeed;
    }
}
