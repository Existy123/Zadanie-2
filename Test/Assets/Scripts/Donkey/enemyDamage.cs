using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    public float damage;
    public float damageRate; //jak czesto dmg
    public float pushBackForce; //szansa na ucieczke

    float nextDamage; //kiedy nastepny raz mozemy otrzymac dmg


    void Start()
    {
        nextDamage = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="Player" && nextDamage<Time.time) //tag w unity
        {
            playerHealth thePlayerHealth = other.gameObject.GetComponent<playerHealth>();
            //odwolanie do skryptu
            thePlayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;

            pushBack(other.transform);

        }

        void pushBack(Transform pushedObject) //push backward, dostep do poruszania sie po czasie;
        {
            Vector2 pushDirection = new Vector2(0, (pushedObject.position.y - transform.position.y)).normalized;
            //direction of push - opposite to direction that is pushin, normalize ma wartosc 1
            //kierunek odwrotny do wektora
            pushDirection *= pushBackForce; //not normalized
            Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>(); //RB popychanego obiektu
            pushRB.velocity = Vector2.zero;
            pushRB.AddForce(pushDirection, ForceMode2D.Impulse); //impuls albo sila wybuch
        }   
    }
}
