# Reach

## Solve `Failed to load window layout` error on unity start

Some people get a "Failed to load window layout" error when opening this project for the first time. Here are the steps to resolve this:
* Open the project with Unity until this error appears
* Press the `Load Default Layout` button. If that fixes it, you're done
* Otherwise verify that a file `Library/CurrentLayout-default.dwlt` is created
* Quit unity and start again
* Press the `Load Default Layout` button again. This will create a new `Library/CurrentLayout-default.dwlt` file. Copy that file to a save location.
* Quit unity again, which will corrupt the `Library/CurrentLayout-default.dwlt` file
* While unity is not running restore the `Library/CurrentLayout-default.dwlt` file with the copy you made
* Start unity again and it should succeed
