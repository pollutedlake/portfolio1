using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Navigator navigator;
    public static GameManager instance;
    public Character character;
    public Monster Rhino;
    public Dictionary<string, GameObject> monsters = new Dictionary<string, GameObject>();
    public Monster[] curMonsters = new Monster[3];
    public int curCharacterArea;
    public int[] curMonsterArea = new int[3] { -1, -1, -1 };
    public UIMgr uiMgr;
    public int[] investigationPoint = new int[3] { 0, 0, 0 };

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        monsters.Add("Rhino", Rhino.gameObject);
        curMonsters = FindObjectsOfType<Monster>();
        if (uiMgr == null)
        {
            uiMgr = FindObjectOfType<UIMgr>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            if (investigationPoint[i] >= 60)
            {
                if (navigator.target == null)
                {
                    navigator.transform.position = character.transform.position;
                    navigator.target = curMonsters[i];
                    navigator.curCharacterArea = curCharacterArea;
                    navigator.curMonsterArea = curMonsterArea[i];
                }
            }
        }
    }
}