using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CharacterSelectScreen : MonoBehaviour
{
    public Button button;
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private Transform content;
    [SerializeField]
    private GameObject prefab;

    private GameObject[] charList;

    public Player2 playerScript;
    public SpriteController spriteScript;

    [SerializeField]
    private int scale;

    public int tempChar;

    public void Awake()
    {
        charList = Globals.getCharList();
        LoadButtons();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        tempChar = Globals.getCharIndex();
    }

    public void OnEnable(){
        //Why this? See https://answers.unity.com/questions/1159573/eventsystemsetselectedgameobject-doesnt-highlight.html
        Button temp = content.GetChild(Globals.getCharIndex()).GetComponent<Button>();
        temp.Select();
        temp.OnSelect(null);
    }

    public void Update()
    {
        if(Input.GetButtonDown("Submit")) {
            Submit();
        }
    }

    private void LoadButtons()
    {
        for (int i = 0; i < charList.Length; i++) {
            GameObject charParent = (GameObject)Instantiate(prefab);
            GameObject charSprite = (GameObject)Instantiate(charList[i]);
            int index = i;
            charParent.name = i.ToString();
            charSprite.transform.SetParent(charParent.transform);
            charParent.transform.SetParent(content);

            charParent.transform.localScale = new Vector3(1, 1);

            charSprite.transform.position = new Vector3(0, 0);
            charSprite.transform.localScale = new Vector3(scale, scale);

            charParent.GetComponent<Button>().onClick.AddListener(() => Select(index));
            charParent.GetComponent<SelectableEvent>().onSelect.AddListener(()=>Select(index));
        }
    }
    public void Select(int index)
    {
        buttonText.text = index + " gold";
        if(playerScript.gold < index){
            button.interactable = false;
            buttonText.color = Color.red;
        }
        else{
            button.interactable = true;
            buttonText.color = Color.black;
        }
        if(tempChar == index) return;
        spriteScript.switchChar(index);
        tempChar = index;
    }

    public void Submit()
    {
        if(Globals.getCharIndex() != tempChar){
            if(playerScript.gold < 0) return;
            playerScript.gold -= tempChar; //temp price for each sprite
        }
        Globals.setCharIndex(tempChar);
        gameObject.SetActive(false);
    }
    void OnDisable(){
        if(Globals.getCharIndex() != tempChar){
            Select(Globals.getCharIndex());
        }
    }
}
