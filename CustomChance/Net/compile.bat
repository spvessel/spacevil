"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\csc.exe" ^
-platform:x64 ^
-optimize ^
/t:winexe ^
/r:System.Drawing.dll ^
/r:SpaceVIL.dll ^
/win32icon:icon.ico ^
-appconfig:App.config ^
-out:customchance.exe ^
-res:icon_big.png,CustomChance.icon_big.png ^
-res:icon_small.png,CustomChance.icon_small.png ^
-recurse:*.cs
