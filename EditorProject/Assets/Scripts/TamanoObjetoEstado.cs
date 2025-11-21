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
                    objetoSeleccionado = hit.collider.gameObject;

                /*
                       Vector3 mousePosition = Input.mousePosition;
                       Vector3 targetPosition = hit.point;
                       Vector3 direction = targetPosition - mousePosition;


                       if (objetoSeleccionado != null)
                       {
                           if (objetoSeleccionado.transform.localScale.x == 1.0f)
                           {
                               objetoSeleccionado.transform.localScale = xyz * mousePosition;
                           }
                        float valorMinimo = 1.0f;
                        if (objetoSeleccionado.transform.localScale.x >= valorMinimo)
                        {
                            return;
                        }
                        float valorMaximo = 15.0f;
                        if (objetoSeleccionado.transform.localScale.x >= valorMaximo)
                        {
                            return;
                        }


                }
                   }
                
                */

                 if (objetoSeleccionado != null)
                {
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
    }
    // Aumentar tamaño
    public void Aumentar()
    {
        if (objetoSeleccionado != null)
            objetoSeleccionado.transform.localScale *= xyz;
        float valorMaximo = 10.0f;
        if (objetoSeleccionado.transform.localScale.x >= valorMaximo)
        {
            objetoSeleccionado.transform.localScale = new Vector3(valorMaximo, valorMaximo, valorMaximo);
        }
    }

    // Disminuir tamaño
    public void Disminuir()
    {
        if (objetoSeleccionado != null)
            objetoSeleccionado.transform.localScale /= xyz;
        float valorMinimo = 1.0f;
        if (objetoSeleccionado.transform.localScale.x <= valorMinimo)
        {
            objetoSeleccionado.transform.localScale = new Vector3(valorMinimo, valorMinimo, valorMinimo);
        }
    }

    public void Salir(Controlador ctrl)
    {
        objetoSeleccionado = null;
    }
}
