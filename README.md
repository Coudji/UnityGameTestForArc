# UnityGame

This project contains the early stages of a Unity game. The scripts have been
organised into clearer folders to ease maintenance:

```
Assets
 └─ Scripts
    ├─ Core         # data classes, scriptable objects and utilities
    ├─ Gameplay
    │   ├─ Units       # character and enemy logic
    │   ├─ Managers    # menu and item managers
    │   ├─ Networking  # lobby and connection code
    │   └─ Systems     # persistence and game events
    ├─ UI           # user interface controllers
    └─ Tests        # temporary test scripts
```

When opening the project after pulling these changes, let Unity refresh the
asset database so that moved scripts are reimported with their meta files. No
manual action is required other than allowing Unity to recompile.
