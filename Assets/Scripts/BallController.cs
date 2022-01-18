using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRigidbody;
    public Rigidbody Rigidbody => ballRigidbody;
    public GameManager Manager { get; set; }

    public void SetVelocity(Vector3 vel)
    {
        ballRigidbody.velocity = vel;
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "wall_side":
                break;
            case "wall_back":
            case "enemy":
                Manager.OnBallDestroyed(false);
                DestroyBall();
                break;
            case "aim":
                Destroy(col.gameObject);
                Manager.OnBallDestroyed(true);
                DestroyBall();
                break;
        }
    }
    
    public void DestroyBall()
    {
        Destroy(gameObject);
    }
}
