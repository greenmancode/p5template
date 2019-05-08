@echo off
title P5 Template ^| Greenman
color 0f
set dom=n
set sound=n

goto start

:error
cls
echo Drag this program to the directory you want the files to be 
echo created in.
echo.
echo Press any key to exit
pause>nul
exit

:start
cls
echo p5 Template - Created By Greenman
echo ------------
cd
echo Is this the directory you want to create your sketch in?
echo 1) Yes
echo 2) No
choice /c:12 >nul
if %errorlevel% == 2 goto error
:size
cls
echo Canvas Width (Pixels)
set /p w=
echo Canvas Height (Pixels)
set /p h=
cls
echo Width:%w% Height:%h%
echo Is this okay?
echo 1) Yes
echo 2) No
choice /c:12 >nul
if %errorlevel% == 2 goto size
cls
echo Do you want to include the p5 DOM library?
echo 1) Yes
echo 2) No
choice /c:12 >nul
if %errorlevel% == 1 set dom=y
cls
echo Do you want to include the p5 Sound library?
echo 1) Yes
echo 2) No
choice /c:12 >nul
if %errorlevel% == 1 set sound=y
cls
echo Creating files...
(
echo ^<html^>
echo ^<head^>
echo ^<script src^="https://cdnjs.cloudflare.com/ajax/libs/p5.js/0.8.0/p5.js"^>^</script^>
if %dom% == y echo ^<script src^="https://cdnjs.cloudflare.com/ajax/libs/p5.js/0.8.0/addons/p5.dom.js"^>^</script^>
if %sound% == y echo ^<script src^="https://cdnjs.cloudflare.com/ajax/libs/p5.js/0.8.0/addons/p5.sound.js"^>^</script^>
echo ^<script src^="sketch.js"^>^</script^>
echo ^</head^>
echo ^</html^>
)>index.html

(
echo function setup^(^){
echo     createCanvas^(%w%^,%h%^)^;
echo }
echo.
echo function draw^(^){
echo background^(0^)^;
echo }
)>sketch.js

echo Created index.html and sketch.js!
echo.
echo Press any key to exit
pause>nul
exit