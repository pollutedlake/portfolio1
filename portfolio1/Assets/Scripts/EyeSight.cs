using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSight : MonoBehaviour
{
    private Ray[] eyeSights = new Ray[6];
    public Monster monster;
    // Start is called before the first frame update
    void Start()
    {
        if (monster == null)
        {
            monster = FindObjectOfType<Monster>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (monster.monState == monster.monStates["Patrol"])
        {
            eyeSights[0] = new Ray(transform.position, transform.parent.right);
            eyeSights[1] = new Ray(transform.position, -transform.parent.right);
            eyeSights[2] = new Ray(transform.position, transform.parent.right + transform.parent.up);
            eyeSights[3] = new Ray(transform.position, transform.parent.right - transform.parent.up);
            eyeSights[4] = new Ray(transform.position, -transform.parent.right + transform.parent.up);
            eyeSights[5] = new Ray(transform.position, -transform.parent.right - transform.parent.up);
            for (int i = 0; i < eyeSights.Length; i++)
            {
                RaycastHit hitInfo;
                bool isCharacter = Physics.Raycast(eyeSights[i], out hitInfo, 7.0f, 1 << 8);
                if (isCharacter)
                {
                    monster.character = hitInfo.transform.GetComponent<Character>();
                    monster.Shout();
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        eyeSights[0] = new Ray(transform.position, transform.parent.right);
        eyeSights[1] = new Ray(transform.position, -transform.parent.right);
        eyeSights[2] = new Ray(transform.position, transform.parent.right + transform.parent.up);
        eyeSights[3] = new Ray(transform.position, transform.parent.right - transform.parent.up);
        eyeSights[4] = new Ray(transform.position, -transform.parent.right + transform.parent.up);
        eyeSights[5] = new Ray(transform.position, -transform.parent.right - transform.parent.up);
        for (int i = 0; i < eyeSights.Length; i++)
        {
            Debug.DrawRay(eyeSights[i].origin, eyeSights[i].direction.normalized*7.0f, Color.red);
        }
    }
}
