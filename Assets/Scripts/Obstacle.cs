using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public int damage = 20;
    public int gainSpeed = 2;
    public string obstacleName;

    void Start() {
        speed = GameObject.Find("Player").GetComponent<PlayerController>().gameSpeed;
    }

    private void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D (Collider2D collide){
        if(collide.CompareTag("Player")) {
            // collide.GetComponent<PlayerController>().health -= damage;
            Destroy(gameObject);
        }
    }

     void OnBecameInvisible() {
         Destroy(gameObject);
     }
}
