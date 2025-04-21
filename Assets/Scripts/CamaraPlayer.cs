using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPlayer : MonoBehaviour
{
    [SerializeField] private Transform objetivo;
    public float alturaJugador = 1f;
    public float distanciaPared = 0.1f;
    public float maxDistancia = 14f;
    public float minDistancia = 1.87f;

    public float xVelocidad = 200f;
    public float yVelocidad = 200f;
    public int yMinLimite = -80;
    public int yMaxLimite = 80;

    public int velocidadZoom = 40;
    public float amortiguacionRotacion = 3f;
    public float amortiguacionZoom = 5f;

    public LayerMask collisionLayers = -1;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;

    private float distanciaBase = 5f;
    private float distanciaActual;
    private float distanciaDeseada;
    private float distanciaCorregida;

    // Start is called before the first frame update
    void Start()
    {             
        Vector3 angulos = transform.eulerAngles;
        xDeg = angulos.x;
        yDeg = angulos.y;

        distanciaActual = distanciaBase;
        distanciaDeseada = distanciaBase;
        distanciaCorregida = distanciaBase;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    private void LateUpdate()
    {

        Vector3 vTargetOffset;

        if (!objetivo)
            return;

        if (Input.GetMouseButton(1))
        {
            xDeg += Input.GetAxis("Mouse X") * xVelocidad * 0.02f;
            yDeg -= Input.GetAxis("Mouse Y") * yVelocidad * 0.02f;
        }
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            float anguloRotacionObjetivo = objetivo.eulerAngles.y;
            float anguloRotacionActualCamara = transform.eulerAngles.y;
            xDeg = Mathf.LerpAngle(anguloRotacionActualCamara, anguloRotacionObjetivo, amortiguacionRotacion * Time.deltaTime);
        }

        yDeg = CorregirAngulo(yDeg, yMinLimite, yMaxLimite);

        Quaternion rotacionFinalCamara = Quaternion.Euler(yDeg, xDeg, 0);

        distanciaDeseada -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * velocidadZoom * Mathf.Abs(distanciaDeseada);
        distanciaDeseada = Mathf.Clamp(distanciaDeseada, minDistancia, maxDistancia);
        distanciaCorregida = distanciaDeseada;

        vTargetOffset = new Vector3(0, -alturaJugador, 0);
        Vector3 posicionFinalCamara = objetivo.position - (rotacionFinalCamara * Vector3.forward * distanciaDeseada + vTargetOffset);

        RaycastHit collisionHit;
        Vector3 posicionRealObjeto = new Vector3(objetivo.position.x, objetivo.position.y + alturaJugador, objetivo.position.z);

        bool isCorredted = false;
        if (Physics.Linecast(posicionRealObjeto, posicionFinalCamara, out collisionHit, collisionLayers.value))
        {
            distanciaCorregida = Vector3.Distance(posicionRealObjeto, collisionHit.point) - distanciaPared;
            isCorredted = true;
        }

        distanciaActual = !isCorredted || distanciaCorregida > distanciaActual ? Mathf.Lerp(distanciaActual, distanciaCorregida, Time.deltaTime * amortiguacionZoom) : distanciaCorregida;

        distanciaActual = Mathf.Clamp(distanciaActual, minDistancia, maxDistancia);

        posicionFinalCamara = objetivo.position - (rotacionFinalCamara * Vector3.forward * distanciaActual + vTargetOffset);

        transform.rotation = rotacionFinalCamara;
        transform.position = posicionFinalCamara;
    }

    private static float CorregirAngulo(float angulo, float min, float max)
    {
        if (angulo < -360)
            angulo += 360;
        if (angulo > 360)
            angulo -= 360;
        return Mathf.Clamp(angulo, min, max);
    }
}
