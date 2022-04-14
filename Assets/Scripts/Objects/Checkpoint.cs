using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool isActivated;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateCheckpoint()
    {
        if (isActivated) return;

        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.isActivated = false;
            checkpoint.anim.SetBool("isActivated", false);
        }

        isActivated = true;
        anim.SetBool("isActivated", isActivated);
        MoveSpawnPointToCheckpoint();
    }

    public void DeactivateCheckpoint()
    {
        if (!isActivated) return;

        isActivated = false;
        anim.SetBool("isActivated", isActivated);
    }

    private void MoveSpawnPointToCheckpoint()
    {
        GameManager.GetInstance().SpawnPoint.position = transform.position;
    }
}
