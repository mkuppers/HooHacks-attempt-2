using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OpenAI_Unity;
public class EncounterUI : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject attackButton;
    GameObject talkButton;
    public GameObject convoMenu;
    GameObject[] characters;
    bool talkMode = false;
    public OAICharacter shroom;


    public string playerSpeech;
    public TextMeshProUGUI inputField;

    public void sendInput() {
        playerSpeech = inputField.text;
        shroom.AddToStory(playerSpeech);
    }
    void Start()
    {
        attackButton = GameObject.Find("AttackButton");
        talkButton = GameObject.Find("TalkButton");
        characters = GameObject.FindGameObjectsWithTag("Character");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hideButtons() {
        attackButton.SetActive(false);
        talkButton.SetActive(false);
    }
    public void onAttack() {
        hideButtons();
        foreach (GameObject character in characters)
        {
            character.GetComponent<Enemy>().aggro = true;
        }
    }

    public void onTalk()
    {
        hideButtons();
        talkMode = true;
        convoMenu.SetActive(true);
    }
}
