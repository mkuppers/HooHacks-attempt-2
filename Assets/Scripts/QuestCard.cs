using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestCard : MonoBehaviour
{
    public Quest adventure;
    public GameObject detailed;
    public GameObject simple;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI steps;
    public TextMeshProUGUI xp;
    public TextMeshProUGUI rewards;
    bool isClicked;
    private Vector3 origPos;
    private Vector3 origSize;
    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;        
        simple.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = adventure.title;
        description.text = adventure.description;
        steps.text = adventure.distance + " steps";
        xp.text = adventure.exp + " xp";
        origPos = transform.position;
        origSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void press()
    {
        //Selecting card
        if (!isClicked)
        {
            title.text = adventure.title;
            detailed.SetActive(true);
            simple.SetActive(false);
            this.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 1);
            this.transform.localScale = new Vector3(this.transform.localScale.x * 2.5f, this.transform.localScale.y * 2.5f, 1);
            isClicked = true;
            this.GetComponent<Button>().interactable = false;
            transform.SetAsLastSibling();
        }
        //else ignore
        else {
            
        }
    }

    public void close() {
        this.transform.position = origPos;
        this.transform.localScale = origSize;
        detailed.SetActive(false);
        simple.SetActive(true);
        isClicked = false;
        this.GetComponent<Button>().interactable = true;
    }

    public void select()
    {
        Player user = GameObject.Find("Player").GetComponent<Player>();
        user.currentQuest = adventure;
        user.startRun = true;
        user.onStep(0, 0);
        close();
    }
}
