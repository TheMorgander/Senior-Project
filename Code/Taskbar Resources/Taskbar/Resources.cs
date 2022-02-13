using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
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

        /* Disk Read Value */
        private string _disk_read_value = "0 B/s";

        public string disk_read_value
        {
            get { return _disk_read_value; }
            set
            {
                _disk_read_value = value;
                OnPropertyChanged("disk_read_value");
            }
        }

        /* Disk Write Value */
        private string _disk_write_value = "0 B/s";

        public string disk_write_value
        {
            get { return _disk_write_value; }
            set
            {
                _disk_write_value = value;
                OnPropertyChanged("disk_write_value");
            }
        }

        /* Network Upload Value */
        private string _network_upload_value = "0 B/s";

        public string network_upload_value
        {
            get { return _network_upload_value; }
            set
            {
                _network_upload_value = value;
                OnPropertyChanged("network_upload_value");
            }
        }

        /* Network Download Value */
        private string _network_download_value = "0 B/s";

        public string network_download_value
        {
            get { return _network_download_value; }
            set
            {
                _network_download_value = value;
                OnPropertyChanged("network_download_value");
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
        public async void GetResources(int delay)
        {
            /* Declare Resource Classes*/
            CPU cpu = new CPU();
            GPU gpu = new GPU();
            RAM ram = new RAM();
            Disk disk = new Disk();
            Network network = new Network();

            /* Asynchronously pull resource values delaying each loop */
            await Task.Run(() =>
            {
                while (true)
                {
                    cpu_value = cpu.GetCpuValue();
                    gpu_value = gpu.GetGpuValue();
                    ram_value = ram.GetRamValue();
                    disk_read_value = disk.GetDiskReadValue();
                    disk_write_value = disk.GetDiskWriteValue();
                    network_upload_value = network.GetNetworkUploadValue();
                    network_download_value = network.GetNetworkDownloadValue();
                    System.Threading.Thread.Sleep(100);
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

                double cpu = cpuValues.PercentProcessorTime;

                return cpu.ToString("F0") + " %";
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

                double memory = memoryValues.FreePhysicalMemory;
                double total_memory = memoryValues.TotalVisibleMemorySize;
                double percent = ((total_memory - memory) / total_memory) * 100;

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

                double disk_read = diskReadValues.DiskReadRate;

                if (disk_read < 1024)
                {
                    return disk_read.ToString("F0") + " B/s";
                }
                else if (disk_read < 1024*1024)
                {
                    return (disk_read / (1024)).ToString("F0") + " KB/s";
                }
                else if (disk_read < 1024*1024*1024)
                {
                    return (disk_read / (1024*1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    return (disk_read / (1024*1024*1024)).ToString("F0") + " GB/s";
                }
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

                double disk_write = diskWriteValues.DiskWriteRate;

                if (disk_write < 1024)
                {
                    return disk_write.ToString("F0") + " B/s";
                }
                else if (disk_write < 1024*1024)
                {
                    return (disk_write / (1024)).ToString("F0") + " KB/s";
                }
                else if (disk_write < 1024*1024*1024)
                {
                    return (disk_write / (1024*1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    return (disk_write / (1024*1024*1024)).ToString("F0") + " GB/s";
                }
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
        /******************************************************************/
        public string GetNetworkUploadValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_Tcpip_NetworkInterface");
                var networkUploadValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    NetworkUpload = Double.Parse(mo["BytesSentPerSec"].ToString())
                }).FirstOrDefault();

                double network_upload = networkUploadValues.NetworkUpload;

                if (network_upload < 1024)
                {
                    return network_upload.ToString("F0") + " B/s";
                }
                else if (network_upload < 1024 * 1024)
                {
                    return (network_upload / (1024)).ToString("F0") + " KB/s";
                }
                else if (network_upload < 1024 * 1024 * 1024)
                {
                    return (network_upload / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    return (network_upload / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                return "0 B/s";
            }
        }
        /******************************************************************/

        /******************************************************************/
        public string GetNetworkDownloadValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_Tcpip_NetworkInterface");
                var networkDownloadValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    NetworkDownload = Double.Parse(mo["BytesReceivedPerSec"].ToString())
                }).FirstOrDefault();

                double network_download = networkDownloadValues.NetworkDownload;

                if (network_download < 1024)
                {
                    return network_download.ToString("F0") + " B/s";
                }
                else if (network_download < 1024 * 1024)
                {
                    return (network_download / (1024)).ToString("F0") + " KB/s";
                }
                else if (network_download < 1024 * 1024 * 1024)
                {
                    return (network_download / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    return (network_download / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                return "0 B/s";
            }
        }
        /******************************************************************/
    }
    /******************************************************************/
}
