using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image ammoImage;
    [SerializeField] private TextMeshProUGUI ammoCounter;

    private PlayerRangedAttack playerRangedAttack;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch playerRangedAttack
        playerRangedAttack = GameManager.GetInstance().ActivePlayer.GetComponent<PlayerRangedAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmoCounter();
    }

    private void UpdateAmmoCounter()
    {
        if (playerRangedAttack)
        {
            // Update current ammo count
            ammoCounter.text = $"{playerRangedAttack.Ammo:00}";
        }
    }
}
