using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    public GameObject success;
    bool talkMode = false;
    public OAICharacter shroom;
    int counter = 0;

    public string playerSpeech;
    public TextMeshProUGUI inputField;
    ///public InputField field;

    public void sendInput() {
        playerSpeech = inputField.text;       
        shroom.AddToStory(playerSpeech);
        inputField.text = "";
        //field.text = "";
        counter++;
        if (counter >= 5) {
            success.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void loadMenu() {
        SceneManager.LoadScene(1);
    }

    void Start()
    {
        attackButton = GameObject.Find("AttackButton");
        talkButton = GameObject.Find("TalkButton");
        IEnumerator flash = redFlash();
        StartCoroutine(flash);
    }

    private IEnumerator redFlash()
    {
        yield return new WaitForSeconds(1f);
        characters = GameObject.FindGameObjectsWithTag("Character");
        shroom = GameObject.FindGameObjectWithTag("AI").GetComponent<OAICharacter>();
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
        talkButton.SetActive(false);
        talkMode = true;
        convoMenu.SetActive(true);
    }
}
