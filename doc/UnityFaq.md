# Unity FAQ

We're all new to Unity and there will be some neat lessons learned along
the way.  Here's a place to collect some of them.

## Can't click on anything in the UI
An `EventSystem` might be missing from the scene.

## Did UI work but everything's broken
Don't start the game from a Prefab, instead start from a scene.

## Project won't load (Linux)
You might have two directories with the same name but different
capitalization.  Don't do that.

## My textures are so fucked, even colorblind people notice
Disable texture compression in import settings.  Or for the entire
project:
```
find . -name "*.png.meta" -exec sed -i "s/textureCompression: 1/textureCompression: 0/g" {} \;
```

## I can't see my sprites! help!
Make sure that the `z coordinate` is not set to some absurd value (like -75) but rather 0 and you should be good
