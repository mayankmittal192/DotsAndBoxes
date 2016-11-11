using UnityEngine;


public class Grid : MonoBehaviour
{

    #region Field Data
    // Minimum bound for grid's dimension (row or column)
    private const int MIN_COUNT = App.GridDimensionBounds.Min;
    // Maximum bound for grid's dimension (row or column)
    private const int MAX_COUNT = App.GridDimensionBounds.Max;
    // Scale of this grid
    private float Scale;
    // Row count tracking variable
    private int rowCount;
    // Column count tracking variable
    private int colCount;
    // Corner prefab storing field to use at runtime for making corner components
    public GameObject corner;
    // Edge prefab storing field to use at runtime for making edge components
    public GameObject edge;
    // Box prefab storing field to use at runtime for making box components
    public GameObject box;
    // Parent game object for containing corners
    public GameObject cornerParent;
    // Parent game object for containing horizontal edges
    public GameObject hEdgeParent;
    // Parent game object for containing vertical edges
    public GameObject vEdgeParent;
    // Parent game object for containing boxes
    public GameObject boxParent;
    // Transform component for containing corners
    private Transform cornerParentTransform;
    // Transform component for containing horizontal edges
    private Transform hEdgeParentTransform;
    // Transform component for containing vertical edges
    private Transform vEdgeParentTransform;
    // Transform component for containing boxes
    private Transform boxParentTransform;
    #endregion


    #region Properties
    // Row count tracking property
    public int RowCount
    {
        get
        {
            return rowCount;
        }

        private set
        {
            rowCount = Mathf.Clamp(value, MIN_COUNT, MAX_COUNT);
        }
    }
    // Column count tracking property
    public int ColCount
    {
        get
        {
            return colCount;
        }

        private set
        {
            colCount = Mathf.Clamp(value, MIN_COUNT, MAX_COUNT);
        }
    }
    // Array list for storing corner components
    public GameObject[,] Corner { get; private set; }
    // Array list for storing horizontal edge components
    public GameObject[,] HEdge { get; private set; }
    // Array list for storing vertical edge components
    public GameObject[,] VEdge { get; private set; }
    // Array list for storing box components
    public GameObject[,] Box { get; private set; }
    // Number of boxes in complete state tracking property
    public int CompleteBoxCount { get; private set; }
    #endregion


    #region Methods
    // Runs only once when this script is enabled
    public void OnEnable()
    {
        Edge.OnClickEvent += OnEdgeClicked;
    }

    // Runs only once when this script is disabled
    public void OnDisable()
    {
        Edge.OnClickEvent -= OnEdgeClicked;
    }

    // Runs only once at the time of the game initialization
    public void Awake()
    {
        RowCount = 4;
        ColCount = 4;

        Scale = Utility.Component.GetScale(RowCount, ColCount);

        cornerParentTransform = cornerParent.GetComponent<Transform>();
        hEdgeParentTransform = hEdgeParent.GetComponent<Transform>();
        vEdgeParentTransform = vEdgeParent.GetComponent<Transform>();
        boxParentTransform = boxParent.GetComponent<Transform>();

        Corner = new GameObject[RowCount + 1, ColCount + 1];
        HEdge = new GameObject[RowCount + 1, ColCount];
        VEdge = new GameObject[RowCount, ColCount + 1];
        Box = new GameObject[RowCount, ColCount];

        for (int row = 0; row < Corner.GetLength(0); row++)
        {
            for (int col = 0; col < Corner.GetLength(1); col++)
            {
                Corner[row, col] = InstantiateCorner(row, col);
            }
        }

        for (int row = 0; row < HEdge.GetLength(0); row++)
        {
            for (int col = 0; col < HEdge.GetLength(1); col++)
            {
                HEdge[row, col] = InstantiateHEdge(row, col);
            }
        }

        for (int row = 0; row < VEdge.GetLength(0); row++)
        {
            for (int col = 0; col < VEdge.GetLength(1); col++)
            {
                VEdge[row, col] = InstantiateVEdge(row, col);
            }
        }

        for (int row = 0; row < Box.GetLength(0); row++)
        {
            for (int col = 0; col < Box.GetLength(1); col++)
            {
                Box[row, col] = InstantiateBox(row, col);
            }
        }

        CompleteBoxCount = 0;

        transform.position += App.GridPosition.Offset;

        GameLogic.Setup(this);
    }

    // Runs whenever an empty edge is clicked
    public void OnEdgeClicked()
    {
        int boxesUpdated = 0;

        for (int row = 0; row < Box.GetLength(0); row++)
        {
            for (int col = 0; col < Box.GetLength(1); col++)
            {
                bool completedJustNow = false;
                int filledNeighbourCount = CountFilledNeighbourEdges(row, col);
                Box[row, col].GetComponent<Box>().UpdateState(filledNeighbourCount, GameLogic.ActivePlayer.Color, out completedJustNow);
                
                if (completedJustNow)
                {
                    boxesUpdated++;
                }
            }
        }

        CompleteBoxCount += boxesUpdated;

        GameLogic.OnTurnPlayed(boxesUpdated);
    }

    // Runs whenever this grid needs to be reset
    public void Reset()
    {
        for (int row = 0; row < HEdge.GetLength(0); row++)
        {
            for (int col = 0; col < HEdge.GetLength(1); col++)
            {
                HEdge[row, col].GetComponent<Edge>().Reset();
            }
        }

        for (int row = 0; row < VEdge.GetLength(0); row++)
        {
            for (int col = 0; col < VEdge.GetLength(1); col++)
            {
                VEdge[row, col].GetComponent<Edge>().Reset();
            }
        }

        for (int row = 0; row < Box.GetLength(0); row++)
        {
            for (int col = 0; col < Box.GetLength(1); col++)
            {
                Box[row, col].GetComponent<Box>().Reset();
            }
        }

        CompleteBoxCount = 0;
    }
    #endregion


    #region Helper Methods
    // Instantiates a corner component
    private GameObject InstantiateCorner(int row, int col)
    {
        Vector3 position = Utility.Map.CornerCoordToPos(row, col, RowCount, ColCount) * Scale;
        Quaternion rotation = Quaternion.identity;
        Transform parent = cornerParentTransform;
        return (GameObject)Instantiate(corner, position, rotation, parent);
    }

    // Instantiates a horizontal edge component
    private GameObject InstantiateHEdge(int row, int col)
    {
        Vector3 position = Utility.Map.HEdgeCoordToPos(row, col, RowCount, ColCount) * Scale;
        Quaternion rotation = Quaternion.identity;
        Transform parent = hEdgeParentTransform;
        return (GameObject)Instantiate(edge, position, rotation, parent);
    }

    // Instantiates a vertical edge component
    private GameObject InstantiateVEdge(int row, int col)
    {
        Vector3 position = Utility.Map.VEdgeCoordToPos(row, col, RowCount, ColCount) * Scale;
        Quaternion rotation = Quaternion.Euler(0, 0, 90);
        Transform parent = vEdgeParentTransform;
        return (GameObject)Instantiate(edge, position, rotation, parent);
    }

    // Instantiates a box component
    private GameObject InstantiateBox(int row, int col)
    {
        Vector3 position = Utility.Map.BoxCoordToPos(row, col, RowCount, ColCount) * Scale;
        Quaternion rotation = Quaternion.identity;
        Transform parent = boxParentTransform;
        return (GameObject)Instantiate(box, position, rotation, parent);
    }

    // Count and returns the number of edges surrounding the box in Filled state
    private int CountFilledNeighbourEdges(int row, int col)
    {
        int filledNeighbourCount = 0;

        if (IsEdgeFilled(EdgeType.Horizontal, row, col))
        {
            filledNeighbourCount++;
        }
        if (IsEdgeFilled(EdgeType.Horizontal, row + 1, col))
        {
            filledNeighbourCount++;
        }
        if (IsEdgeFilled(EdgeType.Vertical, row, col))
        {
            filledNeighbourCount++;
        }
        if (IsEdgeFilled(EdgeType.Vertical, row, col + 1))
        {
            filledNeighbourCount++;
        }

        return filledNeighbourCount;
    }

    // Check and returns whether the given edge has filled state or not
    private bool IsEdgeFilled(EdgeType type, int row, int col)
    {
        bool isEdgeFilled = false;

        try
        {
            switch (type)
            {
                case EdgeType.Horizontal:
                    isEdgeFilled = (HEdge[row, col].GetComponent<Edge>().State == EdgeState.Filled);
                    break;

                case EdgeType.Vertical:
                    isEdgeFilled = (VEdge[row, col].GetComponent<Edge>().State == EdgeState.Filled);
                    break;
            }
        }
        catch { }

        return isEdgeFilled;
    }
    #endregion

}
