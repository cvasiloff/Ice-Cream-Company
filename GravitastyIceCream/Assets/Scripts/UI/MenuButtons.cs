using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem.Sample
{
    public class MenuButtons : MonoBehaviour
    {
        public HoverButton beginButton;
        public HoverButton quitButton;
        public HoverButton nextButton;

        public GameObject playerTemp;
        public GameObject playerSpawn;

        private void Start()
        {
            beginButton.onButtonDown.AddListener(OnBeginDown);
            quitButton.onButtonDown.AddListener(OnQuitDown);
        }

        private void OnBeginDown(Hand hand)
        {
            Debug.Log("Transition to game");
            //GameObject.FindObjectOfType<mySceneController>().switchScenes(1);
            // start game and game timer
            ScoopSpawner spawner = FindObjectOfType<ScoopSpawner>();

            


            if (!spawner.GameStarted)
            {
                spawner.StartGame();
            }
                


        }

        private void OnQuitDown(Hand hand)
        {
            Debug.Log("QUITTING THE GAME");
            Application.Quit();
        }




    }

}

