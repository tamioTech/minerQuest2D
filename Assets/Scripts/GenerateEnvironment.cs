// GameDev.tv Challenge Club. Got questions or want to share your nifty solution?
// Head over to - http://community.gamedev.tv

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    [SerializeField] private GameObject groundTilePrefab;
    [SerializeField] private Sprite[] groundSprites;
    [SerializeField] private GameObject rockTilePrefab;
    [SerializeField] private Sprite[] rockSprites;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Sprite[] foodSprites;
    [SerializeField] private GameObject exitPrefab;
    [SerializeField] private int amountOfEnemies;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] public GameObject parentTransform;
    [SerializeField] public GameObject foodObj;
    [SerializeField] public GameObject rockObj;
    [SerializeField] public GameObject groundObj;
    [SerializeField] public GameObject exitObj;

    private List<Vector2> exitVector2List = new List<Vector2>();
    private int exitLocation;
    private bool exitCreated = false;
    private int floorSizeY;
    private int floorSizeX;

    private void Awake()
    {
        floorSizeX = Random.Range(8, 19);
        floorSizeY = Random.Range(8, 19);

        exitVector2List.Add(new Vector2(0, 0));
        exitVector2List.Add(new Vector2(0, floorSizeY-1));
        exitVector2List.Add(new Vector2(floorSizeX-1, 0));
        exitVector2List.Add(new Vector2(floorSizeX-1, floorSizeY-1));
    }


    void Start()
    {
        GenerateFloor();

        SpawnExit();
        SpawnEnemies();
    }

    private void GenerateFloor()
    {
        

        for (int x = 0; x < floorSizeX; x++)
        {
            for (int y = 0; y < floorSizeY; y++)
            {
                InstantiateFloorTile(x, y);
            }
        }
    }


    private void InstantiateFloorTile(int x, int y)
    {
        SpawnExit();

        GameObject newFloorTile = Instantiate(groundTilePrefab, new Vector2(x, y), Quaternion.identity, groundObj.transform);
            

        int rngGround = Random.Range(1, 3);
        int rngFood = Random.Range(1, 3);
        switch(rngGround)
        {
            case 1:
                //print("case0");
                newFloorTile.GetComponent<SpriteRenderer>().sprite = groundSprites[Random.Range(0, groundSprites.Length)];

                switch(rngFood)
                {
                    case 1:
                        //print("noFood");
                        break;

                    case 2:
                        GameObject newFoodTile = Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity, foodObj.transform);
                        newFoodTile.GetComponent<SpriteRenderer>().sprite = foodSprites[Random.Range(0, foodSprites.Length)];
                        break;

                    default:
                        //print("rngFood error");
                        break;
                }
                break;
            case 2:
                //print("case1");
                newFloorTile.AddComponent<BoxCollider2D>();
                newFloorTile.GetComponent<SpriteRenderer>().sprite = rockSprites[Random.Range(0, rockSprites.Length)];
                newFloorTile.tag = "Rock";
                break;
            default:
                print("whichSpriteError");
                break;
        }
    }
    

    private void SpawnExit()
    {
        if (!exitCreated)
        {
            int rngExit = Random.Range(0, 4);
            GameObject exitTile = Instantiate(exitPrefab, exitVector2List[rngExit], Quaternion.identity, exitObj.transform);
            exitCreated = true;
        }
    }
     
    private void SpawnEnemies()
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            Vector2 spawnLocation = new Vector2(Random.Range(0, 19), Random.Range(0, 19));
            GameObject newEnemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        }
    }
}
