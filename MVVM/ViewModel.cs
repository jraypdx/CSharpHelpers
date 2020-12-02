using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyProgram
{
    /*
     * Important note: Viewmodel has to be set on the View (via DataContext) for bindings to work.  This can be done in several different ways, ex:
     *	Creating a new object of the view and passing in an already created or new ViewModel object:
     *		var view = new View() { DataContext = MyViewModelObject };  -OR-  view.DataContext = MyViewModelObject
     *	If you don't need to access the ViewModel from anywhere else, and just use it for UI control:
     *		var view = new View() { DataContext = new ViewModel() };  -OR-  view.DataContext = new ViewModel;
     *	Similar to above, can also set the type directly in the View.xaml without having to create an object or use code
     *		At top of View.xaml (below the <UserControl ..options...options>)
     *		<UserControl.DataContext>
     *			<local:ViewModel /> <!-- where local is the namespace for your ViewModel ex. xmlns:local="clr-namespace:MyProgram.ViewModels" -->
     *		</UserControl.DataContext>
     */
    class ViewModel : ViewModelBase
    {
        public ICommand BasicButton_Command { get; }

        private string _TextBoxString; //This can go in the _model object as well if it will need to access it (also best in general to store data + working functions in the model and keep the VM to UI stuff)
	public string TextBoxString 
	{ 
	    get => _TextBoxString; //If you move to model, change this and the set to '_model._TextBoxString' or whatever variable name you choose
	    set
	    {
	        _TextBoxString = value;
	        OnPropertyChanged(); //When this is triggered, the property gets updated on the view (can also trigger manually)
	    }
	}

	//An object of the model, which holds functions to call.  If it needs to be able to update anything in here/view (logs, etc.) then we can pass it events
	private Model _model; 

        public PackageSigningViewModel()
        {
	    //Can also pass a 'CommandParamater' from view and use 'o => BasicButton_Function(o)' - or ...Function((string)o), etc.
            BasicButton_Command = new DelegateCommand(o => BasicButton_Function());
        }


        private async void Basicbutton_Function()
        {
            MessageBox.Show("Button clicked");
	    TextBoxString = "Button clicked";
        }

    }

}
