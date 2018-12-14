# SpaceVIL
**[SpaceVIL](http://spvessel.com/index.html)** (Space of Visual Items Layout) is a cross-platform and multilingual framework for **creating GUI client applications for .NET Standard, .NET Core and JVM**. SpaceVIL is based on **OpenGL** graphic technology and [glfw](https://www.glfw.org). Using this framework in conjunction with Core or with a JVM, you can work and create graphical client applications on Linux operating systems.
See more on youtube channel - https://youtu.be/glYqBboBL5s
You can also view SpaceVIL [documentation](http://spvessel.com/spacevil/doc/doc.html).

## Get started with SpaceVIL

In this tutorial, you will learn how to create a simple cross-platform application using the SpaceVIL framework. We hope you will enjoy the framework.

### Step 1: Implementing SpaceVIL into you Project

**C# / .NET Standart / Visual Studio**
* [Download](http://spvessel.com/index.html) SpaceVIL for **.NET Standart**
* [Download](https://www.glfw.org) **glfw** and copy next to the executable. 
* Add Reference to **SpaceVIL.dll**
* Check the framework by adding the line: 
    ```
    using SpaceVIL;
    ```

**C# / .NET Core / Visual Studio Code (or any other text editor)**

* [Download](http://spvessel.com/index.html) SpaceVIL for **.NET Core**
* **Windows:** [Download](https://www.glfw.org) **glfw** and copy next to the executable. **Linux:** install **libglfw3** and **libglfw3-dev** via repository
* Create console project by executing the command in terminal:
    ```
    dotnet new console --output MyProject
    ```
* Copy **SpaceVIL.dll** in the MyProject folder
* Add code below into **MyProject.csproj**:
    ```
    <ItemGroup> 
      <Reference Include="SpaceVIL.dll"/> 
    </ItemGroup>
    ```
* Install **System.Drawing.Common** from NuGet by command:
    ```
    dotnet add package System.Drawing.Common --version 4.5.0
    ```
    or just add code below and then execute command in terminal: ```dotnet restore```
    ```
    <ItemGroup> 
      <PackageReference Include="System.Drawing.Common" Version="4.5.0" /> 
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
* Create instance of **WindowLayout**
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
          WindowLayout Handler = new WindowLayout(nameof(MainWindow), nameof(MainWindow), 
                    800, 600, false);
          SetHandler(Handler);
          Handler.SetMinSize(400, 300);
          Handler.SetBackground(32, 34, 37);
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
    MainWindow mw = new MainWindow();
    WindowLayoutBox.TryShow(nameof(MainWindow));
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
          MainWindow mw = new MainWindow();
          WindowLayoutBox.TryShow(nameof(MainWindow));
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
* Create instance of **WindowLayout**
* Code should look like this:
    ```
    import java.awt.Color;
    import com.spvessel.spacevil.*;
    class MainWindow extends ActiveWindow {
      @Override
      public void initWindow() {
        WindowLayout Handler = new WindowLayout(this.getClass().getSimpleName(), "App",
                     360, 500, false);
        setHandler(Handler);
        Handler.setMinSize(350, 500);
        Handler.setBackground(new Color(45, 45, 45));
      }
    }
    ```
* In **App.java** add line on top of the file: **import com.spvessel.spacevil.*;**
* In **App.java** of project source file inside main function add lines:
    ```
    MainWindow mw = new MainWindow();
    WindowLayoutBox.tryShow(MainWindow.class.getName());
    ```
* Code should look like this:
    ```
    import com.spvessel.*;
    public class App {
      public static void main(String[] args) {
        MainWindow mw = new MainWindow();
        WindowLayoutBox.tryShow(MainWindow.class.getSimpleName());
      }
    }
    ```
* Compile and run project to check


### Step 3: Adding items to the window

**C#**

* Items can be added to the window as follows:
    ```
    Handler.AddItem(item2);
    ```
    where **Handler** is WindowLayout of current ActiveWindow (or DialogWindow) 
    ###

* Items can be added to another item simply by calling method:
    ```
    item1.AddItem(item2);
    ```

**JAVA**
* Items can be added to the window as follows:
    ```
    Handler.addItem(item2);
    ```
    where **Handler** is WindowLayout of current ActiveWindow (or DialogWindow) 
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
* **Roman Sedaykin**
* **Valeriya Sedaykina**


### License

Examples is licensed under the MIT License. See the LICENSE.md file for details.
