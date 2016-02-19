using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Puma.Net.Utilities;

namespace Puma.Net.Sample
{
    public partial class MainForm : Form
    {
        private readonly PumaPage pumaPage;
        private readonly ImageViewer imageViewer = new ImageViewer();

        public MainForm()
        {
                pumaPage = new PumaPage();

                InitializeComponent();

                PopulateControls();

                imageViewer.RecognizeClick += recognizeButton_Click;
                pumaPage.RecognitionProgress += pumaPage_RecognitionProgress;

        }

        private void PopulateControls()
        {
            languageComboBox.DataSource = LanguageStrings.GetSupportedLanguages(pumaPage);
            languageComboBox.Text = "Russian"; // not safe in localized apps
            fileFormatComboBox.DataSource = FileFormatStrings.GetAllFormats();
            courierComboBox.DataSource = FontStrings.GetRegisteredFonts();
            courierComboBox.Text = "Courier New";
            serifComboBox.DataSource = FontStrings.GetRegisteredFonts();
            serifComboBox.Text = "Times New Roman";
            sansSerifComboBox.DataSource = FontStrings.GetRegisteredFonts();
            sansSerifComboBox.Text = "Arial";
        }

        public void SynchronizePumaPage()
        {
            pumaPage.Language = LanguageStrings.GetSupportedLanguage(pumaPage, languageComboBox.Text);
            pumaPage.FileFormat = FileFormatStrings.GetFormat(fileFormatComboBox.Text);
            pumaPage.FontSettings.CourierName = courierComboBox.Text;
            pumaPage.FontSettings.SerifName = serifComboBox.Text;
            pumaPage.FontSettings.SansSerifName = sansSerifComboBox.Text;

            pumaPage.AutoRotateImage = imageImprovementCheckedListBox.GetItemChecked(0);
            pumaPage.ImproveDotMatrix = imageImprovementCheckedListBox.GetItemChecked(2);
            pumaPage.ImproveFax100 = imageImprovementCheckedListBox.GetItemChecked(3);
            if (imageImprovementCheckedListBox.GetItemChecked(1)) pumaPage.ScaleImage = 210;
            else
                pumaPage.ScaleImage = 100;

            pumaPage.UseTextFormating = recognitionCheckedListBox.GetItemChecked(0);
            pumaPage.EnableSpeller = recognitionCheckedListBox.GetItemChecked(1);
            pumaPage.OneColumnMode = recognitionCheckedListBox.GetItemChecked(2);
            pumaPage.PreserveLineBreaks = recognitionCheckedListBox.GetItemChecked(3);
            pumaPage.RecognizePictures = recognitionCheckedListBox.GetItemChecked(4);
            pumaPage.RecognizeTables = recognitionCheckedListBox.GetItemChecked(5);

            pumaPage.FontSettings.DetectBold = detectCheckedListBox.GetItemChecked(0);
            pumaPage.FontSettings.DetectItalic = detectCheckedListBox.GetItemChecked(1);
            pumaPage.FontSettings.DetectSize = detectCheckedListBox.GetItemChecked(2);
        }

        private void openFileButton_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileNameTextBox.Text = openFileDialog.FileName;
                openImageButton.PerformClick();
            }
        }

        private void openImageButton_Click(object sender, System.EventArgs e)
        {
            if (File.Exists(fileNameTextBox.Text))
            {
                errorProvider.SetError(fileNameTextBox, "");
                pumaPage.LoadImage(fileNameTextBox.Text);
                imageViewer.SourceBitmap = pumaPage.Bitmap;
                imageViewer.FileName = pumaPage.TempFileName;
                imageViewer.Hide();
                imageViewer.Show();
            }
            else errorProvider.SetError(fileNameTextBox, "Enter a valid file name");
        }

        private void recognizeButton_Click(object sender, System.EventArgs e)
        {
            SynchronizePumaPage();

            pumaPage.RecognizeToFile(imageViewer.FileName);

            imageViewer.Recognized = true;
            imageViewer.SetProgress(100);
        }

        private void pumaPage_RecognitionProgress(object sender, RecognitionEventArgs e)
        {
            imageViewer.SetProgress(e.ProgressPercents);
        }

    }
}
