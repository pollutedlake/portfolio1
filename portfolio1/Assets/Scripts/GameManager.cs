using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
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
        Monster[] curMonsters = FindObjectsOfType<Monster>();
        FindShortestPath(8, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            
        }
    }

    public void FindShortestPath(int start, int finish)
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
        isCheck.Initialize();
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
    }

    public void FindMonster(Monster monster)
    {
        Vector3 direction = monster.transform.position - character.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(character.transform.position, Rhino.transform.position);
    }
}
