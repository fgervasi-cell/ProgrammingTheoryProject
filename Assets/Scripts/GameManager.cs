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
    private Text healthDispalay;
    [SerializeField]
    private int environmentalObjectsCount;
    [SerializeField]
    private GameObject[] environmentalObjects;
    private GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        plane = GameObject.FindGameObjectWithTag("Ground");
        Collider planeCollider = plane.GetComponent<MeshCollider>();
        float min = plane.transform.position.x - (planeCollider.bounds.size.x / 2);
        float max = plane.transform.position.x + (planeCollider.bounds.size.x / 2);
        for (int i = 0; i < environmentalObjectsCount; i++)
        {
            float spawnX = Random.Range(min, max);
            float spawnZ = Random.Range(min, max);
            int index = Random.Range(0, environmentalObjects.Length - 1);
            Instantiate(environmentalObjects[index], new Vector3(spawnX, 0.0f, spawnZ), Quaternion.identity, GameObject.Find("Environment").transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthDispalay.text = $"Health: {player.LifePoints}";
    }
}
