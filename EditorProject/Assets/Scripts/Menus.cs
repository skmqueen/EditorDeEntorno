using UnityEngine;

public class Menus : MonoBehaviour
{
    public static Menus Instance { get; private set; }
   
    public GameObject menuPrincipal;
    public GameObject menuLateral;
    public GameObject mensajeCrear;
    public GameObject mensajeMover;
    public GameObject mensajeEliminar;
    public GameObject mensajeRotar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void DesactivarTodos()
    {
        menuPrincipal.SetActive(true);
        menuLateral.SetActive(false);
        mensajeCrear.SetActive(false);
        mensajeMover.SetActive(false);
        mensajeEliminar.SetActive(false);
        mensajeRotar.SetActive(false);
    }

    public void PulsarCrear()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        menuLateral.SetActive(true);
    }

    public void MensajeCrear()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        mensajeCrear.SetActive(true);
    }

    public void MensajeMover()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        mensajeMover.SetActive(true);
    }

    public void MensajeEliminar()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        mensajeEliminar.SetActive(true);
    }

    public void MensajeRotar()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        mensajeRotar.SetActive(true);
    }
}
