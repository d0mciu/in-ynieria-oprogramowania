using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyHandler : MonoBehaviour
{

    public static int money;
    Text txt;
    private void Awake(){
        txt = GetComponent<Text>();
    } 
    private void Update(){
        txt.text = money + "$";
    }
}
