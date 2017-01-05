﻿//using SmartLocalization;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    #region variables
    public const string sceneName = "bar";

    /// <summary>Баристо за стойкой Gameplay</summary>
    public GameObject BaristoWork;
    /// <summary>Баристо оттдыхает Win</summary>
    public GameObject BaristoRelax;
    /// <summary>Баристо грустит Lose</summary>
    public GameObject BaristoDepressed;
    /// <summary>места для посадки клиентов</summary>
    public BarstoolController[] СlientList;
    /// <summary>Список всех спрайтов товаров</summary>
    public Sprite[] GoodSprite;
    public Sprite VrongSprite;
    /// <summary>Список всех префабов клиентов</summary>
    public GameObject[] CustomerPrefab;
    /// <summary>Анимация товара при клике</summary>
    public GameObject ItemPrefab;


    /// <summary>жизни</summary>
    public GameObject[] liveList;
    /// <summary>потерянные жизни</summary>
    public GameObject[] liveoffList;

    /// <summary>счетчик клиентов</summary>
    public Text LiveText;
    #endregion

    #region unity
    public void Awake()
    {
        CoreGame.Instance.StartBar();

        ShowStats();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();

        if (CoreGame.Instance.GameWin)
        {
            BaristoRelax.SetActive(true);
            BaristoDepressed.SetActive(false);
            BaristoWork.SetActive(false);

            foreach (var client in СlientList) client.Hide();

            if (Input.GetMouseButtonDown(0)) RestartClick();
        }
        else if (CoreGame.Instance.GameLose)
        {
            BaristoDepressed.SetActive(true);
            BaristoRelax.SetActive(false);
            BaristoWork.SetActive(false);

            foreach (var client in СlientList) client.Hide();

            if (Input.GetMouseButtonDown(0)) RestartClick();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
                if (hit.transform != null)
                {
                    var itemButton = hit.transform.gameObject.GetComponent<ItemButton>();
                    if (itemButton != null) CheeseCakeClick(hit.point, itemButton);

                }
            }

            CoreGame.Instance.TurnTime(Time.deltaTime);
            ShowStats();

            BaristoWork.SetActive(true);
            BaristoRelax.SetActive(false);
            BaristoDepressed.SetActive(false);
        }
    }

    public void RestartClick()
    {
        CoreGame.Instance.RestartGame();
    }
    #endregion

    #region stuff
    private void ShowStats()
    {
        if (CoreGame.Instance==null) return;

        for (var i = 0; i < СlientList.Length; i++)
            СlientList[i].ShowState(this);

        LiveText.text = string.Format("{0}",CoreGame.Instance.ScoreCount);

        for (var i = 0; i < liveList.Length; i++)
        {
            var fullLive = CoreGame.Instance.LiveCount > i;

            liveList[i].SetActive(fullLive);
            liveoffList[i].SetActive(!fullLive);
        }
    }

    private void CheeseCakeClick(Vector2 point, ItemButton clickItem)
    {
        var result = CoreGame.Instance.ClickItem(clickItem.ItemType);

        if (result)
        {
            var item = (GameObject) Instantiate(ItemPrefab, transform);
            item.transform.localPosition = new Vector3(point.x, point.y, -0.01f);
            item.GetComponentInChildren<SpriteRenderer>().sprite = GoodSprite[(int) clickItem.ItemType];
            Destroy(item, 3f);
        }
        else
        {
            var item = (GameObject)Instantiate(ItemPrefab, transform);
            item.transform.localPosition = new Vector3(point.x, point.y, -0.01f);
            item.GetComponentInChildren<SpriteRenderer>().sprite = VrongSprite;
            Destroy(item, 3f);
        }
    }
    #endregion

    #region achievements

    /// <summary>большой улов</summary>
    private void BigFishAchievement(int fishing)
    {
        /*if (fishing < 3) return;
        if (PlayerPrefs.HasKey(GPGSIds.achievement_big_fish)) return;

        Social.ReportProgress(GPGSIds.achievement_big_fish, 100.0f, (bool success) =>
        {
            // handle success or failure
            if (success)
            {
                PlayerPrefs.SetInt(GPGSIds.achievement_big_fish, 100);
            }
        });*/
    }
    #endregion
}
