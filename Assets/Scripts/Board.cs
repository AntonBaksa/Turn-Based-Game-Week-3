using UnityEngine;

public class Board : ActionStack.Action
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3 TileToWorld(Vector2Int tile)
    {
        return new Vector3(tile.x, 0.0f, tile.y);
    }

    public static Vector2Int ToXZ(Vector3 v)
    {
        return new Vector2Int(Mathf.RoundToInt(v.x),
                              Mathf.RoundToInt(v.z));
    }
}
