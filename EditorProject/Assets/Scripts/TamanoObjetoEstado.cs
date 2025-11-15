using UnityEngine;

public class TamanoObjetoEstado : IEstado
{
    private Controlador controlador;
    private string tagSeleccionable = "Seleccionable";
    private GameObject objetoSeleccionado;

    // Factor de escala
    private float xyz = 1.05f;

    public TamanoObjetoEstado(Controlador ctrl)   // <-- El nombre coincide con la clase
    {
        controlador = ctrl;
    }

    public void Entrar(Controlador ctrl)
    {
        //Menus.Instance.MensajeTamano();
    }

    public void Ejecutar(Controlador ctrl)
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Seleccionar objeto
        if (objetoSeleccionado == null && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.collider.CompareTag(tagSeleccionable))
                {
                    AudioSingleton.Instance.PlaySFX(AudioSingleton.Instance.sonidoColocar);
                    objetoSeleccionado = hit.collider.gameObject;
                    Debug.Log("Objeto seleccionado para escalar: " + objetoSeleccionado.name);
                }
            }
        }

        if (objetoSeleccionado != null)
        {
            float scroll = Input.mouseScrollDelta.y;

            if (scroll > 0)
                objetoSeleccionado.transform.localScale *= xyz;

            else if (scroll < 0)
                objetoSeleccionado.transform.localScale /= xyz;

            // Salir con clic derecho
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