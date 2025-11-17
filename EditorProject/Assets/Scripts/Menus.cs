using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    public static Menus Instance { get; private set; }

    public GameObject menuPrincipal;
    public GameObject menuLateral;
    public GameObject mensajeCrear;
    public GameObject mensajeMover;
    public GameObject mensajeEliminar;
    public GameObject mensajeRotar;
    public GameObject mensajeTamano;

    // Arrays para animaciones
    public Button[] botonesMenuPrincipal;
    public Button[] botonesMenuLateral;
    public Image[] imagenesMensajes;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Pop in inicial del menú principal al arrancar el juego
        
        //AnimarBotones(botonesMenuPrincipal);
    }
     void Start()
    {
        menuPrincipal.SetActive(true);
        menuLateral.SetActive(false);
        mensajeCrear.SetActive(false);
        mensajeEliminar.SetActive(false);
        mensajeMover.SetActive(false);
        mensajeRotar.SetActive(false);
        AnimarBotones(botonesMenuPrincipal);
    }

    public void DesactivarTodos()
    {
        menuPrincipal.SetActive(true);
        menuLateral.SetActive(false);
        mensajeCrear.SetActive(false);
        mensajeMover.SetActive(false);
        mensajeEliminar.SetActive(false);
        mensajeRotar.SetActive(false);
        mensajeTamano.SetActive(false);
        AnimarBotones(botonesMenuPrincipal);
    }

    //Animaciones
    private void AnimarBotones(Button[] btns)
    {
        for (int i = 0; i < btns.Length; i++)
        {
            RectTransform rt = btns[i].GetComponent<RectTransform>();
            rt.localScale = Vector3.zero;
            LeanTween.scale(rt, Vector3.one, 0.4f)
                .setEaseOutBack()
                .setDelay(i * 0.1f); // efecto cascada
        }
    }

    private void PopInImagen(Image img)
    {
        RectTransform rt = img.GetComponent<RectTransform>();
        rt.localScale = Vector3.zero;
        LeanTween.scale(rt, Vector3.one, 0.4f).setEaseOutBack();
    }

    private void PopOutImagen(Image img)
    {
        RectTransform rt = img.GetComponent<RectTransform>();
        LeanTween.scale(rt, Vector3.zero, 0.3f).setEaseInBack()
            .setOnComplete(() => img.gameObject.SetActive(false));
    }

    //Funciones

    public void PulsarCrear()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        menuLateral.SetActive(true);

        AnimarBotones(botonesMenuLateral);
    }

    public void MensajeCrear()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        mensajeCrear.SetActive(true);

        foreach (var img in imagenesMensajes)
        {
            img.gameObject.SetActive(true);
            PopInImagen(img);
        }
    }

    public void MensajeMover()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        mensajeMover.SetActive(true);

        foreach (var img in imagenesMensajes)
        {
            img.gameObject.SetActive(true);
            PopInImagen(img);
        }
    }

    public void MensajeEliminar()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        mensajeEliminar.SetActive(true);

        foreach (var img in imagenesMensajes)
        {
            img.gameObject.SetActive(true);
            PopInImagen(img);
        }
    }

    public void MensajeRotar()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        mensajeRotar.SetActive(true);

        foreach (var img in imagenesMensajes)
        {
            img.gameObject.SetActive(true);
            PopInImagen(img);
        }
    }

    public void MensajeTamano()
    {
        DesactivarTodos();
        menuPrincipal.SetActive(false);
        mensajeTamano.SetActive(true);
        
        foreach (var img in imagenesMensajes)
        {
            img.gameObject.SetActive(true);
            PopInImagen(img);
        }
    }
}