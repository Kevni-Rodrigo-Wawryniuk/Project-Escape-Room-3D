using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    // Esto es para poder llamar a este codigo en otros codigos
    public static Player player;

    [Header("Components")]
    // componente de fisicas
    [SerializeField] Rigidbody rgbP;

    [Header("Move")]
    // variable que determina si puedes moverte o no
    public bool move;
    // fuerza del movimiento
    [SerializeField] float speedp;
    // Esta es la velocidad con la que rota el jugador
    [SerializeField] float rotateY;
    // Botones De  movimientos
    [SerializeField] Image[] imageBottonMove;
 

    [Header("Jumps")]
    // variable que determina si el jugador puede saltar
    public bool jump;
    // la cantidad de saltos disponibles
    [SerializeField] int numberJumps;
    // fuerza del salto
    [SerializeField] float forceJ;
    // varible que da el tamaño del cubo que detecta el suelo, la posicion del jugador , y la direccion del cubo detector
    [SerializeField] Vector3 cubeSize, positionPlayer, direccionCube;
    // capa a detectar 
    [SerializeField] LayerMask layerGround;
    // Esta es la distancia del rayo
    [SerializeField] float distanceRay, distanceMax;
    // boton de salto 
    [SerializeField] Image imageBottonJump;


    [Header("Player interaction")]
    // variable que permite la interaccion don los objetos
    public bool interaction;
    // boton que permite saber si interactuaste con el objeto
    [SerializeField] Image imageBottonInteration;
    // tamaño de la esfera de deteccion
    [SerializeField] float radioSphere, distanceMaxSphere;
    // capas a colicionar capas pistas
    [SerializeField] LayerMask layerTracks;


    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    // Estee es la funcion que solo se llama una vez al iniciar el codigo
    void StartProgram()
    {
        if(player == null)
        {
            player = this;
        }

        // Esto es para llamar al componente de fisicas del objeto
        rgbP = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        JumpPlayer();
        PlayerInterative();
    }
    // llamada cada 30 fps
    private void FixedUpdate()
    {
        MovePlayer();
    }

    // dibujar los los radios de deteccion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // esto es para dibujar un cubo de salto
        Gizmos.DrawWireCube(positionPlayer + direccionCube * distanceRay, cubeSize /2.1f);
    }
    // Esta funcion es para mover al personaje
    private void MovePlayer()
    {
        if (move == true)
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speedp = 10;
            }
            else
            {
                speedp = 5;
            }
            
            // Botones al ser presionados
            if (Input.GetKey(KeyCode.W))
            {
                imageBottonMove[0].color = Color.yellow;
            }
            else
            {
                imageBottonMove[0].color = Color.white;
            }
            if (Input.GetKey(KeyCode.S))
            {
                imageBottonMove[1].color = Color.yellow;
            }
            else
            {
                imageBottonMove[1].color = Color.white;
            }
            if (Input.GetKey(KeyCode.D))
            {
                imageBottonMove[2].color = Color.yellow;
            }
            else
            {
                imageBottonMove[2].color = Color.white;
            }
            if (Input.GetKey(KeyCode.A))
            {
                imageBottonMove[3].color = Color.yellow;
            }
            else
            {
                imageBottonMove[3].color = Color.white;
            }

            float moveX = speedp * Input.GetAxis("Horizontal");
            float moveZ = speedp * Input.GetAxis("Vertical");

            transform.Rotate(0,moveX * rotateY * Time.deltaTime,0);

            transform.Translate(0, 0, moveZ * Time.deltaTime);

            //rgbP.velocity = new Vector3(moveX, rgbP.velocity.y, rgbP.velocity.z);
        }
    }
    // Esta funcion es como puede saltar el personaje
    private void JumpPlayer()
    {
        if(jump == true)
        {
            // Determinar la posicion del jugador
            positionPlayer = transform.position;
            // darle una direccion al cubo detector
            direccionCube = Vector3.down;
            // determinar el rayo
            RaycastHit hit;
            // darle una sentencia para poder usar el raycasthit
            // el boxcast se compone de los siguientes componentes
                            // posicion,       tamaño,       , direccion    , rayo   , rotacion           , distancia  ,   capa     ,  interaccion con el mundo      
            if(Physics.BoxCast(positionPlayer, cubeSize/ 2.1f, direccionCube, out hit, Quaternion.identity, distanceMax, layerGround, QueryTriggerInteraction.UseGlobal))
            {
                // aqui limitamos la distancia del rayo con otra variable
                distanceRay = hit.distance;
                numberJumps = 1;
            }

            if (Input.GetKeyDown(KeyCode.Space) && numberJumps > 0)
            {
                numberJumps--;
                //print("Salto: " + numberJumps.ToString());
                rgbP.AddForce(new Vector3(rgbP.velocity.x, forceJ, rgbP.velocity.z), ForceMode.Impulse);
                imageBottonJump.color = Color.yellow;
            }
            else
            {
                imageBottonJump.color = Color.white;
            }
        }
    }
    // Esta funcion es para interactuar con lo que el jugador se encuentre
    private void PlayerInterative()
    {
        if(interaction == true)
        {
            RaycastHit hit;

            if(Physics.SphereCast(transform.position,radioSphere, Vector3.zero ,out hit,distanceMax, layerTracks,QueryTriggerInteraction.UseGlobal))
            {

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                imageBottonInteration.color = Color.yellow;
            }
            else
            {
                imageBottonInteration.color = Color.white;
            }
        }
    }
}
