using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 100f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float rotAxis = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotateSpeed * rotAxis * Time.deltaTime);

        transform.position = player.transform.position;
    }
}
