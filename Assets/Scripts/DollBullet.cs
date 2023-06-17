using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollBullet : MonoBehaviour
{
    public Vector3 playerTarget;

    public float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTarget, bulletSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, playerTarget) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Dead();
        }

        Destroy(gameObject);
    }
}
