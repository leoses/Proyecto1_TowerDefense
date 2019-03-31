using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BalaEscopeta esc = collision.gameObject.GetComponent<BalaEscopeta>();
        Destroy(esc.gameObject);
    }
}
