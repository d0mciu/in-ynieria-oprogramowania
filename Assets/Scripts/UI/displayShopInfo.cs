using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayShopInfo : MonoBehaviour
{
    Text infoText;
    public void SetText(string text){
        infoText.text = text;
    }
}
