using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private int DropRange = 2;

    [SerializeField]
    private float dropMultiplier = 1;

    [SerializeField]
    private int maxDrop = 4, minDrop = 1;

    public void Drop()
    {
        float randNum;
        int randAmt = Random.Range(minDrop, maxDrop);
        float lowerBound = 0, upperBound;

        for (int i = 0; i < randAmt; i++) {
            randNum = (float)Random.Range(0, 1000) / 1000;
            lowerBound = 0;

            foreach (GameObject item in Globals.getItemList()) {
                upperBound = (item.GetComponent<IItem>().dropChance / Globals.totalDropChance()) + lowerBound;

                if (randNum <= upperBound && randNum >= lowerBound) {
                    GenItem(item);
                    break;
                }

                lowerBound = upperBound;
            }
        }
    }

    private void GenItem(GameObject item)
    {
        GameObject obj = (GameObject)Instantiate(item, gameObject.transform.position, gameObject.transform.rotation);
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-DropRange, DropRange), Random.Range(0, DropRange));
    }
}