// Copyright (c) 2015 Bartlomiej Wolk (bartlomiejwolk@gmail.com)
//  
// This file is part of the InstantiateUIPrefab module for Unity.
// Licensed under the MIT license. See LICENSE file in the project root folder.

using Rotorz.ReorderableList;
using UnityEditor;
using UnityEngine;

namespace InstantiateUIPrefabModule.InstantiateUIPrefabComponent {

    [CustomEditor(typeof(InstantiateUIPrefab))]
    [CanEditMultipleObjects]
    public sealed class InstantiateUIPrefabEditor : Editor {
        #region FIELDS

        private InstantiateUIPrefab Script { get; set; }

        #endregion FIELDS

        #region SERIALIZED PROPERTIES

        private SerializedProperty description;
        private SerializedProperty templatePrefab;
        private SerializedProperty parentGameObject;
        private SerializedProperty guiTextElements;
        private SerializedProperty editorToolsFoldout;
        private SerializedProperty testTextValues;
        private SerializedProperty verticalOffset;

        #endregion SERIALIZED PROPERTIES

        #region UNITY MESSAGES

        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawVersionLabel();
            DrawDescriptionTextArea();

            EditorGUILayout.Space();

            DrawTemplatePrefabField();
            DrawParentGameObjectField();
            DrawVerticalOffsetField();

            EditorGUILayout.Space();

            DrawGUITextElementsList();

            EditorGUILayout.Space();

            DrawEditorToolsFoldout();

            serializedObject.ApplyModifiedProperties();
        }
        private void OnEnable() {
            Script = (InstantiateUIPrefab)target;

            description = serializedObject.FindProperty("description");
            templatePrefab =
                serializedObject.FindProperty("templatePrefab");
            parentGameObject =
                serializedObject.FindProperty("parentGameObject");
            guiTextElements = serializedObject.FindProperty("guiTextElements");
            editorToolsFoldout =
                serializedObject.FindProperty("editorToolsFoldout");
            testTextValues = serializedObject.FindProperty("testTextValues");
            verticalOffset = serializedObject.FindProperty("verticalOffset");
        }

        #endregion UNITY MESSAGES

        #region INSPECTOR CONTROLS
        private void DrawVerticalOffsetField() {
            EditorGUILayout.PropertyField(
                verticalOffset,
                new GUIContent(
                    "Vertical Offset",
                    "How much to offset prefab's vertical position against the previous one."));
        }

        private void DrawEditorToolsFoldout() {
            editorToolsFoldout.boolValue = EditorGUILayout.Foldout(
                editorToolsFoldout.boolValue,
                "InstantiateAndUpdateUIPrefab(string[])");

            if (editorToolsFoldout.boolValue) {
                DrawTestTextValuesList();
                DrawInstantiateAndUpdateButton();
                DrawRemoveAllChildren();
            }
        }

        private void DrawRemoveAllChildren() {
            var btnPressed = GUILayout.Button("RemoveAllChildren()");

            if (btnPressed) {
                Script.RemoveAllChildren();
            }
        }

        private void DrawTestTextValuesList() {
            // todo add tooltip
            ReorderableListGUI.Title("Test Text Values");
            ReorderableListGUI.ListField(testTextValues);
        }

        private void DrawParentGameObjectField() {
            EditorGUILayout.PropertyField(
                parentGameObject,
                new GUIContent(
                    "Parent Game Object",
                    "Game object used as a parent for the template prefab."));
        }

        private void DrawTemplatePrefabField() {
            EditorGUILayout.PropertyField(
                templatePrefab,
                new GUIContent(
                    "Template Prefab",
                    "Prefab to instantiate."));
        }

        private void DrawGUITextElementsList() {
            ReorderableListGUI.Title(new GUIContent(
                "GUI Text Elements",
                "Drag here template prefab's children. Each should have its " +
                "own UI Text component attached."));
            ReorderableListGUI.ListField(guiTextElements);
        }

        private void DrawInstantiateAndUpdateButton() {
            var btnPressed = GUILayout.Button("InstantiateAndUpdateUIPrefab()");

            if (btnPressed) {
                Script.InstantiateAndUpdateUIPrefab(
                    Script.TestTextValues.ToArray());
            }
        }


        private void DrawVersionLabel() {
            var labelText = string.Format(
                "{0} ({1})",
                InstantiateUIPrefab.Version,
                InstantiateUIPrefab.Extension);

            var moduleDescription = "";

            EditorGUILayout.LabelField(
                new GUIContent(
                    labelText,
                    moduleDescription));
        }

        private void DrawDescriptionTextArea() {
            description.stringValue = EditorGUILayout.TextArea(
                description.stringValue);
        }

        #endregion INSPECTOR

        #region METHODS

        //[MenuItem("Component/Modules/InstantiateUIPrefab/InstantiateUIPrefab")]
        //private static void AddLapTimeGUIComponentToGameObject() {
        //    if (Selection.activeGameObject != null) {
        //        Selection.activeGameObject.AddComponent(typeof(InstantiateUIPrefab));
        //    }
        //}

        #endregion METHODS
    }

}