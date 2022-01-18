using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameConfig config;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private InputController inputController;
    [SerializeField] private EnemyController enemy;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject aimsPrefab;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform ballStartPosition;
    private BallController currentBall;
    private Transform arrowTransform;
    private int level = 1;
    private int currentAimsCounter = 3;
    private bool arrowIsCreated;
    private bool lastBallWasDestroyed = true;

    public GameStates GameState { private set; get; } = GameStates.Pause;

    private void Start()
    {
        Instance = this;
    }

    public void StartGame()
    {
        GameState = GameStates.NoBall;
        inputController.Enabled = true;
        inputController.ActionAfterReset = PushBall;
        CreateBall();
    }

    private void CreateBall()
    {
        currentBall = Instantiate(ballPrefab, parentTransform).GetComponent<BallController>();
        enemy.CurrentBall = currentBall.transform;
        lastBallWasDestroyed = false;
    }

    public void OnBallDestroyed(bool aimWasGot)
    {
        lastBallWasDestroyed = true;
        GameState = GameStates.NoBall;
        if (aimWasGot)
        {
            currentAimsCounter--;
            if (currentAimsCounter == 0)
            {
                currentAimsCounter = 3;
                LevelUpGame();
            }
        }

        CreateBall();
    }

    private void LevelUpGame()
    {
        Instantiate(aimsPrefab, parentTransform);
        level++;
        enemy.IncreaseSpeed(config.difficultyRise);
        uiManager.UpdateLevelMonitorText(level);
    }

    public void PauseGame()
    {
        currentBall.PauseMovement();
        GameState = GameStates.Pause;
        inputController.Enabled = false;
    }

    public void ResumeGame()
    {
        currentBall.ResumeMovement();
        GameState = GameStates.BallMoves;
        inputController.Enabled = true;
    }

    #region Drag Logic

    private void Update()
    {
        if (!inputController.IsDragging) return;
        if (inputController.DragDelta.magnitude <= config.startSensitivity) return;

        if (!arrowIsCreated)
        {
            arrowTransform = Instantiate(arrowPrefab, parentTransform).transform;
            arrowIsCreated = true;
        }

        playerTransform.position = CalculatePlayerPosition(
            ballStartPosition.position, inputController.DragDelta);

        DrawArrow(ballStartPosition.position, playerTransform.position);
    }

    private Vector3 CalculatePlayerPosition(Vector3 originPosition, Vector2 dragDelta)
    {
        Vector3 playerPosition = originPosition + config.draggingSensitivity *
            new Vector3(dragDelta.y >= 0 ? dragDelta.x : -dragDelta.x,
                0.5f, Mathf.Abs(dragDelta.y));

        float hypothesis = CalculateHypothesis(playerPosition, originPosition);
        if (hypothesis > config.playerBoarder)
        {
            float alpha = config.playerBoarder / (hypothesis - config.playerBoarder);
            playerPosition =
                new Vector3((originPosition.x + alpha * playerPosition.x) / (1 + alpha), 0.5f,
                    (originPosition.z + alpha * playerPosition.z) / (1 + alpha));
        }

        return playerPosition;
    }

    private void DrawArrow(Vector3 originPosition, Vector3 playerPosition)
    {
        arrowTransform.position = (originPosition + playerPosition) / 2;
        arrowTransform.LookAt(playerPosition);
        arrowTransform.localScale = CalculateArrowScale(originPosition, playerPosition);
    }

    private Vector3 CalculateArrowScale(Vector3 originPosition, Vector3 playerPosition)
    {
        float scale = CalculateHypothesis(playerPosition, originPosition) * config.arrowScaleRatio;
        return new Vector3(scale, 1, scale);
    }

    private float CalculateHypothesis(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.z - b.z, 2));
    }

    #endregion

    private void PushBall()
    {
        if (!arrowIsCreated) return;

        //resetting the arrow
        arrowIsCreated = false;
        Destroy(arrowTransform.gameObject);

        //destroying old ball and creating new
        if (!lastBallWasDestroyed)
        {
            currentBall.DestroyBall();
            CreateBall();
        }

        //Pushing the ball
        currentBall.StartMovement(
            CalculateBallVelocity(playerTransform.position, ballStartPosition.position));

        GameState = GameStates.BallMoves;
    }

    private Vector3 CalculateBallVelocity(Vector3 playerPosition, Vector3 originPosition)
    {
        return config.ballSpeed * new Vector3(
            playerPosition.x - originPosition.x,
            0, playerPosition.z - originPosition.z);
    }
}

public enum GameStates
{
    Pause,
    BallMoves,
    NoBall
}
