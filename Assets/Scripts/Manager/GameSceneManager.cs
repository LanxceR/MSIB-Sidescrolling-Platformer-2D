using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    MAIN_MENU,
    TEST_LEVEL,
    LEVEL_ONE,
    LEVEL_TWO,
    LEVEL_THREE,
    CREDITS
}
public class GameSceneManager : MonoBehaviour
{
    // Singleton instance
    private static GameSceneManager instance;

    [SerializeField] private Animator anim;
    [SerializeField] private MusicTrack musicTheme;

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
        AudioManager.GetInstance().ChangeMusic(musicTheme);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameSceneManager GetInstance()
    {
        return instance;
    }

    // Go to scene
    public void GotoScene(SceneName scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene, 0));
    }
    public void GotoScene(string scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene, 0));
    }

    // Go to scene with delay
    public void GotoSceneWithDelay(SceneName scene, float delay)
    {
        StartCoroutine(LoadSceneCoroutine(scene, delay));
    }
    public void GotoSceneWithDelay(string scene, float delay)
    {
        StartCoroutine(LoadSceneCoroutine(scene, delay));
    }

    // Load scene coroutine
    private IEnumerator LoadSceneCoroutine(SceneName scene, float delay)
    {
        // Reset Timescale now (IMPORTANT: Coroutines wont run if timescale = 0)
        Time.timeScale = 1f;

        yield return new WaitForSeconds(delay);

        // Animation
       anim.SetTrigger("triggerStart");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync((int)scene);

    }
    private IEnumerator LoadSceneCoroutine(string scene, float delay)
    {
        // Reset Timescale now (IMPORTANT: Coroutines wont run if timescale = 0)
        Time.timeScale = 1f;

        yield return new WaitForSeconds(delay);

        // Animation
        anim.SetTrigger("triggerStart");

        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(scene);

    }

    // Reload scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(GetCurrentScene().name);
    }

    // Get current scene
    public Scene GetCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }
}
