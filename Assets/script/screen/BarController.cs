﻿//using SmartLocalization;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BarController : MonoBehaviour
{
    #region variables
    public const string sceneName = "bar";

    /// <summary>обучающие стрелки косить сено</summary>
    public Image helpHay;
    /// <summary>префаб "клиента"</summary>
    public GameObject sheepPrefab;
    /// <summary>префаб выбранного ингридиента</summary>
    public GameObject haylagePrefab;
    #endregion

    #region unity
    public void Awake()
    {
        CoreGame.Instance.StartBar();

        ShowStats();

        Random.InitState(DateTime.Now.Second);
        //for (var i = 0; i < CoreGame.Instance.SheepCount; i++) CreateSheep();

        //longhouseButton.GetComponentInChildren<Text>().text = LanguageManager.Instance.GetTextValue("winter_button");
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
        if (CoreGame.Instance.DayCount <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.transform != null)
            {
                if (hit.transform.name.Contains("land")) HaylageClick(hit.point);
            }
        }
    }

    public void WinterClick()
    {
        CoreGame.Instance.Save();
        //SceneManager.LoadScene(AutumnController.sceneName);
    }

    public void RestartClick()
    {
        CoreGame.Instance.RestartGame();
    }
    #endregion

    #region stuff
    private void ShowStats()
    {
        //title.text = string.Format(LanguageManager.Instance.GetTextValue("summer_title"), CoreGame.Instance.DayCount);
    }

    private void HaylageClick(Vector2 point)
    {
        helpHay.gameObject.SetActive(false);

        var stone = CoreGame.Instance.HaylageBar();
        ShowStats();

        var prefab = haylagePrefab;

        var item = (GameObject)Instantiate(prefab, transform);
        item.transform.position = new Vector3(point.x, point.y, 0f);
    }

    private void CreateSheep()
    {
        var cnt = 0;
        var height = Camera.allCameras[0].orthographicSize;
        var width = height * Camera.allCameras[0].aspect * height;

        while (cnt < 100)
        {
            cnt++;
            var point = new Vector2(Random.Range(-width, width), Random.Range(-height, height));

            var hit = Physics2D.Raycast(point, Vector2.zero);
            if (hit.transform != null)
            {
                if (hit.transform.name.Contains("land"))
                {
                    var item = (GameObject)Instantiate(sheepPrefab, transform);
                    item.transform.position = new Vector3(point.x, point.y, 0f);
                    return;
                }
            }
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
