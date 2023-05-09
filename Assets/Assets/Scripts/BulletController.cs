using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRGB2D;//Hace referencia al rigidbode de la bala
    [SerializeField] private float velocityMultiplier;//Hace referencia a la velocidad de la bala
    [SerializeField] private int damage;//Hace refrencia al daño de la bala
    // Es una sintaxis que permite declarar un evento que puede ser utilizado por otras partes del código para suscribirse
    // En este caso este evento esta recibiendo dos parametros
    public event Action<int, HealthBarController> onCollision;
    /*Este metodo se utiliza para que la bala se pueda mover en la direccion de mouse, y como se llama una sola vez
    entonces solo ira al punto en el cual al momento de ser instancio, el mouse se encontraba*/
    public void SetUpVelocity(Vector2 velocity, string newTag, AudioClip newAudio){
        myRGB2D.velocity = velocity * velocityMultiplier;
        gameObject.tag = newTag;

        GetComponent<AudioSource>().PlayOneShot(newAudio);
        DamageManager.instance.SubscribeFunction(this);
    }
    /*El OnBecameInvisible es un metodo que nos permite hacer una accion que al momento de salirno de la camara
    mi objeto en si haga algo en especial*/ 
    private void OnBecameInvisible() {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //El if hace una comparacion para que la logica no haya que colicionen con su propio intanciador
        if(!other.CompareTag(gameObject.tag) && (other.CompareTag("Player") || other.CompareTag("Enemy"))){
            //Preguanque si el objeto con el que ha colisionado tiene el codido de HealthBarController
            if(other.GetComponent<HealthBarController>()){
                onCollision?.Invoke(damage,other.GetComponent<HealthBarController>());
            }//y por ultimo se destruye
            Destroy(this.gameObject);
        }
    }
}
