using UnityEngine;


public class Box : MonoBehaviour
{

    #region Field Data
    // Cached renderer component
    private SpriteRenderer rend;
    // Complete state tracking variable
    private bool hasCompleted;
    #endregion


    #region Properties
    // State tracking property
    public BoxState State { get; private set; }
    #endregion
    

    #region Methods
    // Runs only once at the start of the game loop
    public void Start()
    {
        State = BoxState.Unsaturated;
        hasCompleted = false;
        rend = GetComponent<SpriteRenderer>();
        rend.material.SetColor(App.SpriteRenderer.MainColorProperty, App.ColorPalette.Black);
    }
    
    // Updates state on the basis of the number of filled neighbour/boundary edges
    public void UpdateState(int filledNeighbourCount, Color currentPlayerColor, out bool completedJustNow)
    {
        completedJustNow = false;

        switch (filledNeighbourCount)
        {
            case 0:
            case 1:
                State = BoxState.Unsaturated;
                break;
            case 2:
                State = BoxState.Saturated;
                break;
            case 3:
                State = BoxState.Trigger;
                break;
            case 4:
                State = BoxState.Complete;
                HandleCompletion(currentPlayerColor, out completedJustNow);
                break;
        }
    }

    // Runs whenever this box needs to be reset
    public void Reset()
    {
        State = BoxState.Unsaturated;
        hasCompleted = false;
        rend.material.SetColor(App.SpriteRenderer.MainColorProperty, App.ColorPalette.Black);
    }
    #endregion


    #region Helper Methods
    // Handles all necessary operations on the completion of a box
    private void HandleCompletion(Color currentPlayerColor, out bool completedJustNow)
    {
        completedJustNow = false;

        if (!hasCompleted)
        {
            hasCompleted = true;
            completedJustNow = true;
            rend.material.SetColor(App.SpriteRenderer.MainColorProperty, currentPlayerColor);
        }
    }
    #endregion

}
