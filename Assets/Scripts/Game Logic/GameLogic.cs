
public static class GameLogic
{

    #region Types
    // Function pointer (function blueprint) valid for broadcasting purpose
    public delegate void TurnPlayAction();
    // Subscription event for observers containing functions similar to UpdateAction
    public static event TurnPlayAction OnTurnPlayEvent;
    #endregion
    

    #region Properties
    // Grid game object object tracking property
    public static Grid Grid { get; private set; }
    // Max player tracking property
    public static Player Max { get; private set; }
    // Min player tracking property
    public static Player Min { get; private set; }
    // Active player tracking property
    public static Player ActivePlayer { get; private set; }
    // Inactive player returning property
    public static Player InactivePlayer
    {
        get
        {
            Player inactivePlayer = Max;

            if (ActivePlayer == Max)
            {
                inactivePlayer = Min;
            }

            return inactivePlayer;
        }
    }
    #endregion
    
    
    #region Methods
    // Runs only once when the game starts
    public static void Setup(Grid grid)
    {
        Grid = grid;

        Max = new Player(App.MaxPlayerSettings.ColorName, PlayerRole.Max, App.MaxPlayerSettings.ColorValue);
        Min = new Player(App.MinPlayerSettings.ColorName, PlayerRole.Min, App.MinPlayerSettings.ColorValue);

        ActivePlayer = Max;
    }

    // Runs whenever a player plays turn
    public static void OnTurnPlayed(int boxesUpdated)
    {
        if (boxesUpdated > 0)
        {
            ActivePlayer.Scored(boxesUpdated);
        }
        else if (boxesUpdated == 0)
        {
            SwapTurn();
        }

        if (OnTurnPlayEvent != null)
        {
            OnTurnPlayEvent();
        }
    }

    // Runs whenever the game needs to be reset
    public static void Reset()
    {
        Grid.Reset();
        Max.Reset();
        Min.Reset();
        ActivePlayer = Max;

        if (OnTurnPlayEvent != null)
        {
            OnTurnPlayEvent();
        }
    }
    #endregion


    #region Helper Methods
    // Swaps the current player
    private static void SwapTurn()
    {
        ActivePlayer = InactivePlayer;
    }
    #endregion
    
}
