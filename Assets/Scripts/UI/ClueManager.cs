using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    private static uint s_nbClues = 7;

    [SerializeField]
    private Clue[] m_clues = new Clue[s_nbClues];

    [SerializeField]
    private int[] m_timings = new int[s_nbClues];

    int m_currentClue = 0;
    bool m_started = false;
    float m_startTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_started)
            return;

        for (int i = m_currentClue; i < s_nbClues; ++i)
        {
            if (m_currentClue == i && Time.time - m_startTime > m_timings[i])
            {
                m_clues[m_currentClue++].ShowClue(true);
            }
        }
    }

    public void Reset()
    {
        m_currentClue = 0;
        m_started = false;

        foreach (Clue c in m_clues)
        {
            c.ShowClue(false);
        }
    }

    public void StartRound(List<ItemClue> clues)
    {
        int i = 0;
        foreach (Clue c in m_clues)
        {
            ItemClue clueDesc = clues[i++];
            c.SetValue(clueDesc.m_text, clueDesc.m_truthness);
        }

        m_startTime = Time.time;
        m_started = true;
    }

    public int GetCurrentClueScore()
    {
        // TODO
        return 10;
    }
}
