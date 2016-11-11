using UnityEngine;
using UnityEngine.UI;


public class StatManager : MonoBehaviour
{

    #region Field Data
    // Max player score game object tracking variable
    public Text maxScore;
    // Min player score game object tracking variable
    public Text minScore;
    // Max player score text component tracking variable
    private Text maxScoreText;
    // Min player score text component tracking variable
    private Text minScoreText;
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
        maxScoreText = maxScore.GetComponent<Text>();
        minScoreText = minScore.GetComponent<Text>();
        maxScoreText.color = App.MaxPlayerSettings.ColorValue;
        minScoreText.color = App.MinPlayerSettings.ColorValue;
        UpdateStat();
    }

    // Runs whenever a player moves
    public void OnPlayerMove()
    {
        UpdateStat();
    }
    #endregion


    #region Helper Methods
    // Updates the game statistics
    private void UpdateStat()
    {
        maxScoreText.text = App.ScoreBoard.GetScore(GameLogic.Max.Name, GameLogic.Max.Score);
        minScoreText.text = App.ScoreBoard.GetScore(GameLogic.Min.Name, GameLogic.Min.Score);
        GetScoreText(GameLogic.ActivePlayer).fontSize = App.StatSettings.ActivePlayerFontSize;
        GetScoreText(GameLogic.ActivePlayer).fontStyle = App.StatSettings.ActivePlayerFontStyle;
        GetScoreText(GameLogic.InactivePlayer).fontSize = App.StatSettings.NormalPlayerFontSize;
        GetScoreText(GameLogic.InactivePlayer).fontStyle = App.StatSettings.NormalPlayerFontStyle;
    }

    // Returns the score text of the given player
    private Text GetScoreText(Player player)
    {
        Text score = maxScoreText;

        if (player == GameLogic.Min)
        {
            score = minScoreText;
        }

        return score;
    }
    #endregion

}
