using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Text healthDisplay;
    [SerializeField]
    private int treeCount;
    [SerializeField]
    private int creaturesCount;
    [SerializeField]
    private GameObject[] trees;
    [SerializeField]
    private GameObject[] creatures;
    private Text scoreText;
    private GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        plane = GameObject.FindGameObjectWithTag("Ground");
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        SpawnObjectsOnPlane();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = $"Health: {player.LifePoints}";
        scoreText.text = $"Score: {player.Score}";
    }

    private void SpawnObjectsOnPlane()
    {
        Collider planeCollider = plane.GetComponent<MeshCollider>();
        float min = plane.transform.position.x - (planeCollider.bounds.size.x / 2);
        float max = plane.transform.position.x + (planeCollider.bounds.size.x / 2);
        SpawnTrees(min, max);
        SpawnCreatures(min, max);
    }

    private void SpawnTrees(float min, float max)
    {
        for (int i = 0; i < treeCount; i++)
        {
            int index = Random.Range(0, trees.Length);
            GameObject tree = trees[index];
            tree.transform.localScale = Vector3.one * Random.Range(0.2f, 0.5f);
            Instantiate(tree, DetermineSpawnCoordinates(min, max, 0.0f), RandomQuaternion(), GameObject.Find("Environment").transform);
        }
    }

    private void SpawnCreatures(float min, float max)
    {
        for (int i = 0; i < creaturesCount; i++)
        {
            int index = Random.Range(0, creatures.Length);
            Instantiate(creatures[index], DetermineSpawnCoordinates(min, max, 5.0f), Quaternion.identity, GameObject.Find("Creatures").transform);
        }
    }

    private Vector3 DetermineSpawnCoordinates(float min, float max, float radius)
    {
        while (true)
        {
            float spawnX = Random.Range(min, max);
            float spawnZ = Random.Range(min, max);
            bool isFreeSpace = !Physics.CheckSphere(new Vector3(spawnX, 1.0f, spawnZ), radius, 6);
            if (isFreeSpace)
            {
                return new Vector3(spawnX, 0.0f, spawnZ);
            }
        }
    }

    private Quaternion RandomQuaternion()
    {
        float x = Random.Range(0, 1);
        float y = Random.Range(0, 1);
        float z = Random.Range(0, 1);
        return new Quaternion(x, y, z, 1);
    }
}
