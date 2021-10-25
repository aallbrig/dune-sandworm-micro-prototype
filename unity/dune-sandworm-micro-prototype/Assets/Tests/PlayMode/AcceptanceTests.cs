using System;
using System.Collections;
using System.Collections.Generic;
using Behaviors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class LevelOne
    {
        private const string TargetScene = "Level1";

        private List<GameObject> FindGameObjectInRoot(Scene scene, Predicate<GameObject> filter)
        {
            var foundGameObjects = new List<GameObject>();

            foreach (var gameObject in scene.GetRootGameObjects())
            {
                if (filter(gameObject))
                {
                    foundGameObjects.Add(gameObject);
                }
            }

            return foundGameObjects;
        }
        private GameObject FindGameObjectByName(string name) => GameObject.Find(name);

        private IEnumerator LoadTargetScene(string targetSceneName)
        {
            var sceneAsync = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);

            while (sceneAsync.isDone == false) yield return null;
        }

        public static string[] ExpectedGameElements = {
            "Desert Sands",
            "Ground",
            "Sandworm (Player)"
        };

        [UnityTest]
        public IEnumerator TheLevelFeaturesTheseElements(
            [ValueSource(nameof(ExpectedGameElements))] string gameObjectName
        )
        {
            yield return LoadTargetScene(TargetScene);

            var sut = FindGameObjectByName(gameObjectName);

            Assert.NotNull(sut);
        }

        // [UnityTest]
        private IEnumerator ASandwormIsOnScreen()
        {
            yield return LoadTargetScene(TargetScene);
            var scene = SceneManager.GetActiveScene();

            var sut = FindGameObjectInRoot(scene, (gameObject => gameObject.name == "Sandworm (Player)"))[0];

            Assert.NotNull(sut);
        }

        // [UnityTest]
        private IEnumerator AnEdibleObjectIsOnScreen()
        {
            yield return LoadTargetScene(TargetScene);
            var scene = SceneManager.GetActiveScene();

            var sut = FindGameObjectInRoot(scene, (gameObject => gameObject.GetComponent<IAmEdible>() != null))[0];

            Assert.NotNull(sut);
        }

        // [UnityTest]
        private IEnumerator TheSandwormCanEatEdibleObjects()
        {
            yield return LoadTargetScene(TargetScene);
            var scene = SceneManager.GetActiveScene();
            var sandworm = FindGameObjectInRoot(scene, (gameObject => gameObject.name == "Sandworm (Player)"))[0];
            var edibleObject = FindGameObjectInRoot(scene, (gameObject => gameObject.GetComponent<IAmEdible>() != null))[0];
        }
    }
}