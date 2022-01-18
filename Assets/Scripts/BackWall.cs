using UnityEngine;

public class BackWall : MonoBehaviour, IObstacle
{
    public void HitObstacle(BallController ball)
    {
        GameManager.Instance.OnBallDestroyed(false);
        ball.DestroyBall();
    }
}
