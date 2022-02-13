using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Taskbar
{
    /******************************************************************/
    public class Resources : INotifyPropertyChanged
    {
        /* CPU Value */
        private string _cpu_value = "0 %";

        public string cpu_value
        {
            get { return _cpu_value; }
            set
            {
                _cpu_value = value;
                OnPropertyChanged("cpu_value");
            }
        }

        /* GPU Value */
        private string _gpu_value = "0 %";

        public string gpu_value
        {
            get { return _gpu_value; }
            set
            {
                _gpu_value = value;
                OnPropertyChanged("gpu_value");
            }
        }

        /* RAM Value */
        private string _ram_value = "0 %";

        public string ram_value
        {
            get { return _ram_value; }
            set
            {
                _ram_value = value;
                OnPropertyChanged("ram_value");
            }
        }

        /* Value change listener */
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        /******************************************************************/
        public async void GetResources(int delay = 1000)
        {
            /* Declare Resource Classes*/
            CPU cpu = new CPU();
            GPU gpu = new GPU();
            RAM ram = new RAM();

            /* Asynchronously pull resource values delaying each loop */
            await Task.Run(() =>
            {
                while (true)
                {
                    cpu_value = cpu.GetCpuValue();
                    gpu_value = gpu.GetGpuValue();
                    ram_value = ram.GetRamValue();

                    System.Threading.Thread.Sleep(delay);
                }
            });
        }
        /******************************************************************/
    }
    /******************************************************************/

    /******************************************************************/
    public class CPU 
    {
        /******************************************************************/
        public string GetCpuValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");

                var cpuValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    PercentProcessorTime = Double.Parse(mo["PercentProcessorTime"].ToString())
                }).FirstOrDefault();

                return cpuValues.PercentProcessorTime.ToString("F0") + " %";
            }
            catch
            {
                return "0 %";
            }
        }
        /******************************************************************/
    }
    /******************************************************************/

    /******************************************************************/
    public class GPU
    {
        /******************************************************************/
        public string GetGpuValue()
        {
            try
            {
                var category = new PerformanceCounterCategory("GPU Engine");
                var counterNames = category.GetInstanceNames();
                var gpuCounters = new List<PerformanceCounter>();
                var result = 0f;

                foreach (string counterName in counterNames)
                {
                    if (counterName.EndsWith("engtype_3D"))
                    {
                        foreach (PerformanceCounter counter in category.GetCounters(counterName))
                        {
                            if (counter.CounterName == "Utilization Percentage")
                            {
                                gpuCounters.Add(counter);
                            }
                        }
                    }
                }

                gpuCounters.ForEach(x =>
                {
                    _ = x.NextValue();
                });

                gpuCounters.ForEach(x =>
                {
                    result += x.NextValue();
                });

                return result.ToString("F0") + " %";
            }
            catch
            {
                return "0 %";
            }
        }
        /******************************************************************/
    }
    /******************************************************************/

    /******************************************************************/
    public class RAM
    {
        /******************************************************************/
        public string GetRamValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

                var memoryValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    FreePhysicalMemory = Double.Parse(mo["FreePhysicalMemory"].ToString()),
                    TotalVisibleMemorySize = Double.Parse(mo["TotalVisibleMemorySize"].ToString())
                }).FirstOrDefault();

                var percent = ((memoryValues.TotalVisibleMemorySize - memoryValues.FreePhysicalMemory) / memoryValues.TotalVisibleMemorySize) * 100;

                return percent.ToString("F0") + " %";
            }
            catch
            {
                return "0 %";
            }
        }
        /******************************************************************/
    }
    /******************************************************************/

    /******************************************************************/
    public class Disk
    {
        /******************************************************************/
        public string GetDiskReadValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfDisk_PhysicalDisk");

                var diskReadValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    DiskReadRate = Double.Parse(mo["DiskReadBytesPersec"].ToString())
                }).FirstOrDefault();

                return diskReadValues.DiskReadRate.ToString("F0") + " B/s";
            }
            catch
            {
                return "0 B/s";
            }
        }
        /******************************************************************/

        /******************************************************************/
        public string GetDiskWriteValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfDisk_PhysicalDisk");

                var diskWriteValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    DiskWriteRate = Double.Parse(mo["DiskWriteBytesPersec"].ToString())
                }).FirstOrDefault();

                return diskWriteValues.DiskWriteRate.ToString("F0") + " B/s";
            }
            catch
            {
                return "0 B/s";
            }
        }
        /******************************************************************/
    }
    /******************************************************************/

    /******************************************************************/
    public class Network
    {

    }
    /******************************************************************/
}
