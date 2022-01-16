using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    public float speed;
    private Vector2 input;

    //capturar el animator
    private Animator _animator;

    public LayerMask solidObjectsLayer;
    public LayerMask pokemonLayer;
      

    private void Awake()
    {
        _animator = GetComponent<Animator>();

    }


    private void Update()
    {
        
        //si está parado permite el movimiento.
        if (!isMoving)
        {
            //captura si se ha pulsado las teclas para mover el personaje.
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));



            //para evitar que personaje pueda desplazarse en diagonal.
            if (input.x != 0)
            {
                input.y = 0;
            }


            //si el valor del vector2 de input es 0, es que no hay movimiento
            if (input != Vector2.zero)
            {

                //pasamos al animator el valor de las entradas X , Y
                _animator.SetFloat("MoveX", input.x);
                _animator.SetFloat("MoveY", input.y);

                var targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;

                if (IsAvailable(targetPosition))
                {
                    //el metodo update se ejecuta cada frame, si hay que programar alguna accion que dure mas de un frame se usa las corrutinas.
                    //si no se hiciera, el movimiento del personaje seria muy rapido ya que debe realizar el desplazamiento en el tiempo que dura
                    //un frame.
                    StartCoroutine(MoveTowards(targetPosition));
                }

            }
        }

    }


    //se ejecuta despues del update.
    private void LateUpdate()
    {
        _animator.SetBool("Is Moving", isMoving);
    }

    /*corrutina.
     una corrutina es una funcion que tiene la potestad de gobernarse a si misma durante mas de un frame.
     para crear una corrutina hay que usar la palabra reservada IEnumerator.
     el bucle while chequea si está en el destino.

     Es muy dificil estar exactamente en las coordenadas de destino, por eso se una la precision de la maquina (Epsilon).
     Mientras la distancia entre el origen y el destino sea mayor que la precision de la maquina, el personaje se va ha
     mover poco a poco.

     para calcular la distancia entre dos posiciones se usa Distance.
     
     El metodo movetowards de Vector3 permite indicar el origen, el destino y el delta (gap) que queremos cubrir en este frame.
     Time.deltaTime es el tiempo que ha pasado desde el ultimo frame.
     para calcular el incremento del espacio que quiero que se mueva en el frame actual se multiplica:
      el tiempo (deltatime) por la velocidad (speed).

     
     yield return null, permite esperar,sin hacer nada, al siguiente frame si ha acabado el movimiento antes de que acabe el frame actual.

     una vez cerrado el bucle porque la distancia entre la posicion y el destino es menor que el el valor de precision, se le pasa
     a la posicion el valor del destino exacto.

     con isMoving en true mientras se ejecuta la corrutina, en el update no se le permitirá capturar cambios de movimiento.
     */


    IEnumerator MoveTowards(Vector3 destination)
    {
        isMoving = true;

        //calcula si la distancia entre la posicion y el destino es mayor a la precision de la maquina.
        while(Vector3.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;

        CheckForPokemon();
    }


    /// <summary>
    /// El metodo comprueba que la zona a la que queremos acceder, este disponible.
    /// </summary>
    /// <param name="target">zona a la que queremos acceder</param>
    /// <returns>Devuelve true, si el target está disponible y false en caso contrario</returns>
    private bool IsAvailable(Vector3 target)
    {

        //physics2D hace un overlaping en la capa solidobjectslayer de un radio de 0.25 (el tamaño de cada celda del grid es de 1.
        //si no detecta el overlaping, devuelve false, sino true.
        if (Physics2D.OverlapCircle(target, 0.1f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void CheckForPokemon()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f,pokemonLayer) != null)
        {
            if (Random.Range(0, 100) < 30)
            {
                Debug.Log("sale un pokemon");
            }
        }
    }
}
