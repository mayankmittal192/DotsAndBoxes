using UnityEngine;


public class Corner : MonoBehaviour
{

    #region Field Data
    // Cached renderer component
    private SpriteRenderer rend;
    #endregion


    #region Methods
    // Runs only once at the start of the game loop
    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material.SetColor(App.SpriteRenderer.MainColorProperty, App.ColorPalette.DarkGray);
    }
    #endregion

}
