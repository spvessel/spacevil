"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\csc.exe" ^
-platform:anycpu ^
-optimize ^
/t:exe ^
/r:System.Drawing.dll ^
/r:spacevil.dll ^
/win32icon:Images\icon.ico ^
-appconfig:App.config ^
-out:advancedgamepad.exe ^
-res:Images\icon.ico,Images.icon.ico ^
-res:Images\icon.png,Images.icon.png ^
-res:Images\simple_icon.png,Images.simple_icon.png ^
-res:Images\battery_empty.png,Images.battery_empty.png ^
-res:Images\battery_full.png,Images.battery_full.png ^
-res:Images\battery_low.png,Images.battery_low.png ^
-res:Images\battery_medium.png,Images.battery_medium.png ^
-res:Images\check_box_icon_off_hover.png,Images.check_box_icon_off_hover.png ^
-res:Images\check_box_icon_off.png,Images.check_box_icon_off.png ^
-res:Images\check_box_icon_on_hover.png,Images.check_box_icon_on_hover.png ^
-res:Images\check_box_icon_on.png,Images.check_box_icon_on.png ^
-res:Images\clear.png,Images.clear.png ^
-res:Images\copy.png,Images.copy.png ^
-res:Images\plus.png,Images.plus.png ^
-res:Images\paste.png,Images.paste.png ^
-recurse:*.cs