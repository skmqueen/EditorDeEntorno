using UnityEngine;

public class CrearObjetoEstado : IEstado
{
    private GameObject objetoPrefab;
    private GameObject objetoInstanciado;
    private LayerMask suelo;
    private float distanciaMaxima = 100f;

    public CrearObjetoEstado(GameObject prefab, LayerMask sueloMask)
    {
        objetoPrefab = prefab;
        suelo = sueloMask;
    }

    public void Entrar(Controlador controlador)
    {
        if (objetoInstanciado == null && objetoPrefab != null)
        {
            objetoInstanciado = GameObject.Instantiate(objetoPrefab);
            objetoInstanciado.layer = 2; // Ignore Raycast mientras lo colocas
        }
    }

    public void Ejecutar(Controlador controlador)
    {
        if (objetoInstanciado == null) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima, suelo))
        {
            Vector3 nuevaPos = hit.point;

            Collider col = objetoInstanciado.GetComponent<Collider>();
            if (col != null)
            {
                // Coloca el prefab exactamente sobre el suelo según su collider
                nuevaPos.y += col.bounds.extents.y;
            }

            objetoInstanciado.transform.position = nuevaPos;

            Debug.DrawLine(ray.origin, hit.point, Color.green);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.red);
        }

        if (Input.GetMouseButtonDown(0))
        {
            objetoInstanciado.layer = 0; // Default
            controlador.CambiarEstado(new EstadoNeutral());
        }
    }

    public void Salir(Controlador controlador)
    {
        if (Menus.Instance != null)
            Menus.Instance.DesactivarTodos();
    }
}
