using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;/*Game objects para ver por que puntos se va a mover*/
    [SerializeField] private Rigidbody2D myRBD2;/*Agarra su propio rigidbody*/
    [SerializeField] private AnimatorController animatorController;/*hace referencia a sus animaciones*/
    [SerializeField] private SpriteRenderer spriteRenderer;/*Para editar las propiedades de su imagene*/
    [SerializeField] private float velocityModifier = 5f;/*Velocidad del enemigo*/
    [SerializeField] private float raycastDistance = 5f;/*Distancia del rayo para detectar al enemigo*/
    [SerializeField] private LayerMask layerInteraction;/*Para escojer con que layers va a interactuar el rayo*/
    private Transform currentPositionTarget;/*transfor que se actualizara segun donde vaya el personaje*/
    private int patrolPos = 0;/*esto hace que el enemigo vaya de un punto a otro segun su valor*/
    private float fastVelocity = 0f;/*Velocidad al detectar el personaje*/
    private float normalVelocity;//Velocidad normal
    [SerializeField]bool tieneCircleCollider;
    /*En el start estoy agustando los parametros para estrablecer los parametros*/
    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;

        normalVelocity = velocityModifier;
        fastVelocity = velocityModifier * 2.5f;
    }
    /*En el Update estoy llamando tanto a la animacion como al meotoo que hace que mi personaja vaya de un punto
    a otro*/
    private void Update() {
        if(tieneCircleCollider==false)
        {
            CheckNewPoint();
        }
        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
    }
    /*Esta logica es para que el enemigo vaya de un punto a otro*/
    public void CheckNewPoint(){
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            transform.position = currentPositionTarget.position;
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            CheckFlip(myRBD2.velocity.x);
        }
        Vector2 distanceTarget = currentPositionTarget.position - transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, distanceTarget, raycastDistance, layerInteraction);
        if(hit2D){
            if(hit2D.collider.CompareTag("Player")){
                velocityModifier = fastVelocity;
            }
        }else{
            velocityModifier = normalVelocity;
        }

        myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
        Debug.DrawRay(transform.position, distanceTarget * raycastDistance, Color.cyan);
        
    }
    //Para que el personaje gire su imagen segun donde se dirije 
    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
}
