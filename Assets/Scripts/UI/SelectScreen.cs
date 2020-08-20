using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SelectScreen : MonoBehaviour
{
    public delegate void IntAction(int index);
    public delegate void Action();

    public IntAction buyEffect;
    public IntAction selectEffect;
    public Action disableEffect;
    public Button button;
    public TextMeshProUGUI priceText;
    private TextMeshProUGUI buttonText;
    public GameObject gridPrefab;
    public GameObject disabledGrid;
    private GameObject[] itemList;
    public Player2 playerScript;
    public SpriteController spriteScript;

    [SerializeField]
    private int selectedIndex;
    private List<Animator> gridAnims;
    [SerializeField]
    private RectTransform contentRect;
    private Vector3 contentDefaultPos;
    private bool isMoving;
    protected virtual void Awake()
    {
        buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        contentDefaultPos = contentRect.localPosition;
    }
    void Start(){
        button.onClick.AddListener(() => Submit());
    }
    public void initScreen(GameObject[] itemList, int index = 0, IntAction selectEffect = null, IntAction buyEffect = null, Action disableEffect = null){
        this.disableEffect = disableEffect;
        this.itemList = itemList;
        this.buyEffect = buyEffect;
        this.selectedIndex = index;
        this.selectEffect = selectEffect;
    }
    void OnEnable(){
        isMoving = false;
        gridAnims = new List<Animator>();
        contentRect.localPosition += new Vector3(-selectedIndex*75f,0,0);
        loadGrid();
        loadGridAnim();
        gridAnims[selectedIndex].Play("select",0,1);
        updatePrice(selectedIndex);
    }
    protected virtual void OnDisable(){
        disableEffect();
        //reset panel
        gridAnims[selectedIndex].SetTrigger("trigger");
        contentRect.localPosition = contentDefaultPos;
        unloadGrid();
    }
    private void loadGrid()
    {
        for(int i = 0; i < 3; i++){ //disabled grid
            GameObject disableObj = (GameObject)Instantiate(disabledGrid);
            disableObj.transform.SetParent(contentRect);
            disableObj.transform.localScale = new Vector3(1,1,1);
        }
        for (int i = 0; i < itemList.Length; i++) {
            GameObject grid = (GameObject)Instantiate(gridPrefab);
            GameObject sprite = (GameObject)Instantiate(itemList[i]);
            sprite.transform.SetParent(grid.transform);
            grid.transform.SetParent(contentRect);
            sprite.transform.localScale = new Vector3(70f,70f,70f);
            grid.name = i.ToString();
            Destroy(sprite.GetComponent<Collider>());
            Destroy(sprite.GetComponent<Rigidbody2D>());
            int foo = i;
            grid.GetComponent<Button>().onClick.AddListener(() => onSelect(foo));
            gridAnims.Insert(foo,grid.GetComponent<Animator>());
        }
        for(int i = 0; i < 3; i++){ //disabled grid
            GameObject disableObj = (GameObject)Instantiate(disabledGrid);
            disableObj.transform.SetParent(contentRect);
            disableObj.transform.localScale = new Vector3(1,1,1);
        }
    }
    private void unloadGrid(){
        foreach(RectTransform t in contentRect){
            Destroy(t.gameObject);
        }
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
            if(playerScript.gold - selectedIndex < 0){
                return;
            } 
            playerScript.gold -= selectedIndex; //temp price for each sprite
        }
        buyEffect(selectedIndex);
        gameObject.SetActive(false);
    }
    void onSelect(int index){
        if(selectedIndex == index) return;
        if(index < 0 || index > itemList.Length -1) return;
        if(!isMoving){
            isMoving = true;
            StartCoroutine(moveToIndex(index));
            selectEffect(index);
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
    public int getSelectedIndex(){
        return selectedIndex;
    }
}
