using UnityEngine;

public class EliminarObjetoEstado : IEstado
{
    private Controlador controlador;
    private string tagSeleccionable = "Seleccionable";

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

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.collider.CompareTag(tagSeleccionable))
                {
                    Object.Destroy(hit.collider.gameObject);
                    ctrl.CambiarEstado(new EstadoNeutral());
                }
            }
        }
    }

    public void Salir(Controlador ctrl)
    {
        Menus.Instance.DesactivarTodos();
    }
}
