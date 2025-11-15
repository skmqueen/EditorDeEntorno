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

            // Hacer transparente mientras lo posicionas
            Renderer rend = objetoInstanciado.GetComponentInChildren<Renderer>();
            if (rend != null)
            {
                Color c = rend.material.color;
                c.a = 0.3f; // 0 = invisible, 1 = opaco
                rend.material.color = c;
            }
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
            AudioSingleton.Instance.PlayColocar();
            // Poner el objeto en sólido (alpha 1)
            Renderer rend = objetoInstanciado.GetComponentInChildren<Renderer>();
            if (rend != null)
            {
                Color c = rend.material.color;
                c.a = 1f;
                rend.material.color = c;
            }

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
