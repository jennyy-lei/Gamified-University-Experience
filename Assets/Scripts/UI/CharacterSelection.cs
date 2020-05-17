using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public int selectedIndex = 0;
    private int childCount;
    private List<Animator> gridAnims;
    private RectTransform gridRect;
    private bool isMoving;
    void Start()
    {
        isMoving = false;
        gridAnims = new List<Animator>();
        gridRect = GetComponent<RectTransform>();
        childCount = gridRect.childCount;
        for(int i = 0; i < childCount - 6; i++){
            int a = i;
            gridRect.GetChild(i+3).GetComponent<Button>().onClick.AddListener(() => onClick(a));
            gridAnims.Insert(i,gridRect.GetChild(i+3).GetComponent<Animator>());
            if(selectedIndex == i){
                gridAnims[i].Play("select",0,1f);
            }else{
                gridAnims[i].Play("unselect",0,1f);
            }
        }
        gridRect.localPosition += new Vector3(-selectedIndex*75f,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveToPosition(Vector3 amtToMove, float seconds,int repeat = 1,bool toLeft = false,bool isLocal = true){
        Vector3 startPos;
        Vector3 endPos;
        float time;
        for(int i = 0; i < repeat; i++){
            startPos = gridRect.localPosition;
            endPos = gridRect.localPosition + (toLeft ? -1 : 1) *amtToMove;
            time = 0f;
            gridAnims[selectedIndex].SetTrigger("trigger");
            selectedIndex += toLeft ? 1 : -1;
            gridAnims[selectedIndex].SetTrigger("trigger");
            while(time < 1){
                time += Time.deltaTime/seconds;
                gridRect.localPosition = Vector3.Lerp(startPos,endPos,time);
                LayoutRebuilder.MarkLayoutForRebuild(gridRect);
                yield return null;
            }
            gridRect.localPosition = endPos;
        }
        isMoving = false;
    }
    void onClick(int i){
        int dist = Mathf.Abs(selectedIndex - i);
        if(!isMoving){
            isMoving = true;
            StartCoroutine(MoveToPosition(new Vector3(75f,0f,0f),0.75f,dist,i > selectedIndex));
        }
    }
}
