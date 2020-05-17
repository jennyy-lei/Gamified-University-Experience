using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public int currentIndex = 0;
    private int childCount;
    private List<Animator> optionAnims;
    private RectTransform rectTransform;
    private bool isMoving;
    void Start()
    {
        isMoving = false;
        optionAnims = new List<Animator>();
        rectTransform = GetComponent<RectTransform>();
        childCount = transform.childCount;
        for(int i = 0; i < childCount - 6; i++){
            int a = i;
            transform.GetChild(i+3).GetComponent<Button>().onClick.AddListener(() => onClick(a));
            optionAnims.Insert(i,transform.GetChild(i+3).GetComponent<Animator>());
            if(currentIndex == i){
                optionAnims[i].Play("select",0,1f);
            }else{
                optionAnims[i].Play("unselect",0,1f);
            }
        }
        transform.localPosition += new Vector3(-currentIndex*75f,0,0);
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
            startPos = transform.localPosition;
            endPos = transform.localPosition + (toLeft ? -1 : 1) *amtToMove;
            time = 0f;
            optionAnims[currentIndex].SetTrigger("trigger");
            currentIndex += toLeft ? 1 : -1;
            optionAnims[currentIndex].SetTrigger("trigger");
            while(time < 1){
                time += Time.deltaTime/seconds;
                transform.localPosition = Vector3.Lerp(startPos,endPos,time);
                LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
                yield return null;
            }
            transform.localPosition = endPos;
        }
        isMoving = false;
    }
    void onClick(int i){
        int dist = Mathf.Abs(currentIndex - i);
        if(!isMoving){
            isMoving = true;
            StartCoroutine(MoveToPosition(new Vector3(75f,0f,0f),0.75f,dist,i > currentIndex));
        }
    }
}
