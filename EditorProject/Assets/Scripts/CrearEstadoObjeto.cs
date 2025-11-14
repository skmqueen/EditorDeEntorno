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
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Raycast contra el suelo
        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima, suelo))
        {
            // Coloca el objeto directamente sobre el suelo
            objetoInstanciado.transform.position = hit.point;

            Debug.DrawLine(ray.origin, hit.point, Color.green);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.red);
        }

        // Confirmar colocación con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            objetoInstanciado.layer = 0; // Default
            controlador.CambiarEstado(new EstadoNeutral());
        }

        // Cancelar con clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            GameObject.Destroy(objetoInstanciado);
            objetoInstanciado = null;
            controlador.CambiarEstado(new EstadoNeutral());
        }
    }

    public void Salir(Controlador controlador)
    {
        if (Menus.Instance != null)
            Menus.Instance.DesactivarTodos();
        objetoInstanciado = null;
    }
}


