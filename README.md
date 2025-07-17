# ArcGame

ArcGame is a prototype multiplayer action game built with Unity and the FishNet networking library. It contains the foundations for lobbies, basic combat and an inventory system. The project structure was cleaned up for easier navigation and future expansion.

## Folder Structure

```
Assets
 └─ Scripts
    ├─ Core         # data definitions, scriptable objects and utilities
    ├─ Gameplay
    │   ├─ Managers    # menu managers, lobby and item logic
    │   ├─ Networking  # network behaviours and spawners
    │   ├─ Systems     # persistence, inventory and global events
    │   └─ Units       # player and enemy logic
    └─ UI            # interface controllers and templates
```

## Getting Started

1. Clone the repository and open it with Unity 2021 or newer.
2. Let Unity reimport the assets so that moved scripts are recognised with their meta files.
3. Open the **MainMenu** scene and press Play to run locally.
4. The game relies on FishNet and Steamworks. Ensure these packages are installed through the Package Manager.

Scripts now use the `Arc.*` namespaces. Update any of your own scripts to use these namespaces if you extend the project.
