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
    public class Resources : INotifyPropertyChanged
    {
        /******************************************************************/
        #region General Variables
        private Dictionary<string, dynamic> resource_values;
        private int timer;
        private int resource_frequency;
        private int history_frequency;
        #endregion
        /******************************************************************/

        /******************************************************************/
        #region Watched Variables
        public string cpu_usage_string
        {
            get { return resource_values["cpu_usage_string"]; }
            set { resource_values["cpu_usage_string"] = value; OnPropertyChanged("cpu_usage_string"); }

        }
        public int cpu_usage_int
        {
            get { return resource_values["cpu_usage_int"]; }
            set { resource_values["cpu_usage_int"] = value; OnPropertyChanged("cpu_usage_int"); }
        }

        public string gpu_usage_string
        {
            get { return resource_values["gpu_usage_string"]; }
            set { resource_values["gpu_usage_string"] = value; OnPropertyChanged("gpu_usage_string"); }

        }
        public int gpu_usage_int
        {
            get { return resource_values["gpu_usage_int"]; }
            set { resource_values["gpu_usage_int"] = value; OnPropertyChanged("gpu_usage_int"); }
        }

        public string ram_usage_string
        {
            get { return resource_values["ram_usage_string"]; }
            set { resource_values["ram_usage_string"] = value; OnPropertyChanged("ram_usage_string"); }

        }
        public int ram_usage_int
        {
            get { return resource_values["ram_usage_int"]; }
            set { resource_values["ram_usage_int"] = value; OnPropertyChanged("ram_usage_int"); }
        }

        public string disk_read_string
        {
            get { return resource_values["disk_read_string"]; }
            set { resource_values["disk_read_string"] = value; OnPropertyChanged("disk_read_string"); }

        }
        public int disk_read_int
        {
            get { return resource_values["disk_read_int"]; }
            set { resource_values["disk_read_int"] = value; OnPropertyChanged("disk_read_int"); }
        }

        public string disk_write_string
        {
            get { return resource_values["disk_write_string"]; }
            set { resource_values["disk_write_string"] = value; OnPropertyChanged("disk_write_string"); }

        }
        public int disk_write_int
        {
            get { return resource_values["disk_write_int"]; }
            set { resource_values["disk_write_int"] = value; OnPropertyChanged("disk_write_int"); }
        }

        public string network_upload_string
        {
            get { return resource_values["network_upload_string"]; }
            set { resource_values["network_upload_string"] = value; OnPropertyChanged("network_upload_string"); }

        }
        public int network_upload_int
        {
            get { return resource_values["network_upload_int"]; }
            set { resource_values["network_upload_int"] = value; OnPropertyChanged("network_upload_int"); }
        }

        public string network_download_string
        {
            get { return resource_values["network_download_string"]; }
            set { resource_values["network_download_string"] = value; OnPropertyChanged("network_download_string"); }

        }
        public int network_download_int
        {
            get { return resource_values["network_download_int"]; }
            set { resource_values["network_download_int"] = value; OnPropertyChanged("network_download_int"); }
        }
        #endregion
        /******************************************************************/

        /******************************************************************/
        #region Variable Listener
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        /******************************************************************/

        /******************************************************************/
        protected internal async void Initalize()
        {
            try
            {
                resource_values = new Dictionary<string, dynamic>();
                //Pull nessesary config values
                //resource_frequency = int.Parse(Config.GetValue("resource_frequency"));
                //history_frequency = int.Parse(Config.GetValue("history_frequency"));
                resource_frequency = 1;
                history_frequency = 1;

                //Select correct resources
                resource_values.Add("cpu_usage_string", "0 %");
                resource_values.Add("cpu_usage_int", 0);
                resource_values.Add("cpu_counter", 0);
                resource_values.Add("cpu_max", 0);
                resource_values.Add("cpu_min", 0);

                resource_values.Add("gpu_usage_string", "0 %");
                resource_values.Add("gpu_usage_int", 0);
                resource_values.Add("gpu_counter", 0);
                resource_values.Add("gpu_max", 0);
                resource_values.Add("gpu_min", 0);

                resource_values.Add("ram_usage_string", "0 %");
                resource_values.Add("ram_usage_int", 0);
                resource_values.Add("ram_counter", 0);
                resource_values.Add("ram_max", 0);
                resource_values.Add("ram_min", 0);

                resource_values.Add("disk_read_string", "0 B/s");
                resource_values.Add("disk_read_int", 0);
                resource_values.Add("disk_read_counter", 0);
                resource_values.Add("disk_read_max", 0);
                resource_values.Add("disk_read_min", 0);

                resource_values.Add("disk_write_string", "0 B/s");
                resource_values.Add("disk_write_int", 0);
                resource_values.Add("disk_write_counter", 0);
                resource_values.Add("disk_write_max", 0);
                resource_values.Add("disk_write_min", 0);

                resource_values.Add("network_upload_string", "0 B/s");
                resource_values.Add("network_upload_int", 0);
                resource_values.Add("network_upload_counter", 0);
                resource_values.Add("network_upload_max", 0);
                resource_values.Add("network_upload_min", 0);

                resource_values.Add("network_download_string", "0 B/s");
                resource_values.Add("network_download_int", 0);
                resource_values.Add("network_download_counter", 0);
                resource_values.Add("network_download_max", 0);
                resource_values.Add("network_download_min", 0);

                await Task.Run(() =>
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    while (true)
                    {
                        GetCpuValue();
                        GetGpuValue();
                        GetRamValue();
                        GetDiskReadValue();
                        GetDiskWriteValue();
                        GetNetworkUploadValue();
                        GetNetworkDownloadValue();

                        //Update timer
                        timer += (int)stopwatch.ElapsedMilliseconds;

                        //Add new values to database if enough time has elapsed
                        if (timer / 1000 > history_frequency)
                        {
                            //TODO: Change to a foreach resource//

                            string cpu_query = "INSERT INTO CPU(Time, Max, Min, Average) VALUES('" +
                            DateTime.Now + "'," +
                            resource_values["cpu_max"] + "," +
                            resource_values["cpu_min"] + "," +
                            resource_values["cpu_counter"] / (timer / 1000) + ");";

                            string gpu_query = "INSERT INTO GPU(Time, Max, Min, Average) VALUES('" +
                            DateTime.Now + "'," +
                            resource_values["gpu_max"] + "," +
                            resource_values["gpu_min"] + "," +
                            resource_values["gpu_counter"] / (timer / 1000) + ");";

                            string ram_query = "INSERT INTO RAM(Time, Max, Min, Average) VALUES('" +
                            DateTime.Now + "'," +
                            resource_values["ram_max"] + "," +
                            resource_values["ram_min"] + "," +
                            resource_values["ram_counter"] / (timer / 1000) + ");";

                            string disk_upload_query = "INSERT INTO DISK_READ(Time, Max, Min, Average) VALUES('" +
                            DateTime.Now + "'," +
                            resource_values["disk_read_max"] + "," +
                            resource_values["disk_read_min"] + "," +
                            resource_values["disk_read_counter"] / (timer / 1000) + ");";

                            string disk_download_query = "INSERT INTO DISK_WRITE(Time, Max, Min, Average) VALUES('" +
                            DateTime.Now + "'," +
                            resource_values["disk_write_max"] + "," +
                            resource_values["disk_write_min"] + "," +
                            resource_values["disk_write_counter"] / (timer / 1000) + ");";

                            string network_upload_query = "INSERT INTO NETWORK_UPLOAD(Time, Max, Min, Average) VALUES('" +
                            DateTime.Now + "'," +
                            resource_values["network_upload_max"] + "," +
                            resource_values["network_upload_min"] + "," +
                            resource_values["network_upload_counter"] / (timer / 1000) + ");";

                            string network_download_query = "INSERT INTO NETWORK_DOWNLOAD(Time, Max, Min, Average) VALUES('" +
                            DateTime.Now + "'," +
                            resource_values["network_download_max"] + "," +
                            resource_values["network_download_min"] + "," +
                            resource_values["network_download_counter"] / (timer / 1000) + ");";

                            //History.Insert(cpu_query);
                            //History.Insert(gpu_query);
                            //History.Insert(ram_query);
                            //History.Insert(disk_upload_query);
                            //History.Insert(disk_download_query);
                            //History.Insert(network_upload_query);
                            //History.Insert(network_download_query);
                        }

                        //Delay
                        System.Threading.Thread.Sleep(resource_frequency * 1000);
                    }

                });
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            
        }
        /******************************************************************/

        /******************************************************************/
        private void GetCpuValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
                var cpuValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    PercentProcessorTime = Double.Parse(mo["PercentProcessorTime"].ToString())
                }).FirstOrDefault();

                double cpu = cpuValues.PercentProcessorTime;

                cpu_usage_int = (int)cpu;
                cpu_usage_string = cpu + " %";

                if (resource_values["cpu_max"] < cpu)
                {
                    resource_values["cpu_max"] = (int)cpu;
                }
                if (resource_values["cpu_min"] > cpu)
                {
                    resource_values["cpu_min"] = (int)cpu;
                }
            }
            catch
            {
                cpu_usage_int = 0;
                cpu_usage_string = "0 %";
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void GetGpuValue()
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

                gpu_usage_int = (int)result;
                gpu_usage_string = (int)result + " %";

                if (resource_values["gpu_max"] < result)
                {
                    resource_values["gpu_max"] = (int)result;
                }
                if (resource_values["gpu_min"] > result)
                {
                    resource_values["gpu_min"] = (int)result;
                }
            }
            catch
            {
                gpu_usage_int = 0;
                gpu_usage_string = "0 %";
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void GetRamValue()
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

                ram_usage_int += (int)percent;
                ram_usage_string = ((int)percent).ToString() + " %";

                if (resource_values["ram_max"] < percent)
                {
                    resource_values["ram_max"] = (int)percent;
                }
                if (resource_values["ram_min"] > percent)
                {
                    resource_values["ram_min"] = (int)percent;
                }
            }
            catch
            {
                ram_usage_int = 0;
                ram_usage_string = "0 %";
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void GetDiskReadValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfDisk_PhysicalDisk");
                var diskReadValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    DiskReadRate = Double.Parse(mo["DiskReadBytesPersec"].ToString())
                }).FirstOrDefault();

                double disk_read = diskReadValues.DiskReadRate;

                disk_read_int += (int)disk_read;

                if (resource_values["disk_read_max"] < disk_read)
                {
                    resource_values["disk_read_max"] = (int)disk_read;
                }
                if (resource_values["disk_read_min"] > disk_read)
                {
                    resource_values["disk_read_min"] = (int)disk_read;
                }

                if (disk_read < 1024)
                {
                    disk_read_string = disk_read.ToString("F0") + " B/s";
                }
                else if (disk_read < 1024 * 1024)
                {
                    disk_read_string = (disk_read / (1024)).ToString("F0") + " KB/s";
                }
                else if (disk_read < 1024 * 1024 * 1024)
                {
                    disk_read_string = (disk_read / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    disk_read_string = (disk_read / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                disk_read_int = 0;
                disk_read_string = "0 B/s";
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void GetDiskWriteValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfDisk_PhysicalDisk");
                var diskWriteValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    DiskWriteRate = Double.Parse(mo["DiskWriteBytesPersec"].ToString())
                }).FirstOrDefault();

                double disk_write = diskWriteValues.DiskWriteRate;

                disk_write_int += (int)disk_write;

                if (resource_values["disk_write_max"] < disk_write)
                {
                    resource_values["disk_write_max"] = (int)disk_write;
                }
                if (resource_values["disk_write_min"] > disk_write)
                {
                    resource_values["disk_write_min"] = (int)disk_write;
                }

                if (disk_write < 1024)
                {
                    disk_write_string = disk_write.ToString("F0") + " B/s";
                }
                else if (disk_write < 1024 * 1024)
                {
                    disk_write_string = (disk_write / (1024)).ToString("F0") + " KB/s";
                }
                else if (disk_write < 1024 * 1024 * 1024)
                {
                    disk_write_string = (disk_write / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    disk_write_string = (disk_write / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                disk_write_int = 0;
                disk_write_string = "0 B/s";
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void GetNetworkUploadValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_Tcpip_NetworkInterface");
                var networkUploadValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    NetworkUpload = Double.Parse(mo["BytesSentPerSec"].ToString())
                }).FirstOrDefault();

                double network_upload = networkUploadValues.NetworkUpload;

                network_upload_int += (int)network_upload;

                if (resource_values["network_upload_max"] < network_upload)
                {
                    resource_values["network_upload_max"] = (int)network_upload;
                }
                if (resource_values["network_upload_min"] > network_upload)
                {
                    resource_values["network_upload_min"] = (int)network_upload;
                }

                if (network_upload < 1024)
                {
                    network_upload_string = network_upload.ToString("F0") + " B/s";
                }
                else if (network_upload < 1024 * 1024)
                {
                    network_upload_string = (network_upload / (1024)).ToString("F0") + " KB/s";
                }
                else if (network_upload < 1024 * 1024 * 1024)
                {
                    network_upload_string = (network_upload / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    network_upload_string = (network_upload / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                network_upload_int = 0;
                network_upload_string = "0 B/s";
            }
        }
        /******************************************************************/

        /******************************************************************/
        private void GetNetworkDownloadValue()
        {
            try
            {
                var wmiObject = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_Tcpip_NetworkInterface");
                var networkDownloadValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new {
                    NetworkDownload = Double.Parse(mo["BytesReceivedPerSec"].ToString())
                }).FirstOrDefault();

                double network_download = networkDownloadValues.NetworkDownload;

                disk_read_int += (int)network_download;

                if (resource_values["network_download_max"] < network_download)
                {
                    resource_values["network_download_max"] = (int)network_download;
                }
                if (resource_values["network_download_min"] > network_download)
                {
                    resource_values["network_download_min"] = (int)network_download;
                }

                if (network_download < 1024)
                {
                    network_download_string = network_download.ToString("F0") + " B/s";
                }
                else if (network_download < 1024 * 1024)
                {
                    network_download_string = (network_download / (1024)).ToString("F0") + " KB/s";
                }
                else if (network_download < 1024 * 1024 * 1024)
                {
                    network_download_string = (network_download / (1024 * 1024)).ToString("F0") + " MB/s";
                }
                else
                {
                    network_download_string = (network_download / (1024 * 1024 * 1024)).ToString("F0") + " GB/s";
                }
            }
            catch
            {
                network_download_int = 0;
                network_download_string = "0 B/s";
            }
        }
        /******************************************************************/
    }
}
