using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using WindowsInput;
using System.IO.Ports;
using System.Collections;
using System.Threading;
namespace POEpotier
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		IntPtr AddHp;
		 IntPtr AddMana;
		 Process[] p;
	
	
		 CheatEngine.Memory cm;
	

		DispatcherTimer timerhp;
		DispatcherTimer timermana;

		int hpstate = 1;
		int manastate = 1;

		bool running = false;

		SerialPort port;
		byte[] tosend = new byte [] { 0 };

		Timer serialtimer;

		uint maxhp=1;


		public MainWindow()
		{
			InitializeComponent();
			
			
		}

		private void hookbth_Click(object sender, RoutedEventArgs e)
		{
			p = Process.GetProcessesByName("PathOfExileSteam");
			hookbth.IsEnabled = false;
			if (p.Length > 0)
			{
				{
					cm = new CheatEngine.Memory(p[0]);

                    //IntPtr Add1 = cm.GetAddress("\"Game.exe\"+0076EFB4+44+­608+7c+7c+1e8");
                    //IntPtr Add2 = cm.GetAddress("Game.exe", (IntPtr)0x0076EFB4, new int[] { 0x44, 0x608, 0x7c, 0x7c, 0x1e8 });




                    //AddHp = cm.GetAddress("PathOfExile.exe", (IntPtr)0x00779FB4, new int[] { 0x44, 0x688, 0x7c, 0x7c, 0x1e8 });
                    //AddMana = cm.GetAddress("PathOfExile.exe", (IntPtr)0x00779FB4, new int[] { 0x48, 0x688, 0x7c, 0x7c, 0x1e8 });

                    //
                    AddHp = cm.GetAddress("PathOfExileSteam.exe", (IntPtr)0x00DAB978, new int[] { 0x15c, 0x7d8, 0x17c, 0x6b4, 0x7c8 });
                    //AddMana = AddHp;
                    AddMana = cm.GetAddress("PathOfExileSteam.exe", (IntPtr)0x00DAB018, new int[] { 0x3d8, 0x10c, 0x1cc, 0x624, 0x6c8 });

                    hpbar.Maximum = cm.ReadUInt32(AddHp);
					maxhp = cm.ReadUInt32(AddHp);
					hpbar.Value = cm.ReadUInt32(AddHp);

					hpcritslide.Maximum = cm.ReadUInt32(AddHp);
					hpcrit.Text = (hpcritslide.Maximum / 2).ToString();
					hpcritslide.Value = cm.ReadUInt32(AddHp) / 2;

					timerhp = new DispatcherTimer();
					timerhp.Interval = new TimeSpan(0, 0, 0, 0, 100);
					timerhp.Tick += dohp;
					if ((bool)hpenab.IsChecked)
						timerhp.Start();

					manabar.Maximum = cm.ReadUInt32(AddMana);
					manabar.Value = cm.ReadUInt32(AddMana);

					manacritslide.Maximum = cm.ReadUInt32(AddMana);
					manacrit.Text = (manacritslide.Maximum / 2).ToString();
					manacritslide.Value = cm.ReadUInt32(AddMana) / 2;

					timermana = new DispatcherTimer();
					timermana.Interval = new TimeSpan(0, 0, 0, 0, 100);
					timermana.Tick += domana;
					if ((bool)manaenab.IsChecked)
						timermana.Start();

					running = true;
				}
			}


		}


		void domana(object sender, EventArgs e)
		{
			uint chp;
			chp = cm.ReadUInt32(AddMana);

			if (manabar.Maximum < chp)
			{
				manabar.Maximum = chp;
				manacritslide.Maximum = chp;
			}


			manabar.Value = chp;
			mananum.Content = chp.ToString();


			if (chp < manacritslide.Value)
			{

			Restartmlab:
				if (manastate == 1)
				{
					if ((bool)mana1.IsChecked)
					{
						InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_1);
						statusb.AppendText("\n Mana poti used in slot: " + manastate.ToString());
						manastate = 2;
						timermana.Stop();
						System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 0, (int)manatimeoutslide.Value));
						timermana.Start();
						return;
					}
					else
					{
						manastate = 2;

					}
				}
			if (manastate == 2)
				{
					if ((bool)mana2.IsChecked)
					{
						InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_2);
						statusb.AppendText("\n Mana poti used in slot: " + manastate.ToString());
						manastate = 3;
						timermana.Stop();
						System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 0, (int)manatimeoutslide.Value));
						timermana.Start();
						return;
					}
					else
					{
						manastate = 3;

					}
				}
			if (manastate == 3)
				{
					if ((bool)mana3.IsChecked)
					{
						InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_3);
						statusb.AppendText("\n Mana poti used in slot: " + manastate.ToString());
						manastate = 4;
						timermana.Stop();
						System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 0, (int)manatimeoutslide.Value));
						timermana.Start();
						return;
					}
					else
					{
						manastate = 4;

					}
				}
			if (manastate == 4)
				{
					if ((bool)mana4.IsChecked)
					{
						InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_4);
						statusb.AppendText("\n Mana poti used in slot: " + manastate.ToString());
						manastate = 5;
						timermana.Stop();
						System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 0, (int)manatimeoutslide.Value));
						timermana.Start();
						return;
					}
					else
					{
						manastate = 5;

					}
				}
			if (manastate == 5)
				{
					if ((bool)mana5.IsChecked)
					{
						InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_5);
						statusb.AppendText("\n Mana poti used in slot: " + manastate.ToString());
						manastate = 1;
						timermana.Stop();
						System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 0, (int)manatimeoutslide.Value));
						timermana.Start();
						return;
					}
					else
					{
						manastate = 1;
						goto Restartmlab;

					}
				}


			}

		}


		void doserialsend(object sender)
		{
			uint chp;
			chp = cm.ReadUInt32(AddHp);


			tosend[0] = (byte)(180 - ((float)chp / maxhp * 180.0f));
			if (port != null && port.IsOpen == true)
				port.Write(tosend, 0, 1);

		}

		void dohp(object sender, EventArgs e)
		{
				uint chp;
				chp = cm.ReadUInt32(AddHp);


				if (hpbar.Maximum < chp)
				{
					hpbar.Maximum = chp;
					hpcritslide.Maximum=chp;
					maxhp = chp;
				}

			


				hpnum.Content = chp;
				hpbar.Value = chp;

				serialsend.Text = tosend[0].ToString();

				if (chp < hpcritslide.Value)
				{

				Restartlab:
					if (hpstate == 1)
					{
						if ((bool)hp1.IsChecked)
						{
							InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_1);
							statusb.AppendText("\n HP poti used in slot: " + hpstate.ToString());
							hpstate = 2;
							
							timerhp.Stop();
							System.Threading.Thread.Sleep(new TimeSpan(0,0,0,0,(int) hptimeoutslide.Value));
							timerhp.Start();
							return;
						}
						else
						{
							hpstate = 2;
							
						}
					}
					if (hpstate == 2)
					{
						if ((bool)hp2.IsChecked)
						{
							InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_2);
							statusb.AppendText("\n HP poti used in slot: " + hpstate.ToString());
							hpstate = 3;

							timerhp.Stop();
							System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 0, (int)hptimeoutslide.Value));
							timerhp.Start();
							return;
						}
						else
						{
							hpstate = 3;
							
						}
					}
					if (hpstate == 3)
					{
						if ((bool)hp3.IsChecked)
						{
							InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_3);
							statusb.AppendText("\n HP poti used in slot: " + hpstate.ToString());
							hpstate = 4;
							timerhp.Stop();
							System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 0, (int)hptimeoutslide.Value));
							timerhp.Start();
							return;
						}
						else
						{
							hpstate = 4;
							
						}
					}
					if (hpstate == 4)
					{
						if ((bool)hp4.IsChecked)
						{
							InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_4);
							statusb.AppendText("\n HP poti used in slot: " + hpstate.ToString());
							hpstate = 5;
							timerhp.Stop();
							System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 0, (int)hptimeoutslide.Value));
							timerhp.Start();
							return;
						}
						else
						{
							hpstate = 5;
							
						}
					}
					if (hpstate == 5)
					{
						if ((bool)hp5.IsChecked)
						{
							InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_5);
							statusb.AppendText("\n HP poti used in slot: " + hpstate.ToString());
							hpstate = 1;
							timerhp.Stop();
							System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 0, (int)hptimeoutslide.Value));
							timerhp.Start();
							return;
						}
						else
						{
							hpstate = 1;
							goto Restartlab;
							
						}
					}

					
				}
		}

		private void hptimeoutslide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (!running)
				return;

			timerhp.Interval = new TimeSpan(0, 0, 0, 0, (int)hptimeoutslide.Value);
			hptimeout.Text = hptimeoutslide.Value.ToString();
		}

		private void manatimeoutslide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (!running)
				return;

			timermana.Interval = new TimeSpan(0, 0, 0, 0, (int)manatimeoutslide.Value);
			manatimeout.Text = manatimeoutslide.Value.ToString();
		}

		private void hpcritslide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			
			hpcrit.Text = hpcritslide.Value.ToString();
		}

		private void manacritslide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			manacrit.Text = manacritslide.Value.ToString();
		}

		private void hpenab_Click(object sender, RoutedEventArgs e)
		{

			if (!running)
				return;
			if ((bool)hpenab.IsChecked)
			{
				timerhp.Start();

			}
			else
			{
				timerhp.Stop();
			}
		}

		private void manaenab_Click(object sender, RoutedEventArgs e)
		{

			if (!running)
				return;
			if ((bool)manaenab.IsChecked)
			{
				timermana.Start();

			}
			else
			{
				timermana.Stop();
			}
		}

		private void wind_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//cm.Dispose();
		//	timerhp.Stop();
			//timermana.Stop();
			
		}

		private void ssbox_Checked(object sender, RoutedEventArgs e)
		{
			port = new SerialPort(
	"COM3", 9600, Parity.None, 8, StopBits.One);

			// Open the port for communications
			port.Open();

			// Write a string
			//byte[] ts = (byte[])tosend.ToArray();
			port.Write(tosend, 0, 1);

			TimerCallback tcb = doserialsend;
			serialtimer = new Timer(tcb);
			serialtimer.Change(0, 100);

		

			// Write a set of bytes
			
			// Close the port
			
		}

		private void ssbox_Unchecked(object sender, RoutedEventArgs e)
		{
			serialtimer.Dispose();

			port.Close();
		}

		private void serialupdateslide_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (serialtimer == null)
			{
				return;
			}
			serialupdatetime.Text = serialupdateslide.Value.ToString();
			serialtimer.Change(500, (int)serialupdateslide.Value);
		}




	}
}
