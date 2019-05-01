using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {
    public float seconds;
    private Text text;
    public float speed;
    private Vector3 direction = new Vector3(0, 1, 0);

	void Start () {
        text = gameObject.GetComponent<Text>();
        Destroy(this.gameObject, seconds);
	}

    private void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime);
    }


    public void ModifyText(string newText)
    {
        if (text == null) text = this.gameObject.GetComponent<Text>();
        text.text = newText;
    }
}
