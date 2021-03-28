using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PedometerU;
using TMPro;
using System;
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    bool inUI = true;
    public float totalHealth = 100f;
    public float currHealth = 100f;
    public float strength = 10f;
    public int experience = 0;
    public int steps = 0;
    public float timer;
    public bool startRun;
    private float endDistPos = -85;
    private float startDistPos = -840;
    public RectTransform dist, healthbar;
    Pedometer p;
    public TextMeshProUGUI timeUI, stepText, missionTitle;
    public Text distanceText;

    public Quest currentQuest;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        p = new Pedometer(onStep);        
        inUI = true;
    }
    
    void OnSceneLoaded() {
        if (SceneManager.GetActiveScene().name == "PhoneUI")
        {
            stepText = GameObject.Find("Steps").GetComponent<TextMeshProUGUI>();
            distanceText = GameObject.Find("Distance").GetComponent<Text>();
            timeUI = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
            inUI = true;
        }
        else {
            inUI = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startRun) {
            timer += Time.deltaTime;
            int minutes = (int) Mathf.Floor(timer / 60);
            int seconds = (int) timer % 60;
            if (inUI)
            {
                timeUI.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }
    }

    public void takeDmg(int dmg) {
        currHealth -= dmg;
        float healthDelta = currHealth / totalHealth;
        healthbar.localScale = new Vector2(healthbar.localScale.x * healthDelta, healthbar.localScale.y);
    }

    public void onStep(int steps, double distance) {
        if (steps == 0 && distance == 0) {
            missionTitle.text = currentQuest.title;
        }
        if (inUI) {
            stepText.text = (currentQuest.distance - steps).ToString();
        }
        dist.position = new Vector2(dist.position.x, dist.position.y + (endDistPos - startDistPos) * steps / currentQuest.distance);
    }
}
