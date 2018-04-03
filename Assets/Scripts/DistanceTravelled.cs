using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTravelled : MonoBehaviour
{

    public float distance;
    public Transform player;
    public Text label;


    void Awake()
    {
        distance = Vector3.Distance(player.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        score();
    }

    public void score()
    {
        distance = Vector3.Distance(player.position, transform.position);
        label.text = distance.ToString();
    }


}