using JetBrains.Annotations;
using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] float velocity, curItem;
    [SerializeField] bool screenAnimal, changeScene = false;
    [SerializeField] GameManager gameManager;
    SceneController sceneController;
    Vector2 direction;
    public bool isMoving, isInteracting, isSecondary, isClicking, isTertiary;
    public Canvas TelaAjuda;
    public SpawnerHandler spawnerHandler;
    public PlayerHandler playerHandler;
    public ItemUIHandler toolKit;

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
            if (toolKit.cur >= 3 || toolKit.cur <=0){
                toolKit.cur = 1;
            }else{
                toolKit.cur = toolKit.cur + 1;
            }



            Scene activeScene = SceneManager.GetActiveScene();
            sceneController = new SceneController();
            Debug.Log(activeScene.name);
            if(changeScene){
                if (activeScene.name == "Mediterraneo")
                {
                    sceneController.LoadScene("Caribe");
                    SceneManager.UnloadSceneAsync("Mediterraneo");
                    sceneController.LoadScene("CENA JP", LoadSceneMode.Additive);
                }
                else if (activeScene.name == "Caribe")
                {
                    sceneController.LoadScene("Mediterraneo");
                    SceneManager.UnloadSceneAsync("Caribe");
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
            /*if(screenAnimal == true && TelaAjuda != null){
                TelaAjuda.enabled = false;
                screenAnimal = false;
            }else{
                TelaAjuda.enabled = true;
                screenAnimal = true;
            }*/
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
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            
        }
        
    }


    void Start()
    {
       
    }

    void Update()
    {
        if (isMoving)
        {
            if (gameManager.OnBoat){
                transform.position += new Vector3(direction.x, 0, 0) * Time.deltaTime * velocity;
            }else{transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * velocity;}

        }
        
        if(isInteracting){

            

        }

        if (isSecondary)
        {

        }

        if (isTertiary)
        {

        }

        if (isClicking){

            /*if(spawnerHandler.animalLock){
               Destroy(GameObject.Find("Animal(Clone)"));
            }*/

        }

    }


}
