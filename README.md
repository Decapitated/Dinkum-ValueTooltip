

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