using System;
using System.Collections.Generic;
using System.Windows.Forms;
using clsGsmar.Models;

namespace GSMArena_Mobile_Brands
{
    public partial class DisplayForm : Form
    {
        private Dictionary<string, List<Phone>> _brandPhones;

        public DisplayForm(Dictionary<string, List<Phone>> brandPhones)
        {
            InitializeComponent();
            _brandPhones = brandPhones ?? new Dictionary<string, List<Phone>>();
            PopulateTreeView();
        }

        /// <summary>
        /// Populates the TreeView with brand -> phones hierarchy.
        /// </summary>
        private void PopulateTreeView()
        {
            TRVmodels.Nodes.Clear();

            // Add Root Node
            TreeNode rootNode = TRVmodels.Nodes.Add("Models / Phones");

            foreach (var kvp in _brandPhones)
            {
                string brandName = kvp.Key;
                var phones = kvp.Value;

                // Add Brand Node
                TreeNode brandNode = rootNode.Nodes.Add(brandName);

                // Add Phones under this Brand
                foreach (var phone in phones)
                {
                    TreeNode phoneNode = brandNode.Nodes.Add(phone.Model);
                    phoneNode.Tag = phone; // Store phone object for later
                }
            }

            TRVmodels.ExpandAll();
        }

        /// <summary>
        /// Optional: Handle selection to show details.
        /// </summary>
        private async void TRVmodels_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Phone phone)
            {
                groupBox1.Text = phone.Model ?? "";

                // Update any text labels you have
                //labelBrand.Text = $"Brand: {phone.Brand}";
                //labelModel.Text = $"Model: {phone.Model}";
                //labelSpecs.Text = $"Specs: {phone.SpecsPreview}";

                // Load the image async
                await LoadImageFromUrlAsync(phone.ImageUrl);
            }
        }

        private async Task LoadImageFromUrlAsync(string url)
        {
            try
            {
                Phon_Img.Image = null; // Clear old image

                if (string.IsNullOrWhiteSpace(url))
                    return;

                using (var client = new HttpClient())
                {
                    var bytes = await client.GetByteArrayAsync(url);
                    using (var ms = new MemoryStream(bytes))
                    {
                        Phon_Img.Image = Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                // Optional: fallback image or error handling
                Phon_Img.Image = Properties.Resources.ImageNotFound;
            }
        }


        /// <summary>
        /// Optional: Handle double-click to open phone URL.
        /// </summary>
        private void TRVmodels_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node?.Tag is Phone phone && !string.IsNullOrWhiteSpace(phone.Url))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = phone.Url,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unable to open URL: {ex.Message}");
                }
            }
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            // Optional: expand/collapse settings, styling
            TRVmodels.HideSelection = false;
            TRVmodels.FullRowSelect = true;
        }

        private void DisplayForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (Phon_Img.Image != null || TRVmodels.SelectedNode == null)
            {
                Form frmPicPrev = new Form();
                frmPicPrev.Size = Phon_Img.Size * 4;
                frmPicPrev.BackgroundImage = Phon_Img.Image;
                frmPicPrev.BackgroundImageLayout = ImageLayout.Zoom;
                frmPicPrev.ShowDialog();
            }
        }
    }
}
