using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CharacterSelectScreen : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI priceText;
    private TextMeshProUGUI buttonText;
    public GameObject gridPrefab;
    public GameObject disabledGrid;
    private GameObject[] charList;
    public Player2 playerScript;
    public SpriteController spriteScript;

    [SerializeField]
    public int selectedIndex;
    private List<Animator> gridAnims;
    [SerializeField]
    private RectTransform contentRect;
    private bool isMoving;
    void Awake()
    {
        isMoving = false;
        charList = Globals.getCharList();
        gridAnims = new List<Animator>();
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        loadGrid();
    }
    void Start(){
        button.onClick.AddListener(() => Submit());
    }
    void OnEnable(){
        selectedIndex = Globals.getCharIndex();
        updatePrice(selectedIndex);
        loadGridAnim();
        updateLocation();
    }
    void OnDisable(){
        if(Globals.getCharIndex() != selectedIndex){
            spriteScript.switchChar(Globals.getCharIndex());
        }
        //reset panel
        gridAnims[selectedIndex].SetTrigger("trigger");
        contentRect.localPosition -= new Vector3(-selectedIndex*75f,0,0);
    }
    private void updateLocation(){
        contentRect.localPosition += new Vector3(-selectedIndex*75f,0,0);
        gridAnims[selectedIndex].Play("select",0,1);
    }
    private void loadGrid()
    {
        for(int i = 0; i < 3; i++){ //disabled grid
            GameObject disableObj = (GameObject)Instantiate(disabledGrid);
            disableObj.transform.SetParent(contentRect);
            disableObj.transform.localScale = new Vector3(1,1,1);
        }
        for (int i = 0; i < charList.Length; i++) {
            GameObject grid = (GameObject)Instantiate(gridPrefab);
            GameObject charSprite = (GameObject)Instantiate(charList[i]);
            charSprite.transform.SetParent(grid.transform);
            grid.transform.SetParent(contentRect);
            charSprite.transform.localScale = new Vector3(70f,70f,70f);
            grid.name = i.ToString();
            Destroy(charSprite.GetComponent<Collider>());
            int foo = i;
            grid.GetComponent<Button>().onClick.AddListener(() => onSelect(foo));
            gridAnims.Insert(foo,grid.GetComponent<Animator>());
        }
        for(int i = 0; i < 3; i++){ //disabled grid
            GameObject disableObj = (GameObject)Instantiate(disabledGrid);
            disableObj.transform.SetParent(contentRect);
            disableObj.transform.localScale = new Vector3(1,1,1);
        }
        updateLocation();
    }
    private void loadGridAnim(){
        foreach(Animator a in gridAnims){
            a.Play("unselect",0,1);
        }
        
    }
    void Update()
    {
        if(Input.GetButtonDown("Submit")) {
            Submit();
        }
        float x = Input.GetAxis("Horizontal");
        if(x > 0f){
            onSelect(selectedIndex + 1);
        }
        else if (x < 0f){
            onSelect(selectedIndex - 1);
        }
    }
    public void updatePrice(int index)
    {
        priceText.text = index + " gold";
        if(index > playerScript.gold){
            button.interactable = false;
            priceText.color = Color.red;
            buttonText.text = "Unaffordable";
            buttonText.color = Color.red;
        }
        else{
            button.interactable = true;
            priceText.color = Color.black;
            buttonText.text = "Buy";
            buttonText.color = Color.black;
        }
    }
    public void Submit()
    {
        if(Globals.getCharIndex() != selectedIndex){
            if(playerScript.gold < 0) return;
            playerScript.gold -= selectedIndex; //temp price for each sprite
        }
        Globals.setCharIndex(selectedIndex);
        gameObject.SetActive(false);
    }
    void onSelect(int index){
        if(selectedIndex == index) return;
        if(index < 0 || index > charList.Length -1) return;
        if(!isMoving){
            isMoving = true;
            StartCoroutine(moveToIndex(index));
            spriteScript.switchChar(index);
            updatePrice(index);
        }
    }
    public IEnumerator moveToIndex(int index){
        Vector3 startPos;
        Vector3 endPos;
        Vector3 amtToMove = new Vector3(75f,0f,0f);
        float seconds = 0.6f;
        float time;
        int repeat = selectedIndex - index;
        int direction = repeat >= 0 ? 1 : -1;
        while(repeat != 0){
            startPos = contentRect.localPosition;
            endPos = contentRect.localPosition + direction * amtToMove;
            time = 0f;
            gridAnims[selectedIndex].SetTrigger("trigger");
            selectedIndex -= direction;
            gridAnims[selectedIndex].SetTrigger("trigger");
            while(time < 1){
                time += Time.deltaTime/seconds;
                contentRect.localPosition = Vector3.Lerp(startPos,endPos,time);
                yield return null;
                LayoutRebuilder.MarkLayoutForRebuild(contentRect);
            }
            repeat -= direction;
            contentRect.localPosition = endPos;
        }
        isMoving = false;
    }
}
