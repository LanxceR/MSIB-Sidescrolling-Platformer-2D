using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // Singleton instance
    private static ObjectPooler instance;

    [Header("Main Setting")]
    [SerializeField] private Transform Parent;
    [SerializeField] private int Size; // Amount to spawn
    [SerializeField] private GameObject[] Prefabs; // Array of prefabs as pooled objects

    [SerializeField] private List<PoolObject> poolObjects;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InstantiateObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Instantiate ALL objects
    private void InstantiateObjects()
    {
        poolObjects = new List<PoolObject>();
        for (int i = 0; i < Size; i++)
        {
            // Spawn all objects in Prefabs[] array * Size
            foreach (GameObject obj in Prefabs)
            {
                poolObjects.Add(Instantiate(obj, Parent).GetComponent<PoolObject>());
            }
        }
    }

    // Request a ready & inactive pooled object
    public PoolObject RequestObject(PoolObjectType type)
    {
        foreach (PoolObject obj in poolObjects)
        {
            // Look for an inactive object in the pool array, then fetch it
            if (obj.ObjectType == type && !obj.IsActive())
            {
                return obj;
            }
        }
        // Otherwise fetch nothing
        return null;
    }

    public static ObjectPooler GetInstance()
    {
        return instance;
    }

    public void DeactivateAllPoolObjects()
    {
        foreach (PoolObject obj in poolObjects)
        {
            obj.Deactivate();
        }
    }
}
