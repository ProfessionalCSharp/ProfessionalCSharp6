using System;
using System.ServiceProcess;

namespace ServiceControlWPF
{
  public class ServiceControllerInfo
  {
    public ServiceControllerInfo(ServiceController controller)
    {
      Controller = controller;
    }

    public ServiceController Controller { get; }

    public string ServiceTypeName
    {
      get
      {
        ServiceType type = Controller.ServiceType;
        string serviceTypeName = String.Empty;
        if ((type & ServiceType.InteractiveProcess) != 0)
        {
          serviceTypeName = "Interactive ";
          type -= ServiceType.InteractiveProcess;
        }
        switch (type)
        {
          case ServiceType.Adapter:
            serviceTypeName += "Adapter";
            break;

          case ServiceType.FileSystemDriver:
          case ServiceType.KernelDriver:
          case ServiceType.RecognizerDriver:
            serviceTypeName += "Driver";
            break;

          case ServiceType.Win32OwnProcess:
            serviceTypeName += "Win32 Service Process";
            break;

          case ServiceType.Win32ShareProcess:
            serviceTypeName += "Win32 Shared Process";
            break;

          default:
            serviceTypeName += "unknown type " + type.ToString();
            break;
        }
        return serviceTypeName;
      }
    }

    public string ServiceStatusName
    {
      get
      {
        switch (Controller.Status)
        {
          case ServiceControllerStatus.ContinuePending:
            return "Continue Pending";
          case ServiceControllerStatus.Paused:
            return "Paused";
          case ServiceControllerStatus.PausePending:
            return "Pause Pending";
          case ServiceControllerStatus.StartPending:
            return "Start Pending";
          case ServiceControllerStatus.Running:
            return "Running";
          case ServiceControllerStatus.Stopped:
            return "Stopped";
          case ServiceControllerStatus.StopPending:
            return "Stop Pending";
          default:
            return "Unknown status";
        }
      }
    }

    public string DisplayName => Controller.DisplayName;

    public string ServiceName => Controller.ServiceName;

    public bool EnableStart => Controller.Status == ServiceControllerStatus.Stopped;

    public bool EnableStop => Controller.Status == ServiceControllerStatus.Running;

    public bool EnablePause => Controller.Status == ServiceControllerStatus.Running && Controller.CanPauseAndContinue;

    public bool EnableContinue => Controller.Status == ServiceControllerStatus.Paused;

  }
}
