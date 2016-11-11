using UnityEngine;


public class Edge : MonoBehaviour
{

    #region Types
    // Function pointer (function blueprint) valid for broadcasting purpose
    public delegate void ClickAction();
    // Subscription event for observers containing functions similar to ClickAction
    public static event ClickAction OnClickEvent;
    #endregion


    #region Field Data
    // Cached renderer component
    private SpriteRenderer rend;
    // Cached collider component
    private Collider2D coll2D;
    #endregion


    #region Properties
    // State tracking property
    public EdgeState State { get; private set; }
    #endregion


    #region Methods
    // Runs only once at the start of the game loop
    public void Start()
    {
        State = EdgeState.Empty;
        coll2D = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();
        rend.material.SetColor(App.SpriteRenderer.MainColorProperty, App.ColorPalette.White);
    }

    // Runs whenever the game loop updates
    public void Update()
    {
        if (OnClickEvent == null || State == EdgeState.Filled)
        {
            return;
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);

                if (coll2D == Physics2D.OverlapPoint(touchPos))
                {
                    State = EdgeState.Filled;
                    rend.material.SetColor(App.SpriteRenderer.MainColorProperty, App.ColorPalette.Gray);
                    OnClickEvent();
                }
            }
        }
    }

    // Runs whenever this edge needs to be reset
    public void Reset()
    {
        State = EdgeState.Empty;
        rend.material.SetColor(App.SpriteRenderer.MainColorProperty, App.ColorPalette.White);
    }
    #endregion


    #region INPUT METHODS FOR STAND ALONE SYSTEMS
    //// Triggers whenever mouse pointer enters on this
    //public void OnMouseEnter()
    //{
    //    if (State != EdgeState.Filled)
    //    {
    //        rend.material.SetColor(App.SpriteRenderer.MainColorProperty, App.ColorPalette.LightGray);
    //    }
    //}

    //// Triggers whenever mouse pointer exits from this
    //public void OnMouseExit()
    //{
    //    if (State != EdgeState.Filled)
    //    {
    //        rend.material.SetColor(App.SpriteRenderer.MainColorProperty, App.ColorPalette.White);
    //    }
    //}

    //// Triggers whenever mouse clicks on this
    //public void OnMouseDown()
    //{
    //    if (State != EdgeState.Filled)
    //    {
    //        State = EdgeState.Filled;
    //        rend.material.SetColor(App.SpriteRenderer.MainColorProperty, App.ColorPalette.Gray);

    //        if (OnClickEvent != null)
    //        {
    //            OnClickEvent();
    //        }
    //    }
    //}
    #endregion

}
