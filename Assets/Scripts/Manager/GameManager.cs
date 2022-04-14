using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;

    [Header("Cinemachine Camera")]
    [SerializeField] private CinemachineVirtualCamera GameCinemachine;

    [Header("Player Prefabs")]
    [SerializeField] private GameObject PlayerPrefab;
    public GameObject ActivePlayer;

    [Header("Player Spawn")]
    public Transform SpawnPoint;
    private bool playerIsSpawning;

    [Header("KeyCode")]
    [SerializeField] private KeyCode PauseKey = KeyCode.Escape;

    [Header("States")]
    public bool IsPlaying = false; // Bool to determine if player is in menu or playing the game

    // Subbed at: MenuUI.cs
    public UnityAction OnPauseAction;
    // Subbed at: MenuUI.cs
    public UnityAction OnResumeAction;

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

        StartGame();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Player Death
        if (!ActivePlayer.activeInHierarchy && !playerIsSpawning)
        {
            playerIsSpawning = true;
            StartCoroutine(SpawnPlayerWithDelay(0.5f));
        }

        // Pause Control
        if (Input.GetKeyDown(PauseKey) && !ActivePlayer.GetComponent<Health>().isDead)
        {
            PauseAndResumeGame();
        }
    }

    // Get singleton instance
    public static GameManager GetInstance()
    {
        return instance;
    }

    public void StartGame()
    {
        IsPlaying = true;
        SpawnPlayer(SpawnPoint);
    }
    public void PauseGame()
    {
        IsPlaying = false;
        Time.timeScale = 0f;
        OnPauseAction?.Invoke();
    }
    public void ResumeGame()
    {
        IsPlaying = true;
        Time.timeScale = 1f;
        OnResumeAction?.Invoke();
    }
    public void PauseAndResumeGame()
    {
        if (IsPlaying)
        {
            // Pause the game
            PauseGame();
        }
        else
        {
            // Resume the game
            ResumeGame();
        }
    }

    // Spawn player
    public void SpawnPlayer()
    {
        SpawnPlayer(PlayerPrefab.transform);
    }
    public void SpawnPlayer(Transform spawnPoint)
    {
        if (!ActivePlayer)
        {
            ActivePlayer = Instantiate(PlayerPrefab, spawnPoint.position, Quaternion.identity);
            GameCinemachine.Follow = ActivePlayer.transform;
        }
        else
        {
            ActivePlayer.SetActive(true);
            ActivePlayer.transform.position = spawnPoint.position;
            foreach (var behaviour in ActivePlayer.GetComponents<Behaviour>())
            {
                behaviour.enabled = true;
            }
        }
    }
    public IEnumerator SpawnPlayerWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnPlayer(SpawnPoint);
        playerIsSpawning = false;
    }
}
