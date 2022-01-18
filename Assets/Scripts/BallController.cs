using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRigidbody;
    public Rigidbody Rigidbody => ballRigidbody;

    public void SetVelocity(Vector3 vel)
    {
        ballRigidbody.velocity = vel;
    }

    private void OnCollisionEnter(Collision col)
    {
        IObstacle obstacle = col.gameObject.GetComponent<IObstacle>();
        obstacle.HitObstacle(this);
    }
    
    public void DestroyBall()
    {
        Destroy(gameObject);
    }
}
