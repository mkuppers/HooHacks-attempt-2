using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject profileButton;
    public GameObject questButton;

    public GameObject questScreen;
    public GameObject dashScreen;
    public GameObject profileScreen;

    public Player user;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void questButtonDown() {
        //Pull up dash
        if (user.startRun)
        {
            dashScreen.SetActive(true);
            questScreen.SetActive(false);
        }
        //Pull up questboard
        else {
            dashScreen.SetActive(false);
            questScreen.SetActive(true);
        }
        profileScreen.SetActive(false);
    }

    public void profileButtonDown()
    {
        profileScreen.SetActive(true);
        dashScreen.SetActive(false);
        questScreen.SetActive(false);
    }

}
