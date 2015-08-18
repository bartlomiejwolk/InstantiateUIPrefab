# InstantiateUIPrefab

*InstantiateUIPrefab* extension for Unity.

Licensed under MIT license. See LICENSE file in the project root folder.

![InstantiateUIPrefab](/Resources/cover_screenshot.png?raw=true)
![](/Resources/in-game_picture.png?raw=true)

## Features

* Easily instantiate and update new UI prefabs.
* Test component's functionality in the editor.

## Quick Start

1. Add _InstantiateUIPrefab_ component to any game object on the scene.
2. Fill _Template Prefab_ field. Template prefab should contain child game objects. Each child should have a UI Text component attached.
3. Fill _Parent Game Object_ field with a on-scene game object. Each instantiated prefab will be parented under this game object.
4. Set the _Vertical Offset_ property. Each instantiated prefab's vertical position will be
offsetted relatively to the previous one by this amount.
5. Draw and drop children of the template prefab. UI Text components on those children will be updated with external call (or from the editor with the instantiate button).
6. In runtime, call the `InstantiateAndUpdateUIPrefab(string[])` and pass string values to update the template prefab that will be instantiated.

## Help

Just create an issue and I'll do my best to help.

## Contributions

Pull requests, ideas, questions or any feedback at all are welcome.

See also: [Unity extensions as git submodules](http://wp.me/p56Vqs-6o)

## Versioning

Example: `v0.2.3+1`

- `0` Major version. Introduces breaking changes.
- `2` Minor version. Adds new features.
- `3` Patch version. Bug fixes.
- `+1` Metadata version.

[Semantic Versioning Specification](http://semver.org/)
