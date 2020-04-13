using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;

public class SendEmailFromInputField : MonoBehaviour
{
    public string emailAddress = "";
    public string sender = "";
    public string password = "";

    public InputField inputField;
    public FeedbackForm feedbackForm;

    public void SendMessage()
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(sender);
        mail.To.Add(emailAddress);
        mail.Subject = "Feedback from in-app form";
        mail.Body = inputField.text;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Timeout = 1000;
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpServer.UseDefaultCredentials = false;
        smtpServer.Credentials = new System.Net.NetworkCredential(sender, password) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
        feedbackForm.ShowThanks();

        //Attachment attachement = new Attachment(path);
        //mail.Attachments.Add(attachement);

    }
}
