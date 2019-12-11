using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    public GameObject m_characterPrefab;

    private ItemsDatabase m_itemsDatabase;

    private void SpawnCharacter(float _xOffset)
    {
        GameObject newCharacter = Instantiate(m_characterPrefab);
        newCharacter.transform.parent = transform;
        newCharacter.transform.localPosition = new Vector3(_xOffset, 0.0f, 0.0f);

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

        List<Item> facialHairList = m_itemsDatabase.GetItems(Item.ItemType.FacialHair);
        int facialHairIndex = Random.Range(0, facialHairList.Count);
        SimpleItem facialHair = (SimpleItem)facialHairList[facialHairIndex];
        newCharacter.GetComponent<Character>().AssignFacialHair(facialHair);
    }

    private void SpawnCharacters(int _charactersCount)
    {
        float offset = 3.0f;
        float currentOffset = 0.0f;

        for (int i = 0; i < _charactersCount; ++i)
        {
            SpawnCharacter(currentOffset);
            currentOffset += offset;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        m_itemsDatabase = new ItemsDatabase();
        m_itemsDatabase.Init();

        SpawnCharacters(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
