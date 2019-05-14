---
tags:
  - git
  - tips
  - source control
---

# Git Cheat Sheet

## Creating a Repository

Create a repository locally

    $ git init [name]

Clone a remote repository

    $ git clone [url]

## Making a Change

Stage a file for commit

    $ git add [file]

Stage all changed files for commit

    $ git add .

Commit all staged files

    $ git commit -m 'commit message'

Commit all tracked files even if not staged

    $ git commit -am 'commit message'

## Reverting a Change

Unstage file but keep the changes

    $ git reset [file]

Revert to the last commit

    $ git reset --hard

Discard local changes to a specific file

    $ git checkout HEAD [file]

## Observing a Repository

List new or modified files not yet committed

    $ git status

Show the changes to files not yet staged

    $ git diff

Show full change history

    $ git log

## Working with a Branch

List all existent branches (local and remotes)

    $ git branch -av

Switch to a branch and update the local working directory

    $ git checkout [branch]

Create a new branch based on the current HEAD

    $ git branch [branch]

Delete the branch

    $ git branch -d [branch]

Mark the current commit in the current branch with a tag

    $ git tag [tag]

## Synchronizing a Repository

Get the latest changes from origin without merging

    $ git fetch

Get the latest changes from origin and merge

    $ git pull

Get the latest changes and rebase, hence, moving a branch to a new base commit

    $ git pull --rebase

Push local changes to the origin

    $ git push
