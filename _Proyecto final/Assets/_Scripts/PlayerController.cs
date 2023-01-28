using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 movement;

    private Animator _animator;

    [SerializeField] private float turnSpeed;

    private Rigidbody _rigidbody;

    private Quaternion rotation = Quaternion.identity;

    private AudioSource playerFootsteps;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        playerFootsteps = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        //une los movimientos horizontales y verticales
        movement.Set(horizontal, 0, vertical);
        
        //esto hace que se mantenga una velocidad fija
        movement.Normalize();

        //una funcion booleana en donde vamos a ver si se esta oprimiendo el axis horizontal
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);//Funcion que se usa para comparar dos valores
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);//Funcion que se usa para comparar dos valores

        bool isWlaking = hasHorizontalInput || hasVerticalInput;

        if (isWlaking)
        {
            if (!playerFootsteps.isPlaying)
            {
                playerFootsteps.Play();
            }
        }
        else
        {
            playerFootsteps.Stop();
        }

        _animator.SetBool("IsWalking", isWlaking);

        //es la direccion en la que me gustaria estar mirando
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,movement, turnSpeed*Time.fixedDeltaTime, 0f);
        
        //Con esto convertimos la direccion a la que vamos a mirar en una rotacion
         rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
