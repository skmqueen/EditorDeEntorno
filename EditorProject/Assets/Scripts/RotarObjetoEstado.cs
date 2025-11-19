using UnityEngine;

public class RotarObjetoEstado : IEstado
{
    private Controlador controlador;
    private string tagSeleccionable = "Seleccionable";
    private GameObject objetoSeleccionado;

    private LayerMask suelo;           // Igual que en Crear
    private float distanciaMaxima = 100f;

    public RotarObjetoEstado(Controlador ctrl, LayerMask sueloMask)
    {
        controlador = ctrl;
        suelo = sueloMask;            // Guardamos el layer del suelo
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

        // Rotación
        if (objetoSeleccionado != null)
        {
            if (Input.GetMouseButton(0))
            {
                // MISMO RAYCAST QUE EN CREAR → ahora sí funciona
                if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima, suelo))
                {
                    Vector3 targetPosition = hit.point;
                    Vector3 direction = targetPosition - objetoSeleccionado.transform.position;

                    direction.y = 0f;

                    if (direction.sqrMagnitude > 0.0001f)
                    {
                        Quaternion rot = Quaternion.LookRotation(direction);
                        objetoSeleccionado.transform.rotation =
                            Quaternion.Euler(0f, rot.eulerAngles.y, 0f);
                    }

                    Debug.DrawLine(ray.origin, hit.point, Color.green);
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.red);
                }
            }

            // Salir
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

