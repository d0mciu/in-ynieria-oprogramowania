using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCategory : MonoBehaviour
{
    public GameObject Category1, Category2, Category3, Category4;
    public void displayCategory(){
        Category1.SetActive(false);
        Category2.SetActive(false);
        Category3.SetActive(false);
        Category4.SetActive(false);

    }
}
