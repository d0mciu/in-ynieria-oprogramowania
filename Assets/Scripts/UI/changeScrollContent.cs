using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeScrollContent : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform scrollableContent;
    // Start is called before the first frame update
    public void setScrollableContent()
    {
        scrollRect.content = scrollableContent;
    }

    // Update is called once per frame

}
