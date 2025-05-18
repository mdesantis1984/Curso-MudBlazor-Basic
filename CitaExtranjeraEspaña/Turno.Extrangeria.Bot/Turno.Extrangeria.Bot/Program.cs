using System;
using System.Net;
using System.Net.Mail;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

class Program
{
    static void Main()
    {
        var options = new EdgeOptions();
        options.PageLoadStrategy = PageLoadStrategy.Normal;
        options.AddArgument("inprivate"); // Modo incógnito
        options.AddArgument("no-first-run");
        options.AddArgument("no-default-browser-check");
        options.AddArgument("--start-maximized"); // Iniciar maximizado
        options.AddArgument("--disable-gpu");     // Desactivar la aceleración de GPU (útil en algunos casos)
        options.AddArgument("--no-sandbox");      // Evitar el uso del sandbox (solo para pruebas)

        var driverService = EdgeDriverService.CreateDefaultService();
        using (IWebDriver driver = new EdgeDriver(driverService, options, TimeSpan.FromSeconds(120)))
        {
            // Navegar a la página web deseada
            driver.Navigate().GoToUrl("https://icp.administracionelectronica.gob.es/icpplus/index.html");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            wait.Until(ExpectedConditions.ElementExists(By.Id("form")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("form")));

            // Set the window size
            driver.Manage().Window.Size = new System.Drawing.Size(1652, 832);

            // Navigate through the form
            driver.FindElement(By.Id("form")).Click();
            var dropdown = new SelectElement(driver.FindElement(By.Id("form")));
            dropdown.SelectByText("Madrid");
            driver.FindElement(By.Id("divProvincias")).Click();
            driver.FindElement(By.Id("btnAceptar")).Click();

            wait.Until(ExpectedConditions.ElementExists(By.Id("sede")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("sede")));
            //driver.FindElement(By.Id("sede")).Click();

            dropdown = new SelectElement(driver.FindElement(By.Id("sede")));
            dropdown.SelectByText("CNP Padre Piquer, Padre Piquer, 18, MADRID");
            //driver.FindElement(By.CssSelector("#fieldset > p:nth-child(2)")).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,205.60000610351562)");
           // driver.FindElement(By.CssSelector("#dv > ul:nth-child(3)")).Click();
            driver.FindElement(By.Id("tramiteGrupo[0]")).Click();
            dropdown = new SelectElement(driver.FindElement(By.Id("tramiteGrupo[0]")));
            dropdown.SelectByText("POLICIA-CERTIFICADO DE REGISTRO DE CIUDADANO DE LA U.E.");
            driver.FindElement(By.CssSelector(".fld:nth-child(2)")).Click();
            driver.FindElement(By.Id("btnAceptar")).Click();
            //driver.FindElement(By.CssSelector(".show_code")).Click();
            //driver.FindElement(By.CssSelector("div:nth-child(4)")).Click();
            driver.FindElement(By.CssSelector("#btnEntrar > .mf-paragraph-header")).Click();
            js.ExecuteScript("window.scrollTo(0,202.39999389648438)");
            driver.FindElement(By.CssSelector("#fieldset")).Click();
            driver.FindElement(By.Id("rdbTipoDocPas")).Click();
            driver.FindElement(By.Id("txtIdCitado")).Click();
            driver.FindElement(By.Id("txtIdCitado")).Click();
            driver.FindElement(By.Id("txtIdCitado")).Click();
            driver.FindElement(By.Id("txtIdCitado")).SendKeys("YC7187125");
            driver.FindElement(By.Id("divIdCitado")).Click();
            driver.FindElement(By.Id("txtDesCitado")).Click();
            driver.FindElement(By.Id("txtDesCitado")).SendKeys("MARCO ALEJANDRO DE SANTIS");
            driver.FindElement(By.Id("btnEnviar")).Click();
            driver.FindElement(By.CssSelector("#mf-layout--main-content")).Click();
            driver.FindElement(By.Id("btnEnviar")).Click();
            driver.FindElement(By.CssSelector(".layout--abajo")).Click();
            driver.FindElement(By.CssSelector(".mf-msg__info")).Click();
            driver.FindElement(By.CssSelector(".mf-msg__info")).Click();
            driver.FindElement(By.CssSelector(".layout--abajo")).Click();

            // Verificar si el resultado es exitoso
            bool resultadoExitoso = VerificarResultado(driver);

            // Enviar el correo si el resultado es exitoso
            if (resultadoExitoso)
            {
                EnviarCorreo("destinatario@correo.com", "Proceso completado", "El proceso de automatización ha finalizado correctamente.");
            }

            // Cerrar el navegador
            driver.Quit();
        }
    }

    static bool VerificarResultado(IWebDriver driver)
    {
        // Verificar un elemento o texto que indique éxito
        try
        {
            var elementoResultado = driver.FindElement(By.Id("resultadoId"));
            return elementoResultado.Text.Contains("Éxito"); // Ajusta según el texto esperado
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    static void EnviarCorreo(string destinatario, string asunto, string mensaje)
    {
        try
        {
            var smtpClient = new SmtpClient("smtp.tucorreo.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("tuCorreo@dominio.com", "tuContraseña"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("tuCorreo@dominio.com"),
                Subject = asunto,
                Body = mensaje,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(destinatario);
            smtpClient.Send(mailMessage);
            Console.WriteLine("Correo enviado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
