using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OpenAI_Unity;

public class Enemy : MonoBehaviour
{
    public Player user;
    public EncounterUI encount;
    public Material dmgMat;
    private Material ogMat;
    private GameObject healthbar;
    private GameObject success;
    public string characterName;
    public bool aggro = false;
    public float totalHealth = 100f;
    public float currHealth = 100f;
    public int exp = 5;
    public int strength = 15;
    public float timer;
    public float attackTime = 3f;

    private Vector3 healthBarSize;
    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.Find("Player").GetComponent<Player>();
        success = GameObject.Find("NoteParent");
        encount = GameObject.Find("Canvas").GetComponent<EncounterUI>();
        healthbar = this.transform.GetChild(0).GetChild(0).gameObject;
        healthBarSize = healthbar.transform.localScale;

        this.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = characterName;

        ogMat = this.transform.GetChild(3).GetChild(0).GetComponent<Renderer>().material;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Camera.main.transform);
        if (aggro) {
            timer += Time.deltaTime;
            if (timer >= attackTime) {
                user.takeDmg(strength);
                timer = 0;
            }           
        }
    }

    //Attacked
    private void OnMouseDown()
    {
        if (aggro)
        {
            takeDmg();
        }
        else {
            encount.shroom = this.transform.GetChild(2).GetComponent<OAICharacter>();
        }
    }

    private IEnumerator redFlash()
    {
        for (int i = 0; i < this.transform.GetChild(3).childCount; ++i) {
            this.transform.GetChild(3).GetChild(i).GetComponent<Renderer>().material = dmgMat;
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < this.transform.GetChild(3).childCount; ++i)
        {
            this.transform.GetChild(3).GetChild(i).GetComponent<Renderer>().material = ogMat;
        }
    }

    private void takeDmg() {
            
        currHealth -= user.strength;
        float healthDelta = currHealth / totalHealth;
        healthbar.transform.localScale = new Vector3(healthBarSize.x * healthDelta, healthBarSize.y, healthBarSize.z);
        IEnumerator flash = redFlash();
        StartCoroutine(flash);
        if (currHealth <= 0) {
            die();
        }
    }

    public void die() {
        user.experience += exp;
        success.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(this.gameObject);
    }  
}
