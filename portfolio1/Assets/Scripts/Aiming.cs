using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    private Ray aiming;
    public Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.active)
        {
            aiming = Camera.main.ScreenPointToRay(transform.position);
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(aiming, out hitInfo, 30.0f, 1 << 0);
            if (hit)
            {

            }
            else
            {

            }
            targetPos = aiming.GetPoint(30.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(aiming);
    }
}
