using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticData", menuName = "Data/StaticData", order = 0)]
public class StaticData : ScriptableObject {
    public List<GameObject> Tiles = new List<GameObject>();
    [Range(20f, 50f)] public float MoveSpeed;
    public int TileCount;
}
