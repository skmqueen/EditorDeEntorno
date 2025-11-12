using UnityEngine;

public class Controlador : MonoBehaviour
{
    public GameObject[] objetosPrefabs;
    public LayerMask sueloMask;

    private IEstado estadoActual;
    private GameObject prefabSeleccionado;

    void Start()
    {
        CambiarEstado(new EstadoNeutral());
    }

    void Update()
    {
        if (estadoActual != null)
            estadoActual.Ejecutar(this);
    }

    public void CambiarEstado(IEstado nuevoEstado)
    {
        if (estadoActual != null)
            estadoActual.Salir(this);

        estadoActual = nuevoEstado;
        estadoActual.Entrar(this);
    }

    public void SeleccionarPrefab(int indice)
    {
        if (indice < 0 || indice >= objetosPrefabs.Length) return;
        
        prefabSeleccionado = objetosPrefabs[indice];
        CambiarEstado(new CrearObjetoEstado(prefabSeleccionado, sueloMask));
    }

    public GameObject ObtenerPrefabSeleccionado()
    {
        return prefabSeleccionado;
    }
}
