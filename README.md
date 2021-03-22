# CSharpHelpers
I'll be adding helpful tools and tricks I find for C#, as well as templates and objects I make that I end up reusing a lot


<p>&nbsp;</p>


## MVVM

Very basic MVVM implementation
View.xaml - A UI file with DataContext bindings (ViewModel bindings)
ViewModel.cs - Handles things from the View/UI such as calling functions when a button is pressed, properties for binding to text/bools/visibilities/etc.
Model.cs - A basic model with the setup for events
ViewModelBase.cs - A parent class that can be inherited by any ViewModels you make, contains the MVVM code common in all ViewModels
DelegateCommand.cs - Object for making commands (usually button presses) that can pass in parameters


<p>&nbsp;</p>


## Runtime_Assembly_Loading

Hot to load assemblies at runtime in WPF applications, either from specified folders or from embedded assemblies.  Also how to get a stack trace for unhandled exceptions rather than the program crashing with no details.


<p>&nbsp;</p>


## Binding_Converters

A few binding converters I regularly use in MVVM.  They help with things such as binding a bool when a Visibility is needed.


<p>&nbsp;</p>


## Timed_Popup_UserControl

This is a custom popup UserControl overlay for displaying messages to users or getting any kind of input.  It can be customized to have any assortment of messages, buttons, textboxes, comboboxes, etc., and it can be closed by clicking outside of it.  The highlight of this control is that when calling it, you give it a time in seconds to display, and if the user has not closed it or triggered it to close (ex. by pressing a button) in that time then it hides itself.

![image](https://user-images.githubusercontent.com/44383003/111663343-f1d14000-87cd-11eb-935e-f070b5207701.png)

Here is an example that can pause/play, cancel, or kill a task that is running.  Notice the countdown in the top right.  Due to WPF the countdown isn't super accurate, but is accurate enough for something like this.


<p>&nbsp;</p>


## Standalone WPF executable
#### AKA how to package everything in to your .exe

There are 3 different ways to package resources in to your .exe to make a standalone executable.

#### 1. Icons and image files
Add icons and image files to a folder in your solution by right clicking the folder and selecting "Add" -> "Existing item..." -> Set the file filter to all file types -> select your file.

Once added to your project, right click on the file in the solution explorer, and select properties.  Set the "Build Action" to "Resource", and set the "Copy to Output Directory" to the "Do not copy" option.

To reference this resource statically in .xaml, you can use "/ResourceFolderName/ResourceFileName.extension"

You can also add this resource to your App.xaml and use it as a DynamicResource, where it can be retrieved using <DynamicResource ResourceName> in xaml, and be retrieved and set using App.Current.Resources["ResourceName"] in code.  App.xaml example for an .png: <ImageSource x:Key="myResourceKEy">../Resources/myResourceFile.png</ImageSource>

#### 2. Text files and scripts
Text files and scripts (.ps1, .bat, etc.) can be added to a folder in your solution explorer the same way as icons and images, also selecting "Resource" and "Do not copy" for the properties.

Once added, open the Properties -> Resources.resx file, and drag the new file from the solution explorer in to the open area.  Note:  You can also use the "Add Resource" button here to import the file and add it at the same time, you will just need to move it to where you want in the solution explorer later.

Added there, you can now write the script from the app using: File.WriteAllBytes(scriptPathToSave, Properties.Resources.scriptFileNameWithoutExtension);


An alternative method that allows you to read files in to variables:
Add the file as an "Embedded Resource", and don't add it to the Properties -> Resources.resx file.

Access it in your app using something like:
string[] linesOfTxtFile = (new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Example_App.myTextFile.txt")).ReadToEnd()).Split('\n');

#### 3. Assemblies and Nuget packages
This applies to assemblies that you would like to embed in your app that get copied to your bin folder by default.  I generally use it for Nuget packages.  Note that when doing this, you will have to redo the steps every time you update an embedded Nuget package.

Go to your References and find the packages that you want to embed, right click on them and view their properties to find the path that they are located.

Right click on a designated Packages folder in your solution explorer, and "Add" -> "Existing item..." -> Navigate to and select the .dll

Once added, right click the .dll and set the "Build Action" to "Embedded Resource", and set the "Copy to Output Directory" to "Do not Copy"

Find the assembly in your References, and rset the "Copy Local" to "False" in its properties

Follow the steps under the "Runtime_Assembly_Loading" to load embedded assemblies at runtime in your App.xaml.cs file


<p>&nbsp;</p>


## Custom WPF Colors
Contains an example on how to use custom colors for your app.  This example is built on the idea that there is a dropdown to change an "Accent" color in the app, and a way to toggle dark/light mode.  You can then use custom styling, setting backgrounds/foregrounds/etc of controls to use {DynamicResource Accent} or other colors that you choose.

There is also a bit of code on how to swap icons that are being used for dark/light mode, assuming that you have a copy of each.  Ex. I had a copy of the inverted icons for use in dark mode in a folder named "Negatives" in the icon folder.  (Requires you to set up a ResourceDictionary for your icons, and add it to App.xaml)


<p>&nbsp;</p>


### TODO

List of things I need to remember to pull and scrub from work projects to add here:
 - Possible idea: Make a few basic timed popups and add to a new repo, maybe make a nuget package for them
 - Clean up this readme and repo to make it easier to read and navigate
