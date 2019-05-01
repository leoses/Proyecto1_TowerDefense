using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {
    public static TextManager instance;
    public RectTransform canvasTransform;
    public TextController textPrefab;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void CreateText(Vector3 position, string infoDisplayed)
    {
        TextController newText = Instantiate<TextController>(textPrefab, position, Quaternion.identity);
        newText.transform.SetParent(canvasTransform);
        newText.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        newText.ModifyText(infoDisplayed);

    }


}
