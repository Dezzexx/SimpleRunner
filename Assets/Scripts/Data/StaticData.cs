using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticData", menuName = "Data/StaticData", order = 0)]
public class StaticData : ScriptableObject {
    public GameObject BrainPrefab;
    public List<GameObject> Tiles = new List<GameObject>();
    public List<GameObject> Brains = new List<GameObject>();
    [Range(20f, 50f)] public float MoveSpeed;
    public int TileCount;
}
