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
using System.Windows;

namespace Taskbar
{
    public class Resources : INotifyPropertyChanged
    {
        /******************************************************************/
        #region General Variables
        private Dictionary<string, dynamic> resource_values;
        private int timer;
        #endregion
        /******************************************************************/

        /******************************************************************/
        #region Watched Variables
        public string cpu_usage_string
        {
            get { return GetValue("cpu_usage_string"); }
            set { SetValue("cpu_usage_string", value); }

        }
        public string gpu_usage_string
        {
            get { return GetValue("gpu_usage_string"); }
            set { SetValue("gpu_usage_string", value); }

        }
        public string ram_usage_string
        {
            get { return GetValue("ram_usage_string"); }
            set { SetValue("ram_usage_string", value); }

        }
        public string disk_read_string
        {
            get { return GetValue("disk_read_string"); }
            set { SetValue("disk_read_string", value); }

        }
        public string disk_write_string
        {
            get { return GetValue("disk_write_string"); }
            set { SetValue("disk_write_string", value); }

        }
        public string network_upload_string
        {
            get { return GetValue("network_upload_string"); }
            set { SetValue("network_upload_string", value); }

        }
        public string network_download_string
        {
            get { return GetValue("network_download_string"); }
            set { SetValue("network_download_string", value); }

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
            resource_values = new Dictionary<string, dynamic>();
            CreateResources();

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

                    timer += (int)stopwatch.ElapsedMilliseconds;

                    if (timer / 1000 > Taskbar.config.GetValue("history_frequency"))
                    {
                        RecordData();
                    }

                    Thread.Sleep(Taskbar.config.GetValue("resource_frequency") * 1000);
                }
            });
        }
        /******************************************************************/

        /******************************************************************/
        protected internal void SetValue(string key, dynamic value)
        {
            try
            {
                resource_values[key] = value;
                OnPropertyChanged(key);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Taskbar Resources encountered an error:\n" +
                ex.Message + "\n" +
                ex.StackTrace + "\n\n");
            }
        }
        /******************************************************************/

        /******************************************************************/
        protected internal dynamic GetValue(string key)
        {
            try
            {
                return resource_values[key];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Taskbar Resources encountered an error:\n" +
                ex.Message + "\n" +
                ex.StackTrace + "\n\n");
                return null;
            }
        }
        /******************************************************************/

        private void CreateResources()
        {
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
        }

        private void RecordData()
        {
            string cpu_query = "INSERT INTO CPU(Time, Max, Min, Average) VALUES('" +
            DateTime.Now + "'," +
            GetValue("cpu_max") + "," +
            GetValue("cpu_min") + "," +
            GetValue("cpu_counter") / (timer / 1000) + ");";
            Taskbar.history.Insert(cpu_query);

            string gpu_query = "INSERT INTO GPU(Time, Max, Min, Average) VALUES('" +
            DateTime.Now + "'," +
            GetValue("gpu_max") + "," +
            GetValue("gpu_min") + "," +
            GetValue("gpu_counter") / (timer / 1000) + ");";
            Taskbar.history.Insert(gpu_query);

            string ram_query = "INSERT INTO RAM(Time, Max, Min, Average) VALUES('" +
            DateTime.Now + "'," +
            GetValue("ram_max") + "," +
            GetValue("ram_min") + "," +
            GetValue("ram_counter") / (timer / 1000) + ");";
            Taskbar.history.Insert(ram_query);

            string disk_read_query = "INSERT INTO DISK_READ(Time, Max, Min, Average) VALUES('" +
            DateTime.Now + "'," +
            GetValue("disk_read_max") + "," +
            GetValue("disk_read_min") + "," +
            GetValue("disk_read_counter") / (timer / 1000) + ");";
            Taskbar.history.Insert(disk_read_query);

            string disk_write_query = "INSERT INTO DISK_WRITE(Time, Max, Min, Average) VALUES('" +
            DateTime.Now + "'," +
            GetValue("disk_write_max") + "," +
            GetValue("disk_write_min") + "," +
            GetValue("disk_write_counter") / (timer / 1000) + ");";
            Taskbar.history.Insert(disk_write_query);

            string network_upload_query = "INSERT INTO NETWORK_UPLOAD(Time, Max, Min, Average) VALUES('" +
            DateTime.Now + "'," +
            GetValue("network_upload_max") + "," +
            GetValue("network_upload_min") + "," +
            GetValue("network_upload_counter") / (timer / 1000) + ");";
            Taskbar.history.Insert(network_upload_query);

            string network_download_query = "INSERT INTO NETWORK_DOWNLOAD(Time, Max, Min, Average) VALUES('" +
            DateTime.Now + "'," +
            GetValue("network_download_max") + "," +
            GetValue("network_download_min") + "," +
            GetValue("network_download_counter") / (timer / 1000) + ");";
            Taskbar.history.Insert(network_download_query);
        }

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

                SetValue("cpu_usage_int", (int)cpu);
                SetValue("cpu_usage_string", cpu + " %");

                if (GetValue("cpu_max") < cpu)
                {
                    SetValue("cpu_max", (int)cpu);
                }
                if (GetValue("cpu_min") > cpu)
                {
                    SetValue("cpu_min", (int)cpu);
                }
            }
            catch
            {
                SetValue("cpu_usage_int", 0);
                SetValue("cpu_usage_string", "0 %");
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
                var gpu = 0f;

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
                    gpu += x.NextValue();
                });

                SetValue("gpu_usage_int", (int)gpu);
                SetValue("gpu_usage_string", gpu + " %");

                if (GetValue("gpu_max") < gpu)
                {
                    SetValue("gpu_max", (int)gpu);
                }
                if (GetValue("gpu_min") > gpu)
                {
                    SetValue("gpu_min", (int)gpu);
                }
            }
            catch
            {
                SetValue("gpu_usage_int", 0);
                SetValue("gpu_usage_string", "0 %");
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
                double ram = ((total_memory - memory) / total_memory) * 100;

                SetValue("ram_usage_int", (int)ram);
                SetValue("ram_usage_string", ram + " %");

                if (GetValue("ram_max") < ram)
                {
                    SetValue("ram_max", (int)ram);
                }
                if (GetValue("ram_min") > ram)
                {
                    SetValue("ram_min", (int)ram);
                }
            }
            catch
            {
                SetValue("ram_usage_int", 0);
                SetValue("ram_usage_string", "0 %");
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

                SetValue("disk_read_int", (int)disk_read);

                if (GetValue("disk_read_max") < disk_read)
                {
                    SetValue("disk_read_max", (int)disk_read);
                }
                if (GetValue("disk_read_min") > disk_read)
                {
                    SetValue("disk_read_min", (int)disk_read);
                }

                if (GetValue("disk_read_int") < 1024)
                {
                    SetValue("disk_read_string", disk_read.ToString("F0") + " B/s");
                }
                else if (GetValue("disk_read_int") < 1024 * 1024)
                {
                    SetValue("disk_read_string", (disk_read / (1024)).ToString("F0") + " KB/s");
                }
                else if (GetValue("disk_read_int") < 1024 * 1024 * 1024)
                {
                    SetValue("disk_read_string", (disk_read / (1024 * 1024)).ToString("F0") + " MB/s");
                }
                else
                {
                    SetValue("disk_read_string", (disk_read / (1024 * 1024 * 1024)).ToString("F0") + " GB/s");
                }
            }
            catch
            {
                SetValue("disk_read_int", 0);
                SetValue("disk_read_string", "0 B/s");
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

                SetValue("disk_write_int", (int)disk_write);

                if (GetValue("disk_write_max") < disk_write)
                {
                    SetValue("disk_write_max", (int)disk_write);
                }
                if (GetValue("disk_write_min") > disk_write)
                {
                    SetValue("disk_write_min", (int)disk_write);
                }

                if (GetValue("disk_write_int") < 1024)
                {
                    SetValue("disk_write_string", disk_write.ToString("F0") + " B/s");
                }
                else if (GetValue("disk_write_int") < 1024 * 1024)
                {
                    SetValue("disk_write_string", (disk_write / (1024)).ToString("F0") + " KB/s");
                }
                else if (GetValue("disk_write_int") < 1024 * 1024 * 1024)
                {
                    SetValue("disk_write_string", (disk_write / (1024 * 1024)).ToString("F0") + " MB/s");
                }
                else
                {
                    SetValue("disk_write_string", (disk_write / (1024 * 1024 * 1024)).ToString("F0") + " GB/s");
                }
            }
            catch
            {
                SetValue("disk_write_int", 0);
                SetValue("disk_write_string", "0 B/s");
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

                SetValue("network_upload_int", (int)network_upload);

                if (GetValue("network_upload_max") < network_upload)
                {
                    SetValue("network_upload_max", (int)network_upload);
                }
                if (GetValue("network_upload_min") > network_upload)
                {
                    SetValue("network_upload_min", (int)network_upload);
                }

                if (GetValue("network_upload_int") < 1024)
                {
                    SetValue("network_upload_string", network_upload.ToString("F0") + " B/s");
                }
                else if (GetValue("network_upload_int") < 1024 * 1024)
                {
                    SetValue("network_upload_string", (network_upload / (1024)).ToString("F0") + " KB/s");
                }
                else if (GetValue("network_upload_int") < 1024 * 1024 * 1024)
                {
                    SetValue("network_upload_string", (network_upload / (1024 * 1024)).ToString("F0") + " MB/s");
                }
                else
                {
                    SetValue("network_upload_string", (network_upload / (1024 * 1024 * 1024)).ToString("F0") + " GB/s");
                }
            }
            catch
            {
                SetValue("network_upload_int", 0);
                SetValue("network_upload_string", "0 B/s");
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

                SetValue("network_download_int", (int)network_download);

                if (GetValue("network_download_max") < network_download)
                {
                    SetValue("network_download_max", (int)network_download);
                }
                if (GetValue("network_download_min") > network_download)
                {
                    SetValue("network_download_min", (int)network_download);
                }

                if (GetValue("network_download_int") < 1024)
                {
                    SetValue("network_download_string", network_download.ToString("F0") + " B/s");
                }
                else if (GetValue("network_download_int") < 1024 * 1024)
                {
                    SetValue("network_download_string", (network_download / (1024)).ToString("F0") + " KB/s");
                }
                else if (GetValue("network_download_int") < 1024 * 1024 * 1024)
                {
                    SetValue("network_download_string", (network_download / (1024 * 1024)).ToString("F0") + " MB/s");
                }
                else
                {
                    SetValue("network_download_string", (network_download / (1024 * 1024 * 1024)).ToString("F0") + " GB/s");
                }
            }
            catch
            {
                SetValue("network_download_int", 0);
                SetValue("network_download_string", "0 B/s");
            }
        }
        /******************************************************************/
    }
}
