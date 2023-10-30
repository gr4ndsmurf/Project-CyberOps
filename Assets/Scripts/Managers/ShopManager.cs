using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : SingletonDontDestroyMono<ShopManager>
{
    public GameObject shopCanvas;

    [SerializeField] int TotalMoney;
    [SerializeField] int TotalCards;
    [SerializeField] int TotalHealthPotion;
    [SerializeField] int TotalPistolMag;
    [SerializeField] int TotalRifleMag;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI cardText;

    [SerializeField] private TextMeshProUGUI healthPotion_CountText;
    [SerializeField] private TextMeshProUGUI pistolMAG_CountText;
    [SerializeField] private TextMeshProUGUI rifleMAG_CountText;

    //Pricing
    [SerializeField] private int healthPotionCashPrice;
    [SerializeField] private int healthPotionCardPrice;

    [SerializeField] private int pistolCashPrice;
    [SerializeField] private int pistolCardPrice;

    [SerializeField] private int rifleCashPrice;
    [SerializeField] private int rifleCardPrice;
    private void Start()
    {
        shopCanvas = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        if (shopCanvas.activeSelf)
        {
            TotalMoney = GameManager.Instance.money;
            TotalCards = GameManager.Instance.cards;
            TotalHealthPotion = GameManager.Instance.HealthPotion;
            TotalPistolMag = GameObject.FindWithTag("Pistol").GetComponent<Weapon>().currentAmmoBox;
            TotalRifleMag = GameObject.FindWithTag("Rifle").GetComponent<Weapon>().currentAmmoBox;

            moneyText.text = TotalMoney.ToString();
            cardText.text = TotalCards.ToString();
            healthPotion_CountText.text = TotalHealthPotion.ToString();
            pistolMAG_CountText.text = TotalPistolMag.ToString();
            rifleMAG_CountText.text = TotalRifleMag.ToString();
        }
    }

    public void BuyHealthPotion()
    {
        if (GameManager.Instance.HealthPotion < GameManager.Instance.MaxHealthPotion && GameManager.Instance.money > 0 && GameManager.Instance.cards > 0)
        {
            GameManager.Instance.HealthPotion++;
            GameManager.Instance.money -= healthPotionCashPrice;
            GameManager.Instance.cards -= healthPotionCardPrice;
        }
    }

    public void BuyPistolMAG()
    {
        if (TotalPistolMag < GameObject.FindWithTag("Pistol").GetComponent<Weapon>().maxAmmoBox && GameManager.Instance.money > 0 && GameManager.Instance.cards > 0)
        {
            GameObject.FindWithTag("Pistol").GetComponent<Weapon>().currentAmmoBox++;
            GameManager.Instance.money -= pistolCashPrice;
            GameManager.Instance.cards -= pistolCardPrice;
        }
    }

    public void BuyRifleMAG()
    {
        if (TotalRifleMag < GameObject.FindWithTag("Rifle").GetComponent<Weapon>().maxAmmoBox && GameManager.Instance.money > 0 && GameManager.Instance.cards > 0)
        {
            GameObject.FindWithTag("Rifle").GetComponent<Weapon>().currentAmmoBox++;
            GameManager.Instance.money -= rifleCashPrice;
            GameManager.Instance.cards -= rifleCardPrice;
        }
    }
}
