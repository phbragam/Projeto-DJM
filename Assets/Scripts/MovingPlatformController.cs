using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlatformController : MonoBehaviour {

    private Vector3 startPosition;
    private Vector3 endPosition;

    public float movingDist = 20f;

    public float secs = 5f;

    // Use this for initialization
    void Awake () {
        startPosition = transform.position;
        endPosition = transform.position + (movingDist*Vector3.right);
    }

    // Update is called once per frame
    void FixedUpdate () {
        transform.position = Vector3.Lerp (startPosition, endPosition, 
        Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.timeSinceLevelLoad/5, 1f))); // (endPosition - startPosition) * Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.timeSinceLevelLoad/5, 1f)) => Que varia entre 0 e 1 => a função smooph aqui apenas suaviza o movimento, uma vez que são passados os parâmetros 0f e 1f
        // Quando o valor de Time.timeSinceLevelLoad/5 passa de 1f (ou seja, para um tempo maior que um segundo, ele começa a voltar, quando passa de 2f, ele retorna ao sentido inicial (movimento de ping) 
        /*L = 2 * length
          T = Time.time mod L
 
         if 0 <= T < length:
          return T
         else:
          return L - T */
    }

    void OnCollisionEnter2D (Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            if (col.gameObject.GetComponent<PlayerController>().isGrounded())
                col.gameObject.transform.parent = transform;
        }    
    }

    void OnCollisionExit2D (Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            col.gameObject.transform.parent = null;
        }    
    }
}