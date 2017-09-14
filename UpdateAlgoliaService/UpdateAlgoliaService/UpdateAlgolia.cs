using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace UpdateAlgoliaService
{
    public partial class UpdateAlgolia : ServiceBase
    {
        private Timer MinuteTimer = null;

        public UpdateAlgolia()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            MinuteTimer = new Timer();
            this.MinuteTimer.Interval = 60000; //every 30 secs
            this.MinuteTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.MinuteTimer_Update);
            MinuteTimer.Enabled = true;
            UpdateAlgoliaObjects.WriteErrorLog("Algolia Objects Upadte Service started");
        }

        private void MinuteTimer_Update(object sender, ElapsedEventArgs e)
        {
            UpdateAlgoliaObjects UAO = new UpdateAlgoliaObjects();
            UAO.UpdateJsonSQLObjects();
            //Write code here to do some job depends on your requirement
            UpdateAlgoliaObjects.WriteErrorLog("Updated OBjects sucessfully..");
        }

        protected override void OnStop()
        {
            MinuteTimer.Enabled = false;
            UpdateAlgoliaObjects.WriteErrorLog("Algolia Objects Upadte Service stopped");
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
