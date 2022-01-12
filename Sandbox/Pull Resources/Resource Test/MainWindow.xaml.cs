using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Resource_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PerformanceCounter cpuPerformanceCounter = new PerformanceCounter();
        private PerformanceCounter memoryPerformanceCounter = new PerformanceCounter();
        private PerformanceCounter diskReadsPerformanceCounter = new PerformanceCounter();
        private PerformanceCounter diskWritesPerformanceCounter = new PerformanceCounter();
        private PerformanceCounter diskTransfersPerformanceCounter = new PerformanceCounter();

        public MainWindow()
        {
            InitializeComponent();

            var counters = PerformanceCounterCategory.GetCategories()
            .SelectMany(x => x.GetCounters("")).Where(x => x.CounterName.Contains("GPU"));

            foreach (var counter in counters)
            {
                Console.WriteLine("{0} - {1}", counter.CategoryName, counter.CounterName);
            }
            Console.ReadLine();

            /*
            this.cpuPerformanceCounter.CategoryName = "Processor";
            this.cpuPerformanceCounter.CounterName = "% Processor Time";
            this.cpuPerformanceCounter.InstanceName = "_Total";

            this.memoryPerformanceCounter.CategoryName = "Memory";
            this.memoryPerformanceCounter.CounterName = "Available MBytes";

            this.diskReadsPerformanceCounter.CategoryName = "PhysicalDisk";
            this.diskReadsPerformanceCounter.CounterName = "Disk Reads/sec";
            this.diskReadsPerformanceCounter.InstanceName = "_Total";

            this.diskWritesPerformanceCounter.CategoryName = "PhysicalDisk";
            this.diskWritesPerformanceCounter.CounterName = "Disk Writes/sec";
            this.diskWritesPerformanceCounter.InstanceName = "_Total";

            while (true)
            {
                PrintData();
                System.Threading.Thread.Sleep(1000);
            }
            */
        }

        public async void PrintData()
        {
            await Task.Run(() =>
            {
                string currentCpuUsage = "CPU Usage : " + this.cpuPerformanceCounter.NextValue().ToString() + " %" + Environment.NewLine;
                string currentMemoryUsage = "Memory Usage : " + this.memoryPerformanceCounter.NextValue().ToString() + " Mb" + Environment.NewLine;
                string currentDiskReads = "Disk reads / sec : " + this.diskReadsPerformanceCounter.NextValue().ToString() + Environment.NewLine;
                string currentDiskWrites = "Disk writes / sec : " + this.diskWritesPerformanceCounter.NextValue().ToString() + Environment.NewLine;

                Console.Write("{0}{1}{2}{3}", currentCpuUsage, currentMemoryUsage, currentDiskReads, currentDiskWrites);
            });
        }
    }
}
