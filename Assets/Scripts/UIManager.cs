using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private AppManager appManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator menuPanelAnimator;
    [SerializeField] private Text startOrResumeButtonText;
    [SerializeField] private Text levelMonitorText;
    private bool gameWasStarted;
    private static readonly int Out = Animator.StringToHash("Out");
    private static readonly int In = Animator.StringToHash("In");

    #region Menu
 
    public void StartOrResumeButton()
    {
        menuPanelAnimator.SetTrigger(Out);
        
        if (!gameWasStarted)
        {
            gameManager.StartGame();
            startOrResumeButtonText.text = "Resume";
            gameWasStarted = true;
        }
        else
        {
            gameManager.ResumeGame();
        }
    }

    public void ExitButton()
    {
        appManager.Exit();
    }
    
    #endregion

    #region Game
    
    public void PauseButton()
    {
        menuPanelAnimator.SetTrigger(In);
        gameManager.PauseGame();
    }

    public void UpdateLevelMonitorText(int level)
    {
        levelMonitorText.text = $"Level {level}";
}

    #endregion
}
