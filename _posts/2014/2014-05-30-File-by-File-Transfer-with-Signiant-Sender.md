---
tags:
  - transfer
  - improvement
  - signiant
---

In this post, we examined how we added the ability to transfer files on a file by file basis in Signiant. The off the shelf implementation of Signiant only provide folder replication at the time.

# Possible Solutions

## Signiant Distribute Solution

    - Put as much signiant distribute folder as you want in the workflow
    - Allow an equal amount of input folder and target folder as the number of signiant distribute folder
    - Map each input/target to a signiant distribute folder
    - Will work
    - Still need to test the concurrency
    - Will not scale nicely

## Script Solution

    - For each file, determine the target directory and overwrite it
    - Will work
    - No clue how to modify the component
    - Still need to test the concurrency
    - Will scale
    - Will not scale with server upgrade (i.e. if signiant does a patch, we need to re-implement)

# Investigation Results

## Signiant Distribute Solution

    - We were unable to workaround the scaling issue

## Script Solution

    - We hit a scale issue with the file list. Maximum 3000ish files.
    - however, a solution to read the list of files from a file will work. We will need to modify the File List Command

Both solution might have a collision problem that can be removed by using "Delivery Mode" = "Fast". Further tests are needed to confirmed the solution and the consequences.

# Future Works

## Signiant Distribute Solution

    Nothing

## Script Solution

    Test "Delivery Mode" = "Fast" for collisions

# References

Target File Readiness Check / Work File Rename
    http://community.signiant.com/phpBB3/viewtopic.php?f=17&t=139

Limit on File Directory / Read file list from file
    http://community.signiant.com/phpBB3/viewtopic.php?f=17&t=138

Multiple files to their respective folders in one transfer
    http://community.signiant.com/phpBB3/viewtopic.php?f=17&t=135
