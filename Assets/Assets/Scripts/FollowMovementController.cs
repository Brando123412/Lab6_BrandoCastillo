using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMovementController : MonoBehaviour
{
    [SerializeField] private Transform ogreTransform;//Agarra el transform del enemigo 
    [SerializeField] private Rigidbody2D ogreRB2D;//Agarra el rigidbody del enemigo 
    [SerializeField] private float velocityModifier;//Variable de velocidad de movimiento 
    [SerializeField] private BulletController bullet;//hace referencia a la bala
    [SerializeField] PatrolMovementController cogidoPatrol;
    private Transform currentTarget;//Posicion que se ira moviendo siempre y cuando que el pesonaje entra a sus area de ataque
    private bool isFollowing;//Condisicion para seguir
    private bool isMoving;// Condisicion para moverse
    private bool canShoot = true;//Condicion para disparar
    [SerializeField] bool tienepoints; 
    [SerializeField] Soundscriptableobjects SoundBala;

    private void Start() {
        currentTarget = transform;//Al principio este target tendra el valor inicial de su mismo objeto
    }

    private void Update() {
        /*Si isMoving es verdad, entonces el ogro se movera a la posicion del player al mismo tiempo
        el enemigo ira atacando cada un segundo
        Si no el enemigo se ira al centro de la posicion del objeto*/
        if(isMoving){
            ogreRB2D.velocity = (currentTarget.position - ogreTransform.position).normalized * velocityModifier;
            if(isFollowing && canShoot){
                StartCoroutine(ShootBullet());
                canShoot = false;
            }
            CalculateDistance();
        }else if(!isMoving && ogreTransform!=null){
            if(isFollowing==true)
            {
                ogreRB2D.velocity = (currentTarget.position - ogreTransform.position).normalized * velocityModifier;
            CalculateDistance();
            }else if(isFollowing==false&&tienepoints==true){
                cogidoPatrol.CheckNewPoint();
            }
            /*ogreRB2D.velocity = (currentTarget.position - ogreTransform.position).normalized * velocityModifier;
            CalculateDistance();
            print("debe correr");
            cogidoPatrol.CheckNewPoint();*/
        }
    }
    //Esto hace el calculo entre el enemigo y el objeto para que este siempre vaya a la direccion correcta del enemigo
    private void CalculateDistance(){
        if((currentTarget.position - ogreTransform.position).magnitude < 0.05f){
            ogreTransform.position = currentTarget.position;
            isMoving = false;
            ogreRB2D.velocity = Vector2.zero;
        }else{
            isMoving = true;
        }
    }
    //Esta corutine hace que el enemigo empieze a intanciar balas cada un segundo
    IEnumerator ShootBullet(){
        Instantiate(bullet, ogreTransform.position, Quaternion.identity).SetUpVelocity(ogreRB2D.velocity, "Enemy",SoundBala);
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
    /*Los dos de abajo hara el hace que vaya cambiando la posicion del current depensido de si salio o entro al 
    area */
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            currentTarget = other.transform;
            isFollowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            currentTarget = transform;
            isFollowing = false;
        }
    }
}
