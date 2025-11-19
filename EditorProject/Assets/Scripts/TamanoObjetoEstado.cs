using UnityEngine;

public class TamanoObjetoEstado : IEstado
{
    private Controlador controlador;
    private string tagSeleccionable = "Seleccionable";
    private GameObject objetoSeleccionado;

    private float xyz = 1.05f;
    [SerializeField] 
    private float distanciaMaxima = 100f;

    public TamanoObjetoEstado(Controlador ctrl)
    {
        controlador = ctrl;
    }

    public void Entrar(Controlador ctrl) { }

    public void Ejecutar(Controlador ctrl)
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Seleccionar con clic
        if (objetoSeleccionado == null && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
            {
                if (hit.collider.CompareTag(tagSeleccionable))
                    Menus.Instance.BotonesTamano();
                    objetoSeleccionado = hit.collider.gameObject;
            }
        }

        // Salir con clic derecho
        if (objetoSeleccionado != null && Input.GetMouseButtonDown(1))
        {
            objetoSeleccionado = null;
            ctrl.CambiarEstado(new EstadoNeutral());
        }
    }

    // Aumentar tamaño
    public void Aumentar()
    {
        if (objetoSeleccionado != null)
            objetoSeleccionado.transform.localScale *= xyz;
    }

    // Disminuir tamaño
    public void Disminuir()
    {
        if (objetoSeleccionado != null)
            objetoSeleccionado.transform.localScale /= xyz;
    }

    public void Salir(Controlador ctrl)
    {
        objetoSeleccionado = null;
    }
}
