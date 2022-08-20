# MaiDXR
Open Source VR Arcade Simulator
QQ Group: 261946477
Discord: Coming Soon

[Chinese 中文 README](https://github.com/xiaopeng12138/MaiDXR/blob/main/README_zh.md)

## About this project
- This project is an update of https://github.com/xiaopeng12138/MaiMai-VR. 
- Support DX version or above
- The model is almost 1:1 to DX cabinet/framework
- Using naative touch input and light outupt
- 90hz or 120hz capture (Bitblt) (Thanks @Thalesalex for the recommendation)
- Customizable haptic feedback
- 3rd person camera and smooth camera
- 3 customizable buttons

## Inspired by
- https://github.com/derole1/MaiMai-VR
- https://github.com/HelloKS/MaiMai-VR

## Used repository
- https://github.com/HelloKS/MaiMai-VR
- https://github.com/hecomi/uWindowCapture
- https://github.com/Sucareto/Mai2Touch
- https://github.com/xiaopeng12138/WACVR

**Special thanks to [@V17AMax](https://github.com/V17AMax) for the beautiful logo and designs**


## Build requirements
- Current Unity version: 2021.3.2f1

## Supported platform
- All SteamVR device
- All Oculus device
- Tested on Quest 2 through Oculus link (Native and via SteamVR) and ALVR (via SteamVR). The Hand Balls position is by default adjusted for the Quest 2 controller.

## Declaimer
- This project is non-profit and some resources came from Internet!
- Although this is under the MIT license, do not use this in commercial/profitable scenarios!
- Please support your local arcade if you can!

## Changelog
Please see Changes.md

## How to use
- Get game somehow and make sure it will run properly. (DO NOT ASK ANYTHING THAT IS DIRECTLY RELATED TO THE GAME IT SELF)
- Download [latest version of MaiDXR](https://github.com/xiaopeng12138/MaiDXR/releases)
- Download and install [com0com](https://storage.googleapis.com/google-code-archive-downloads/v2/code.google.com/powersdr-iq/setup_com0com_W7_x64_signed.exe)
- Configure com0com to bind COM3 and COM5 (it must be these two ports), COM4 and COM6 is optional (bind them will make your startup process faster).
- You must enable the enable buffer option in com0com on both ports of all pairs. Otherwise, your MaiDXR will crash after the logo.
- Disable DummyTouchPanel in mai2.ini.
- If you need button light, pls bind COM21 to COM51 (it must be these two ports)(Do not disable DummyLED!).
- Run the game in window mode by adding [Unity Standalone Player command line arguments](https://docs.unity3d.com/Manual/PlayerCommandLineArguments.html) in xxxxx.bat and make sure there is no black bar. Recommend setting for 1080p display: "xxxxxx.exe -screen-fullscreen 0 -screen-width 1170 -screen-height 1050"
- Start MaiDXR first then start the game.
- If your touch is not working, try to enable somehow Test mod then exit Test mode.

## Configuration
The green button on the bottom of the cabinet is lock button. Long press it will disable all unnecessary buttons, controller pointer, and the config panel.

You can adjust all settings in the config panel or via config.json. The changes of config.json will only apply after the MaiDXR reboot. 

If you want to adjust the settings, please take a step back. The controller pointer will automatically be disabled when the controller are too close to the cabinet.

Some configs in config.json are only the index of the dropdown.

You can use the pointer to point the third-person camera and grab it to the position where you want to be.


## Preview
![Image Capture](https://github.com/xiaopeng12138/MaiDXR/blob/main/PreviewImage/MaiDXR_PreviewImage.png?raw=true)

## ToDo
- √ Add user-adjustable settings (controller position etc.)
- √ Add non-VR window and camera smooth
- √ Add button light
- √ Add button vibration
- √ Add 3rd person camera
- √ Add 2p
- √ Add custom button
- √ Support 1p only capture
- √ Add in game setting panel 
- Add Multiplay
- Add visual touch and button feedback

Huge thanks to HelloKS, derole1, and every one in BSAH

If you want to add any function pls commit PR, I will accept it as soon as possible and make a new build/release.
