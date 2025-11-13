using UnityEngine;

public class MoverObjetoEstado : IEstado
{
    private Controlador controlador;
    private string tagSeleccionable = "Seleccionable"; // Tag del prefab
    private LayerMask sueloMask;                        // Layer del suelo
    private GameObject objetoSeleccionado;
    private bool objetoListoParaMover = false;

    //Evitamos números mágicos
    [SerializeField] 
    private float distanciaMaxima = 100f; // Distancia máxima para raycast

    public MoverObjetoEstado(Controlador ctrl, LayerMask suelo)
    {
        controlador = ctrl;
        sueloMask = suelo;
    }

    public void Entrar(Controlador ctrl)
    {
        Menus.Instance.MensajeMover();
    }

    public void Ejecutar(Controlador ctrl)
    {
        if (Camera.main == null)
        {
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            ctrl.CambiarEstado(new EstadoNeutral());
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!objetoListoParaMover)
        {
            // Selección del objeto
            if (Input.GetMouseButtonDown(0))
            {
                Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.red, 2f);

                if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
                {
                    if (hit.collider.CompareTag(tagSeleccionable))
                    {
                        objetoSeleccionado = hit.collider.gameObject;
                        objetoListoParaMover = true;
                     
                    }
                }
            }
        }
        else
        {
            // Movimiento del objeto sobre el suelo
            if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima, sueloMask))
            {
                if (objetoSeleccionado != null)
                {
                    // Coloca el objeto sobre el punto del raycast
                    objetoSeleccionado.transform.position = hit.point;
                    Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.green);
                }
            }

            // Colocar objeto al hacer clic
            if (Input.GetMouseButtonDown(0) && objetoSeleccionado != null)
            {
                objetoListoParaMover = false;
                objetoSeleccionado = null;
                ctrl.CambiarEstado(new EstadoNeutral());
            }
                if (Input.GetMouseButtonDown(1))
            {
                objetoSeleccionado = null;
                ctrl.CambiarEstado(new EstadoNeutral());
            }
        }
    }

    public void Salir(Controlador ctrl)
    {
        Menus.Instance.DesactivarTodos();
        objetoSeleccionado = null;
        objetoListoParaMover = false;
    }
}
