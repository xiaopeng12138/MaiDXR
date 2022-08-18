# MaiDXR
Open Source VR Arcade Simulator

[Chinese 中文 README](https://github.com/xiaopeng12138/MaiDXR/blob/main/README_zh.md)

## About this project
- This project is an update of https://github.com/xiaopeng12138/MaiMai-VR. 
- Support DX version or above
- The model is almost 1:1 to DX cabinet/framework
- Native touch input and light outupt
- 90hz or 120hz capture (Bitblt) (Thanks @Thalesalex for the recommendation)
- Customizable haptic feedback
- 3rd person camera and smooth camera
- 4 customizable buttons (+ 1 select button)
- only 1 player

## Inspired by
- https://github.com/derole1/MaiMai-VR
- https://github.com/HelloKS/MaiMai-VR

## Used repository
- https://github.com/HelloKS/MaiMai-VR
- https://github.com/hecomi/uWindowCapture
- https://github.com/Sucareto/Mai2Touch

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
- You must enable the enable buffer option in com2com on both ports of all pairs. Otherwise, your MaiDXR will crash after the Unity logo.
- Disable DummyTouchPanel in xxxx.ini.
- If you need button light, pls bind COM21 to COM51 (it must be these two ports)(Do not disable DummyLED!).
- Run the game in window mode by adding [Unity Standalone Player command line arguments](https://docs.unity3d.com/Manual/PlayerCommandLineArguments.html) in xxxxx.bat and make sure there is no black bar. Recommend setting for 1080p display: "xxxxxx.exe -screen-fullscreen 0 -screen-width 1170 -screen-height 1050"
- Start MaiDXR.
- Enable somehow Test mod then exit Test mode.

## Configuration
In MaiDXR window press "M" on the keyboard to toggle local motion on-off.

Edit Settings.json file under the root directory. Press F5 in the MaiDXR window or defocus then focus window again to update settings.

HandSize, HandPosition, PlayerHigh: in CM

CaptureFrameRate, TouchRefreshRate: in FPS

Capture1P: true (9:16 aspect ratio) or false (default) (2*9:16 aspect ratio)

CameraSmooth: 0.0 - 1.0, 1.0 = no smoothing

CameraFOV: in degree

CameraPosition: in M

To enable 3rd person mod: set CameraSmooth to 0 then move your headset to where your camera wants to be. Then focus/select MaiDXR window to lock position. You can show your head by enabling the setting below.

HapticDuration: in second

HapticAmplitude: 0.0 - 1.0, 1.0 = max vibration

SelectButton and Button(1-4: top to bottom): Pls see [VK Code](https://docs.microsoft.com/windows/win32/inputdev/virtual-key-codes). Some keys do not require "VK_" at the beginning.

## Preview
![Image Capture](https://github.com/xiaopeng12138/MaiDXR/blob/main/PreviewImage/MaiDXR_PreviewImage.png?raw=true)

## ToDo
- √ Add user-adjustable settings (controller position etc.)
- √ Add non-VR window and camera smooth
- √ Add button light
- √ Add button vibration
- √ Add 3rd person camera
- √ Add 2p
- Add Multiplay
- √ Add custom button
- √ Support 1p only capture
- Add visual touch and button feedback
- √ Add in game setting panel 

Huge thanks to HelloKS and derole1

If you want to add any function pls commit PR, I will accept it as soon as possible and make a new build/release.
