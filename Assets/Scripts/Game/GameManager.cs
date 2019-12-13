using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static uint s_nbPlayers = 6;
    private static uint s_nbRoundsPerGame = 4;
    private static float s_timeBetweenRounds = 5f;

    public static GameManager Instance;
    
    private ClueManager m_clueManager;
    private CharactersSpawner m_charactersSpawner;

    [SerializeField]
    private GameObject m_pressStartObject;
    [SerializeField]
    private GameObject m_nextRoundObject;

    [SerializeField]
    private PlayerScore[] m_scores = new PlayerScore[s_nbPlayers];

    private List<PlayerController> m_players = new List<PlayerController>();
    private List<int> m_availableSlots = new List<int>();

    private bool m_gameStarted = false;
    private uint m_currentRound = 0;
    private float m_previousRoundEndTime;
    private bool m_waitingForNextRound;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject.transform);
        }

        m_clueManager = FindObjectOfType<ClueManager>();
        m_charactersSpawner = FindObjectOfType<CharactersSpawner>();

        for (int i = 0; i < s_nbPlayers; ++i)
        {
            m_availableSlots.Add(i);
        }

        foreach (PlayerScore s in m_scores)
        {
            s.ShowScore(false);
        }
    }

    void Start()
    {
        m_pressStartObject.SetActive(true);
        m_nextRoundObject.SetActive(false);
    }

    void Update()
    {
        if (m_waitingForNextRound)
        {
            if (Time.time - m_previousRoundEndTime < s_timeBetweenRounds)
            {
                float remainingTime = s_timeBetweenRounds - (Time.time - m_previousRoundEndTime);
                m_nextRoundObject.GetComponent<TextMeshProUGUI>().SetText("Next round in " + (int)Mathf.Ceil(remainingTime) + " sec");
            }
            else
            {
                m_nextRoundObject.GetComponent<TextMeshProUGUI>().SetText("");
                BeginNextRound();
            }
        }
    }

    public int RegisterPlayer(PlayerController player)
    {
        if (m_availableSlots.Count == 0)
            return -1;

        m_players.Add(player);

        int slot = m_availableSlots[0];
        m_availableSlots.RemoveAt(0);

        m_scores[slot].ShowScore(true);

        return slot;
    }

    public void UnregisterPlayer(PlayerController player)
    {
        m_players.Remove(player);
        m_availableSlots.Insert(0, player.Slot);
    }

    public void HandleStartPressed()
    {
        if (!m_gameStarted)
        {
            StartGame();
            return;
        }
    }

    private void StartGame()
    {
        m_pressStartObject.SetActive(false);
        m_nextRoundObject.SetActive(false);

        foreach (PlayerController p in m_players)
        {
            p.Reset();
            m_scores[p.Slot].SetText("Player " + (p.Slot + 1) + ": " + p.Score.ToString());
        }

        List<ItemClue> clues = m_charactersSpawner.SpawnCharacters(30);
        m_clueManager.StartRound(clues);

        m_gameStarted = true;
    }

    private void BeginNextRound()
    {
        m_pressStartObject.SetActive(false);
        m_nextRoundObject.SetActive(false);

        List<ItemClue> clues = m_charactersSpawner.SpawnCharacters(30);
        m_clueManager.StartRound(clues);

        m_waitingForNextRound = false;
    }

    public void NextRound()
    {
        m_nextRoundObject.SetActive(true);

        m_charactersSpawner.Reset();
        m_clueManager.Reset();

        ++m_currentRound;

        if (m_currentRound >= s_nbRoundsPerGame)
        {
            m_currentRound = 0;
            m_pressStartObject.SetActive(true);
            m_gameStarted = false;
        }
        else
        {
            m_previousRoundEndTime = Time.time;
            m_waitingForNextRound = true;
        }
    }

    public void HandlePlayerKill(PlayerController player, SuspectController suspect)
    {
        suspect.OnKilled();
        if (suspect.IsTarget)
        {
            player.AddPoints(m_clueManager.GetCurrentClueScore());
            NextRound();
        }
        else
        {
            player.AddPoints(-m_clueManager.GetCurrentClueScore());
        }

        m_scores[player.Slot].SetText("Player " + (player.Slot + 1) + ": " + player.Score.ToString());
    }

}
