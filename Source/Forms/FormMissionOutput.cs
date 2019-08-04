using Headquarters4DCS.Generator;
using Headquarters4DCS.Mission;
using Headquarters4DCS.MizExport;
using Headquarters4DCS.Template;
using System;
using System.Windows.Forms;

namespace Headquarters4DCS.Forms
{
    public partial class FormMissionOutput : Form
    {
        private readonly MissionGenerator MissGenerator;
        private readonly MissionTemplate Template;

        private HQMission Mission = null;

        public FormMissionOutput(MissionTemplate template)
        {
            InitializeComponent();

            Template = template;
            MissGenerator = new MissionGenerator();
        }

        private void FormMissionOutput_Load(object sender, EventArgs e)
        {
            GenerateToolStripButton.Image = GUITools.GetImageFromResource("Icons.refresh.png");
            ExportMizToolStripButton.Image = GUITools.GetImageFromResource("Icons.exportMIZ.png");
            GenerateMission();
            if (Mission == null) Close();
        }

        private void ToolStripButtonClick(object sender, EventArgs e)
        {
            if (sender == GenerateToolStripButton)
                GenerateMission();
            else if (sender == ExportMizToolStripButton)
            {
                if (Mission == null) return;

                string defaultFileName = HQTools.RemoveInvalidFileNameCharacters(Mission.BriefingName ?? "");
                if (string.IsNullOrEmpty(defaultFileName)) defaultFileName = "NewMission";

                string mizFilePath = GUITools.ShowSaveFileDialog("miz", HQTools.GetDCSMissionPath(), defaultFileName, "DCS World mission files");
                if (!string.IsNullOrEmpty(mizFilePath))
                {
                    using (MizExporter mizExporters = new MizExporter())
                    {
                        if (!mizExporters.CreateMizFile(Mission, mizFilePath))
                            MessageBox.Show("ERROR"); // TODO: proper message
                    }
                }
            }
            else if (sender == ExportBriefingToHTMLToolStripMenuItem) ExportBriefing("html");
            else if (sender == ExportBriefingToJPEGToolStripMenuItem) ExportBriefing("jpg");
            else if (sender == ExportBriefingToPNGToolStripMenuItem) ExportBriefing("png");
        }

        private void ExportBriefing(string fileFormat)
        {
            if (Mission == null) return;

            string defaultFileName = HQTools.RemoveInvalidFileNameCharacters(Mission.BriefingName ?? "");
            if (string.IsNullOrEmpty(defaultFileName)) defaultFileName = "NewMission";

            string briefingFilePath = GUITools.ShowSaveFileDialog(fileFormat, HQTools.GetDCSMissionPath(), defaultFileName, $"{fileFormat.ToUpperInvariant()} files");

            if (briefingFilePath == null) return;

            bool result;

            using (BriefingExporter briefingExporter = new BriefingExporter())
            {
                switch (fileFormat)
                {
                    default: return;
                    case "html": result = briefingExporter.ExportToHTML(briefingFilePath, Mission.BriefingHTML); break;
                    case "jpg": result = briefingExporter.ExportToJPEG(briefingFilePath, Mission.BriefingHTML); break;
                    case "png": result = briefingExporter.ExportToPNG(briefingFilePath, Mission.BriefingHTML); break;
                }
            }

            if (!result)
                MessageBox.Show("Failed to export briefing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerateMission()
        {
            ExportMizToolStripButton.Enabled = false;
            ExportBriefingToolStripDropDownButton.Enabled = false;
            
            DestroyMission();
            Mission = MissGenerator.Generate(Template);

            BriefingWebBrowser.Navigate("about:blank");
            BriefingWebBrowser.Document.OpenNew(false);
            if (Mission == null) return; // No mission, no need to go further
            BriefingWebBrowser.Document.Write(Mission.BriefingHTML);
            BriefingWebBrowser.Refresh();

            Text = string.IsNullOrEmpty(Mission.BriefingName) ? "New mission" : Mission.BriefingName;

            ExportMizToolStripButton.Enabled = true;
            ExportBriefingToolStripDropDownButton.Enabled = true;
        }

        private void FormMissionOutput_FormClosed(object sender, FormClosedEventArgs e)
        {
            DestroyMission();
        }

        private void DestroyMission()
        {
            if (Mission == null) return;

            Mission.Dispose();
            Mission = null;
        }
    }
}
