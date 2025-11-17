using UnityEngine;

public class Controlador : MonoBehaviour
{
    public GameObject[] objetosPrefabs;
    public LayerMask sueloMask;
    public LayerMask seleccionableMask; // solo prefabs que se pueden mover/rotar/eliminar

    private IEstado estadoActual;
    private GameObject prefabSeleccionado;
    
    [SerializeField]
    Grid grid;

    void Start()
    {
        CambiarEstado(new EstadoNeutral());
        AudioSingleton.Instance.PlayMusic();
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
    
    public void PulsarMover()
    {
        CambiarEstado(new MoverObjetoEstado(this, sueloMask));
    }
    public void PulsarRotar()
    {
        CambiarEstado(new RotarObjetoEstado(this));
    }
    public void PulsarTamano()
    {
        CambiarEstado(new TamanoObjetoEstado(this));
    }
    public void PulsarEliminar()
    {
        CambiarEstado(new EliminarObjetoEstado(this));
    }
}
