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
        menuPrincipal.SetActive(false);
        menuLateral.SetActive(false);
        mensajeCrear.SetActive(false);
        mensajeMover.SetActive(false);
        mensajeEliminar.SetActive(false);
        mensajeRotar.SetActive(false);
    }

    public void PulsarCrear()
    {
        DesactivarTodos();
        LeanTweenController.Instance.AnimarEntrada(menuLateral);
    }

    public void MensajeCrear()
    {
        DesactivarTodos();
        LeanTweenController.Instance.AnimarEntrada(mensajeCrear);
    }

    public void MensajeMover()
    {
        DesactivarTodos();
        LeanTweenController.Instance.AnimarEntrada(mensajeMover);
    }

    public void MensajeEliminar()
    {
        DesactivarTodos();
        LeanTweenController.Instance.AnimarEntrada(mensajeEliminar);
    }

    public void MensajeRotar()
    {
        DesactivarTodos();
        LeanTweenController.Instance.AnimarEntrada(mensajeRotar);
    }

    public void VolverMenuPrincipal()
    {
        DesactivarTodos();
        LeanTweenController.Instance.AnimarEntrada(menuPrincipal);
    }
}
