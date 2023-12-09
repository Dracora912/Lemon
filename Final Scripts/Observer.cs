using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameObject parent;
    public GameEnding gameEnding;
    private PlayerMovement Shield;
    bool m_IsPlayerInRange;
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (parent == null)
        {
            Debug.LogWarning("A gameObject does not have an attached parent, scripts may not work as intended.");
        }
        Shield = player.GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }
    void Update()
    {
        bool hasShield = Shield.shield;
        Debug.Log(hasShield);
        if(m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray (transform.position, direction);
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    if (hasShield == false)
                    {
                        gameEnding.CaughtPlayer();
                    }
                    else
                    {
                        Shield.shield = false;
                        Destroy(parent);
                    }                      
                }
            }
        }
    }
}
