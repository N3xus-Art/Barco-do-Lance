using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] float velocity;
    bool isMoving, isInteracting, isSecondary, isClicking, isTertiary;
    Vector2 direction;
    SceneController sceneController;

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

            if (activeScene.name == "Cena Navegacao")
            {
                sceneController.LoadScene("Cena Mapa");
                SceneManager.UnloadSceneAsync("Cena Navegacao");
                sceneController.LoadScene("CENA JP", LoadSceneMode.Additive);

            }
            else if (activeScene.name == "Cena Mapa")
            {
                sceneController.LoadScene("Cena Navegacao");
                SceneManager.UnloadSceneAsync("Cena Navegacao");
                sceneController.LoadScene("CENA JP", LoadSceneMode.Additive);

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
