using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class LevelOne
    {
        private const string TargetScene = "Level1";

        public static string[] ExpectedGameElements =
        {
            "Desert Sands",
            "Ground",
            "Player Spawner",
            "Spice Spawner",
            "Sandworm (Player)",
            "Score Keeper"
        };

        private List<GameObject> FindGameObjectInRoot(Scene scene, Predicate<GameObject> filter)
        {
            var foundGameObjects = new List<GameObject>();

            foreach (var gameObject in scene.GetRootGameObjects())
                if (filter(gameObject))
                    foundGameObjects.Add(gameObject);

            return foundGameObjects;
        }

        private GameObject FindGameObjectByName(string name) => GameObject.Find(name);

        private IEnumerator LoadTargetScene(string targetSceneName)
        {
            var currentScene = SceneManager.GetActiveScene();
            var sceneAsync = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);

            while (sceneAsync.isDone == false) yield return null;
            if (currentScene.name == targetSceneName)
            {
                var sceneAsyncUnload = SceneManager.UnloadSceneAsync(currentScene);
                while (sceneAsyncUnload.isDone == false) yield return null;
            }
        }

        [UnityTest]
        public IEnumerator TheLevelFeaturesTheseElements(
            [ValueSource(nameof(ExpectedGameElements))]
            string gameObjectName
        )
        {
            yield return LoadTargetScene(TargetScene);

            var sut = FindGameObjectByName(gameObjectName);

            Assert.NotNull(sut);
        }
    }
}