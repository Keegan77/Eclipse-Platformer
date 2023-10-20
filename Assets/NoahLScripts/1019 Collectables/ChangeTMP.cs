using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTMP : MonoBehaviour
{

    public StarContainer stars;

    public TMP_Text text;

    public int star;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         text.SetText(stars.Stars.ToString());
    }

    //goes onto TMP asset on Canvas
}
