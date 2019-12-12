using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static uint s_nbPlayers = 6;

    public static GameManager Instance;

    [SerializeField]
    private ClueManager m_clueManager;

    [SerializeField]
    private PlayerScore[] m_scores = new PlayerScore[s_nbPlayers];

    private List<PlayerController> m_players = new List<PlayerController>();
    private List<int> m_availableSlots = new List<int>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject.transform);
        }

        for (int i = 0; i < s_nbPlayers; ++i)
        {
            m_availableSlots.Add(i);
        }

        foreach (PlayerScore s in m_scores)
        {
            s.ShowScore(false);
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

    public void HandlePlayerKill(PlayerController player, SuspectController suspect)
    {
        suspect.OnKilled();
        if (suspect.IsTarget())
        {
            player.AddPoints(m_clueManager.GetCurrentClueScore());
        }
        else
        {
            player.AddPoints(-m_clueManager.GetCurrentClueScore());
        }

        m_scores[player.Slot].SetText(player.Score.ToString());
    }

}
