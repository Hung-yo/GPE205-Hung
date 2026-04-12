using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public enum RandomType { Random, Seeded, MapOfTheDay };
public class MapGenerator : MonoBehaviour
{
    public int seed;
    public TMP_InputField seedInputField;
    public RandomType randomType;
    public TMP_Dropdown typeDropdown;
    public List<Tile> availableTiles;
    public Tile bossTile;
    public Vector2 bossTileLocation;
    public float tileWidth;
    public float tileLength;
    public int mapCols;
    public int mapRows;

    public Tile[,] grid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGenerationType()
    {
        if (typeDropdown.options[typeDropdown.value].text == "Random Map")
        {
            randomType = RandomType.Random;
        }
        else if (typeDropdown.options[typeDropdown.value].text == "Custom Seed")
        {
            randomType = RandomType.Seeded;
            if (!int.TryParse(seedInputField.text, out seed))
            {
                seed = 0; // In case the user enters letters or something, default to 0
            }
        }
        else
        {
            randomType = RandomType.MapOfTheDay;
        }
    }

    public void InitializeRandom()
    {
        if (randomType == RandomType.Seeded)
        {
            UnityEngine.Random.InitState(seed);
        }
        else if (randomType == RandomType.Random)
        {
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        }
        else if (randomType == RandomType.MapOfTheDay)
        {
            UnityEngine.Random.InitState(DateToInt(DateTime.Now.Date));
        }
    }

    public int DateToInt(DateTime date)
    {
        return date.Year + date.Month + date.Day + date.Hour + date.Minute + date.Second;
    }

    public void GenerateMap()
    {
        InitializeRandom();

        grid = new Tile[mapCols, mapRows];

        for (int currentRow = 0; currentRow < mapRows; currentRow++)
        {
            for (int currentCol = 0; currentCol < mapCols; currentCol++)
            {
                Tile tempTile;

                if (currentCol == bossTileLocation.x && currentRow == bossTileLocation.y)
                {
                    tempTile = Instantiate<Tile>(bossTile) as Tile;
                }
                else
                {
                    tempTile = Instantiate<Tile>(GetRandomTile()) as Tile;
                }
                
                Vector3 correctPosition = Vector3.zero;
                correctPosition.x = currentRow * tileWidth;
                correctPosition.z = currentCol * tileLength;
                tempTile.transform.position = correctPosition;

                tempTile.name = "Tile (" + currentCol + "," + currentRow + ")";

                if (currentRow == 0)
                {
                    tempTile.doorNorth.SetActive(false);
                }
                else if (currentRow == mapRows - 1)
                {
                    tempTile.doorSouth.SetActive(false);
                }
                else
                {
                    tempTile.doorNorth.SetActive(false);
                    tempTile.doorSouth.SetActive(false);
                }

                if (currentCol == mapCols - 1)
                {
                    tempTile.doorWest.SetActive(false);
                }
                else if (currentCol == 0)
                {
                    tempTile.doorEast.SetActive(false);
                }
                else
                {
                    tempTile.doorWest.SetActive(false);
                    tempTile.doorEast.SetActive(false);
                }

                grid[currentRow, currentCol] = tempTile;
            }
        }
    }

    public Tile GetRandomTile()
    {
        int tileNumber = UnityEngine.Random.Range(0, availableTiles.Count);
        return availableTiles[tileNumber];
    }
}