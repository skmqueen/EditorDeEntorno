using UnityEngine;

public class RotarObjetoEstado : IEstado
{
    private Controlador controlador;
    private string tagSeleccionable = "Seleccionable";
    private GameObject objetoSeleccionado;

    [SerializeField] private float gradosPorFrame = 2f; // Cuántos grados rotar por frame al mantener el clic

    public RotarObjetoEstado(Controlador ctrl)
    {
        controlador = ctrl;
    }

    public void Entrar(Controlador ctrl)
    {
        Menus.Instance.MensajeRotar();
        Debug.Log("Entrando a RotarObjetoEstado: selecciona un objeto.");
    }

    public void Ejecutar(Controlador ctrl)
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Selección del objeto
        if (objetoSeleccionado == null && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.collider.CompareTag(tagSeleccionable))
                {
                    objetoSeleccionado = hit.collider.gameObject;
                    Debug.Log("Objeto seleccionado para rotar: " + objetoSeleccionado.name);
                }
            }
        }

        // Rotación mientras se mantiene clic izquierdo
        if (objetoSeleccionado != null)
        {
            if (Input.GetMouseButton(0)) // Mientras se presiona el botón izquierdo
            {
                objetoSeleccionado.transform.Rotate(0f, gradosPorFrame, 0f);
            }

            // Finalizar rotación con clic derecho
            if (Input.GetMouseButtonDown(1))
            {
                objetoSeleccionado = null;
                ctrl.CambiarEstado(new EstadoNeutral());
            }
        }
    }

    public void Salir(Controlador ctrl)
    {
        objetoSeleccionado = null;
        Menus.Instance.DesactivarTodos();
    }
}
