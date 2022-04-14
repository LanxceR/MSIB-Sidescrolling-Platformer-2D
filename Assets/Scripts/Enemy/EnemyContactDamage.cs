using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    private enum EventType
    {
        ENTER, STAY, EXIT
    }

    [Header("Main Settings")]
    [SerializeField] private float damage;
    [SerializeField] private EventType eventType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && eventType == EventType.ENTER && enabled)
        {
            // Damage player
            if (other.GetComponent<Health>())
                other.GetComponent<Health>().TakeDamage(damage);
        }
    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger (2D physics only)
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && eventType == EventType.EXIT && enabled)
        {
            // Damage player
            if (other.GetComponent<Health>())
                other.GetComponent<Health>().TakeDamage(damage);
        }
    }

    // OnTriggerStay2D is called once per frame for every Collider2D other that is touching the trigger (2D physics only)
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && eventType == EventType.STAY && enabled)
        {
            //Debug.Log("Collision: " + gameObject.tag + ", Collision tag: " + other.tag);
            // Damage player
            if (other.GetComponent<Health>())
                other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
