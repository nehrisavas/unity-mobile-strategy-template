@echo off
echo EmpireWars Android Build - Batch Mode
echo ======================================

set UNITY_PATH="C:\Program Files\Unity\Hub\Editor\6000.3.2f1\Editor\Unity.exe"
set PROJECT_PATH="D:\GitProjeler\oyun\src\client\EmpireWars"
set BUILD_METHOD=BuildScript.BuildAndroidFromCommandLine
set LOG_FILE="D:\GitProjeler\oyun\android-build.log"

echo Unity Path: %UNITY_PATH%
echo Project: %PROJECT_PATH%
echo.
echo Build baslatiliyor... (Bu islem 5-10 dakika surebilir)
echo Log dosyasi: %LOG_FILE%
echo.

%UNITY_PATH% -quit -batchmode -projectPath %PROJECT_PATH% -executeMethod %BUILD_METHOD% -logFile %LOG_FILE% -buildTarget Android

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo BUILD BASARILI!
    echo APK: %PROJECT_PATH%\Builds\Android\EmpireWars.apk
    echo ========================================
) else (
    echo.
    echo ========================================
    echo BUILD BASARISIZ! Hata kodu: %ERRORLEVEL%
    echo Log dosyasini kontrol edin: %LOG_FILE%
    echo ========================================
)

pause
