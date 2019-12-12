using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    public GameObject m_characterPrefab;
    public float m_xMin;
    public float m_xMax;
    public float m_yMin;
    public float m_yMax;
    public GameObject m_zManager;

    private ItemsDatabase m_itemsDatabase;

    private void SpawnCharacter()
    {
        GameObject newCharacter = Instantiate(m_characterPrefab);
        newCharacter.transform.parent = transform;
        float randomX = Random.Range(m_xMin, m_xMax);
        float randomY = Random.Range(m_yMin, m_yMax);
        newCharacter.transform.localPosition = new Vector3(randomX, randomY, 0.0f);
        m_zManager.GetComponent<ZManager>().m_playersAndObstacles.Add(newCharacter);

        List<Item> topList = m_itemsDatabase.GetItems(Item.ItemType.Top);
        int topIndex = Random.Range(0, topList.Count);
        TopItem top = (TopItem)topList[topIndex];
        newCharacter.GetComponent<Character>().AssignTop(top);

        List<Item> bottomList = m_itemsDatabase.GetItems(Item.ItemType.Bottom);
        int bottomIndex = Random.Range(0, bottomList.Count);
        BottomItem bottom = (BottomItem)bottomList[bottomIndex];
        newCharacter.GetComponent<Character>().AssignBottom(bottom);

        List<Item> headList = m_itemsDatabase.GetItems(Item.ItemType.Head);
        int headIndex = Random.Range(0, headList.Count);
        SimpleItem head = (SimpleItem)headList[headIndex];
        newCharacter.GetComponent<Character>().AssignHead(head);

        List<Item> hairList = m_itemsDatabase.GetItems(Item.ItemType.Hair);
        int hairIndex = Random.Range(0, hairList.Count);
        SimpleItem hair = (SimpleItem)hairList[hairIndex];
        newCharacter.GetComponent<Character>().AssignHair(hair);

        List<Item> faceAccessoryList = m_itemsDatabase.GetItems(Item.ItemType.FaceAccessory);
        int faceAccessoryIndex = Random.Range(0, faceAccessoryList.Count);
        SimpleItem faceAccessory = (SimpleItem)faceAccessoryList[faceAccessoryIndex];
        newCharacter.GetComponent<Character>().AssignFaceAccessory(faceAccessory);
    }

    private void SpawnCharacters(int _charactersCount)
    {
        for (int i = 0; i < _charactersCount; ++i)
        {
            SpawnCharacter();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        m_itemsDatabase = new ItemsDatabase();
        m_itemsDatabase.Init();

        SpawnCharacters(30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
