using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Player user;
    public bool aggro = true;
    public float health = 100f;
    public float exp = 5f;
    // Start is called before the first frame update
    void Start()
    {
        user = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Attacked
    private void OnMouseDown()
    {
        if (aggro) {
            takeDmg();
        }
    }

    private void takeDmg() {
        health -= user.strength;
        if (health <= 0) {
            die();
        }
    }

    private void die() {
        user.experience += exp;
        Destroy(this.gameObject);
    }
}
