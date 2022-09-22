# SpaceVIL
**[SpaceVIL](http://spvessel.com/index.html)** (Space of Visual Items Layout) is a cross-platform and multilingual framework for **creating GUI client applications for .NET Standard, .NET Core and JVM**. SpaceVIL is based on **OpenGL** graphic technology and [glfw](https://www.glfw.org). Using this framework in conjunction with .Net Core or with a JVM, you can work and create graphical client applications on Linux, Mac OS X and Windows.
See more on youtube channel - https://youtu.be/kJ6n1fTHXws
You can also view SpaceVIL [documentation](http://spvessel.com).

SpaceVIL's source code is available on GitHub for [Java](https://github.com/spvessel/SpaceVILJava) and [C#](https://github.com/spvessel/SpaceVILSharp).

## Get started with SpaceVIL

In this tutorial, you will learn how to create a simple cross-platform application using the SpaceVIL framework. We hope you will enjoy the framework.

### Step 1: Implementing SpaceVIL into you Project

**C# / .NET Standard / Visual Studio**

**ATTENTION FOR WINDOWS OS USERS:**
GLFW library should be renamed to **"glfw.dll"** (if you download binaries from the official GLFW website, you get a library named **"glfw3.dll"**)

* Create console project
* [Download](http://spvessel.com/index.html) SpaceVIL for **.NET Standard**
* [Download](https://www.glfw.org) **glfw** and copy next to the executable file. 
* Add Reference to **SpaceVIL.dll**
* Check the framework by adding the line: 
    ```
    using SpaceVIL;
    ```

**C# / .NET Core / Visual Studio Code (or any other text editor)**

* Create console project by executing the command in terminal:
    ```
    dotnet new console --output MyProject
    ```
* [Download](http://spvessel.com/index.html) SpaceVIL for **.NET Core**
* **Windows:** [Download](https://www.glfw.org) **glfw** and copy next to the executable file. **Linux:** install **libglfw3** and **libglfw3-dev** via repository.  **Mac OS X (simplest way):** extract **libglfw.dylib** from **lwjgl-glfw-natives-macos.jar** (or you can compile **libglfw.dylib** from sources) and copy next to the executable file.

* Copy **SpaceVIL.dll** in the MyProject folder
* Add code below into **MyProject.csproj**:
    ```
    <ItemGroup> 
      <Reference Include="SpaceVIL.dll"/> 
    </ItemGroup>
    ```
**Windows / Linux:**
* Install **System.Drawing.Common** from NuGet by command:
    ```
    dotnet add package System.Drawing.Common --version 4.6.0-preview7.19362.9
    ```
    or just add code below and then execute command in terminal: ```dotnet restore```
    ```
    <ItemGroup> 
      <PackageReference Include="System.Drawing.Common" Version="4.6.0-preview7.19362.9" /> 
    </ItemGroup>
    ```
    
**For Mac OS:**
* Install **CoreCompat.System.Drawing.v2** from NuGet by command:
    ```
    dotnet add package System.Drawing.Common --version 4.6.0-preview7.19362.9
    ```
* Install **runtime.osx.10.10-x64.CoreCompat.System.Drawing** from NuGet by command:
    ```
    dotnet add package runtime.osx.10.10-x64.CoreCompat.System.Drawing --version 5.8.64
    ```
    or just add code below and then execute command in terminal: ```dotnet restore```
    ```
    <ItemGroup> 
      <PackageReference Include="System.Drawing.Common" Version="4.6.0-preview7.19362.9" /> 
      <PackageReference Include="runtime.osx.10.10-x64.CoreCompat.System.Drawing" Version="5.8.64" /> 
    </ItemGroup>
    ```
* Check the framework by adding the line: 
    ```
    using SpaceVIL;
    ```

**JAVA / Gradle**

* [Download](http://spvessel.com/index.html) SpaceVIL for **JVM**
* [Download](https://www.lwjgl.org) **lwjgl**
* Create directory for project
* Create gradle project by executing the command in terminal:
    ```
    gradle init --type java-application
    ```
* Create directory **libs** in the project directory
* Copy into **libs** downloaded file **spacevil.jar**
* Copy this lwjgl files below into **libs** directory:
    ```
    lwjgl.jar
    lwjgl-glfw.jar
    lwjgl-glfw-natives-linux.jar
    lwjgl-glfw-natives-macos.jar
    lwjgl-glfw-natives-windows.jar
    lwjgl-natives-linux.jar
    lwjgl-natives-macos.jar
    lwjgl-natives-windows.jar
    lwjgl-opengl.jar
    lwjgl-opengl-natives-linux.jar
    lwjgl-opengl-natives-macos.jar
    lwjgl-opengl-natives-windows.jar
    ```
* In **build.gradle** add line into dependencies block:
    ```
    compile fileTree(dir: 'libs', include: '*.jar')
    ```
**For Mac OS:**
* **ATTENTION!** To get your app work you should run jvm on Mac OS with **-XstartOnFirstThread** argument! In **build.gradle** add line: 
    ```
    applicationDefaultJvmArgs = ['-XstartOnFirstThread']
    ```
    or run your compiled ***.jar** file with command: 
    ```
    java -jar -XstartOnFirstThread your_app.jar
    ```

* Check the framework by adding the line:
    ```
    import com.spvessel.spacevil;
    ```


### Step 2: Creating and running a new window

**C#**

* Create in project source file named for example **MainWindow.cs** (name may be different)
* Create class **MainWindow**
* Class **MainWindow** must be inherited from class **SpaceVIL.ActiveWindow**
* Override method **InitWindow**
* Set basic parameters of window via method **SetParameters()**
* Code should look like this:
    ```
    using System;
    using System.Drawing;
    using SpaceVIL;
    namespace MyProject
    {
      class MainWindow : ActiveWindow
      {
        public override void InitWindow()
        {
          SetParameters(nameof(MainWindow), nameof(MainWindow), 800, 600);
          SetMinSize(400, 300);
          SetBackground(32, 34, 37);
        }
      }
    }
    ```
* In **Program.cs** add line on top of the file: 
    ```
    using SpaceVIL;
    ```
* In **Program.cs** of project source file inside **Main** function add lines:
    ```
    Common.CommonService.InitSpaceVILComponents();
    MainWindow mw = new MainWindow();
    mw.Show();
    ```
* Code should look like this:
    ```
    using System;
    using SpaceVIL;
    namespace MyProject
    {
      class Program
      {
        static void Main(string[] args)
        {
          Common.CommonService.InitSpaceVILComponents();
          MainWindow mw = new MainWindow();
          mw.Show();
        }
      }
    }
    ```
* Compile and run project to check

**JAVA**

* Create in project source file named for example **MainWindow.java** (name may be different)
* Create class **MainWindow**
* Class **MainWindow** must be inherited from class **com.spvessel.spacevil.ActiveWindow**
* Override method **initWindow**
* Set basic parameters of window via method **setParameters()**
* Code should look like this:
    ```
    import java.awt.Color;
    import com.spvessel.spacevil.*;
    class MainWindow extends ActiveWindow {
      @Override
      public void initWindow() {
        setParameters(this.getClass().getSimpleName(), "App", 360, 500);
        setMinSize(350, 500);
        setBackground(45, 45, 45);
      }
    }
    ```
* In **App.java** add line on top of the file: **import com.spvessel.spacevil.*;**
* In **App.java** of project source file inside main function add lines:
    ```
    com.spvessel.spacevil.Common.CommonService.initSpaceVILComponents();
    MainWindow mw = new MainWindow();
    mw.show();
    ```
* Code should look like this:
    ```
    import com.spvessel.spacevil.*;
    public class App {
      public static void main(String[] args) {
        com.spvessel.spacevil.Common.CommonService.initSpaceVILComponents();
        MainWindow mw = new MainWindow();
        mw.show();
      }
    }
    ```
* Compile and run project to check


### Step 3: Adding items to the window

**C#**

* Items can be added to the window as follows:
    ```
    AddItem(item2);
    ``` 
    ###

* Items can be added to another item simply by calling method:
    ```
    item1.AddItem(item2);
    ```

**JAVA**
* Items can be added to the window as follows:
    ```
    addItem(item2);
    ```
    ###
* Items can be added to another item simply by calling method:
    ```
    item1.addItem(item2);
    ```

### Step 4: Assigning actions to events

You can assign unlimited count of action to one event of an item.

**C#**

* Assign an action (lambda expression or method) to all avaliable item events as follows:
     ```
    btn.EventMouseClick += (sender, args) =>
    {
      //do something
    };
    ```


**JAVA**
* In Java, you can assign an action to item events as follows:
    ```
    btn.eventMouseClick.add( (sender, args) -> {
      //do something
    });
    ```


### Authors
* **Roman Sedaikin**
* **Valeriia Sedaikina**


### License

Examples is licensed under the MIT License. See the LICENSE.md file for details.
