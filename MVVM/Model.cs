using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MyProgram.Models
{
    class Model //Empty model object with an example of how to pass an event in to here (using a logging with a Log object example)
    {
	//Set the event in the ViewModel by using '_model.logUpdateHandler += AddLog_Event;' where AddLog_Event is just a function that takes a Log object, and adds it to our ObservableCollection<Log>
        public delegate void LogUpdateHandler(Log e);
        public event LogUpdateHandler logUpdateHandler;


        public Model()
        {
        }

	//These are functions that call the LogUpdateHandler event which sends the Log back to the View Model
        private void PrintLog(string message) { Application.Current.Dispatcher.Invoke(delegate { logUpdateHandler?.Invoke(new Log(message, Log.LogLevelOption.Debug)); }); } //Print a regular log
        private void PrintWarning(string message) { Application.Current.Dispatcher.Invoke(delegate { logUpdateHandler?.Invoke(new Log(message, Log.LogLevelOption.Warning)); }); } //Print an orange log
        private void PrintError(string message) { Application.Current.Dispatcher.Invoke(delegate { logUpdateHandler?.Invoke(new Log(message, Log.LogLevelOption.Error)); }); } //Print a red log
    }

}
