using JetBrains.Annotations;
using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] float velocity, curItem;
    [SerializeField] bool screenAnimal, changeScene = false;
    bool isMoving, isInteracting, isSecondary, isClicking, isTertiary;
    Vector2 direction;
    SceneController sceneController;
    public Canvas TelaAjuda;
    
    public Canvas Cut, Web, Treat;

    //Classe Move que contém a movimentação do jogador
    public void Move(InputAction.CallbackContext context){
        
        if(context.phase == InputActionPhase.Started){
           
        }else if(context.phase == InputActionPhase.Performed){
            isMoving = true;
            direction = context.ReadValue<Vector2>();
            
        
        }else if(context.phase == InputActionPhase.Canceled){
            isMoving = false;       
        
        }
    }

    public void Interact(InputAction.CallbackContext context){

        if(context.phase == InputActionPhase.Started){
           
        }else if(context.phase == InputActionPhase.Performed){
            isInteracting = true;
            
        }else if(context.phase == InputActionPhase.Canceled){
            isInteracting = false;       
        }
    }

    public void Secondary(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {

        }
        else if (context.phase == InputActionPhase.Performed)
        {
            isSecondary = true;

        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isSecondary = false;
        }
    }

    public void Tertiary(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {

            Scene activeScene = SceneManager.GetActiveScene();
            sceneController = new SceneController();

            if(curItem >= 2){
                curItem = 0;
            }else if(curItem <= -1){
                curItem = 0;
            }else{
                curItem++;
            }

            Debug.Log(activeScene.name);

            if(changeScene){
                if (activeScene.name == "Cena teste Navegacao")
                {
                    sceneController.LoadScene("Cena Mapa Teste");
                    SceneManager.UnloadSceneAsync("Cena teste Navegacao");
                    sceneController.LoadScene("CENA JP", LoadSceneMode.Additive);
                }
                else if (activeScene.name == "Cena Mapa Teste")
                {
                    sceneController.LoadScene("Cena teste Navegacao");
                    SceneManager.UnloadSceneAsync("Cena Mapa Teste");
                    sceneController.LoadScene("CENA JP", LoadSceneMode.Additive);

                }
            }
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            isTertiary = true;

        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isTertiary = false;
        }
    }

    public void Click(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Started)
        {

        }
        else if (context.phase == InputActionPhase.Performed)
        {
            isClicking = true;

        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isClicking = false;
        }
    }

    public void Animal(InputAction.CallbackContext context){

        if (context.phase == InputActionPhase.Started)
        {

        }
        else if (context.phase == InputActionPhase.Performed)
        {
            if(screenAnimal == true && TelaAjuda != null){
                TelaAjuda.enabled = false;
                screenAnimal = false;
            }else{
                TelaAjuda.enabled = true;
                screenAnimal = true;
            }

        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            
        }
    }

    public void Item(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Started)
        {

        }
        else if (context.phase == InputActionPhase.Performed)
        {
           switch(curItem){
            case 0:
                Debug.Log("Caso 0");
                Cut.enabled = false;
                Web.enabled = false;
                Treat.enabled = true;
                break;
            case 1:
                Debug.Log("Caso 1");
                Cut.enabled = true;
                Web.enabled = false;
                Treat.enabled = false;
                break;
            case 2:
                Debug.Log("Caso 2");
                Cut.enabled = false;
                Web.enabled = true;
                Treat.enabled = false;
                break;
           }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            
        }
        
    }


    void Start()
    {
        TelaAjuda.enabled = false;
        Cut.enabled = false;
        Web.enabled = true;
        Treat.enabled = false;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * velocity;
        }
        
        if(isInteracting){
            Debug.Log("Foi");
        }

        if (isSecondary)
        {
            Debug.Log("Foi denovo");

        }

        if (isTertiary)
        {
            Debug.Log("IMPOSSIVEL");

        }

        if (isClicking)
        {
            Debug.Log("Você não vai acreditar");

        }

    }


}
