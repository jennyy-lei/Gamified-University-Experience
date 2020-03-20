using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemList;

    private int DropRange = 2;

    public void Drop()
    {
        int randNum = Random.Range(0, 100);

        GenItem(itemList[0]);
        GenItem(itemList[0]);
        GenItem(itemList[0]);
    }

    private void GenItem(GameObject item)
    {
        GameObject obj = (GameObject)Instantiate(item, gameObject.transform.position, gameObject.transform.rotation);
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-DropRange, DropRange), Random.Range(0, DropRange));
    }
}
