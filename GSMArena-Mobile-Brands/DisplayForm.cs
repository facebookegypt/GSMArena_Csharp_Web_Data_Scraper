using System;
using System.Collections.Generic;
using System.Windows.Forms;
using clsGsmar.Models;

namespace GSMArena_Mobile_Brands
{
    public partial class DisplayForm : Form
    {
        private Image _rootImage = Properties.Resources.RootIcon;
        private Image _brandImage = Properties.Resources.BrandIcon;

        private Dictionary<string, List<Phone>> _brandPhones;
        public DisplayForm(Dictionary<string, List<Phone>> scrapedData)
        {
            InitializeComponent();
            //_brandPhones = brandPhones ?? new Dictionary<string, List<Phone>>();
            PopulateTreeView(scrapedData);
        }

        public void PopulateTreeView(Dictionary<string, List<Phone>> scrapedData)
        {
            TRVmodels.Nodes.Clear();
            TRVmodels.ImageList = TRVimglst;
            // Root
            TreeNode rootNode = new TreeNode("Models / Phones",0,0);

            foreach (var kvp in scrapedData)
            {
                string brandName = kvp.Key;
                List<Phone> phones = kvp.Value;

                // Brand
                TreeNode brandNode = new TreeNode($"{brandName} ({phones.Count})",1,1);

                // Numbered Phones (no image)
                int counter = 1;
                foreach (var phone in phones)
                {
                    TreeNode phoneNode = new TreeNode($"{counter}. {phone.Model}",2,2)
                    {
                        Tag = phone
                    };
                    brandNode.Nodes.Add(phoneNode);
                    counter++;
                }

                rootNode.Nodes.Add(brandNode);
            }

            TRVmodels.Nodes.Add(rootNode);
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
