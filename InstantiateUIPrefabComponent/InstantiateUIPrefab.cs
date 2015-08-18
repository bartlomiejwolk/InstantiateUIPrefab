// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
//  
// This file is part of the InstantiateUIPrefab module for Unity.
// Licensed under the MIT license. See LICENSE file in the project root folder.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InstantiateUIPrefabModule.InstantiateUIPrefabComponent {

    public sealed class InstantiateUIPrefab : MonoBehaviour {

        #region CONSTANTS

        public const string Version = "v0.1.0";
        public const string Extension = "InstantiateUIPrefab";

        #endregion CONSTANTS

        #region DELEGATES
        #endregion DELEGATES

        #region EVENTS
        #endregion EVENTS

        #region FIELDS

#pragma warning disable 0414
        /// <summary>
        ///     Allows identify component in the scene file when reading it with
        ///     text editor.
        /// </summary>
        [SerializeField]
        private string componentName = "InstantiateUIPrefab";
#pragma warning restore 0414

        //[SerializeField]
        //private List<GameObject> instantiatedPrefabs;

            #endregion FIELDS

        #region INSPECTOR FIELDS

        [SerializeField]
        private string description = "Description";

        /// <summary>
        /// Game object that will be instantiated and updated with new data.
        /// </summary>
        [SerializeField]
        private GameObject templatePrefab;

        /// <summary>
        /// Game object to be parent for the new instance of the template
        /// prefab.
        /// </summary>
        // todo should be Transform
        // todo create properties
        [SerializeField]
        private GameObject parentGameObject;

        /// <summary>
        /// Game object names of the UI text elements to be updated with new
        /// data.
        /// </summary>
        [SerializeField]
        private List<GameObject> guiTextElements;

        [SerializeField]
        private bool editorToolsFoldout;

        /// <summary>
        /// Values used to test the component functionality within the editor.
        /// </summary>
        [SerializeField]
        private List<string> testTextValues;

        /// <summary>
        /// Value used to offset vertical position of each instantiated prefab.
        /// </summary>
        [SerializeField]
        private float verticalOffset = 30;

        #endregion INSPECTOR FIELDS

        #region PROPERTIES

        /// <summary>
        ///     Optional text to describe purpose of this instance of the component.
        /// </summary>
        public string Description {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Values used to test the component functionality within the editor.
        /// </summary>
        public List<string> TestTextValues {
            get { return testTextValues; }
            set { testTextValues = value; }
        }

        #endregion PROPERTIES

        #region UNITY MESSAGES
        #endregion UNITY MESSAGES

        #region EVENT INVOCATORS
        #endregion INVOCATORS

        #region EVENT HANDLERS
        #endregion EVENT HANDLERS

        #region METHODS

        public void InstantiateAndUpdateUIPrefab(params string[] args) {
            var instantiatedPrefab = InstantiateTemplatePrefab();
            UpdateInstantiatedPrefabValues(instantiatedPrefab, args);
        }

        /// <summary>
        /// Updates UI text field of the instantiated prefab.
        /// </summary>
        /// <param name="instantiatedPrefab"></param>
        /// <param name="textValues"></param>
        private void UpdateInstantiatedPrefabValues(
            GameObject instantiatedPrefab,
            string[] textValues) {

            // For each text element in the template prefab..
            for (int i = 0; i < guiTextElements.Count; i++) {
                // Guard against insufficient number of text values.
                if (i > textValues.Length - 1) {
                    break;
                }

                // Get name of the game object to update.
                var gameObjectName = guiTextElements[i].name;

                // Get child GO by name.
                var childToUpdate =
                    instantiatedPrefab.transform.FindChild(gameObjectName);

                // Find Text component.
                var textComponent = childToUpdate.GetComponentInChildren<Text>();

                // Update UI text.
                textComponent.text = textValues[i];
            }
        }

        private GameObject InstantiateTemplatePrefab() {
            // Instantiate prefab.
            var instantiatedPrefab = Instantiate(
                templatePrefab);

            // Cache transform.
            var instantiatedPrefabTransform = instantiatedPrefab.transform;

            // Calculate vertical position offset.
            var verticalPositionOffset = GetNewVerticalPositionOffset();

            // Calculate new vertical position.
            var newVerticalPosition =
                instantiatedPrefabTransform.position.y - verticalPositionOffset;

            var newPositionVector = new Vector3(
                instantiatedPrefabTransform.position.x,
                newVerticalPosition,
                instantiatedPrefabTransform.position.z);

            // Apply vertical position offset.
            instantiatedPrefab.transform.localPosition = newPositionVector;

            // Parent GO.
            instantiatedPrefab.transform.SetParent(parentGameObject.transform, false);
            return instantiatedPrefab;
        }

        private float GetNewVerticalPositionOffset() {
            // Get number of instantiated prefabs.
            var instantiatedPrefabsNo = parentGameObject.transform.childCount;

            if (instantiatedPrefabsNo == 0) {
                return 0;
            }

            var offset = instantiatedPrefabsNo * verticalOffset;

            return offset;
        }

        public void RemoveAllChildren() {
            var children = new List<GameObject>();

            // Get children.
            foreach (Transform child in parentGameObject.transform) {
                children.Add(child.gameObject);
            }

            // Destroy children.
            if (Application.isPlaying) {
                children.ForEach(Destroy);
            }
            else {
                children.ForEach(DestroyImmediate);
            }
        }

        #endregion METHODS

    }

}