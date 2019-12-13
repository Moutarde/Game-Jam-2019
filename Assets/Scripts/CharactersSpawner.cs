using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    public class Constraint
    {
        public Item.ItemType m_itemType;
        public Item m_item;

        public Constraint(Item.ItemType _itemType, Item _item)
        {
            m_itemType = _itemType;
            m_item = _item;
        }

        public void Display()
        {
            Debug.Log("Constraint display: Item type: " + m_itemType + "Item: " + m_item);
        }
    }

    public class CharactersGroup
    {
        public int m_charactersCount;
        public List<Constraint> m_constraints;

        public CharactersGroup()
        {
            m_charactersCount = 0;
            m_constraints = new List<Constraint>();
        }

        public void Display()
        {
            Debug.Log("Characters group display: Characters count: " + m_charactersCount);
            for (int i = 0; i < m_constraints.Count; ++i)
            {
                m_constraints[i].Display();
            }
        }
    }

    public class Node
    {
        public Item.ItemType m_itemType;
        public Item m_item;
        public int m_depth;
        public Node m_node1;
        public Node m_node2;

        public Node(List<Item.ItemType> _availableTypes, ItemsDatabase _itemsDatabase)
        {
            m_itemType = Item.ItemType.Top; // random, do not use it
            m_item = null;
            m_depth = 0;

            CreateChildrenNodes(_availableTypes, _itemsDatabase);
        }

        public Node(Item.ItemType _itemType, Item _item, bool _hasChildren, int _depth, List<Item.ItemType> _availableTypes, ItemsDatabase _itemsDatabase)
        {
            m_itemType = _itemType;
            m_item = _item;
            m_depth = _depth;

            if (m_depth > 3)
            {
                _hasChildren = false;
            }

            if (_hasChildren)
            {
                CreateChildrenNodes(_availableTypes, _itemsDatabase);
            }
            else
            {
                m_node1 = null;
                m_node2 = null;
            }   
        }

        public void CreateChildrenNodes(List<Item.ItemType> _availableTypes, ItemsDatabase _itemsDatabase)
        {
            int randomTypeIndex = Random.Range(0, _availableTypes.Count);
            Item.ItemType childrenItemType = _availableTypes[randomTypeIndex];
            _availableTypes.RemoveAt(randomTypeIndex);

            List<Item> items = _itemsDatabase.GetItems(childrenItemType);
            int itemIndex = Random.Range(0, items.Count - 1);
            int item0 = 0;
            int item1 = 0;

            int randomNodeIndex = Random.Range(0, 2);
            if (randomNodeIndex == 0)
            {
                item0 = itemIndex;
                item1 = itemIndex + 1;
            }
            else
            {
                item0 = itemIndex + 1;
                item1 = itemIndex;
            }

            m_node1 = new Node(childrenItemType, items[item0], true, m_depth + 1, _availableTypes, _itemsDatabase);
            m_node2 = new Node(childrenItemType, items[item1], false, m_depth + 1, _availableTypes, _itemsDatabase);
        }

        public void DisplayNode()
        {
            if (m_depth == 0)
            {
                Debug.Log("Root");
            }
            else
            {
                Debug.Log("Node type " + m_itemType + " item " + m_item);
            }

            Debug.Log("node 1");
            if (m_node1 == null)
            {
                Debug.Log("null");
            }
            else
            {
                m_node1.DisplayNode();
            }

            Debug.Log("node 2");
            if (m_node2 == null)
            {
                Debug.Log("null");
            }
            else
            {
                m_node2.DisplayNode();
            }
        }

        public List<CharactersGroup> CreateCharactersGroup()
        {
            CharactersGroup cg0 = new CharactersGroup();
            CharactersGroup cg1 = new CharactersGroup();
            CharactersGroup cg2 = new CharactersGroup();
            CharactersGroup cg3 = new CharactersGroup();
            CharactersGroup cg4 = new CharactersGroup();

            // 5 to 9 characters per group * 4 = 20 to 36 + unique one = 21 to 37
            cg0.m_charactersCount = Random.Range(5, 10);
            cg1.m_charactersCount = Random.Range(5, 10);
            cg2.m_charactersCount = Random.Range(5, 10);
            cg3.m_charactersCount = Random.Range(5, 10);
            cg4.m_charactersCount = Random.Range(5, 10);

            // unique character in one group
            int index = Random.Range(0, 5);
            switch (index)
            {
                case 0:
                    cg0.m_charactersCount = 1;
                    break;
                case 1:
                    cg1.m_charactersCount = 1;
                    break;
                case 2:
                    cg2.m_charactersCount = 1;
                    break;
                case 3:
                    cg3.m_charactersCount = 1;
                    break;
                case 4:
                    cg4.m_charactersCount = 1;
                    break;
            }

            cg0.m_constraints.Add(new Constraint(m_node2.m_itemType, m_node2.m_item));

            cg1.m_constraints.Add(new Constraint(m_node1.m_itemType, m_node1.m_item));
            cg1.m_constraints.Add(new Constraint(m_node1.m_node2.m_itemType, m_node1.m_node2.m_item));

            cg2.m_constraints.Add(new Constraint(m_node1.m_itemType, m_node1.m_item));
            cg2.m_constraints.Add(new Constraint(m_node1.m_node1.m_itemType, m_node1.m_node1.m_item));
            cg2.m_constraints.Add(new Constraint(m_node1.m_node1.m_node2.m_itemType, m_node1.m_node1.m_node2.m_item));

            cg3.m_constraints.Add(new Constraint(m_node1.m_itemType, m_node1.m_item));
            cg3.m_constraints.Add(new Constraint(m_node1.m_node1.m_itemType, m_node1.m_node1.m_item));
            cg3.m_constraints.Add(new Constraint(m_node1.m_node1.m_node1.m_itemType, m_node1.m_node1.m_node1.m_item));
            cg3.m_constraints.Add(new Constraint(m_node1.m_node1.m_node1.m_node2.m_itemType, m_node1.m_node1.m_node1.m_node2.m_item));

            cg4.m_constraints.Add(new Constraint(m_node1.m_itemType, m_node1.m_item));
            cg4.m_constraints.Add(new Constraint(m_node1.m_node1.m_itemType, m_node1.m_node1.m_item));
            cg4.m_constraints.Add(new Constraint(m_node1.m_node1.m_node1.m_itemType, m_node1.m_node1.m_node1.m_item));
            cg4.m_constraints.Add(new Constraint(m_node1.m_node1.m_node1.m_node1.m_itemType, m_node1.m_node1.m_node1.m_node1.m_item));

            List<CharactersGroup> charactersGroupList = new List<CharactersGroup>();
            charactersGroupList.Add(cg0);
            charactersGroupList.Add(cg1);
            charactersGroupList.Add(cg2);
            charactersGroupList.Add(cg3);
            charactersGroupList.Add(cg4);
            return charactersGroupList;
        }
    }

    public GameObject m_characterPrefab;
    public float m_xMin;
    public float m_xMax;
    public float m_yMin;
    public float m_yMax;
    public ZManager m_zManager;

    private ItemsDatabase m_itemsDatabase;

    private List<GameObject> m_characters = new List<GameObject>();

    private void AssignItem(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignHead(item);
    }

    private void AssignHead(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignHead(item);
    }

    private void AssignFace(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignFace(item);
    }

    private void AssignHeadAccessory(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignHeadAccessory(item);
    }

    private void AssignFaceAccessory(Item.ItemType _itemType, GameObject _character)
    {
        List<Item> itemsList = m_itemsDatabase.GetItems(_itemType);
        int itemIndex = Random.Range(0, itemsList.Count);
        SimpleItem item = (SimpleItem)itemsList[itemIndex];
        _character.GetComponent<Character>().AssignFaceAccessory(item);
    }

    private void SpawnCharacter()
    {
        GameObject newCharacter = Instantiate(m_characterPrefab);
        newCharacter.transform.parent = transform;
        float randomX = Random.Range(m_xMin, m_xMax);
        float randomY = Random.Range(m_yMin, m_yMax);
        newCharacter.transform.localPosition = new Vector3(randomX, randomY, 0.0f);

        List<Item> topList = m_itemsDatabase.GetItems(Item.ItemType.Top);
        int topIndex = Random.Range(0, topList.Count);
        TopItem top = (TopItem)topList[topIndex];
        newCharacter.GetComponent<Character>().AssignTop(top);

        List<Item> bottomList = m_itemsDatabase.GetItems(Item.ItemType.Bottom);
        int bottomIndex = Random.Range(0, bottomList.Count);
        BottomItem bottom = (BottomItem)bottomList[bottomIndex];
        newCharacter.GetComponent<Character>().AssignBottom(bottom);

        AssignHead(Item.ItemType.Head, newCharacter);
        AssignFace(Item.ItemType.Face, newCharacter);
        AssignHeadAccessory(Item.ItemType.HeadAccessory, newCharacter);
        AssignFaceAccessory(Item.ItemType.FaceAccessory, newCharacter);

        newCharacter.GetComponent<Character>().InitOrders();
        newCharacter.GetComponentInChildren<SuspectController>().IsTarget = false;

        m_characters.Add(newCharacter);
        m_zManager.m_playersAndObstacles.Add(newCharacter);
    }

    private void SpawnFirstCharacter()
    {
        GameObject newCharacter = Instantiate(m_characterPrefab);
        newCharacter.transform.parent = transform;
        float randomX = Random.Range(m_xMin, m_xMax);
        float randomY = Random.Range(m_yMin, m_yMax);
        newCharacter.transform.localPosition = new Vector3(randomX, randomY, 0.0f);

        List<Item> topList = m_itemsDatabase.GetItems(Item.ItemType.Top);
        int topIndex = Random.Range(0, topList.Count);
        TopItem top = (TopItem)topList[topIndex];
        newCharacter.GetComponent<Character>().AssignTop(top);

        List<Item> bottomList = m_itemsDatabase.GetItems(Item.ItemType.Bottom);
        int bottomIndex = Random.Range(0, bottomList.Count);
        BottomItem bottom = (BottomItem)bottomList[bottomIndex];
        newCharacter.GetComponent<Character>().AssignBottom(bottom);

        AssignHead(Item.ItemType.Head, newCharacter);
        AssignFace(Item.ItemType.Face, newCharacter);
        AssignHeadAccessory(Item.ItemType.HeadAccessory, newCharacter);
        AssignFaceAccessory(Item.ItemType.FaceAccessory, newCharacter);

        newCharacter.GetComponent<Character>().InitOrders();
        newCharacter.GetComponentInChildren<SuspectController>().IsTarget = true;

        m_characters.Add(newCharacter);
        m_zManager.m_playersAndObstacles.Add(newCharacter);
    }

    private void SpawnCharacterV3(List<ItemClue> _defClues)
    {
        GameObject newCharacter = Instantiate(m_characterPrefab);
        newCharacter.transform.parent = transform;
        float randomX = Random.Range(m_xMin, m_xMax);
        float randomY = Random.Range(m_yMin, m_yMax);
        newCharacter.transform.localPosition = new Vector3(randomX, randomY, 0.0f);

        List<Item> topList = m_itemsDatabase.GetItems(Item.ItemType.Top);
        int topIndex = Random.Range(0, topList.Count);
        TopItem top = (TopItem)topList[topIndex];
        newCharacter.GetComponent<Character>().AssignTop(top);

        List<Item> bottomList = m_itemsDatabase.GetItems(Item.ItemType.Bottom);
        int bottomIndex = Random.Range(0, bottomList.Count);
        BottomItem bottom = (BottomItem)bottomList[bottomIndex];
        newCharacter.GetComponent<Character>().AssignBottom(bottom);

        AssignHead(Item.ItemType.Head, newCharacter);
        AssignFace(Item.ItemType.Face, newCharacter);
        AssignHeadAccessory(Item.ItemType.HeadAccessory, newCharacter);
        AssignFaceAccessory(Item.ItemType.FaceAccessory, newCharacter);

        newCharacter.GetComponent<Character>().InitOrders();

        if (m_characters.Count > 0 && newCharacter.GetComponent<Character>().RespectClues(_defClues))
        {
            // same as the bad guy
            // Debug.Log("Duplicate");
        }
        else
        {
            m_characters.Add(newCharacter);
            m_zManager.m_playersAndObstacles.Add(newCharacter);
        }
    }

    private Item HasConstraint(List<Constraint> _constraints, Item.ItemType _itemType)
    {
        for (int i = 0; i < _constraints.Count; ++i)
        {
            if (_constraints[i].m_itemType == _itemType)
            {
                return _constraints[i].m_item;
            }
        }
        return null;
    }

    private TopItem HasTopConstraint(List<Constraint> _constraints)
    {
        for (int i = 0; i < _constraints.Count; ++i)
        {
            if (_constraints[i].m_itemType == Item.ItemType.Top)
            {
                return (TopItem)_constraints[i].m_item;
            }
        }
        return null;
    }

    private BottomItem HasBottomConstraint(List<Constraint> _constraints)
    {
        for (int i = 0; i < _constraints.Count; ++i)
        {
            if (_constraints[i].m_itemType == Item.ItemType.Bottom)
            {
                return (BottomItem)_constraints[i].m_item;
            }
        }
        return null;
    }

    private void SpawnCharacterWithConstraints(List<Constraint> _constraints)
    {
        GameObject newCharacter = Instantiate(m_characterPrefab);
        newCharacter.transform.parent = transform;
        float randomX = Random.Range(m_xMin, m_xMax);
        float randomY = Random.Range(m_yMin, m_yMax);
        newCharacter.transform.localPosition = new Vector3(randomX, randomY, 0.0f);

        TopItem itemTop = HasTopConstraint(_constraints);
        if (itemTop == null)
        {
            List<Item> topList = m_itemsDatabase.GetItems(Item.ItemType.Top);
            int topIndex = Random.Range(0, topList.Count);
            TopItem top = (TopItem)topList[topIndex];
            newCharacter.GetComponent<Character>().AssignTop(top);
        }
        else
        {
            newCharacter.GetComponent<Character>().AssignTop(itemTop);
        }

        BottomItem itemBottom = HasBottomConstraint(_constraints);
        if (itemBottom == null)
        {
            List<Item> bottomList = m_itemsDatabase.GetItems(Item.ItemType.Bottom);
            int bottomIndex = Random.Range(0, bottomList.Count);
            BottomItem bottom = (BottomItem)bottomList[bottomIndex];
            newCharacter.GetComponent<Character>().AssignBottom(bottom);
        }
        else
        {
            newCharacter.GetComponent<Character>().AssignBottom((BottomItem)itemBottom);
        }

        Item itemHead = HasConstraint(_constraints, Item.ItemType.Head);
        if (itemHead == null)
        {
            AssignHead(Item.ItemType.Head, newCharacter);
        }
        else
        {
            newCharacter.GetComponent<Character>().AssignHead((SimpleItem)itemHead);
        }

        Item itemFace = HasConstraint(_constraints, Item.ItemType.Face);
        if (itemFace == null)
        {
            AssignFace(Item.ItemType.Face, newCharacter);
        }
        else
        {
            newCharacter.GetComponent<Character>().AssignFace((SimpleItem)itemFace);
        }

        Item itemHeadAccessory = HasConstraint(_constraints, Item.ItemType.HeadAccessory);
        if (itemHeadAccessory == null)
        {
            AssignHeadAccessory(Item.ItemType.HeadAccessory, newCharacter);
        }
        else
        {
            newCharacter.GetComponent<Character>().AssignHeadAccessory((SimpleItem)itemHeadAccessory);
        }

        Item itemFaceAccessory = HasConstraint(_constraints, Item.ItemType.FaceAccessory);
        if (itemFaceAccessory == null)
        {
            AssignFaceAccessory(Item.ItemType.FaceAccessory, newCharacter);
        }
        else
        {
            newCharacter.GetComponent<Character>().AssignFaceAccessory((SimpleItem)itemFaceAccessory);
        }
        
        newCharacter.GetComponent<Character>().InitOrders();

        m_characters.Add(newCharacter);
        m_zManager.m_playersAndObstacles.Add(newCharacter);
    }

    public List<ItemClue> SpawnCharacters(int _charactersCount)
    {
        SpawnFirstCharacter();

        List<ItemClue> firstCharacterClues = GenerateClues();

        while (m_characters.Count < _charactersCount)
        {
            SpawnCharacterV3(firstCharacterClues);
        }

        return firstCharacterClues;
    }

    public List<ItemClue> GenerateClues()
    {
        return m_characters[0].GetComponent<Character>().GetClues();
    }

    public void SpawnCharactersWithCharactersGroup(CharactersGroup _charactersGroup)
    {
        for (int i = 0; i < _charactersGroup.m_charactersCount; ++i)
        {
            //SpawnCharacter();
            SpawnCharacterWithConstraints(_charactersGroup.m_constraints);
        }
    }

    private List<CharactersGroup> GenerateCharactersGroups()
    {
        List<Item.ItemType> availableTypes = new List<Item.ItemType> { Item.ItemType.Top, Item.ItemType.Bottom, Item.ItemType.Face, Item.ItemType.FaceAccessory, Item.ItemType.HeadAccessory };
        Node root = new Node(availableTypes, m_itemsDatabase);
        //root.DisplayNode();
        List<CharactersGroup> charactersGroupList = root.CreateCharactersGroup();
        for (int i = 0; i < charactersGroupList.Count; ++i)
        {
            charactersGroupList[i].Display();
        }
        return charactersGroupList;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_itemsDatabase = new ItemsDatabase();
        m_itemsDatabase.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        foreach (GameObject c in m_characters)
        {
            Destroy(c);
        }

        m_characters.Clear();
        m_zManager.RemoveAllCharacters();
    }
}
