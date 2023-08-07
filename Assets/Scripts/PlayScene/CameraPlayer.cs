using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class CameraPlayer : MonoBehaviour
{
    // Esto es para llamar a este codigo en otros codigos
    public static CameraPlayer cameraPlayer;

    [Header("Components")]
    public Camera mainCamera;

    [Header("Follow Player")]
    // Esto es para determinar si esta siguiendo al jugador
    public bool followPlayer;
    // Esto solo determina la posicion inicial de la camara
    [SerializeField] Vector3 offSet;
    // Esto es para tener la posicion del jugador
    [SerializeField] Transform playerPosition;
    // Esto es la velocidad a la que sigue al jugador
    [SerializeField] float speed;

    [Header("RotateCamera")]
    // Esto es para determinar si el jugador puede o no rotar la camara
    public bool rotateCamera;
    // Esta es la fuerza con la que va a rotar la camara
    [SerializeField] float forceR, distanceCamera, distanceMaxCamera;
    // estas son las posiciones en las que se va a rotar la camara
    private float positionX, positionY;
 
    [Header("Zoom Camera")]
    // Esto es para determinar si el jugador puede hacer zoom
    public bool zoomCamera;
    // Esto es para la velocidad con la que puedes hacer zoom
    [SerializeField] float zoom;

    [Header("Control collision")]
    // punto de colicion de la camara
    [SerializeField] Transform pointCollision;
    // distancia del rayo
    [SerializeField] float distanceRay;


    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    // Esta es la funcion que solo se ejecuta una ves se inicia el juego
    private void StartProgram()
    {
        if(cameraPlayer == null)
        {
            cameraPlayer = this;
        }
        // Esto es usar el componente de la camara
        mainCamera = GetComponent<Camera>();

        // Con esto buscamos la posicion el jugador
        playerPosition = GameObject.Find("Capsule(Player)").transform;
        // buscar el punto de colicion de la camara 
        pointCollision = GameObject.Find("Punto de colicion").transform;
        // Aca le damos la posicion inicial a el offset
        offSet = transform.position - playerPosition.position;
    }
    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        ZoomCamera();
        PreventTraversingObjects();
    }

    // llamar cada 30 fps
    private void FixedUpdate()
    {
        FollowPlayerCamera();
    }

    // dibujar los rayos que sean necesarios
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // dibujar la caja de collicion de la camara
        Gizmos.DrawLine(playerPosition.position, pointCollision.position);

    }

    // Esto es para seguir al jugador
    private void FollowPlayerCamera()
    {
        if(followPlayer == true)
        {
            // Esto es para que optengamos la posicion del jugador mas la posicion de la camara
            Vector3 position0 = playerPosition.position + offSet;
            // Esto nos da la posicion de la camara
            Vector3 position1 = transform.position;
            // aca transformamos la posicion de la camara en base a la posicion del jugador
            // transform.position = Vector3.Lerp(position1, position0, speed * Time.deltaTime);
            transform.Translate(position0);
        }

    }
    // Esto es lo que permite mover la camara y dar la sensacion de que la camara se mueve alrededor del jugador
    private void RotateCamera()
    {
        if (rotateCamera == true)
        {
            positionX += forceR * Time.deltaTime * Input.GetAxis("Mouse X");
            positionY -= forceR * Time.deltaTime * Input.GetAxis("Mouse Y");

            Quaternion rotation = Quaternion.Euler(positionY, positionX, 0);

            Vector3 newPos = playerPosition.position - (rotation * Vector3.forward * distanceCamera);

            transform.position = newPos;

            transform.LookAt(playerPosition.position);
        }
    }
    // Esta funcion evita que la camara atraviece los objetos del entorno
    private void PreventTraversingObjects()
    {
        RaycastHit hit;

        
        if (Physics.Linecast(playerPosition.position, pointCollision.position, out hit))
        {
            distanceRay = hit.distance;
            // Debug.Log("Colicion con algo");
            distanceCamera = distanceRay;
        }
        else
        {
            distanceRay = distanceMaxCamera;

            distanceCamera = distanceRay;
            
        }
    }
    // Esta funcion es para acercar o alejar la camara
    private void ZoomCamera()
    {
        if(zoomCamera == true)
        {
            if (mainCamera.fieldOfView >= 30 && mainCamera.fieldOfView <= 60)
            {
                mainCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoom * Time.deltaTime;
            }
            if(mainCamera.fieldOfView < 30)
            {
                mainCamera.fieldOfView = 30;
            }
            if(mainCamera.fieldOfView > 60)
            {
                mainCamera.fieldOfView = 60;
            }
        }
    }
}
