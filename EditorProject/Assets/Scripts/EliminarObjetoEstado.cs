using UnityEngine;

public class EliminarObjetoEstado : IEstado
{
    private Controlador controlador;
    private string tagSeleccionable = "Seleccionable";
    private GameObject objetoSeleccionado;

    [SerializeField]
    private float distanciaMaxima = 100f;

    public EliminarObjetoEstado(Controlador ctrl)
    {
        controlador = ctrl;
    }

    public void Entrar(Controlador ctrl)
    {
        Menus.Instance.MensajeEliminar();
    }

    public void Ejecutar(Controlador ctrl)
    {
        if (Camera.main == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            AudioSingleton.Instance.PlaySFX(AudioSingleton.Instance.sonidoColocar);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
            {
                if (hit.collider.CompareTag(tagSeleccionable))
                {
                    objetoSeleccionado = hit.collider.gameObject;  // guardar objeto
                    Menus.Instance.SiONo(); // mostrar menú confirmar
                }
            }
        }
    }

    public void Destruir()
    {
        if (objetoSeleccionado != null)
        {
            Object.Destroy(objetoSeleccionado); // destruir objeto real
            objetoSeleccionado = null;
            Menus.Instance.DesactivarTodos();
            controlador.CambiarEstado(new EstadoNeutral());
        }
    }

    public void Conservar()
    {
        objetoSeleccionado = null;
        Menus.Instance.DesactivarTodos();
        controlador.CambiarEstado(new EstadoNeutral());
    }

    public void Salir(Controlador ctrl)
    {
        objetoSeleccionado = null;
    }
}

