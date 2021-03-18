# CSharpHelpers
I'll be adding helpful tools and tricks I find for C#, as well as templates and objects I make that I end up reusing a lot




## MVVM

Very basic MVVM implementation
View.xaml - A UI file with DataContext bindings (ViewModel bindings)
ViewModel.cs - Handles things from the View/UI such as calling functions when a button is pressed, properties for binding to text/bools/visibilities/etc.
Model.cs - A basic model with the setup for events
ViewModelBase.cs - A parent class that can be inherited by any ViewModels you make, contains the MVVM code common in all ViewModels
DelegateCommand.cs - Object for making commands (usually button presses) that can pass in parameters


## Runtime_Assembly_Loading

Hot to load assemblies at runtime in WPF applications, either from specified folders or from embedded assemblies.  Also how to get a stack trace for unhandled exceptions rather than the program crashing with no details.


## Binding_Converters

A few binding converters I regularly use in MVVM.  They help with things such as binding a bool when a Visibility is needed.


## Timed_Popup_UserControl

This is a custom popup UserControl overlay for displaying messages to users or getting any kind of input.  It can be customized to have any assortment of messages, buttons, textboxes, comboboxes, etc., and it can be closed by clicking outside of it.  The highlight of this control is that when calling it, you give it a time in seconds to display, and if the user has not closed it or triggered it to close (ex. by pressing a button) in that time then it hides itself.

![image](https://user-images.githubusercontent.com/44383003/111663343-f1d14000-87cd-11eb-935e-f070b5207701.png)

Here is an example that can pause/play, cancel, or kill a task that is running.  Notice the countdown in the top right.  Due to WPF the countdown isn't super accurate, but is accurate enough for something like this.


### TODO

List of things I need to remember to pull and scrub from work projects to add here:
 - Standalone exe (bundling external assemblies, icons, and files)
 - Custom app (WPF) themes and colors by using DynamicResources and overriding styles
