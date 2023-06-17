using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonShooter : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public Transform cannonShotPoint;
    public float shootForce;

    private AudioSource audioSource;
    public AudioClip shootSfx;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shootSfx);
        GameObject cannonBall = Instantiate(cannonBallPrefab, cannonShotPoint.position, cannonShotPoint.rotation);
        cannonBall.GetComponent<Rigidbody>().AddForce(cannonBall.transform.forward * shootForce, ForceMode.Impulse);
    }
}
