using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTestScript : MonoBehaviour
{
    public bool save;
    public Player2 data;
    // Start is called before the first frame update
    void Start()
    {
        save = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(save){
            save = !save;
            LevelController.saveData<PlayerState>(data.getGameState(),"PlayerState");
        }
    }
}
