using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManagerMenuu : MonoBehaviour
{
    public GameObject Player;
    public static bool JuegoPausado = false;
    public GameObject MenuPausaUI;
    public GameObject PlayerUI;
    public GameObject MenuOpciones;
    public bool OpAct = false;

    // Update is called once per frame
    private void Start()
    {
        Player = GameObject.Find("ControladorDeArmas");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (JuegoPausado)
            {
                Continuar();
            }
            else
            {
                Pausa();
            }
        }

    }

    void Pausa()
    {
        MenuPausaUI.SetActive(true);
        PlayerUI.SetActive(false);
        Time.timeScale = 0f;
        JuegoPausado = true;
        Player.SetActive(false);
        Cursor.visible = true;
    }

    public void Continuar()
    {
        MenuPausaUI.SetActive(false);
        PlayerUI.SetActive(true);
        Time.timeScale = 1f;
        JuegoPausado = false;
        Player.SetActive(true);

    }

    public void CargarMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Cargando Menú...");
        SceneManager.LoadScene("Main");
        JuegoPausado = false;
        Cursor.visible = true;
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del Juego...");
        Application.Quit();
    }

    void Opciones()
    {
        Cursor.visible = true;
    }

}
