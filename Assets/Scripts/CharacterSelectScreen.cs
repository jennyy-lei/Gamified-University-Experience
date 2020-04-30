using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private SpriteController spriteScript;

    [SerializeField]
    private int scale;

    public int tempChar;

    public void Awake()
    {
        charList = Globals.getCharList();

        LoadButtons();

        content.GetChild(Globals.getCharIndex()).GetComponent<Selectable>().Select();
        spriteScript = playerScript.gameObject.GetComponent<SpriteController>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "0 gold";
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

            charSprite.transform.SetParent(charParent.transform);
            charParent.transform.SetParent(content);

            charParent.transform.localScale = new Vector3(1, 1);

            charSprite.transform.position = new Vector3(0, 0);
            charSprite.transform.localScale = new Vector3(scale, scale);

            charParent.GetComponent<Button>().onClick.AddListener(() => Select(index));
        }
    }

    public void Select(int index)
    {
        spriteScript.switchChar(index);
        tempChar = index;
        buttonText.text = tempChar + " gold";
        if(playerScript.gold < tempChar){
            button.interactable = false;
            buttonText.color = Color.red;
        }
        else{
            button.interactable = true;
            buttonText.color = Color.black;
        }
    }

    public void Submit()
    {
        if(Globals.getCharIndex() != tempChar){
            if(playerScript.gold < tempChar) return;
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
