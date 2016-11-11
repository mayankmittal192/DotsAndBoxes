using UnityEngine;


public class Player
{

    #region Properties
    // Name tracking property
    public string Name { get; private set; }
    // Role tracking property
    public PlayerRole Role { get; private set; }
    // Color Value tracking property
    public Color Color { get; private set; }
    // Score tracking property
    public int Score { get; private set; }
    #endregion


    #region Constructor
    // Constructor
    public Player(string name, PlayerRole role, Color color)
    {
        Name = name;
        Role = role;
        Color = color;
        Score = 0;
    }
    #endregion


    #region Methods
    // Increase this player's score by given number of boxes updated in a move
    public void Scored(int boxesUpdated)
    {
        Score += boxesUpdated;
    }

    // Runs whenever this player needs to be reset
    public void Reset()
    {
        Score = 0;
    }
    #endregion

}
