using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private string[] targetTags;

    [SerializeField] private UnityEvent onTrigger;
    [SerializeField] private UnityEvent<GameObject> onTriggerWithGameObject; //Unity event that accepts a gameObject parameter

    [SerializeField] private UnityEvent onTriggerExitEvent;

    [SerializeField] private UnityEvent onTriggerStayEvent;

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
        foreach (string tag in targetTags)
        {
            //Debug.Log("Target collision: " + TargetTag + ", Collision tag: " + other.tag);
            // Check if object collided with a desired tagged object
            if (other.tag == tag)
            {
                // Invoke all method inside OnCollision event
                onTrigger?.Invoke();
                onTriggerWithGameObject?.Invoke(other.gameObject);
            }
        }
    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger (2D physics only)
    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (string tag in targetTags)
        {
            //Debug.Log("Target collision: " + TargetTag + ", Collision tag: " + other.tag);
            // Check if object collided with a desired tagged object
            if (other.tag == tag)
            {
                // Invoke all method inside OnCollision event
                onTriggerExitEvent?.Invoke();
            }
        }
    }

    // OnTriggerStay2D is called once per frame for every Collider2D other that is touching the trigger (2D physics only)
    private void OnTriggerStay2D(Collider2D other)
    {
        foreach (string tag in targetTags)
        {
            //Debug.Log("Target collision: " + TargetTag + ", Collision tag: " + other.tag);
            // Check if object collided with a desired tagged object
            if (other.tag == tag)
            {
                // Invoke all method inside OnCollision event
                onTriggerStayEvent?.Invoke();
            }
        }
    }
}
