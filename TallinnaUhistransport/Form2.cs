using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

/// <summary>
/// This form shows location of selected bus/trolleybus/tram on the map.
/// Using Google Map.
/// </summary>
namespace TallinnaUhistransport
{
    public partial class Form2 : Form
    {

        // map markers list
        private List<PointLatLng> _points;
        public Form2()
        {
            InitializeComponent();
            // map markers list
            _points = new List<PointLatLng>();
        }

        // get latitude and longtitude from Form1
        public DataGridView Dgv { get; set; }
        public double mapLat { get; set; }
        public double mapLng { get; set; }

        private void Form2_Load(object sender, EventArgs e, Timer timer)
        {
            // Form2 is on top of other window
            this.TopMost = true;

            // refresh the map in every 1 sec
            timer = new Timer();
            timer.Interval = 1 * 1000; // 1 secs
            timer.Tick += new EventHandler(refreshMap_Tick);
            timer.Start();
            // coordinates shown on the map

            // enable navigate by using mouse left button
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.BingMap;
            map.ShowCenter = false;
            map.SetPositionByKeywords("Tallinn, Estonia");

            double MapLat = Convert.ToDouble("59.41952");
            double MapLng = Convert.ToDouble("24.74332");
            map.Position = new PointLatLng(this.mapLat, this.mapLng);
            // default zoom
            map.MinZoom = 5;
            map.MaxZoom = 20;
            map.Zoom = 15;

            // custom icon, marker type
            PointLatLng point = new PointLatLng(this.mapLat, this.mapLng);
            Bitmap dot = (Bitmap)Image.FromFile("img/reddot.png");
            GMapMarker marker = new GMarkerGoogle(point, dot);
            
            // overlay
            GMapOverlay markers = new GMapOverlay("markers");
            // add all available markers
            markers.Markers.Add(marker);
            // cover map with the overlay
            map.Overlays.Add(markers);
        }

        /// <summary>
        /// Enable select zooming percentage
        /// </summary>
        private void btn100_Click(object sender, EventArgs e)
        {
            map.Zoom = 16.5;
        }

        private void btn75_Click(object sender, EventArgs e)
        {
            map.Zoom = 14.5;
        }

        private void btn50_Click(object sender, EventArgs e)
        {
            map.Zoom = 13;
        }

        private void btn25_Click(object sender, EventArgs e)
        {
            map.Zoom = 12;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // close the window
            this.Close();

        }

        private void refreshMap_Tick(object sender, EventArgs e)
        {
            refreshMap.Stop();
        }
    }
}
