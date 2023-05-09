using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private int maxValue;//Variable de vida
    [Header("Health Bar Visual Components")] 
    [SerializeField] private RectTransform healthBar;//Barra de vida
    [SerializeField] private RectTransform modifiedBar;//Barra de viva
    [SerializeField] private float changeSpeed;//Cambio de velocidad para la variable de vida
    public int currentValue;//Variable de vida
    private float _fullWidth;//Calcular la vida
    private float TargetWidth => currentValue * _fullWidth / maxValue;//ancho de la barra de vida
    private Coroutine updateHealthBarCoroutine;//Las corrutinas son algo que que ocurren en segundo plano

    public event Action onHit;//Evento de recibir daño
    public event Action onDeath;//evento de Morrirse 

    private void Start() {
        currentValue = maxValue;//La variable current toma el valor maximo de vida
        _fullWidth = healthBar.rect.width;//creo que hace que la barra de vida sea al maximo
    }

    /// <summary>
    /// Metodo <c>UpdateHealth</c> actualiza la vida del personaje de manera visual. Recibe una cantidad de vida modificada.
    /// </summary>
    /// <param name="amount">El valor de vida modificada.</param>
    public void UpdateHealth(int amount){
        /*Se declara que la variable currentValue solo puede tomar valores entre 0 y 100 ademas esto dependera 
        de daño que pueda recibir el personaje*/
        currentValue = Mathf.Clamp(currentValue + amount, 0, maxValue);
        //Esta llamando a evento onHit en cual dependera quien lo llame ya sea el player o los enemigos
        onHit?.Invoke();

        if(updateHealthBarCoroutine != null){
            StopCoroutine(updateHealthBarCoroutine);
        }
        updateHealthBarCoroutine = StartCoroutine(AdjustWidthBar(amount));

        //Este if esta preguntando si mi vida llego a 0, para que llame al evento de perder
        if(currentValue == 0){
            onDeath?.Invoke();
        }
    }
    //Esta corrutina y el metodo de abajo hace la actualizacion de la barra de vida
    IEnumerator AdjustWidthBar(int amount){
        RectTransform targetBar = amount >= 0 ? modifiedBar : healthBar;
        RectTransform animatedBar = amount >= 0 ? healthBar : modifiedBar;

        targetBar.sizeDelta = SetWidth(targetBar,TargetWidth);

        while(Mathf.Abs(targetBar.rect.width - animatedBar.rect.width) > 1f){
            animatedBar.sizeDelta = SetWidth(animatedBar,Mathf.Lerp(animatedBar.rect.width, TargetWidth, Time.deltaTime * changeSpeed));
            yield return null;
        }

        animatedBar.sizeDelta = SetWidth(animatedBar,TargetWidth);
    }

    private Vector2 SetWidth(RectTransform t, float width){
        return new Vector2(width, t.rect.height);
    }
}
