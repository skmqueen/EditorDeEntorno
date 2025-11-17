using UnityEngine;

public class RotarObjetoEstado : IEstado
{
    private Controlador controlador;
    private string tagSeleccionable = "Seleccionable";
    private GameObject objetoSeleccionado;

    [SerializeField]
    private float gradosPorFrame = 2f; // Cuántos grados rotar por frame al mantener el clic
    [SerializeField] 
    private float distanciaMaxima = 100f; // Distancia máxima para raycast

    public RotarObjetoEstado(Controlador ctrl)
    {
        controlador = ctrl;
    }

    public void Entrar(Controlador ctrl)
    {
        Menus.Instance.MensajeRotar();
    }

    public void Ejecutar(Controlador ctrl)
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Selección del objeto
        if (objetoSeleccionado == null && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
            {
                if (hit.collider.CompareTag(tagSeleccionable))
                {
                    AudioSingleton.Instance.PlaySFX(AudioSingleton.Instance.sonidoColocar);
                    objetoSeleccionado = hit.collider.gameObject;

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
