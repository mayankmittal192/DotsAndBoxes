using UnityEngine;


public static class App
{

    // Theme Colors of the App
    public static class ColorPalette
    {
        public static Color Black = new Color(0.1f, 0.1f, 0.1f);
        public static Color DarkGray = new Color(0.4f, 0.4f, 0.4f);
        public static Color Gray = new Color(0.6f, 0.6f, 0.6f);
        public static Color LightGray = new Color(0.8f, 0.8f, 0.8f);
        public static Color White = new Color(0.95f, 0.95f, 0.95f);
        public static Color Red = new Color(1.0f, 0.0f, 0.4f);
        public static Color Green = new Color(0.0f, 0.8f, 0.8f);
        public static Color TransparentRed = new Color(1.0f, 0.0f, 0.4f, 0.8f);
        public static Color TransparentGreen = new Color(0.0f, 0.8f, 0.8f, 0.8f);
        public static Color TransparentGray = new Color(0.6f, 0.6f, 0.6f, 0.8f);
    }


    // 2D Sprite Renderer Component of Game Components
    public static class SpriteRenderer
    {
        public const string MainColorProperty = "_Color";
    }

    
    // Grid's dimension (row count or column count) bounding values
    public static class GridDimensionBounds
    {
        public const int Min = 4;
        public const int Max = 8;
    }


    // Grid's position offset value
    public static class GridPosition
    {
        public static Vector3 Offset = new Vector3(-3, 0, 0);
    }


    // Score board canvas of the App
    public static class ScoreBoard
    {
        public static string GetScore(string scoreBase, int scoreValue)
        {
            return (scoreBase + ": " + scoreValue);
        }
    }


    // Statistics settings of the App
    public static class StatSettings
    {
        public const int NormalPlayerFontSize = 48;
        public const int ActivePlayerFontSize = 64;
        public const FontStyle NormalPlayerFontStyle = FontStyle.Normal;
        public const FontStyle ActivePlayerFontStyle = FontStyle.Bold;
    }


    // Game over overlay canvas of the App
    public static class GameOverOverlay
    {
        public static Color DrawColorValue = ColorPalette.TransparentGray;

        public static string GetResultText(string winnerColorName, bool isDraw)
        {
            string result;

            if (!isDraw)
            {
                result = winnerColorName + " Won !";
            }
            else
            {
                result = "Draw !";
            }

            return result;
        }
    }
    

    // Max player settings
    public static class MaxPlayerSettings
    {
        public const string ColorName = "Red";
        public static Color ColorValue = ColorPalette.Red;
        public static Color WinColorValue = ColorPalette.TransparentRed;
    }


    // Min player settings
    public static class MinPlayerSettings
    {
        public const string ColorName = "Green";
        public static Color ColorValue = ColorPalette.Green;
        public static Color WinColorValue = ColorPalette.TransparentGreen;
    }

}
