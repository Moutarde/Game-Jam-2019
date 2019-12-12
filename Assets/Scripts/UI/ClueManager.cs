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

    // Start is called before the first frame update
    void Start()
    {
        foreach (Clue c in m_clues)
        {
            c.ShowClue(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = m_currentClue; i < s_nbClues; ++i)
        {
            if (m_currentClue == i && Time.time > m_timings[i])
            {
                m_clues[m_currentClue++].ShowClue(true);
            }
        }
    }

    public void GenerateClues()
    {
        // TODO
        foreach (Clue c in m_clues)
        {
            c.SetText("YOLO");
        }
    }

    public int GetCurrentClueScore()
    {
        // TODO
        return 10;
    }
}
