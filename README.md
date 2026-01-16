# Unity 6.3 Sample App with EDM4U & Singular

This project is a **Unity 6.3** sample app that demonstrates how to integrate the **External Dependency Manager for Unity (EDM4U)** with an iOS project that uses **CocoaPods**, and how to add the **Singular Unity SDK** on top. [web:129][web:123]

---

## Goals

- Show a clean way to:
  - Install and configure **EDM4U** via Unity Package Manager. [web:120]
  - Generate and manage an iOS **Podfile** and CocoaPods workspace from Unity.
  - Integrate the **Singular Unity SDK** using EDM4U and CocoaPods. [web:123]
- Provide a minimal Unity scene that can be built and run on iOS.

---

## Prerequisites

- Unity **6.3.x** (or newer 6.x) with iOS build support.
- macOS with:
  - **Xcode** installed.
  - **CocoaPods** installed and working from Terminal (`pod --version`). [web:129]
  - A recent **Homebrew Ruby** (recommended) if you want to avoid the system Ruby/ffi issues.
- A valid Apple Developer account to run on a device (any test bundle ID is fine).

---

## Project Structure

Key items in this repo:

- `unity-63-with-EDM4U/` – Unity project folder.
- `Builds/` – iOS export folder (ignored by Git; generated on each build).
- `Assets/` – Unity scenes, scripts, and settings.
- `Packages/` – includes EDM4U and the Singular SDK via UPM.

Unity creates the iOS Xcode project directly under `Builds/` (for example, `Builds/Unity-iPhone.xcodeproj`) and EDM4U generates a `Podfile` in the same folder. [web:129]

---

## How EDM4U is Used

This project uses **External Dependency Manager for Unity** to manage native dependencies:

- EDM4U is added as a Unity package (not as a legacy `.unitypackage`). [web:120]
- The iOS Resolver is enabled to:
  - Generate a `Podfile` under `Builds/`.
  - Add CocoaPods integration to the Xcode workspace. [web:120][web:125]

EDM4U is configured **not** to auto-install CocoaPods with the system Ruby; instead, a post-build step runs `pod install` using your Homebrew Ruby toolchain.

---

## Singular Unity SDK Integration

The **Singular Unity SDK** is installed via Git URL in Unity Package Manager. [web:123]

High‑level setup:

1. Add the Singular package to the project via **Window → Package Manager → Add package from Git URL…**. [web:123]
2. Add the Singular iOS dependency to EDM4U so it appears in the generated `Podfile` (the Podfile will include `Singular-SDK`). [web:123][web:129]
3. Build for iOS, run `pod install`, and then open the generated workspace in Xcode.

The `Podfile` generated in `Builds/` will list the `Singular-SDK` pod and is automatically processed by CocoaPods during the post‑build step.

---

## Build & Run – iOS

Use this sequence when building:

1. **In Unity**
   - Open the project located in `unity-63-with-EDM4U/`.
   - Set **Build Settings → Platform** to **iOS**.
   - Choose **Build** and set the output folder to `Builds/` in the repo root.

2. **Automatic CocoaPods step**
   - After the build completes, a Unity post‑build script runs:
     - `pod install --repo-update` in the `Builds/` directory.
   - This:
     - Updates local pod specs.
     - Installs / updates the `Singular-SDK` pod.
     - Creates `Unity-iPhone.xcworkspace` in `Builds/`. [web:68][web:129]

3. **In Xcode**
   - Open `Builds/Unity-iPhone.xcworkspace`.
   - Set your signing team and bundle identifier.
   - Build and run on a device.

If the post‑build script ever fails for any reason, you can manually run:

```bash
cd Builds
pod install --repo-update
