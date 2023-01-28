using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Observer : MonoBehaviour
{
    //variable de el objeto al que vamos a observar
    public Transform player;

    //con esto verificaremos si el jugador esta en el rango predeterminado
    private bool isPlayerInRange;

    public GameEnding gameEnding;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        
        if (isPlayerInRange)
        {
            //la direccion que va desde los ojos de la gargola hasta el jugador
            Vector3 direction = player.position - transform.position + Vector3.up;

            //esto es un rayo que va desde la posicion del objeto hasta la direccion ya creada
            Ray ray = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction, Color.green, Time.deltaTime, true);
            
            //una variable de salida en la que guardara el resultado del raycast y le dara un nuevo valor, siendo este el de el objeto con el que se estreyo
            RaycastHit raycastHit;

            //Con este rayo podremos saber si lo que mira la gargola es al jugador o si es otro objeto
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CatchPlayer();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(transform.position, player.position + Vector3.up);
        
    }
}
