using UnityEngine;

public class Controlador : MonoBehaviour
{
    //Agregamos todos esos elementos importantes en el videojuego
    public GameObject[] objetosPrefabs;
    public LayerMask sueloMask;
    public LayerMask seleccionableMask; // solo prefabs que se pueden mover/rotar/eliminar

    private IEstado estadoActual;
    private GameObject prefabSeleccionado;
    
    [SerializeField]
    Grid grid;
    [SerializeField] 
    public GameObject gridVisual;


    void Start()
    {
        CambiarEstado(new EstadoNeutral());
        AudioSingleton.Instance.PlayMusic();
    }

    void Update()
    {
        //Si un estado está activo, se ejecuta
        if (estadoActual != null)
            estadoActual.Ejecutar(this);
    }

    public void CambiarEstado(IEstado nuevoEstado)
    {
        //Al empezar la función, si hay estado activo se sale y se abre el nuevo estado
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
        CambiarEstado(new RotarObjetoEstado(this, sueloMask));
    }
    public void PulsarTamano()
    {
        CambiarEstado(new TamanoObjetoEstado(this));
    }
    public void PulsarEliminar()
    {
        CambiarEstado(new EliminarObjetoEstado(this));
    }
    public void AumentarTamano()
    {
        if (estadoActual is TamanoObjetoEstado t)
            t.Aumentar();
    }
    public void DisminuirTamano()
    {
        if (estadoActual is TamanoObjetoEstado t)
            t.Disminuir();
    }
    public void SalirAlEstadoNeutral()
    {
        CambiarEstado(new EstadoNeutral());
        Menus.Instance.DesactivarTodos();
    }
    public void DestruirObjeto()
    {
        if(estadoActual is EliminarObjetoEstado e)
            e.Destruir();

    }
    public void ConservarObjeto()
    {
        if(estadoActual is EliminarObjetoEstado e)
            e.Conservar();

    }

}
