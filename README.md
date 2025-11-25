# Dinkum Value Tooltip

<a href="https://www.nexusmods.com/dinkum/mods/406">
    <img alt="nexusmods.com shield" src="https://img.shields.io/endpoint?url=https%3A%2F%2Fgist.githubusercontent.com%2Fradj307%2Fe9a80731ee236cc67fb00b698e75201e%2Fraw%2F5230074dfb1a60fba917a1232f9382fa5cfec5db%2Fendpoint.json">
</a>

A mod that adds the ability to see the value of items in the inventory. And, the ability to see the total value of selected items in the sell menu.

## Requirements
- Dinkum v1.0.7
- [MelonLoader v0.7.1 Beta](https://github.com/LavaGang/MelonLoader)
- [DivineDinkum](https://github.com/Decapitated/DivineDinkum) v1.0.0

## Features
| Value Tooltip |
| - |
| <img src="docs/Tooltip.png"> |

| Total Sell Value |
| - |
| <img src="docs/Sell.png"> |

## Contributing
Before making changes, update `DinkumPath` in the `Directory.Build.props` file to the absolute path of your `Dinkum` game folder.

To prevent committing changes to `Directory.Build.props`, run:
```
git update-index --skip-worktree Directory.Build.props
```

To allow committing changes to `Directory.Build.props`, run:

```
git update-index --no-skip-worktree Directory.Build.props
```
