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
    public int nodeNumber = 9;
    public float[,] mapGraph = new float[9, 9] { { 99.0f, 1.0f, 99.0f, 99.0f, Mathf.Sqrt(2.0f), 99.0f, 99.0f, 99.0f, 99.0f},
                                             { 1.0f, 99.0f, 1.0f, 99.0f, 1.0f,99.0f, 99.0f, 99.0f, 99.0f},
                                             { 99.0f, 1.0f, 99.0f, 99.0f, 99.0f, 1.0f, 99.0f, 99.0f, 99.0f},
                                             { 99.0f, 99.0f, 99.0f, 99.0f, 1.0f, 99.0f, 1.0f, 99.0f, 99.0f},
                                             { Mathf.Sqrt(2.0f), 1.0f, 99.0f, 1.0f, 99.0f, 1.0f, Mathf.Sqrt(2.0f), 1.0f, 99.0f},
                                             { 99.0f, 99.0f, 1.0f, 99.0f, 1.0f, 99.0f, 99.0f, 99.0f, 99.0f},
                                             { 99.0f, 99.0f, 99.0f, 1.0f, Mathf.Sqrt(2.0f), 99.0f, 99.0f, 99.0f, 99.0f},
                                             { 99.0f, 99.0f, 99.0f, 99.0f, 1.0f, 99.0f, 99.0f, 99.0f, 1.0f},
                                             { 99.0f, 99.0f, 99.0f, 99.0f, 99.0f, 99.0f, 99.0f, 1.0f, 99.0f} };
    public bool[] isCheck = new bool[9] { false, false, false, false, false, false, false, false, false };
    public int curCharacterArea;
    public int[] curMonsterArea = new int[3] { -1, -1, -1 };
    public Vector3[] areaPositions = new Vector3[9];
    public FireFliesManager fireFlies;
    List<Vector3> navigatePath = new List<Vector3>();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(Rhino);
            navigator.transform.position = character.transform.position;
            navigator.target = Rhino;
            navigator.curCharacterArea = curCharacterArea;
            navigator.curMonsterArea = curMonsterArea[0];
            Debug.Log(curCharacterArea);
            Debug.Log(curMonsterArea[0]);
            //navigator.GetComponent<NavMeshAgent>().SetDestination(Rhino.transform.position + new Vector3(0.0f, 1.0f, 0.0f));
            //character.GetComponent<NavMeshAgent>().SetDestination(Rhino.transform.position);
            //List<int> shortestPath = new List<int>(FindShortestPath(curCharacterArea, curMonsterArea[0]));
            //for (int i = 0; i < shortestPath.Count; i++) {
            //    navigatePath.Add(areaPositions[shortestPath[i]]);
            //}
            //navigatePath.Add(curMonsters[0].transform.position);
            //for(int i = 0; i < navigatePath.Count; i++)
            //{
            //    Debug.Log(navigatePath[i]);
            //}
            //FireFliesManager fireFliesManager = Instantiate(fireFlies);
            //fireFliesManager.transform.position = character.transform.position + new Vector3(0.0f, 2.0f, 0.0f);
            //fireFliesManager.navigatePath = navigatePath;
            //fireFliesManager.isNavigate = true;
        }
    }

    /*
    public List<int> FindShortestPath(int start, int finish)
    {
        //isCheck.Initialize();
        //int[] shortestPath = new int[9];
        //float[] distance = new float[9];
        //for(int i = 0; i < nodeNumber; i++)
        //{
        //    distance[i] = mapGraph[start, i];
        //}
        //distance[start] = 0.0f;
        //shortestPath[start] = start;
        //isCheck[start] = true;
        //int current = start;
        //for(int i = 0; i < nodeNumber - 1; i++)
        //{
        //    float shortestDistance = 99.0f;
        //    int shortestNode = -1;
        //    for(int j = 0; j < nodeNumber; j++)
        //    {
        //        if(mapGraph[current, j] + distance[current] < distance[j])
        //        {
        //            distance[j] = mapGraph[current, j] + distance[current];
        //            shortestPath[j] = current;
        //        }
        //        if(mapGraph[current, j] < shortestDistance && !isCheck[j])
        //        {
        //            shortestDistance = mapGraph[current, j];
        //            shortestNode = j;
        //        }
        //    }
        //    isCheck[shortestNode] = true;
        //    current = shortestNode;
        //}
        for(int i = 0; i < isCheck.Length; i++)
        {
            isCheck[i] = false;
        }
        float shortestDistance = 99.0f;
        float tempDistance = 0.0f;
        List<int> shortestPath = new List<int>();
        List<int> tempPath = new List<int>();
        tempPath.Add(start);
        isCheck[start] = true;
        int current = start;
        while (tempPath.Count != 0)
        {
            int count = 0;
            for (int i = 0; i < nodeNumber; i++)
            {
                if (mapGraph[current, i] > 0.0f && !isCheck[i] && tempDistance + mapGraph[current, i] < shortestDistance)
                {
                    tempPath.Add(i);
                    tempDistance += mapGraph[current, i];
                    if (i == finish)
                    {
                        shortestPath = new List<int>(tempPath);
                        shortestDistance = tempDistance;
                    }
                    else
                    {
                        isCheck[i] = true;
                        current = i;
                    }
                    break;
                }
                count++;
            }
            if (count == nodeNumber)
            {
                tempPath.RemoveAt(tempPath.Count - 1);
            }
        }
        return shortestPath;
    }
    */
}