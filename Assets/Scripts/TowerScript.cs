using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TowerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject leftChoice,middleChoice,rightChoice,bottomChoice;
    public int goldValue;
    public float range;
    void Start()
    {
        if (gameObject.name=="Build Point")
        {
            Instantiate(middleChoice, gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            WeaponFSM weaponFSM = gameObject.GetComponent<WeaponFSM>();
            setVals(weaponFSM.range, weaponFSM.cost);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string none = "none";
    public void towerClicked()
    {
        switch (gameObject.name)
        {
            case "BuildPlatform(Clone)":
                radialMenu(none,"basicTower",none,none);
                Debug.Log("Tower Clicked");
                break;
        }
    }
    public void setVals(float nRange,int nVal)
    {
        range = nRange;
        goldValue = nVal;
    }
    //create struct of tower
    //it needs goldcost,dmg,range,upgrades,downgrade
    void radialMenu(string option1,string option2,string option3,string option4)
    {
        //later set individual menu's that are each a slice. left, top, right, bottom, if next click isn't one of them, delete menu
        //also try to put ring around as visual
        //this needs to be set so it can take any input. so an array of strings options[]{faster(machinegun);slower,more range, more damage(sniper); same speed, more damage(betterrifle?)} stuff like that
        //and ofc last option which would be the tower that it came from so in this case something like ;basicTower]
        //the switch will be in the loop and take option[x] and send them to (0 goes to menu left, 1 menu top, 2 right, 3 bottom
        if (option1 != "none")
        {

        }
        if (option2 != "none")
        {
            switch (option2)
            {
                case "basicTower":               
                    GameObject cam = GameObject.Find("Main Camera");
                    if (cam.GetComponent<GuiScript>().spendGold(middleChoice.GetComponent<WeaponFSM>().cost)){//later make 100 option[i](0-2).value
                        Destroy(gameObject,.1f);
                        Instantiate(middleChoice, gameObject.transform.position, Quaternion.identity);
                    }
                    break;
            }
        }
        if (option4 != "none")
        {
            //instantiate button below tower that can be clicked to sell tower to downgrade it and recieve 1/3 its value;(cost)
        }
    }
    private void upgrade(string option)
    {

    }
}
