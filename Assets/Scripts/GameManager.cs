using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform leftGroundPlatform, rightGroundPlatform, player;
    public CanonController leftCanonController, rightCanonController;
    public CanonShooter leftCanonShooter, rightCanonShooter;
    public GameObject youWinText;
}
