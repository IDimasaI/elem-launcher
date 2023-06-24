using System;
using System.Net;
using System.Windows.Forms;

public class DownloadFileForm : Form
{
    private readonly Button downloadButton;
    private readonly TextBox urlTextBox;
    private readonly TextBox savePathTextBox;

    public DownloadFileForm()
    {
        // Создаем элементы управления формой
        downloadButton = new Button
        {
            Location = new System.Drawing.Point(12, 100),
            Size = new System.Drawing.Size(150, 23),
            Text = "Загрузить"
        };
        downloadButton.Click += DownloadButton_Click;

        Label urlLabel = new Label
        {
            Location = new System.Drawing.Point(12, 14),
            Size = new System.Drawing.Size(150, 23),
            Text = "URL-адрес файла:"
        };

        Label savePathLabel = new Label
        {
            Location = new System.Drawing.Point(12, 44),
            Size = new System.Drawing.Size(150, 23),
            Text = "Путь сохранения файла:"
        };

        Label helpLabel = new Label
        {
            Location = new System.Drawing.Point(12, 70),
            Size = new System.Drawing.Size(150, 70),
            Text = "Незабудьте указать расширение"
        };

        urlTextBox = new TextBox
        {
            Location = new System.Drawing.Point(168, 14),
            Size = new System.Drawing.Size(300, 23)
        };
        urlTextBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);

        savePathTextBox = new TextBox
        {
            Location = new System.Drawing.Point(168, 44),
            Size = new System.Drawing.Size(300, 23)
        };
        savePathTextBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);
        // Добавляем элементы управления на форму
        Controls.Add(downloadButton);
        Controls.Add(helpLabel);
        Controls.Add(urlLabel);
        Controls.Add(savePathLabel);
        Controls.Add(urlTextBox);
        Controls.Add(savePathTextBox);

        // Настраиваем свойства формы
        Text = "Загрузка файла";
        Size = new System.Drawing.Size(500, 200);
    }

    private void DownloadButton_Click(object sender, EventArgs e)
    {
        // Проверяем, что URL-адрес и путь сохранения были указаны
        if (string.IsNullOrWhiteSpace(urlTextBox.Text) || string.IsNullOrWhiteSpace(savePathTextBox.Text))
        {MessageBox.Show("Введите URL-адрес файла и путь сохранения");return;}
        // Устанавливаем URL-адрес файла и путь сохранения
        string url = urlTextBox.Text;
        string savePath = savePathTextBox.Text;
        
        using (WebClient client = new WebClient())
        {
            try
            {
                // Загружаем файл и сохраняем его на диск
                client.DownloadFile(url, savePath);

                // Сообщаем пользователю о том, что файл успешно загружен
                MessageBox.Show("Файл успешно загружен и сохранен на диск");
            }
            catch (Exception ex)
            {
                // Сообщаем об ошибке, если файл не удалось загрузить
                MessageBox.Show("Ошибка загрузки файла: " + ex.Message);
            }
        }
    }
    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(urlTextBox.Text) && !string.IsNullOrEmpty(savePathTextBox.Text))
        {
            downloadButton.PerformClick();
        }
    }
    public static void Main()
    {
        Application.Run(new DownloadFileForm());
    }
}