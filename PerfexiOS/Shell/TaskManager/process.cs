using Cosmos.HAL;
using Cosmos.System;
using PerfexiOS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace PerfexiOS.Shell.TaskManager
{
    public class process
    {
        public int id ; 
        public string name; 
        public string description;
        public List<process> Instances;
        public DateTime created = DateTime.Now;

        public enum state
        {
            Starting,
            Running,
            Stopped,
            Crashed,
            Halt,
        }
        public state State;

        public process(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Start the appplication
        /// </summary>
        public virtual void start()
        {
            State = state.Starting;
            State = state.Running;
        }
        /// <summary>
        /// The Main Loop of the application
        /// Runs every frame while the application is running
        /// </summary>


        public virtual void loop()
        {
            
        }
        /// <summary>
        /// The stopping function of the application
        /// This will stop the application and then it will be eventualy sweapt away by the task
        /// Manager 
        /// </summary>

        public virtual void stop()
        {
            State = state.Stopped;
        }

        /// <summary>
        /// Halting is used to pause the application 
        /// in it's current State.
        /// An Appliation HALTS when it is not Focused.
        /// </summary>
        public virtual void HALT()  
        {
            State = state.Halt;
        }



        public Signal<KeyboardArgs> OnKeyboardInput;
    }
}
