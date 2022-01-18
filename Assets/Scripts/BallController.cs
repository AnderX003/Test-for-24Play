using UnityEngine;
using UnityEngine.Assertions;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRigidbody;
    private Vector3 previousBallVelocity;

    public void StartMovement(Vector3 velocity)
    {
        ballRigidbody.velocity = velocity;
    }

    private void OnCollisionEnter(Collision col)
    {
        IObstacle obstacle = col.gameObject.GetComponent<IObstacle>();
        Assert.IsNotNull(obstacle);
        obstacle.HitObstacle(this);
    }
    
    public void DestroyBall()
    {
        Destroy(gameObject);
    }

    public void PauseMovement()
    {
        previousBallVelocity = ballRigidbody.velocity;
        ballRigidbody.velocity = Vector3.zero;
    }

    public void ResumeMovement()
    {
        ballRigidbody.velocity = previousBallVelocity;
    }
}
