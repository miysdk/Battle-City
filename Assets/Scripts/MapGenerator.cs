using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileTypes
{
    Player,
    Brick,
    Metal,
    Air,
    Base,
    EnemySpawner
}

public class MapGenerator : MonoBehaviour
{
    public int width = 13;
    public int height = 13;
    public Transform mapObject;
    public GameObject cam;
    public GameObject brickPrefab;
    public GameObject metalPrefab;
    public GameObject playerPrefab;
    public GameObject basePrefab;
    public GameObject enemySpawner;

    TileTypes[,] map;

    private void Start()
    {
        GenerateMaze();
    }

    private void GenerateMaze()
    {
        map = new TileTypes[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    map[x, y] = TileTypes.Metal;
                else 
                    map[x, y] = TileTypes.Brick;
            }
        }
        
        RecursiveBacktracking(1, 1);
        for (int x = width / 2 - 1; x <= width / 2 + 1; x++)
        {
            for (int y = 1; y <= 2; y++)
            {
                if(y == 1 && x == width / 2)
                {
                    map[x, y] = TileTypes.Base;
                    continue;
                }

                map[x, y] = TileTypes.Brick;
            }
        }

        SetPlayerPos();
        SetEnemySpawners();

        DisplayMaze();

        AdjustCamera();
    }

    private void RecursiveBacktracking(int x, int y)
    {
        map[x, y] = TileTypes.Air;

        int[] directions = { 0, 1, 2, 3 };

        for (int i = 0; i < directions.Length; i++)
        {
            int temp = directions[i];
            int randomIndex = Random.Range(i, directions.Length);
            directions[i] = directions[randomIndex];
            directions[randomIndex] = temp;
        }

        for (int i = 0; i < directions.Length; i++)
        {
            int dx = 0, dy = 0;
            switch (directions[i])
            {
                case 0: // Up
                    dy = 2;
                    break;
                case 1: // Right
                    dx = 2;
                    break;
                case 2: // Down
                    dy = -2;
                    break;
                case 3: // Left
                    dx = -2;
                    break;
            }

            int newX = x + dx;
            int newY = y + dy;

            if (newX > 0 && newX < width && newY > 0 && newY < height && map[newX, newY] == TileTypes.Brick)
            {
                map[x + dx / 2, y + dy / 2] = TileTypes.Air;
                
                RecursiveBacktracking(newX, newY);
            }
        }
    }

    private void DisplayMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tmp = null;

                switch (map[x, y])
                {
                    case TileTypes.Brick:
                        tmp = Instantiate(brickPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case TileTypes.Metal:
                        tmp = Instantiate(metalPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case TileTypes.Player:
                        tmp = Instantiate(playerPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case TileTypes.Base:
                        tmp = Instantiate(basePrefab, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                    case TileTypes.EnemySpawner:
                        tmp = Instantiate(enemySpawner, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                }

                if(tmp != null && map[x, y] != TileTypes.Player)
                    tmp.transform.SetParent(mapObject);
            }
        }
    }

    private void SetPlayerPos()
    {
        int y = 1;
        for (int i = width / 2 - 1; i > 0; i--)
        {
            if(map[i, y] == TileTypes.Air){
                map[i, y] = TileTypes.Player;
                return;
            }
        }
    }

    void SetEnemySpawners()
    {
        int y = height - 2;
        map[1, y] = TileTypes.EnemySpawner;
        map[width - 2, y] = TileTypes.EnemySpawner;
        map[width / 2, y] = TileTypes.EnemySpawner;
    }

    void AdjustCamera()
    {
        Vector3 camPos = new Vector3(width / 2, height / 2, -10);
        cam.transform.position = camPos;
        cam.GetComponent<Camera>().orthographicSize = height * 0.5f;
    }
}