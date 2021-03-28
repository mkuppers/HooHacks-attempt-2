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
    public int totalSteps = 0;
    int encounters = 1;
    public float timer = 0;
    public bool startRun;
    private float endDistPos = -85;
    private float startDistPos = -840;
    public RectTransform dist, healthbar;
    Pedometer p;
    public TextMeshProUGUI timeUI, stepText, missionTitle;
    public Text distanceText;
    public Image redFlash;

    public Quest currentQuest;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SceneManager.GetActiveScene().name == "LoadInScene") {
            SceneManager.LoadScene(1);
        }
        p = new Pedometer(onStep);
        onStep(0, 0);
        inUI = true;
    }

    public void doneWithEncounter() {
        SceneManager.LoadScene(1);
    }
    
    void OnLevelWasLoaded(int level) {
        if (level == 1)
        {
            stepText = GameObject.Find("Steps").GetComponent<TextMeshProUGUI>();
            distanceText = GameObject.Find("Distance").GetComponent<Text>();
            timeUI = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
            missionTitle = GameObject.Find("MissionTitle").GetComponent<TextMeshProUGUI>();
            dist = GameObject.Find("In").GetComponent<RectTransform>();
            inUI = true;
            stepText.text = currentQuest.distance - totalSteps + "";
        }
        if (level == 2) {
            inUI = false;
            healthbar = GameObject.Find("Fill").GetComponent<RectTransform>();
            redFlash = GameObject.Find("ScreenFlash").GetComponent<Image>();
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
        if (redFlash) {
            if (redFlash.color.a >= 0) {
                Color c = new Color(redFlash.color.r, redFlash.color.g, redFlash.color.b, redFlash.color.a - Time.deltaTime);
                redFlash.color = c;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            SceneManager.LoadScene(2);
        }
    }

    public void takeDmg(int dmg) {
        currHealth -= dmg;
        float healthDelta = currHealth / totalHealth;
        healthbar.localScale = new Vector2(healthbar.localScale.x * healthDelta, healthbar.localScale.y);
        Color c = new Color(redFlash.color.r, redFlash.color.g, redFlash.color.b, 1f);
        redFlash.color = c;
    }

    public void onStep(int steps, double distance) {
        totalSteps = steps;
        if (steps == 0 && distance == 0) {
            missionTitle.text = currentQuest.title;
            encounters = 1;
        }
        if (inUI) {
            stepText.text = (currentQuest.distance - steps).ToString();
        }
        dist.position = new Vector2(dist.position.x, dist.position.y + (endDistPos - startDistPos) * (totalSteps / currentQuest.distance));
        if (steps / currentQuest.distance >= (1 / 2) * currentQuest.distance * encounters && steps != 0) {
            encounters++;
            SceneManager.LoadScene(2);
        }
    }
}
