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



### TODO

List of things I need to remember to pull and scrub from work projects to add here:
 - Timed popup usercontrol that closes when you click out of it
 - Standalone exe (bundling external assemblies, icons, and files)
 - Custom app (WPF) themes and colors by using DynamicResources and overriding styles
 - Various convertors I found helpful
