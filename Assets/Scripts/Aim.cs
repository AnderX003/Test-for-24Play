using UnityEngine;

public class Aim : MonoBehaviour, IObstacle
{
    public void HitObstacle(BallController ball)
    {
        ball.DestroyBall();
        GameManager.Instance.OnBallDestroyed(true);
        Destroy(gameObject);
    }
}
