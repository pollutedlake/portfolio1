using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlies : MonoBehaviour
{
    public Character character;
    public Vector3 characterPosition;
    public GameObject target;
    public UIMgr uiMgr;

    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    private void Start()
    {
        if (character == null)
        {
            character = FindObjectOfType<Character>();
        }
        if (target.CompareTag("InteractiveObject"))
        {
            StartCoroutine(DetectInteractiveObject());
        }
        if (uiMgr == null)
        {
            uiMgr = FindObjectOfType<UIMgr>();
        }
    }

    private void Update()
    {
        if (target.CompareTag("InteractiveObject"))
        {
            
            Vector3 distance = target.transform.position - transform.position;
            if (distance.sqrMagnitude < 0.1f)
            {
                target.GetComponent<SpriteRenderer>().color = Color.green;
                uiMgr.ShowObjectName(target);
                Destroy(gameObject);
            }
            
        }
        else
        {
            characterPosition = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
            if ((characterPosition - transform.position).sqrMagnitude < 0.1f || (characterPosition - transform.position).sqrMagnitude > 25.0f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DetectInteractiveObject()
    {
        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, target.transform.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
