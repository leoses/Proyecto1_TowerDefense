using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int money;
    Animator sprite;
    private void Start()
    {
        sprite = this.gameObject.GetComponentInChildren<Animator>();
    }

    //Metodo que resta vida y destruye o respawnea cuando esta llega a 0;
    public void RecieveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //El resto de objetos con este componente son enemigos o barricadas

            if (this.gameObject.CompareTag("Barricade"))
            {
                transform.Translate(100, 100, 0);
                Destroy(gameObject, 0.1f);
            }

            else
            {
                GameManager.instance.GanaDinero(money);
                sprite.SetBool("IsDead", true);
                Destroy(this.gameObject, 1f);
            }
        }
    }

}

