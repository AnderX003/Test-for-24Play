using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody ballRigidbody;
    public Rigidbody Rigidbody => ballRigidbody;
    public GameManager Manager { get; set; }
    public Vector3 Velocity { get; set; }

    private void Update()
    {
        ballRigidbody.velocity = Velocity;
    }

    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "wall_side":
                Velocity = new Vector3(-Velocity.x, 0, Velocity.z);
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
