using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// Static instance of GameManager which allows it to be accessed by any other script.
    /// </summary>
    public static GameManager Instance = null;

    #endregion

    #region Public Fields

    /// <summary>
    /// Says if the game is currently paused or not.
    /// </summary>
    [HideInInspector] 
    public bool IsGamePaused = false;

    /// <summary>
    /// Initial countdown.
    /// Unit: seconds.
    /// </summary>
    public int InitialCountdownDuration = 3;

    /// <summary>
    /// Says if the game is currently in initial game countdown.
    /// </summary>
    [HideInInspector] 
    public bool IsInInitialCountdown = false;

    /// <summary>
    /// Reference to the prefab the players will control.
    /// </summary>
    public GameObject PlayerPrefab = null;

    #endregion

    #region Unity Engine

    // Awake is always called before any Start functions
    void Awake()
    {
        // Check if instance already exists.
        if (Instance == null)
            // If not, set instance to this.
            Instance = this;

        // If instance already exists and it's not this.
        else if (Instance != this)
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        // Sets this to not be destroyed when reloading scene.
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region Private Methods

    /// <summary>
    /// All initialization configuration
    /// </summary>
    private void InitializeGame()
    {
        // Stop the time for initialization.
        PauseGameTime();

        // Spawn all units at the beginning of the game.
        SpawnAllUnits();

        // Start initial countdown for the game.
        StartInitialCountdown();

        // Start the game routine.
        StartTheGame();
    }

    /// <summary>
    /// Start initial coundown of the game.
    /// </summary>
    private void StartInitialCountdown()
    {
        IsInInitialCountdown = true;
        StartCoroutine(InitialCountdownProgress(InitialCountdownDuration));
    }

    /// <summary>
    /// Progress the countdown during time is stopped.
    /// </summary>
    /// <param name="duration">Countdown duration</param>
    /// <returns>IEnumerator.</returns>
    private IEnumerator InitialCountdownProgress(int duration)
    {
        float countdownTimer = 0f;
        float previousTime = Time.realtimeSinceStartup;

        while (countdownTimer <= duration)
        {
            string textLabel = "";

            // Countdown calculation
            countdownTimer += Time.realtimeSinceStartup - previousTime;
            previousTime = Time.realtimeSinceStartup;
            int countdown = (int)Math.Round(duration - countdownTimer);

            // Get text label of the process
            if (countdown == duration)
                textLabel = "GET READY";
            else if (countdown > 0)
                textLabel = countdown.ToString();
            else
                textLabel = "GO";

            // APlly countdown label
            ApplyCountdownLabel(textLabel);

            yield return 0;
        }
    }

    /// <summary>
    /// Apply countdown label to the UI.
    /// </summary>
    /// <param name="textLabel"></param>
    private void ApplyCountdownLabel(string textLabel)
    {
        // TODO
    }

    /// <summary>
    /// Spawn all units.
    /// </summary>
    private void SpawnAllUnits()
    {
        SpawnPlayer();
    }

    /// <summary>
    /// Depspawn all units.
    /// </summary>
    private void DespawnAllUnits()
    {
        DespawnPlayer();
    }

    /// <summary>
    /// Spawn player objects.
    /// </summary>
    private void SpawnPlayer()
    {
        // TODO
    }

    /// <summary>
    /// Despawn player objects.
    /// </summary>
    private void DespawnPlayer()
    {
        // TODO
    }

    /// <summary>
    /// Respawn all units as new.
    /// </summary>
    public void RespawnAllUnits()
    {
        DespawnAllUnits();
        SpawnAllUnits();
    }

    // Start the game time.
    private void ResumeGameTime()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    /// <summary>
    /// Stop the game time.
    /// </summary>
    private void PauseGameTime()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void StartTheGame()
    {
        ResumeGameTime();
        // TODO
    }

    /// <summary>
    /// End the game.
    /// </summary>
    public void EndTheGame()
    {
        PauseGameTime();
        // TODO
    }

    #endregion
}
