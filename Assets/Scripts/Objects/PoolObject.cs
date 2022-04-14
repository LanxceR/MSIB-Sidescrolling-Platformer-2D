using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum for pool object type
public enum PoolObjectType
{
    FIREBALL
}
public class PoolObject : MonoBehaviour
{
    [Header("Main Setting")]
    public PoolObjectType ObjectType;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        Deactivate();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }
    public void Activate(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    internal bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }
}
