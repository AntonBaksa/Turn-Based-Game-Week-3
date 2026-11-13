using System;
using UnityEngine;

public enum Team
{
    Red,
    Blue
}

public class GamePiece : MonoBehaviour
{
    [SerializeField] private Team team;
    public Team Team => team;
    public Vector2Int? Tile {  get; set; }
}
