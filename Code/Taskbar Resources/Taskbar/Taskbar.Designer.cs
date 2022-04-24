using System.Drawing;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taskbar
{
    partial class Taskbar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cpuHeader = new System.Windows.Forms.Label();
            this.gpuHeader = new System.Windows.Forms.Label();
            this.ramHeader = new System.Windows.Forms.Label();
            this.diskHeader = new System.Windows.Forms.Label();
            this.networkHeader = new System.Windows.Forms.Label();
            this.cpuValue = new System.Windows.Forms.Label();
            this.gpuValue = new System.Windows.Forms.Label();
            this.ramValue = new System.Windows.Forms.Label();
            this.diskReadValue = new System.Windows.Forms.Label();
            this.diskWriteValue = new System.Windows.Forms.Label();
            this.networkUploadValue = new System.Windows.Forms.Label();
            this.networkDownloadValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cpuHeader
            // 
            this.cpuHeader.AutoSize = true;
            this.cpuHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuHeader.Location = new System.Drawing.Point(15, 4);
            this.cpuHeader.Name = "cpuHeader";
            this.cpuHeader.Size = new System.Drawing.Size(31, 13);
            this.cpuHeader.TabIndex = 0;
            this.cpuHeader.Text = "CPU";
            // 
            // gpuHeader
            // 
            this.gpuHeader.AutoSize = true;
            this.gpuHeader.Enabled = false;
            this.gpuHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpuHeader.Location = new System.Drawing.Point(84, 4);
            this.gpuHeader.Name = "gpuHeader";
            this.gpuHeader.Size = new System.Drawing.Size(32, 13);
            this.gpuHeader.TabIndex = 14;
            this.gpuHeader.Text = "GPU";
            this.gpuHeader.Visible = false;
            // 
            // ramHeader
            // 
            this.ramHeader.AutoSize = true;
            this.ramHeader.Enabled = false;
            this.ramHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ramHeader.Location = new System.Drawing.Point(152, 4);
            this.ramHeader.Name = "ramHeader";
            this.ramHeader.Size = new System.Drawing.Size(33, 13);
            this.ramHeader.TabIndex = 1;
            this.ramHeader.Text = "RAM";
            this.ramHeader.Visible = false;
            // 
            // diskHeader
            // 
            this.diskHeader.AutoSize = true;
            this.diskHeader.Enabled = false;
            this.diskHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diskHeader.Location = new System.Drawing.Point(227, 4);
            this.diskHeader.Name = "diskHeader";
            this.diskHeader.Size = new System.Drawing.Size(31, 13);
            this.diskHeader.TabIndex = 2;
            this.diskHeader.Text = "Disk";
            this.diskHeader.Visible = false;
            // 
            // networkHeader
            // 
            this.networkHeader.AutoSize = true;
            this.networkHeader.Enabled = false;
            this.networkHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.networkHeader.Location = new System.Drawing.Point(321, 4);
            this.networkHeader.Name = "networkHeader";
            this.networkHeader.Size = new System.Drawing.Size(52, 13);
            this.networkHeader.TabIndex = 3;
            this.networkHeader.Text = "Network";
            this.networkHeader.Visible = false;
            // 
            // cpuValue
            // 
            this.cpuValue.AutoSize = true;
            this.cpuValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuValue.Location = new System.Drawing.Point(16, 25);
            this.cpuValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cpuValue.Name = "cpuValue";
            this.cpuValue.Size = new System.Drawing.Size(32, 15);
            this.cpuValue.TabIndex = 4;
            this.cpuValue.Text = "33%";
            // 
            // gpuValue
            // 
            this.gpuValue.AutoSize = true;
            this.gpuValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpuValue.Location = new System.Drawing.Point(83, 25);
            this.gpuValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gpuValue.Name = "gpuValue";
            this.gpuValue.Size = new System.Drawing.Size(32, 15);
            this.gpuValue.TabIndex = 15;
            this.gpuValue.Text = "33%";
            // 
            // ramValue
            // 
            this.ramValue.AutoSize = true;
            this.ramValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ramValue.Location = new System.Drawing.Point(151, 25);
            this.ramValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ramValue.Name = "ramValue";
            this.ramValue.Size = new System.Drawing.Size(32, 15);
            this.ramValue.TabIndex = 5;
            this.ramValue.Text = "33%";
            // 
            // diskReadValue
            // 
            this.diskReadValue.AutoSize = true;
            this.diskReadValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diskReadValue.Location = new System.Drawing.Point(225, 17);
            this.diskReadValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.diskReadValue.Name = "diskReadValue";
            this.diskReadValue.Size = new System.Drawing.Size(24, 15);
            this.diskReadValue.TabIndex = 6;
            this.diskReadValue.Text = "1.2";
            // 
            // diskWriteValue
            // 
            this.diskWriteValue.AutoSize = true;
            this.diskWriteValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diskWriteValue.Location = new System.Drawing.Point(225, 32);
            this.diskWriteValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.diskWriteValue.Name = "diskWriteValue";
            this.diskWriteValue.Size = new System.Drawing.Size(28, 15);
            this.diskWriteValue.TabIndex = 7;
            this.diskWriteValue.Text = "920";
            // 
            // networkUploadValue
            // 
            this.networkUploadValue.AutoSize = true;
            this.networkUploadValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.networkUploadValue.Location = new System.Drawing.Point(321, 17);
            this.networkUploadValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.networkUploadValue.Name = "networkUploadValue";
            this.networkUploadValue.Size = new System.Drawing.Size(28, 15);
            this.networkUploadValue.TabIndex = 8;
            this.networkUploadValue.Text = "500";
            // 
            // networkDownloadValue
            // 
            this.networkDownloadValue.AutoSize = true;
            this.networkDownloadValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.networkDownloadValue.Location = new System.Drawing.Point(321, 32);
            this.networkDownloadValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.networkDownloadValue.Name = "networkDownloadValue";
            this.networkDownloadValue.Size = new System.Drawing.Size(24, 15);
            this.networkDownloadValue.TabIndex = 9;
            this.networkDownloadValue.Text = "3.0";
            // 
            // Taskbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(533, 49);
            this.ControlBox = false;
            this.Controls.Add(this.gpuValue);
            this.Controls.Add(this.gpuHeader);
            this.Controls.Add(this.networkDownloadValue);
            this.Controls.Add(this.networkUploadValue);
            this.Controls.Add(this.diskWriteValue);
            this.Controls.Add(this.diskReadValue);
            this.Controls.Add(this.ramValue);
            this.Controls.Add(this.cpuValue);
            this.Controls.Add(this.networkHeader);
            this.Controls.Add(this.diskHeader);
            this.Controls.Add(this.ramHeader);
            this.Controls.Add(this.cpuHeader);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Taskbar";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TransparencyKey = this.BackColor;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cpuHeader;
        private System.Windows.Forms.Label gpuHeader;
        private System.Windows.Forms.Label ramHeader;
        private System.Windows.Forms.Label diskHeader;
        private System.Windows.Forms.Label networkHeader;
        private System.Windows.Forms.Label cpuValue;
        private System.Windows.Forms.Label gpuValue;
        private System.Windows.Forms.Label ramValue;
        private System.Windows.Forms.Label diskReadValue;
        private System.Windows.Forms.Label diskWriteValue;
        private System.Windows.Forms.Label networkUploadValue;
        private System.Windows.Forms.Label networkDownloadValue;
    }
}

