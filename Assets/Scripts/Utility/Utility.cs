using UnityEngine;


public static class Utility
{

    public static class Component
    {
        // Compute and returns a value againt which the grid should scale itself
        public static float GetScale(int rowCount, int colCount)
        {
            float dim = Mathf.Max(rowCount, colCount);
            return Mathf.Lerp(1, 2, (App.GridDimensionBounds.Max - dim) / App.GridDimensionBounds.Min);
        }
    }

    
    public static class Map
    {
        // Map and returns the absolute position on screen of the corner based on its row and column values
        public static Vector3 CornerCoordToPos(int row, int col, int rowCount, int colCount)
        {
            Vector3 pos = new Vector3();
            pos.x = (col - colCount * 0.5f);
            pos.y = (rowCount * 0.5f - row);
            pos.z = 0;
            return pos;
        }

        // Map and returns the absolute position on screen of the horizontal edge based on its row and column values
        public static Vector3 HEdgeCoordToPos(int row, int col, int rowCount, int colCount)
        {
            Vector3 pos = new Vector3();
            pos.x = (col - (colCount - 1) * 0.5f);
            pos.y = (rowCount * 0.5f - row);
            pos.z = 0;
            return pos;
        }

        // Map and returns the absolute position on screen of the vertical edge based on its row and column values
        public static Vector3 VEdgeCoordToPos(int row, int col, int rowCount, int colCount)
        {
            Vector3 pos = new Vector3();
            pos.x = (col - colCount * 0.5f);
            pos.y = ((rowCount - 1) * 0.5f - row);
            pos.z = 0;
            return pos;
        }

        // Map and returns the absolute position on screen of the box based on its row and column values
        public static Vector3 BoxCoordToPos(int row, int col, int rowCount, int colCount)
        {
            Vector3 pos = new Vector3();
            pos.x = (col - (colCount - 1) * 0.5f);
            pos.y = ((rowCount - 1) * 0.5f - row);
            pos.z = 0;
            return pos;
        }
    }

}
