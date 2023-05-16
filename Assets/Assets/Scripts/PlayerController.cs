using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;//Para su mismo rigidbody
    [SerializeField] private float velocityModifier = 5f;//Para velocidad del Player
    [SerializeField] private float rayDistance = 10f;//Para el rayo del player
    [SerializeField] private AnimatorController animatorController;//Para las Animaciones
    [SerializeField] private SpriteRenderer spriteRenderer;//Para modificar todos los componentes de la imagen del player
    [SerializeField] private BulletController bulletPrefab;//Objeto de la bala del player
    [SerializeField] private CameraController cameraReference;/*Hace referencia a un Cinemachine, que cuando el
    personaje reciba daño*/ 

    [SerializeField] Soundscriptableobjects SoundBala;
    [SerializeField] Soundscriptableobjects SoundDamage;
    [SerializeField] AudioSource lowLifeSound;
    [SerializeField] HealthBarController lowLife;

    Vector2 movementPlayer;
    Vector3 mouseInput;
    Vector3 distance;
    private void Start() {
        GetComponent<HealthBarController>().onHit += cameraReference.CallScreenShake;/*
        Siempre y cuando el personaje reciba daño la camara se movera
        Y siempre y cuando que el evento onHit se llame*/
        GetComponent<HealthBarController>().onHit +=SoundDamage.CreateSound;
    }

    private void Update() {
        //Para mover el personaje en el eje X y Y
        //Vector2 movementPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Aqui se esta multiplicando el movimiento generado por el player la velocidad modificada
        myRBD2.velocity = movementPlayer * velocityModifier;
        //Llama a la animacion de moverse cuando el personaje se mueve
        animatorController.SetVelocity(velocityCharacter: myRBD2.velocity.magnitude);
        //Este vector3 se crea para guarda la posicion del mouse en tiempo actual
        //Vector3 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Este metodo llamado es para que el personaje siempre mire a la posicion del mouse
        CheckFlip(mouseInput.x);
        //Este vector3 es para hallar la distancia entre el personaje en tiempo real
        distance = mouseInput - transform.position;
        //Esto dibuja el posible rayo que se genera con el calculo que se hizo en la parte superio
        Debug.DrawRay(transform.position, distance * rayDistance, Color.red);

        /*if(Input.GetMouseButtonDown(0)){
            //Si hacemos click entonces vamos a instanciar una bala, todo dependiendo de  la posicion del player
            BulletController myBullet =  Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            //Al momento de generar la bala vamos a pasar parametros para que la bala salga en la direccion correcta y al mismo tiempo se le da el tag de player
            //audioEfectos.PlayOneShot(audioFire);
            myBullet.SetUpVelocity(distance.normalized, gameObject.tag,SoundBala);
        }else if(Input.GetMouseButtonDown(1)){
            Debug.Log("Left Click");
        }*/
        if(lowLife.currentValue<25){
            lowLifeSound.Play();
        }
    }
    //Esto simplemente hace que se cambie la direccion de mirar 
    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        movementPlayer = new Vector2(inputMovement.x, inputMovement.y);
    }
    public void OnAim(InputAction.CallbackContext value)
    {
        Vector2 inputMouse = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());
        mouseInput = inputMouse;
    }
    public void OnFire(InputAction.CallbackContext value)
    {
        if (value.started) {
            BulletController myBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            myBullet.SetUpVelocity(distance.normalized, gameObject.tag,SoundBala);
        }
    }
}
