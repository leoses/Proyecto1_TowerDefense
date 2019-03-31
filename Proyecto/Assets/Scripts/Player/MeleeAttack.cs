using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

    GameObject meleeArea;
    public float attackRate = 1f;
    float time;
    bool active;
    private Animator animator;

    private void Start()
    {
        //El área del ataque es el primer hijo del objeto
        meleeArea = gameObject.transform.GetChild(0).gameObject;
        time = attackRate;
        animator = this.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        //Si se dan las condiciones se activa el área de colisión (el booleano evita repetir el if innecesariamente)
        if (!active && Input.GetKeyDown("mouse 1") && time >= attackRate)
        {
            meleeArea.SetActive(true);
            animator.SetTrigger("MeleeAtack");
            time = 0;
            active = true;
        }

        else if (active)
        {
            active = false;
            meleeArea.SetActive(false);
        }
    }
}
