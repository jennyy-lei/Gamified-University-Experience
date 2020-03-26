using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform content;
    [SerializeField]
    private GameObject prefab;

    private GameObject[] charList;

    [SerializeField]
    private int scale;

    public void Awake()
    {
        charList = Resources.LoadAll<GameObject>("Units/PlayerSprites/");
        Debug.Log(charList.Length);

        LoadButtons();
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

            Debug.Log(charSprite.transform.position);

            charParent.GetComponent<Button>().onClick.AddListener(() => Select(index));
        }
    }

    public void Select(int index)
    {
        GameObject newObj = (GameObject)Instantiate(charList[index], player.transform.GetChild(0).position, player.transform.GetChild(0).rotation);
        newObj.transform.localScale = new Vector3(1, 1, 1);

        Destroy(player.transform.GetChild(0).gameObject);

        newObj.transform.SetParent(player.transform);
        newObj.transform.SetSiblingIndex(0);

        getAnimator();
    }

    public void StartGame()
    {
        Debug.Log("start!");

        getAnimator();

        player.GetComponent<InputController>().enabled = true;
        gameObject.SetActive(false);
    }

    private void getAnimator() => player.GetComponent<Player2>().animator = player.GetComponentInChildren<Animator>();
}
