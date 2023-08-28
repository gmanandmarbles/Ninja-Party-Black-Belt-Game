using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dice : MonoBehaviour
{
    public GameObject die;
    public bool spin;
    private Transform target;
    public GameObject diePrefab;
    private bool diceSpawned;
    public GameObject diceUI;
    public GameObject cubePrefab;
    public GameObject cube;


    public GameObject die1;
    public GameObject die2;
    public GameObject die3;
    public GameObject die4;
    public GameObject die5;
    public GameObject die6;


    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (spin == true) {
            if (diceSpawned == false){
                diceSpawn();
            }
           
        }
        else if (spin == false){
            diceHide();
        } else {
            spin = true;
        }
    }
    public void diceHide()
    {
        // die.SetActive(false);
        // GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("die");
 
        // foreach(GameObject go in gameObjectArray)
        // {
        //    go.SetActive (false);
        // }
    }
    public void diceSpawn()
    {
        diceUI.SetActive(true);
        


    }
    public void diceNumber(int num)
    {
        if (num == 1)
        {
            die = Instantiate(die1, transform, false);
            die.transform.localPosition = new Vector3(-2, 7, 0);
        } else if (num == 2)
        {
            die = Instantiate(die2, transform, false);
            die.transform.localPosition = new Vector3(-2, 7, 0);
        } else if (num ==3)
        {
            die = Instantiate(die3, transform, false);
            die.transform.localPosition = new Vector3(-2, 7, 0);
        } else if (num == 4)
        {
            die = Instantiate(die4, transform, false);
            die.transform.localPosition = new Vector3(-2, 7, 0);
        } else if (num == 5)
        {
            die = Instantiate(die5, transform, false);
            die.transform.localPosition = new Vector3(-2, 7, 0);
        } else if (num == 6)
        {
            die = Instantiate(die6, transform, false);
            die.transform.localPosition = new Vector3(-2, 7, 0);
        };
    }

}
