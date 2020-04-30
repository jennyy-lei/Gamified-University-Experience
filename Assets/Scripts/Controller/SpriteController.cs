using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private GameObject[] charList;

    // Start is called before the first frame update
    void Awake(){
        charList = Globals.getCharList();
    }

    void Start(){
        switchChar(Globals.getCharIndex());
    }

    public void switchChar(int index){
        GameObject newObj = (GameObject)Instantiate(charList[index], transform.GetChild(0).position, transform.GetChild(0).rotation);
        newObj.transform.localScale = new Vector3(1, 1, 1);

        Destroy(transform.GetChild(0).gameObject);

        newObj.transform.SetParent(transform);
        newObj.transform.SetSiblingIndex(0);

        GetComponent<Unit2>().animator = GetComponentInChildren<Animator>();
    }
}
