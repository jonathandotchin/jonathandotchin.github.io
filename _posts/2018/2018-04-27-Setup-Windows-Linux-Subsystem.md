## Background

The Windows Subsystem for Linux lets developers run Linux environments -- including most command-line tools, utilities, and applications -- directly on Windows, unmodified, without the overhead of a virtual machine. However, the official instructions to install the Windows Linux Subsystem will not work for most users since the Microsoft Store is blocked within Ubisoft. Therefore, we must fallback to the command prompt.

## Confirm your Windows Version

Before proceeding, we must confirm your Windows version.

1. Open a command prompt and run the following command

   ```
   winver
   ```

2. Take note of the Version and OS Build

## Install the Windows Subsystem for Linux

First we must enabled the "Windows Subsystem for Linux" optional feature.

1. Run the following command in an administrator powershell window.

   ```
   Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux

   ```

2. Once completed, restart your computer.

## Install Ubuntu

With the Microsoft Store block, we must fallback to using the command prompt. Therefore, we cannot benefit from the different and latest distribution available. Instead, we will simply use Ubuntu.

1. If you are on Windows Version lower than 1709, you must t

   urn on Developer Mode. This step is not required if you are running 1709 or above.

   1. Open Settings -> Update and Security -> For developers
   2. Select the appropriate radio button

2. Open a command prompt and run the following command

    ```
    `lxrun ``/install` `/y`
    ```

## Updating Ubuntu

Since moving to the store, we have stopped keeping this user-mode image up to date. When you're done, run `apt-get update`.

1. If you are on Windows Version lower than 1709, you should change the default account, as it will be the root account otherwise. It will ask you to enter a password. Enter the password you want, as these accounts are completely separate from your windows account's password. This step is not required if you are running 1709 or above.

   ```
   lxrun /setdefaultuser <desired username>
   ```

2. open a command prompt and enter bash mode by entering the command

   ```
    bash
   ```

3. once you enter bash mode, enter the following linux command to update and follow the prompt

    ```
    `sudo apt-get update`
    ```

## Upgrading Ubuntu

With Windows version 1603, the Ubuntu version installed is 14.04, which means that you will want to upgrade to Ubuntu 16.04. The following commands will handle that for you.

    ```
    `sudo -S apt-mark hold procps strace`
    `sudo -S RELEASE_UPGRADER_NO_SCREEN=``1` `do``-release-upgrade`
    ```

After that is done, you should run:

    ```
    `sudo -S rm /etc/apt/apt.conf.d/50unattended-upgrades.ucf-old`
    `sudo -S dpkg --configure -a`
    `sudo -S apt-get update`
    `sudo -S apt-get upgrade`
    `sudo -S apt-get dist-upgrade`
    `sudo -S apt-get autoremove`
    ```

If you run into any issues, this [github issue thread](https://github.com/Microsoft/WSL/issues/482#issuecomment-274584657) (and the post directly linked in particular) contains information about troubleshooting your install. You will most likely want to add an alias to your .bashrc file. To do that, you just need to add " alias sudo='sudo -S' " at the end of the file.

## Reference

- https://docs.microsoft.com/en-us/windows/wsl/install-win10
- https://www.howtogeek.com/261188/how-to-uninstall-or-reinstall-windows-10s-ubuntu-bash-shell/
- https://github.com/Microsoft/WSL/issues/482#issuecomment-274584657 (Github issue about upgrading 14.04 to 16.04 on Windows 1603)