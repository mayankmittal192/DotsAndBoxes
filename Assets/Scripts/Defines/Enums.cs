
/// <summary>
/// Role of the player in the game
/// Max => Player who plays first move
/// Min => Player who plays second move
/// </summary>
public enum PlayerRole
{
    Min = 0,
    Max = 1
}


/// <summary>
/// Type of the edge
/// Horizontal => Edge which connect corners horizontally
/// Vertical   => Edge which connect corners vertically
/// </summary>
public enum EdgeType
{
    Horizontal = 0,
    Vertical = 1
}


/// <summary>
/// State of the edge
/// Empty  => Corners connected by the edge are not joined
/// Filled => Corners connected by the edge are joined
/// </summary>
public enum EdgeState
{
    Empty = 0,
    Filled = 1
}


/// <summary>
/// State of the box
/// Unsaturated => Either zero or one edge surrounding the box is Filled
/// Saturated   => Exactly two edges surrounding the box are Filled
/// Trigger     => Exactly three edges surrounding the box are Filled
/// Complete    => Exactly four edges surrounding the box are Filled
/// </summary>
public enum BoxState
{
    Unsaturated = 0,
    Saturated = 1,
    Trigger = 2,
    Complete = 3
}


/// <summary>
/// State of the game
/// Beginning => // TODO
/// Middle    => // TODO
/// Deep      => // TODO
/// Climax    => // TODO
/// Saturated => Each box is either Saturated or Complete
/// </summary>
public enum GameState
{
    Beginning = 0,
    Middle = 1,
    Deep = 2,
    Climax = 3,
    Saturated = 4
}
