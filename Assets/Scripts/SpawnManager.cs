using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int treeCount;
    [SerializeField] private int creaturesCount;
    [SerializeField] private int grassCount;
    [SerializeField] private int numberOfAbilities;
    [SerializeField] private GameObject[] trees;
    [SerializeField] private GameObject[] creatures;
    [SerializeField] private GameObject[] grass;
    [SerializeField] private GameObject healthPotion;
    [SerializeField] private GameObject abilityPrefab;
    [SerializeField] private LayerMask layerMask;
    private GameObject plane;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.IsInitialized);
        plane = GameObject.FindGameObjectWithTag("Ground");
        SpawnObjectsOnPlane();
    }

    private void SpawnObjectsOnPlane()
    {
        Collider planeCollider = plane.GetComponent<MeshCollider>();
        float min = plane.transform.position.x - (planeCollider.bounds.size.x / 2);
        float max = plane.transform.position.x + (planeCollider.bounds.size.x / 2);
        SpawnTrees(min, max);
        SpawnCreatures(min, max);
        SpawnGrass(min, max);
        SpawnHealth(min, max);
        SpawnAbilities(min, max);
    }

    private void SpawnTrees(float min, float max)
    {
        for (int i = 0; i < treeCount; i++)
        {
            int index = Random.Range(0, trees.Length);
            GameObject tree = trees[index];
            tree.transform.localScale = Vector3.one * Random.Range(0.2f, 0.5f);
            Instantiate(tree, DetermineSpawnCoordinates(min, max, 2.0f), RandomQuaternion(), GameObject.Find("Environment").transform);
        }
    }

    private void SpawnCreatures(float min, float max)
    {
        for (int i = 0; i < creaturesCount; i++)
        {
            int index = Random.Range(0, creatures.Length);
            Instantiate(creatures[index], DetermineSpawnCoordinates(min, max, 2.0f), RandomQuaternion(), GameObject.Find("Creatures").transform);
        }
    }

    private void SpawnGrass(float min, float max)
    {
        for (int i = 0; i < grassCount; i++)
        {
            int index = Random.Range(0, grass.Length);
            Instantiate(grass[index], DetermineSpawnCoordinates(min, max, 0.0f), RandomQuaternion(), GameObject.Find("Grass").transform);
        }
    }

    private void SpawnHealth(float min, float max)
    {
        int numberOfHealthPotions = Random.Range(1, 3);
        for (int i = 0; i < numberOfHealthPotions; i++)
        {
            Instantiate(healthPotion, DetermineSpawnCoordinates(min, max, 0.5f, 0.25f), healthPotion.transform.rotation, GameObject.Find("Pickups").transform);
        }
    }

    private void SpawnAbilities(float min, float max)
    {
        for (int i = 0; i < numberOfAbilities; i++)
        {
            Ability.AbilityData abilityData = GameManager.playerAbilities[Random.Range(0, GameManager.playerAbilities.Length)];
            Ability ability = abilityPrefab.GetComponent<Ability>();
            ability.SetImage(abilityData.imgIndex);
            ability.description = abilityData.description;
            ability.abilityName = abilityData.name;
            ability.cooldown = abilityData.cooldown;
            ability.damage = abilityData.damage;
            Instantiate(abilityPrefab, DetermineSpawnCoordinates(min, max, 0.5f, 0.25f), abilityPrefab.transform.rotation, GameObject.Find("Pickups").transform);
        }
    }

    private Vector3 DetermineSpawnCoordinates(float min, float max, float radius, float yComponent)
    {
        Vector3 vector = DetermineSpawnCoordinates(min, max, radius);
        return new Vector3(vector.x, yComponent, vector.z);
    }

    private Vector3 DetermineSpawnCoordinates(float min, float max, float radius)
    {
        while (true)
        {
            float spawnX = Random.Range(min, max);
            float spawnZ = Random.Range(min, max);
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(spawnX, 1.0f, spawnZ), radius, layerMask);
            bool isFreeSpace = hitColliders.Length == 0;
            if (hitColliders.Length > 0)
            {
                Debug.Log(hitColliders.Length);
            }
            if (isFreeSpace)
            {
                return new Vector3(spawnX, 0.0f, spawnZ);
            }
        }
    }

    private Quaternion RandomQuaternion()
    {
        Quaternion quat = Random.rotation;
        return new Quaternion(0, quat.y, 0, 1);
    }
}
