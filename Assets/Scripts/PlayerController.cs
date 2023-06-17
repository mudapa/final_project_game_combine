using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 jumpTarget;

    public Animator animator;

    public float jumpForce, moveSpeed;

    public bool canJump, isJumping;

    public int currentPlatformIndex;

    public GameObject realBody, ragdollBody;

    private AudioSource audioSource;
    public AudioClip jumpSfx, gotShotSfx;

    public ParticleSystem bloodEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ActivateLeftCanon(true);
        ActivateRightCanon(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(currentPlatformIndex == 0) return;
            PressJump(false);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            if (currentPlatformIndex == 1) return;
            PressJump(true);
        }
        
        Jump();
    }

    void PressJump(bool jumpToRight)
    {
        if (isJumping || !canJump) return;

        canJump = false;

        StartCoroutine(PressJumpCoroutine(jumpToRight));
        IEnumerator PressJumpCoroutine(bool jumpToRight)
        {
            animator.SetTrigger("Jump");
            yield return new WaitForSeconds(0.5f);
            audioSource.PlayOneShot(jumpSfx);
            isJumping = true;
            rb.velocity = Vector3.up * jumpForce;

            if (jumpToRight)
            {
                currentPlatformIndex = 1;
                jumpTarget = GameManager.instance.rightGroundPlatform.position;
            }
            else
            {
                currentPlatformIndex = 0;
                jumpTarget = GameManager.instance.leftGroundPlatform.position;
            }
        }
    }

    void Jump()
    {
        if(!isJumping) return;

        transform.position = Vector3.MoveTowards(transform.position, jumpTarget, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, jumpTarget) < 1)
        {
            canJump = true;
            isJumping = false;
            animator.SetTrigger("Idle");
            ChangeCannon();
        }
    }

    void ChangeCannon()
    {
        if (currentPlatformIndex == 0)
        {
            ActivateLeftCanon(true);
            ActivateRightCanon(false);
        }
        else
        {
            ActivateLeftCanon(false);
            ActivateRightCanon(true);
        }
    }

    void ActivateLeftCanon(bool activate)
    {
        GameManager.instance.leftCanonController.enabled = activate;
        GameManager.instance.leftCanonShooter.enabled = activate;
    }

    void ActivateRightCanon(bool activate)
    {
        GameManager.instance.rightCanonController.enabled = activate;
        GameManager.instance.rightCanonShooter.enabled = activate;
    }

    public void Dead()
    {
        bloodEffect.Play();
        audioSource.PlayOneShot(gotShotSfx);
        realBody.SetActive(false);
        ragdollBody.SetActive(true);
    }
}
