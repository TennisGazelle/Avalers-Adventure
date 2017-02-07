# Senior Project 2016

### Authors

+ Catherine Pollock
+ Daniel Lopez
+ Gunnar Wambaugh
+ Luis Almanzar

### Simulator
#### Installation
Requires python, python-tk, and python library 'MatPlotLib' which can be installed by typing:

```bash
sudo apt-get install python python-tk python-matplotlib
```

### Running
To run as a program:

```bash
cd Simulator/
python numGen.py
```

This runs on a set number of iterations, but this will be modified to run indefinitely, and to use keyboard input for waveform modification. Network testing is still to be implemented.
See content of `numGen.py` for data script details.

### Therapy Games

### Folder Structure:

Folder Name | Contents
 ---------- | --------
/AnimationController/ | Holds all animation controller files.
/Animations/ | Holds all animations for character rigs.
/Objects/ | Holds all .fbx files.
/Originals/ | Holds all .obj, .mtl, and texture files.
/Prefabs/ | Holds all Unity finished assets.
/Scenes/ | Holds all scenes/levels of the game.
/Scripts/ | Holds all game scripts.

### Instructions on how to branch and complete project
Assuming there are preexisting branches named `master` and `develop` and you wanted to *activate* the develop branch you can use this command.  

``` bash
git checkout develop
```
Note that all your file changes, if any that you have made on master will be gone if not committed.  That being said, do not commit anything to master from here on out.  All commits will be made to your own branch or `develop`.

Let's pretend that I'm about to pull a ticket from Trello and I'm going to make some changes, I also wish to encapsulate every change into a branch.  For the sake of example, let's pretend I'm working on the camera settings.  These will thus be binary files.

1. I must declare that new branch.
``` bash
git checkout -b camera
```
This both creates and checks out the branch.  The same can be done in two separate calls, with just:
``` bash
git branch camera
git checkout camera
```

2. Make your changes and commit them under this new branch.  To ensure that this is happening, look at the very first line that shows when hitting `git status`.  It will say something like `on branch master`.

3. Once you have finished your task, attach the latest commit to the corresponding card on Trello.

4. When merging your approved branch to the `develop` branch (and eventually when merging `develop` to `master`), checkout the branch to which you want to merge, and then merge that.
```bash
git checkout develop
git merge camera
```

5. In the event that `develop` was updated while your branch was still under construction, you need to __rebase__ you branch.  This merely means that the commit from which you branch stems now needs to be updated to the most recent commit in *that* branch.
```bash
git checkout camera
git rebase develop
```
