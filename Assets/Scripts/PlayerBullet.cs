using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float minDamage, maxDamage;

    public GameObject bulletEffectPrefab;

    private void OnDisable()
    {
        Instantiate(bulletEffectPrefab, transform.position, Quaternion.identity);
    }
}
