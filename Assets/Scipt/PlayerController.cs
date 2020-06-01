using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb { get { return GetComponent<Rigidbody>(); } }
    public GameObject focalPoint { get { return GameObject.Find("FocalPoint"); } }

    private float moveSpeed = 500;
    private float powerupStrength = 25;
    private float normalStrength = 10;
    private float turboBoost = 10;
    public bool isPowerup;

    [SerializeField]
    private GameObject powerIndicator;

    [SerializeField]
    private ParticleSystem smokePartical;

    // Start is called before the first frame update
    void Start()
    {
        //focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float moveAxis = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * moveAxis * moveSpeed * Time.deltaTime);

        powerIndicator.transform.position = transform.position + new Vector3(0, -0.3f, 0);

        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            rb.AddForce(focalPoint.transform.forward * turboBoost, ForceMode.Impulse);
            smokePartical.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            isPowerup = true;
            powerIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDown());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 dir = (other.transform.position - transform.position).normalized;
            
            if (isPowerup)
            {
                enemyRb.AddForce(dir * powerupStrength, ForceMode.Impulse);
            }
            else
            {
                enemyRb.AddForce(dir * normalStrength, ForceMode.Impulse);
            }
        }
    }

    IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(7);
        isPowerup = false;
        powerIndicator.SetActive(false);
    }
}
