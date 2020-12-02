using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Character character;
    public Monster Rhino;
    public Dictionary<string, GameObject> monsters = new Dictionary<string, GameObject>();
    public Monster[] curMonsters = new Monster[3];
    public int nodeNumber = 9;
    public float[,] mapGraph = new float[9, 9] { { 0, 1.0f, 0, 0, Mathf.Sqrt(2.0f), 0, 0, 0, 0},
                                             { 1.0f, 0, 1.0f, 0, 1.0f, 0, 0, 0, 0}, 
                                             { 0, 1.0f, 0, 0, 0, 1.0f, 0, 0, 0}, 
                                             { 0, 0, 0, 0, 1.0f, 0, 1.0f, 0, 0}, 
                                             { Mathf.Sqrt(2.0f), 1.0f, 0, 1.0f, 0, 1.0f, Mathf.Sqrt(2.0f), 1.0f, 0}, 
                                             { 0, 0, 1.0f, 0, 1.0f, 0, 0, 0, 0},
                                             { 0, 0, 0, 1.0f, Mathf.Sqrt(2.0f), 0, 0, 0, 0},
                                             { 0, 0, 0, 0, 1.0f, 0, 0, 0, 1.0f},
                                             { 0, 0, 0, 0, 0, 0, 0, 1.0f, 0} };
    public bool[] isCheck = new bool[9] { false, false, false, false, false, false, false, false, false };

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
           
    }

    public void FindShortestPath(int start, int finish)
    {
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
        Debug.Log(shortestPath.Count);
        for(int i = 0; i < shortestPath.Count; i++)
        {
            Debug.Log(shortestPath[i]);
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
