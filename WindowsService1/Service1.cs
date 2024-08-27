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

namespace WindowsService1
{
  public partial class Service1 : ServiceBase
  {
    private Timer timer;
    public Service1()
    {
      InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
      if (timer == null)
      {
        timer = new Timer();
      }

      timer.Interval = 50000;
      timer.Elapsed += Timer_Elapsed;

    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      timer.Stop();

      Console.WriteLine("Windows servis çalıştı");
    }

    protected override void OnStop()
    {
    }
  }
}
