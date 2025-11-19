using UnityEngine;

public class CrearObjetoEstado : IEstado
{
    private GameObject objetoPrefab;           // Prefab final que se colocará
    private GameObject objetoInstanciado;      // Objeto que se mueve con el mouse
    private LayerMask suelo;
    private float distanciaMaxima = 100f;

    // --------- NUEVO: Prefab fantasma para previsualización ----------
    // private GameObject prefabFantasma;      // Prefab de previsualización (transparente)
    // private GameObject objetoFantasmaInstanciado; // Instancia del fantasma
    // -----------------------------------------------------------------

    public CrearObjetoEstado(GameObject prefab, LayerMask sueloMask /*, GameObject fantasmaPrefab = null*/)
    {
        objetoPrefab = prefab;
        suelo = sueloMask;

        // --------- NUEVO: Asignar prefab fantasma ----------
        // prefabFantasma = fantasmaPrefab;
        // ----------------------------------------------------
    }

    public void Entrar(Controlador controlador)
    {
        if (objetoInstanciado == null && objetoPrefab != null)
        {
            // Instanciamos el prefab final (podrías instanciar solo el fantasma aquí)
            objetoInstanciado = GameObject.Instantiate(objetoPrefab);
            objetoInstanciado.layer = 2; // Ignore Raycast mientras se instancia

            // Instanciar prefab fantasma en lugar del objeto final 
            /*
            if (prefabFantasma != null)
            {
                objetoFantasmaInstanciado = GameObject.Instantiate(prefabFantasma);
                objetoFantasmaInstanciado.layer = 2; // Ignore Raycast
            }
            else
            {
                objetoFantasmaInstanciado = objetoInstanciado; // Si no hay fantasma, usamos el final
            }
            */
        }

        //MOSTRAR GRID VISUAL
        if (controlador.gridVisual != null)
            controlador.gridVisual.SetActive(true);

    }

    public void Ejecutar(Controlador controlador)
    {
        if (objetoInstanciado == null) return;
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima, suelo))
        {
            Vector3 posicion = hit.point;

            /*
            // Tamaño de la celda
            float tamanoCelda = 1f; // Ajusta según el tamaño de tu grid visual

            // Referencia al plano visual del grid
            Transform plano = controlador.gridVisual.transform;

            // Convertir la posición del hit a coordenadas locales del plano visual
            Vector3 localHit = plano.InverseTransformPoint(posicion);

            // Centrar en la celda usando Floor
            localHit.x = Mathf.Floor(localHit.x / tamanoCelda) * tamanoCelda + tamanoCelda / 2f;
            localHit.z = Mathf.Floor(localHit.z / tamanoCelda) * tamanoCelda + tamanoCelda / 2f;

            // Convertir de nuevo a coordenadas globales
            posicion = plano.TransformPoint(localHit);

            // Mantener altura del suelo
            posicion.y = hit.point.y;

            // Asignar la posición centrada al objeto/fantasma
            objetoInstanciado.transform.position = posicion;
            // Si estuviera usando fantasma:
            // objetoFantasmaInstanciado.transform.position = posicion;
            */

            // Sin modificaciones, solo colocamos en hit.point
            objetoInstanciado.transform.position = hit.point;
        }

        // Confirmar colocación con clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            AudioSingleton.Instance.PlayColocar();
            objetoInstanciado.layer = 0; // Default

            //Reemplazar prefab fantasma
            /*
            if (objetoFantasmaInstanciado != null && objetoFantasmaInstanciado != objetoInstanciado)
            {
                Vector3 posicionFinal = objetoFantasmaInstanciado.transform.position;
                GameObject.Destroy(objetoFantasmaInstanciado);
                objetoInstanciado = GameObject.Instantiate(objetoPrefab);
                objetoInstanciado.transform.position = posicionFinal;
            }
            */

            controlador.CambiarEstado(new EstadoNeutral());
        }

        // Cancelar con clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            GameObject.Destroy(objetoInstanciado);
            objetoInstanciado = null;

            // Destruir fantasma 
            // if (objetoFantasmaInstanciado != null) GameObject.Destroy(objetoFantasmaInstanciado);
        

            controlador.CambiarEstado(new EstadoNeutral());
        }
    }

    public void Salir(Controlador controlador)
    {
        if (Menus.Instance != null)
            Menus.Instance.DesactivarTodos();

        objetoInstanciado = null;

        //OCULTAR GRID VISUAL 
        if (controlador.gridVisual != null)
            controlador.gridVisual.SetActive(false);

    }
}
