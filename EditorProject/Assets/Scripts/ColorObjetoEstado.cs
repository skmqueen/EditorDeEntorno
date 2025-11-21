using UnityEngine;

public class ColorObjetoEstado : IEstado
{
    private Controlador controlador;
    private LayerMask suelo;
    private Material materialSeleccionado;
    private string tagSeleccionable = "Seleccionable";
    private GameObject objetoSeleccionado;
    private float distanciaMaxima = 100f;
 
    public ColorObjetoEstado(Material material, LayerMask sueloMask)
    {
        materialSeleccionado = material;
        suelo = sueloMask;
    }

    public void Entrar(Controlador ctrl) { }

    public void Ejecutar(Controlador ctrl)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Seleccionar con clic
        if (objetoSeleccionado == null && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
            {
                if (hit.collider.CompareTag(tagSeleccionable))
                    objetoSeleccionado = hit.collider.gameObject;

                if (objetoSeleccionado != null)
                {
                    objetoSeleccionado.GetComponent<MeshRenderer>().material = materialSeleccionado;

                }
            }
            if (objetoSeleccionado != null && Input.GetMouseButtonDown(0))
            {
                {
                    Menus.Instance.PulsarColor();
                    if (hit.collider.CompareTag(tagSeleccionable))
                        objetoSeleccionado = hit.collider.gameObject;

                    if (objetoSeleccionado != null)
                    {
                        objetoSeleccionado.GetComponent<MeshRenderer>().material = materialSeleccionado;

                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    objetoSeleccionado = null;
                    controlador.CambiarEstado(new EstadoNeutral());
                }

            }
        }
    }
        public void Salir(Controlador ctrl)
        {
            if (Menus.Instance != null)
                Menus.Instance.DesactivarTodos();

            objetoSeleccionado = null;

        }
    }

