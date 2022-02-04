# MaiDXR
Open Source VR Arcade Simulator

## About this project
- This project is an update of https://github.com/xiaopeng12138/MaiMai-VR. 

## Inspired by
- https://github.com/derole1/MaiMai-VR
- https://github.com/HelloKS/MaiMai-VR

## Used repository
- https://github.com/HelloKS/MaiMai-VR
- https://github.com/hecomi/uWindowCapture
- https://github.com/Sucareto/Mai2Touch

## Supported platform
- All SteamVR device
- All Oculus device
- Only tested on Quest 2 through Oculus link (Native and via SteamVR). The Hand Balls position is by default adjusted for the Quest 2 controller.

## Declaimer
- This project is non-profit and some resources came from Internet!
- Although this is under the MIT license, do not use this in commercial/profitable scenarios!
- Please support your local arcade if you can!

## Changelog
Please see Changes.md

## How to use
- Download [latest version of MaiDXR](https://github.com/xiaopeng12138/MaiDXR/releases)
- Download and install [com0com](https://storage.googleapis.com/google-code-archive-downloads/v2/code.google.com/powersdr-iq/setup_com0com_W7_x64_signed.exe)
- Configure com0com to bind COM3 and COM5 (it must be these two ports), COM4 and COM6 is optional (bind them will make your startup process faster).
- Disable somehow DummyTouchPanel.
- Run the game in window mode and make sure there is no black bar.
- Start MaiDXR.
- Enable somehow Test mod then exit Test mode.

## Configuration
In MaiDXR window press "M" on the keyboard to toggle local motion on-off.

Edit Settings.json file under the root directory. Press F5 in the MaiDXR window or defocus then focus window again to update settings.

HandSize, HandPosition, PlayerHigh: in CM

CaptureFrameRate, TouchRefreshRate: in FPS

CameraSmooth: 0.0 - 1.0, 1.0 = no smoothing

CameraFOV: in degree

HapticDuration: in second

HapticAmplitude: 0.0 - 1.0, 1.0 = max vibration

## Preview
![Image Capture](https://github.com/xiaopeng12138/MaiDXR/blob/main/PreviewImage/MaiDXR_PreviewImage.png?raw=true)

## ToDo
- √ Add user-adjustable settings (controller position etc.)
- √ Add non-VR window and camera smooth
- Add button light
- √ Add button vibration
- Add 3rd person camera
- Add 2p

Huge thanks to HelloKS and derole1
