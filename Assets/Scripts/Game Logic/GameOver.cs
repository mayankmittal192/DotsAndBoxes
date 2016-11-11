using UnityEngine;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{

    #region Field Data
    // Game over panel game object tracking variable
    public GameObject gameOverPanel;
    // Result panel game object tracking variable
    public GameObject resultPanel;
    // Result game object tracking variable
    public GameObject result;
    // Result container component tracking variable
    private Image resultContainer;
    // Result text component tracking variable
    private Text resultText;
    #endregion


    #region Methods
    // Runs only once when this script is enabled
    public void OnEnable()
    {
        GameLogic.OnTurnPlayEvent += OnPlayerMove;
    }

    // Runs only once when this script is disabled
    public void OnDisable()
    {
        GameLogic.OnTurnPlayEvent -= OnPlayerMove;
    }

    // Runs only once at the start of the game loop
    public void Start()
    {
        gameOverPanel.SetActive(false);
        resultContainer = resultPanel.GetComponent<Image>();
        resultText = result.GetComponent<Text>();
    }

    // Runs whenever a player moves
    public void OnPlayerMove()
    {
        if (IsGameOver())
        {
            Player winner = GetWinner();
            bool isDraw = IsDraw();

            if (winner == GameLogic.Max)
            {
                resultContainer.color = App.MaxPlayerSettings.WinColorValue;
                resultText.text = App.GameOverOverlay.GetResultText(App.MaxPlayerSettings.ColorName, isDraw);
            }
            else if (winner == GameLogic.Min)
            {
                resultContainer.color = App.MinPlayerSettings.WinColorValue;
                resultText.text = App.GameOverOverlay.GetResultText(App.MinPlayerSettings.ColorName, isDraw);
            }
            else
            {
                resultContainer.color = App.GameOverOverlay.DrawColorValue;
                resultText.text = App.GameOverOverlay.GetResultText(null, isDraw);
            }

            gameOverPanel.SetActive(true);
        }
    }

    // Runs whenever the play again button is pressed
    public void OnPlayAgain()
    {
        gameOverPanel.SetActive(false);
        GameLogic.Reset();
    }

    // Runs whenever the quit button is pressed
    public void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion


    #region Helper Methods
    // Compute and returns whether the game is over or not
    private bool IsGameOver()
    {
        bool gameOver = false;

        int rows = GameLogic.Grid.RowCount;
        int cols = GameLogic.Grid.ColCount;

        if (GameLogic.Grid.CompleteBoxCount == (rows * cols))
        {
            gameOver = true;
        }

        return gameOver;
    }

    // Compute and returns the winner player
    private Player GetWinner()
    {
        Player winner = null;

        if (GameLogic.Max.Score > GameLogic.Min.Score)
        {
            winner = GameLogic.Max;
        }
        else if (GameLogic.Max.Score < GameLogic.Min.Score)
        {
            winner = GameLogic.Min;
        }

        return winner;
    }

    // Compute and returns whether the game ended up in a draw or not
    private bool IsDraw()
    {
        return (GameLogic.Max.Score == GameLogic.Min.Score);
    }
    #endregion

}
