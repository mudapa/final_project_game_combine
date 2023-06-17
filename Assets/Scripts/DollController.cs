using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DollController : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject dollBulletPrefab;

    public Transform dollShotPoint;

    [Header("Movement")]
    public Transform leftSideBorder;
    public Transform rightSideBorder;

    public bool isGoingRight;

    public float movementSpeed;

    [Header("Health")]
    public float currentHp;
    public float maxHp;

    public Image healthBarUI;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip shootSfx;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Shoot()
    {
        StartCoroutine(ShootCoroutine());
        IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(Random.Range(3,5));
            audioSource.PlayOneShot(shootSfx);
            GameObject bulletGO = Instantiate(dollBulletPrefab, dollShotPoint.position, Quaternion.identity);
            bulletGO.GetComponent<DollBullet>().playerTarget = GameManager.instance.player.position;

            StartCoroutine(ShootCoroutine());
        }
    }

    void Movement()
    {
        if (isGoingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightSideBorder.position, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, rightSideBorder.position) < 1)
            {
                isGoingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftSideBorder.position, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, leftSideBorder.position) < 1)
            {
                isGoingRight = true;
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHp -= damageAmount;
        healthBarUI.fillAmount = currentHp / maxHp;

        if (currentHp <= 0)
        {
            GameManager.instance.youWinText.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // lebih ringan dari GetComponent
        if (other.TryGetComponent(out PlayerBullet playerBullet))
        {
            TakeDamage(Random.Range(playerBullet.minDamage, playerBullet.maxDamage));
            Destroy(other.gameObject);
        }

        //if (other.GetComponent<PlayerBullet>())
        //{
        //    TakeDamage(Random.Range(other.GetComponent<PlayerBullet>().minDamage, other.GetComponent<PlayerBullet>().maxDamage));
        //}
    }
}
