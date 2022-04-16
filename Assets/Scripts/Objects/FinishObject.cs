using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishObject : MonoBehaviour
{
    [SerializeField] private SceneName sceneName;
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

    public void CompleteLevel()
    {
        if (isActivated) return;

        anim.SetTrigger("triggerActivate");
        GameManager.GetInstance().CompleteLevel();
        GameSceneManager.GetInstance().GotoSceneWithDelay(sceneName, 2f);
    }
}
