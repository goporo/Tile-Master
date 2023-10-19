
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
    public float playTime;
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
    Tile_06,
    Tile_07,
    Tile_08,
    Tile_09,
    Tile_10,
    Tile_11,
    Tile_12,
    Tile_13,
    Tile_14,
    Tile_15,
    Tile_16,
    Tile_17,
    Tile_18,
    Tile_19,
    Tile_20,
    Tile_21,
    Tile_22,
    Tile_23,
    Tile_24,
    Tile_25,
    Tile_26,

}