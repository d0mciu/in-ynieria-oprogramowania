using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour
{
    // Start is called before the first frame update
    public bool inRange;
    public GameObject MerchantPanel;

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E)){
            MerchantPanel.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            inRange = false;
            MerchantPanel.SetActive(false);
        }
    }
}
