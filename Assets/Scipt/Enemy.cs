using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody enemyRb { get { return GetComponent<Rigidbody>(); } }

    private float speed = 3;
    private GameObject playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        playerGoal = GameObject.Find("Player Goal");   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (playerGoal.transform.position - transform.position);
        enemyRb.AddForce(dir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player Goal"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy Goal"))
        {
            Destroy(gameObject);
        }
    }
}
