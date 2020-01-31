# Introduction

I'll tell you about the self-developed framework, its capabilities and a brief story of its creation.

SpaceVIL (Space of visual item layouts) is a cross-platform and multilingual framework based on **OpenGL** technology with the **GLFW** library for creating windows. Using the framework you can create GUI applications for Linux, Mac OS X and Windows operating systems.

When I decide to create SpaceVIL I already studied (or just familiarized) a bunch of UI frameworks. All of them were very different from each other. Each time when I started working with a new framework, I realized that I need to discard most of my knowledge about other UI frameworks, because each of them too differ from others and each of them has its own atmosphere, rules and the main idea of using. Qt Widgets, QML, HTML, CSS, WPF, Windows Forms, Borland, JavaAWT, JavaSwing, JavaFX. Knowledge one of them will not be useful to others. If you know how to do something in six different frameworks, you still need to read the manual in 80% of cases to find out how to do this ”something” in the seventh. Unfortunately, UI systems are not cross-intuitive.

This was one of the reasons for creating a universal multilingual UI framework. When I change the programming language I want to immerse myself in its features, but not into the new (to me) UI system (creating UI for apps was important in my previous work), because the previous UI system depends on the language, platform or OS. There is nothing complicated in creating such a framework. GUI apps have been around for a long time and we could assume that cross-platform, easy-to-use, multilingual framework should exist. But for now when I use C# and I need a cross-platform app with a UI, I can do this using Electron and JavaScript. But now I just need to learn JS first and then Electron and figure out how to link them to .Net Core.

It may still look too exaggerated but let's think about it. There is no a single enthusiastic company that would decide to combine all the best practices and make the creation of interfaces simple, quick and intuitive. Qt does something similar for C++, but to be honest, Qt is more like a separate language with its implementation of the standard type library and offers more than pure C++. After using Qt, it is very difficult to return to C++ again. Qt includes classes for working with images, graphics, fonts, etc., which is not included to standard C++. If you want to return to C++ you have to collect all the pieces from open source to have something like Qt. And it'll be good if all the pieces are licensed by LGPL.

Another reason for writing my own UI system: an existing systems are overly complex, work in different ways, have many exceptions, are closed, don't give enough freedom to control all UI features that you might want and the rendering process.

The system should be easy to use and flexible in implementing the developer's plans. Nothing should prevent developers from realizing their creativity. Actually it's not so hard to provide flexibility and functionality, because all possible ways of interacting with the user are strictly limited by operating systems and input methods.

In my work, I often used OpenGL rendering technology. Using this technology, I successfully solved all my problems. Then the tasks switched to user interaction, and I began to think that all this was already starting to look like a GUI system. I'm not a pro in OpenGL, but this knowledge was enough to use them in my own UI building system.

As a fun, I decided to make some prototypes. They were mostly based on various open source OpenGL wrappers with a built-in window creation and management system. At the same time, I was trying to improve my C# skills, so I used platform-dependent modules in prototypes. The platform was usually Windows. Each prototype was made to solve certain problems and to test platform capabilities. When the prototypes grew enough, I began to analyze the future project, establish strict rules and developing goals. The prototypes were in C#, so I decided to include the .Net Core platform to make the project cross-platform (besides, at that time it was impossible to create an UI app for Net Core). My wife used Java, so we decided to add the JVM platform to the project. Together, this should give us the following perspectives:

1. Support for the following programming languages:
  - .Net platform: C#, Visual Basic, C++ CLI
  - JVM platform: Java, Scala, Kotlin
2. Support of the following OS:
  - Windows, Linux, Mac OS X
3. The ability to port the system to mobile platforms

And that's quite good. Only one UI building system with the same use for 3 OS types and 6 programming languages. The technology stack wasn't random – the use of OOP and OpenGL made it easy porting the framework to another pair of language+platform: C++, Python and etc.

To make the system flexible and capable of ease porting and development, its modules were strictly separated so that they could be easily replaced if necessary without harming the entire system. These separate modules became the core of the system. These modules are: recipes and rules module (algorithms, interfaces, abstract classes), common layout module, and three basic abstract classes of items. Visualization is divided into a rendering engine with service static classes (for partial rendering management, styling and etc) and a window creation/management system. All systems interact with each other according to strict rules.

This is the essence of the SpaceVIL framework. That is all the system needs, and from these moment it shows its true flexibility.

&#x200B;

# Item system

Items in the framework is the main data packet type. The type passes through all systems and is the base construction material of the framework. Actually, I can remove all the items that already exist in the framework (about 54 items) and leave only three basic. It wouldn't affect workability. It may be hard do understand, but all 54 items are nothing more than a demonstration of the system's capabilities. These are just recipes and instructions. Any framework user can use the framework rules to create their own recipes (items). There are no restrictions, because the kitchen of incredible opportunities is right in front of you.

There are only three main items - *IBaseItem, Primitive*`,` *Prototype*. The first one is the basic template for your own recipe, the second is the recipe implementation for simple non-interactive items, and the third is the *IBaseItem* implementation for interactive items (which receive events, they are also containers for the first two types).

Let's take a look on the system's evolution. I'll show you how to create your own item and will use the button as one of the easiest.

To create our own item, we need to inherit from one of the three basic items. In the case of the button, this is Prototype, because we need the ability to receive events and interact with the item. After that we need to style the button shape directly (or we can use a style system). And that's it, the button is almost ready. The only thing it still needs is the text on it. Buttons usually have some text.

So we need text. The text is a non-interactive item, so for its implementation we can choose Primitive as the base class. According to the framework's rules, many items can have their own interface to let the framework know how to process such recipes (just like in the kitchen – the main course is the first, a drink with it but it's not necessarily, the dessert at the end, etc.). In the case of text, we can use the *ITextContainer* interface. With Primitive and *ITextContainer* we only need to implement a text type item with any convenient approach and any libraries that we want.

Suppose the text item is ready. Now we need to place it into the button. That's easy. Since the button inherits Prototype, it's a container for all *IBaseItem* type items and it has a strong layout system inside. Therefore, we'll use one of the recipe rules – the *InitElements()* implementation. Inside the method, we'll call the *AddItem(text)* method, where text is an instance of *ITextContainer*, which we made earlier.

Then we can add some useful functions to the button class to change the text such as font, position, etc.

So we made the first item in the system that we can use.

The next item will be a button with switching states (toggle button). Since it's almost done, we can just inherit the button and add some logic. As one of the options we can use a boolean variable to define the button's on/off state. Also we need to change a visual state according to the variable. We'll use two color variables – one for the ON state and the other for the OFF state. When does the state change? It changes when we click on the button. To do this, we need to add action to the *EventMouseClick* event inside the overridden method *InitElements()*. This event will switch the button colors or just mix them (do you still remember about service static classes?). That's it, the toggle button is ready.

We already have two items. What else can we do? Using these items we can make a CheckBox.

Also easy to create. We inherit the Prototype and will use the CheckBox as a container for our toggle button and text. In this case, we need to receive the *EventMouseClick* event in CheckBox and redirect it to same event in toggle button. Also, in the overridden method *InitElements()*, we need to add two items – toggle button and text item and set their location inside the CheckBox.

We already have three complex items. Use them, and especially the last one, we can make a RadioButton item. Essentially, RadioButton is similar to CheckBox, but only one RadioButton can be turned ON in the container. It is easily achieved by creating the *UncheckOthers()* method, which will turn all other RadioButtons off if one of them has been turned on. Here is an example algorithm: use the *GetParent()* method to get the RudioButton's container, get the list of container items – *GetItems()* method – and then just turn off all RadionButtons.

It's quite easy.

Using this approach, you can create your own library of items, because the main feature of the framework is not the elements, but the kitchen with the rules and recipes. Using the rules and recipes, you can create items of any complexity and for any purpose.

Now let's talk about the rules and recipes that give us so much flexibility and variability.

&#x200B;

# The main rule

Every item can be in one of two states: created or created and initialized by the system. This means that each item goes through two states: creation and initialization. It is important to note that the basic functionality of an item becomes available only after the initialization state.

**Creation**: an item is created with the initial visualization parameters, and also creates all its internal items (example: constructor (internal items) + instruction (the initial parameters). After creation, the item is not yet completely built (not initialized), so methods such as *AddItem()/RemoveItem()* are not available. Items cannot be added to an uninitialized container.

**Initialization**: this is a process when the item is built from its parts and then it added to the items global storage. When is an item initialized? An item is initialized when it is added into another initialized item. The first initialized item is the window itself of the program. Let's see the code to better understand.

**Valid example** (the code below is part of the window class, the *InitComponents()* method):
```
ButtonCore btn = new ButtonCore(); //item creation stage, the item is NOT initialized yet
ImageItem img = new ImageItem(<some image>); //item creation stage, the item is NOT initialized yet
AddItem(btn); //the btn item is added to the program window, btn is now initialized and its functions are fully available
btn.AddItem(img); //the img item added into the btn item, img is also initialized
```
**Invalid example** (the code below is part of the window class, the *InitComponents()* method):
```
ButtonCore btn = new ButtonCore(); //item creation stage, the item is NOT initialized yet
ImageItem img = new ImageItem(<some image>); //item creation stage, the item is NOT initialized yet
btn.AddItem(img); //trying to add the img item into the btn item. The button has not yet been initialized and attempt throws a runtime exception
AddItem(btn); //the programm will not reach this line
```
This rule is very strict, and sometimes it can be difficult or inconvenient to follow. There are two ways to “trick” the rule (in fact, the system always follows the rule).

**First way:** to wrap items in a higher level item. Using the previous example, we can use *ButtonCore* and *ImageItem* to create a higher level item – *ImagedButton*. Let's look at the implementation:
```
public class ImagedButton : ButtonCore {
  private ImageItem _img = null;
    public ImagedButton(String text, Bitmap picture) {
      SetText(text);
      _img = new ImageItem(picture);
    }
    
    public override void InitElements() {
      base.InitElements(); //item (ImagedButton) initialization stage
      AddItem(_img); //the _img item is added to the button, ImageItem is now initialized
    }
}
```
The main code will change as follows:
```
ImagedButton btn = new ImagedButton("", <some image>); //item creation stage, the item is NOT initialized yet
AddItem(btn); //the btn item is added to the program window, btn is now initialized and its functions are available
```
We followed the rule, but now we have an easier way to add the image to the button.

**Second way:** to override the *AddItem()* method to delay internal initialization:
```
public class MyButton : ButtonCore {
  private List<IBaseItem> _list = new List<IBaseItem>(); //prepare a list
  
  public MyButton(String text) : base(text) { }
  
  public override void AddItem(IBaseItem item) { //override the AddItem method. Now it will add items to the container only after its initialization
    if(item == null) return;
    if(_init) //we can know about item initialization in another way, but using a flag is easier to understand
      base.AddItem(item);
    else
      _list.Add(item); // until an item (MyButton) is not initialized, all internal items are added to the list
  }  
  
  private bool _init = false;
  public override void InitElements() {
    base.InitElements(); //item (MyButton) initialization stage
    foreach(var item in _list)
      base.AddItem(item); //the item has been initialized, and now we can initialize all the internal items stored in the list
      _list = null; //we no longer need this list (or we can save it for some other reasons)
      _init = true; //set the initialization flag as true
  }
}
```
Now the following code (earlier it was **invalid** and threw an exception) will work:
```
ButtonCore btn = new MyButton("My Button"); //item creation stage, the item is NOT initialized yet
ImageItem img = new ImageItem(<any image>); //item creation stage, the item is NOT initialized yet
btn.AddItem(img); //it's ok, the img item added to the list and will be initialized later after the btn item is initialized
AddItem(btn); //item initialization (InitElements method in MyButton), now all internal items from the list _list will be added and initialized
```
Thus, we “trick” the main rule, in fact, we just followed it differently. SpaceVIL has such elements. For example, a *ComboBox* item – its constructor can get any number of *MenuItem* items, and all of them will be initialized after the *ComboBox* is initialized.

Now let's move on to the rules for special items.

&#x200B;

**Containers with special layout that ignore the main layout**

Interfaces:

\- *IHorizantalLayout* \- for realization our own horizontal layout with basic vertical layout. Examples: *HorizontalStack*, *HorizontalScrollBar*, *CheckBox*, *RadioButton*

\- *IVerticalLayout* \- for realization our own vertical layout with basic horizontal layout. Examples: *VerticalStack*, *ListBox*, *TreeView*, *VerticalScrollBar*

\- *IFreeLayout* \- for realization our own vertical and horizontal layout. User must set all layout rules. Examples: Grid, *WrapGrid*, *FreeArea*, *RadialMenu*

Usage rules:

\- Implement one of the interfaces. The *UpdateLayout()* method declares item layout rules (algorithm).

\- According to purpose, override some of the following methods: *SetX/SetY, SetWidth/SetHeight, AddItem/RemoveItem* (obviously these methods should update items layout). Override methods as follows:
```
public override void SetWidth(int value) {
  base.SetWidth(value); //not just override but improve
  UpdateLayout(); //call the method to update the layout of items
}
```
The rules are simple, but the results can be impressive. For example, the Grid layout is not like *WrapGrid*, and *FreeArea* and *RadialMenu* are completely different. Items in *FreeArea* are independent, they can overlap or be hidden outside the container. *RadialMenu* arranges items in a circle with the ability to scroll.

&#x200B;

**Floating independent items**

Interfaces:

\- *IFloatingItem*

Usage rules:

\- Implement interface

\- Inside the class constructor or inside *InitElements()* method add the floating item to the global floating item storage (items are independent and don't have container items to which they can be added):
```
ItemsLayoutBox.AddItem(handler, this, LayoutType.Floating);
```
There are even fewer rules, but with their help you can do interesting things. For example, *ComboBox*, *ContextMenu*, *SideArea* and all types of dialog windows.

Generally, any new item does not belong to only one type. Types are mixed. For example, *ContextMenu* and *RadialMenu* are a mixture of a container and a floating item. Thus, one can create elements of any complexity and for any purpose.

&#x200B;

**Draggable items that receive a EventMouseDrag event**

Interfaces:

\- *IDraggable*

Usage rules:

\- Implement interface

\- The interface is a marker. The system will send the *EventMouseDrag* event to classes marked with this interface.

Like the two previous ones, this type is very useful and helps to create many items, such as *Slider*, *ScrollBar* and any other item need to be held and dragged. Here are some good use cases: *SideArea* (you can expand the visible area), *RadialMenu* (items scroll when the mouse button is held down and the mouse moves), *FreeArea* (you can shift the visible area).

&#x200B;

**Window drag item**

Interfaces:

\- *IWindowAnchor*

Usage rules:

\- Implement interface

\- Like the previous one, this interface is a marker. The system processes classes marked with this interface in a special way. If you hold down the mouse button on such an item, the position of the window will correspond to the movements of the mouse.

Core implementations: *TitleBar* and *WindowAnchor*.

&#x200B;

**Items that are images**

Interfaces:

\- *IImageItem*

Usage rules:

\- Just implement interface.

The main advantage of using this interface (over the standard implementation presented in the framework - *ImageItem*) is improving processing algorithms, parallelization, support for rare formats, etc.

&#x200B;

**Items that are text, or contain text, work with text, etc**

Interfaces:

\- *ITextContainer*

\- *ITextShortcuts*

Usage rules:

\- Implement one or all interfaces

*ITextContainer* is used to render text to texture. *ITextShortcuts* is an additional marker for special processing by the system. This interface includes methods for implementing standard text shortcuts: copy, paste, cut, select all, undo, redo.

These rules are designed to create unique items of any complexity. By combining the rules, you can create items that realize any of your ideas, starts with a text editor and ending with a graphic editor.

There are other rules designed not to create, but to configure or manage items or system. For example, window management, styling items, creation/edition/addition style themes for an application, creating and managing special effects, managing visual state of the item, rules for creating vector shapes, many service classes for implementing developer ideas, rules for event processing system, rules for caching and rendering optimization, items focus control rules, rules for two-layer rendering, etc. You don't need a deep knowledge of the system to use most of this features, because they are intuitive and work as you expect.

What has been described looks too much, but keep in mid that you will not need to learn most of the features of the system. I tried to make a “quick start” framework. You don't need deep knowledge to start working with the system. Just look at the contents of the framework, its methods and items, and you'll understand how it works. To make the development of new items interesting, the framework contains more than 54 different items that one can improved, inherited, edited and just know that such items can be created using SpaceVIL. The main thing you have to remember: the system requires a choice of recipes and following the rules, and the graphics engine will draw all your items in accordance with the general rules without any pitfalls.

Now let's see how the styling of items works. The styling module consists of three parts – theme, style, state. Using them all together, you can effectively control the visual interactivity of items.

&#x200B;

# Application style themes

The style theme is a set of styles (or storage) for each item used in an application. The system will “automatically” use the style to newly created item if it is present in the current theme.

But to make it clearer, we first consider the main stages of preparing the framework for work. Here are four common steps to do this:

1. Initializing framework components via *Common.CommonService.InitSpaceVILComponents()* at the program entry point (Main method)At this stage, the OS is checked, the availability of libraries and OS dependencies are also checked. The basic state of the system is initialized, including the base theme for all items in the framework.
2. The window class is created and initialized (*InitWindow()* method) with *ActiveWindow* as the basis.This is usually a step to customize the window and place items.
3. A window instance is created at the program entry point (*Main* method)
4. Call the *Show()* method of the window, either directly, or using a window manager (*WindowManager*), or using the global window store (*WindowsBox*).

All developer actions to configure SpaceVIL must be performed between the first and the second stages. For example, replacing main SpaceVIL style theme of styles with developer style theme, changing or replacing the basic styles in the current theme, changing the default SpaceVIL global settings and etc.

Now consider the case when a developer uses only elements that are built into the framework or their combinations (usually, if a wrapper-element is created, a separate style is not created for it). Let’s say that developer is not satisfied with the basic stylization of the button element (in the basic style, it has blue color, sharp corners and no border) and the developer would like to change the style a little, for example, the color of the button, sizes and add rounded edges. Since the changes are minor, the developer can use the style change method in the current theme to achieve this.
```
DefaultsService.GetDefaultStyle(typeof(SpaceVIL.ButtonCore)).Background = Color.Gray; //change the color of the button, after that all instances of the button class will be created with gray color
DefaultsService.GetDefaultStyle(typeof(SpaceVIL.ButtonCore)).SetSize(100, 35); //now all buttons will be created with the specified size
DefaultsService.GetDefaultStyle(typeof(SpaceVIL.ButtonCore)).BorderRadius = new CornerRadius(8); //all buttons will be created withround corners
```
But what if are there too many changes? Or do you even have to change the internal styles? Then it’s better to create your own style and replace in the base theme as follows:

* create a method that will return your new style.
```
public static Style GetButtonStyle() {
  Style style = Style.GetButtonCoreStyle(); //there is no need to create a style from scratch, but we can significantly change it
  style.Background = Color.FromArgb(255, 13, 176, 255); //background color
  style.Foreground = Color.Black; //text color
  style.BorderRadius = new CornerRadius(6); //corner border radius
  style.Font = DefaultsService.GetDefaultFont(FontStyle.Regular, 18); //take the default font and set it a new style and size
  style.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand); //a button will occupy all available space
  style.SetAlignment(ItemAlignment.HCenter,ItemAlignment.VCenter); //set button alignment to center
  style.SetTextAlignment(ItemAlignment.HCenter,ItemAlignment.VCenter); //set text alignment in the button to center
  style.ItemStates.Add(ItemStateType.Hovered, new ItemState(Color.FromArgb(60, 255, 255, 255))); //change the state of a button on hover
  return style;
}
```
* replace the button style in the default style theme with our own style.
```
DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(SpaceVIL.ButtonCore), GetButtonStyle());
```
Done, style replaced. All newly created buttons will get a new look (if for some reason the buttons were created before replacing the style, they will remain with the old style).

Let's take a close look at the line "*Style style = Style.GetButtonCoreStyle();*". Why haven't I used "*new Style();*"? In fact, everything is simple, the Style class is very voluminous and you need to remember it well, since some properties of the class are strictly required and if I created style from scratch, then only for a completely new element and since I only need to change the appearance of the button (in fact, modify the existing basic style), then this is the cheapest option. It is simpler, less time is spent and the possibility of making mistakes in the style is excluded (basic styles are always correctly filled).

Of course, you can create, modify, replace and apply your own style themes in your application. For example, if you want different themes for different OS or at the request of the user.

Registering a style for your own element in the current theme is also simple. All you need to do is create a style, add this style to the base theme, apply style (I recommend apply the style at the end of the constructor) and (if you create a complex element) override *SetStyle()* method.

&#x200B;

# The states system of elements and ways of user interaction with the element

The user usually does not have many ways to interact with interactive elements, in usually there are only 6:

* *ItemStateType.Base* // Basic static idle state
* *ItemStateType.Hovered* // hover state
* *ItemStateType.Pressed* // pressed state
* *ItemStateType.Toggled* // toggled state (on/off)
* *ItemStateType.Focused* // focused state when an element receives events from the keyboard
* *ItemStateType.Disabled* // disabled state, when an element ignores all events

The states system applies only to interactive elements and can be completely ignored by the developer. The developer has the right to implement his own state system.

Using such a system is very simple, for each interactive element the following basic methods are available:

* adding a new state via *AddItemState(ItemStateType.Hovered, state)* where state is an instance of the *ItemState* class (greatly truncated compared to the *Style* class)
* state removal via *RemoveItemState(ItemStateType.Hovered)*

The state system is also available through styling of elements and is rarely used in isolation from it.

As mentioned earlier the framework has fairly simple rules and that is why the adding of a markup system through files such as *xml* and *json* (planned in the future) will be a simple and tedious task.

SpaceVIL is a powerful, flexible and easy to use UI framework that can cover up to 80-90% of all types of desktop programs. Developed by just two programmers.

We will be glad if you try our framework in action and tell us your opinion about it.

&#x200B;
