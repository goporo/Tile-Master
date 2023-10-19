
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "ScriptableObjects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public List<LevelContent> levelContent = new();
}

[System.Serializable]
public class LevelContent
{
    public string levelName;
    public int levelNumber;
    public int playTime;
    public List<LevelTile> levelTiles = new();
}

[System.Serializable]
public class LevelTile
{
    public TileType tileType;
    public Sprite tileSprite;
    public int tileChance;
}

public enum TileType
{
    Tile_00,
    Tile_01,
    Tile_02,
    Tile_03,
    Tile_04,
    Tile_05,

}