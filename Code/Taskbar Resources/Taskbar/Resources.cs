using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Taskbar
{
    /******************************************************************/
    public class Resources : INotifyPropertyChanged
    {

        //Values for storing history, move to History class in further development
        private int timer;

        private int cpu_counter;
        private int cpu_max;
        private int cpu_min;

        private int gpu_counter;
        private int gpu_max;
        private int gpu_min;

        private int ram_counter;
        private int ram_max;
        private int ram_min;

        private int disk_upload_counter;
        private int disk_upload_max;
        private int disk_upload_min;

        private int disk_download_counter;
        private int disk_download_max;
        private int disk_download_min;

        private int network_upload_counter;
        private int network_upload_max;
        private int network_upload_min;

        private int network_download_counter;
        private int network_download_max;
        private int network_download_min;

        public int timeout = 1000;

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
            timeout = 1;

            /* Asynchronously pull resource values delaying each loop */
            await Task.Run(() =>
            {
                while(true)
                {
                    GetCpuValue();
                    GetGpuValue();
                    GetRamValue();
                    GetDiskReadValue();
                    GetDiskWriteValue();
                    GetNetworkUploadValue();
                    GetNetworkDownloadValue();
                    
                    //TEMP, change based on elapsed time later
                    timer++;

                    //Add new values to database if enough time has elapsed
                    if (timer > timeout)
                    {
                        string cpu_query = "INSERT INTO CPU(Time, Max, Min, Average) VALUES('" +
                        DateTime.Now + "'," +
                        cpu_max + "," +
                        cpu_min + "," +
                        cpu_counter / timer + ");";

                        string gpu_query = "INSERT INTO GPU(Time, Max, Min, Average) VALUES('" +
                        DateTime.Now + "'," +
                        gpu_max + "," +
                        gpu_min + "," +
                        gpu_counter / timer + ");";

                        string ram_query = "INSERT INTO RAM(Time, Max, Min, Average) VALUES('" +
                        DateTime.Now + "'," +
                        ram_max + "," +
                        ram_min + "," +
                        ram_counter / timer + ");";

                        string disk_upload_query = "INSERT INTO DISK_UPLOAD(Time, Max, Min, Average) VALUES('" +
                        DateTime.Now + "'," +
                        disk_upload_max + "," +
                        disk_upload_min + "," +
                        disk_upload_counter / timer + ");";

                        string disk_download_query = "INSERT INTO DISK_DOWNLOAD(Time, Max, Min, Average) VALUES('" +
                        DateTime.Now + "'," +
                        disk_download_max + "," +
                        disk_download_min + "," +
                        disk_download_counter / timer + ");";

                        string network_upload_query = "INSERT INTO NETWORK_UPLOAD(Time, Max, Min, Average) VALUES('" +
                        DateTime.Now + "'," +
                        network_upload_max + "," +
                        network_upload_min + "," +
                        network_upload_counter/ timer + ");";

                        string network_download_query = "INSERT INTO NETWORK_DOWNLOAD(Time, Max, Min, Average) VALUES('" +
                        DateTime.Now + "'," +
                        network_download_max + "," +
                        network_download_min + "," +
                        network_download_counter / timer + ");";

                        History.Insert(cpu_query);
                        History.Insert(gpu_query);
                        History.Insert(ram_query);
                        History.Insert(disk_upload_query);
                        History.Insert(disk_download_query);
                        History.Insert(network_upload_query);
                        History.Insert(network_download_query);
                    }

                    //TEMP
                    System.Threading.Thread.Sleep(500);
                }
                
            });
        }
        /******************************************************************/

        /******************************************************************/
        public void GetCpuValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
                var cpuValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    PercentProcessorTime = Double.Parse(mo["PercentProcessorTime"].ToString())
                }).FirstOrDefault();

                double cpu = cpuValues.PercentProcessorTime;

                cpu_counter += (int)cpu;
                if (cpu_max < cpu)
                {
                    cpu_max = (int)cpu;
                }
                if (cpu_min > cpu)
                {
                    cpu_min = (int)cpu;
                }

                cpu_value = cpu.ToString("F0") + " %";
            }
            catch
            {
                cpu_value = "0 %";
            }
        }
        /******************************************************************/

        /******************************************************************/
        public void GetGpuValue()
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

                gpu_counter += (int)result;
                if (gpu_max < result)
                {
                    gpu_max = (int)result;
                }
                if (gpu_min > result)
                {
                    gpu_min = (int)result;
                }

                gpu_value = result.ToString("F0") + " %";
            }
            catch
            {
                gpu_value = "0 %";
            }
        }
        /******************************************************************/

        /******************************************************************/
        public void GetRamValue()
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

                ram_counter += (int)percent;
                if (ram_max < percent)
                {
                    ram_max = (int)percent;
                }
                if (ram_min > percent)
                {
                    ram_min = (int)percent;
                }

                ram_value = percent.ToString("F0") + " %";
            }
            catch
            {
                ram_value = "0 %";
            }
        }
        /******************************************************************/

        /******************************************************************/
        public void GetDiskReadValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfDisk_PhysicalDisk");
                var diskReadValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    DiskReadRate = Double.Parse(mo["DiskReadBytesPersec"].ToString())
                }).FirstOrDefault();

                double disk_read = diskReadValues.DiskReadRate;

                disk_upload_counter += (int)disk_read;
                if (disk_upload_max < disk_read)
                {
                    disk_upload_max= (int)disk_read;
                }
                if (disk_upload_min> disk_read)
                {
                    disk_upload_min = (int)disk_read;
                }

                if (disk_read < 1024)
                {
                    disk_read_value = disk_read.ToString("F0") + " B/s";
                }
                else if (disk_read < 1024 * 1024)
                {
                    disk_read_value = (disk_read / (1024)).ToString("F0") + " KB/s";
                }
                else if (disk_read < 1024 * 1024 * 1024)
                {
                    disk_read_value = (disk_read / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    disk_read_value = (disk_read / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                disk_read_value = "0 B/s";
            }
        }
        /******************************************************************/

        /******************************************************************/
        public void GetDiskWriteValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfDisk_PhysicalDisk");
                var diskWriteValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    DiskWriteRate = Double.Parse(mo["DiskWriteBytesPersec"].ToString())
                }).FirstOrDefault();

                double disk_write = diskWriteValues.DiskWriteRate;

                disk_download_counter += (int)disk_write;
                if (disk_download_max < disk_write)
                {
                    disk_download_max = (int)disk_write;
                }
                if (disk_download_min > disk_write)
                {
                    disk_download_min = (int)disk_write;
                }

                if (disk_write < 1024)
                {
                    disk_write_value = disk_write.ToString("F0") + " B/s";
                }
                else if (disk_write < 1024 * 1024)
                {
                    disk_write_value = (disk_write / (1024)).ToString("F0") + " KB/s";
                }
                else if (disk_write < 1024 * 1024 * 1024)
                {
                    disk_write_value = (disk_write / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    disk_write_value = (disk_write / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                disk_write_value = "0 B/s";
            }
        }
        /******************************************************************/

        /******************************************************************/
        public void GetNetworkUploadValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_Tcpip_NetworkInterface");
                var networkUploadValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    NetworkUpload = Double.Parse(mo["BytesSentPerSec"].ToString())
                }).FirstOrDefault();

                double network_upload = networkUploadValues.NetworkUpload;

                network_upload_counter += (int)network_upload;
                if (network_upload_max < network_upload)
                {
                    network_upload_max = (int)network_upload;
                }
                if (network_upload_min > network_upload)
                {
                    network_upload_min = (int)network_upload;
                }

                if (network_upload < 1024)
                {
                    network_upload_value = network_upload.ToString("F0") + " B/s";
                }
                else if (network_upload < 1024 * 1024)
                {
                    network_upload_value = (network_upload / (1024)).ToString("F0") + " KB/s";
                }
                else if (network_upload < 1024 * 1024 * 1024)
                {
                    network_upload_value = (network_upload / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    network_upload_value = (network_upload / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                network_upload_value = "0 B/s";
            }
        }
        /******************************************************************/

        /******************************************************************/
        public void GetNetworkDownloadValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_Tcpip_NetworkInterface");
                var networkDownloadValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    NetworkDownload = Double.Parse(mo["BytesReceivedPerSec"].ToString())
                }).FirstOrDefault();

                double network_download = networkDownloadValues.NetworkDownload;

                network_download_counter += (int)network_download;
                if (network_download_max < network_download)
                {
                    network_download_max = (int)network_download;
                }
                if (network_download_min > network_download)
                {
                    network_download_min = (int)network_download;
                }

                if (network_download < 1024)
                {
                    network_download_value = network_download.ToString("F0") + " B/s";
                }
                else if (network_download < 1024 * 1024)
                {
                    network_download_value = (network_download / (1024)).ToString("F0") + " KB/s";
                }
                else if (network_download < 1024 * 1024 * 1024)
                {
                    network_download_value = (network_download / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    network_download_value = (network_download / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                network_download_value = "0 B/s";
            }
        }
        /******************************************************************/
    }
    /******************************************************************/
}
