namespace MedTrackPro.UtilityMethods;

public class IdentityMethods
{
    public static string GenerateWelcomeConfirmedEmailHtml(string userFirstName)
    {
        return $@"
    <html>
        <body style='font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;'>
            <div style='width: 100%; padding: 20px 0;'>
                <table align='center' cellpadding='0' cellspacing='0' width='600' style='border-collapse: collapse; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);'>
                    <tr>
                        <td align='center' style='padding: 0;'>
                            <img src='https://clipplytest.com/welcome.jpg' alt='Welcome to MedTrackPro' style='display: block; width: 100%; max-width: 600px; background-color: #673ab7;'>
                        </td>
                    </tr>
                    <tr>
                        <td align='left' style='padding: 40px 30px; background-color: #ffffff;'>
                            <h1 style='color: #673ab7; margin-bottom: 20px;'>Hi {userFirstName},</h1>
                            <p style='color: #555555; font-size: 16px; line-height: 24px;'>
                                Welcome to <strong>MedTrackPro!</strong> We’re excited to have you with us.
                            </p>
                            <p style='color: #555555; font-size: 16px; line-height: 24px;'>
                                MedTrackPro is a platform designed for seamless online consultation with skilled doctors.
                            </p>
                            <ul style='color: #555555; font-size: 16px; line-height: 24px; padding-left: 20px;'>
                                <li><strong>Reach More Patients</strong>: Showcase your expertise and attract the right audience.</li>
                                <li><strong>Streamline Your Practice</strong>: Simplify your appointment and consultation processes.</li>
                                <li><strong>Track Your Performance</strong>: Gain valuable insights and improve patient outcomes.</li>
                            </ul>
                            <p style='color: #555555; font-size: 16px; line-height: 24px;'>
                                To get started, please complete your profile by adding details like your qualifications, experience, and working hours.
                            </p>
                            <div style='text-align: center; margin: 20px 0;'>
                                <a href='{{payment}}' style='display: inline-block; background-color: #673ab7; color: #ffffff; text-decoration: none; font-size: 16px; font-weight: bold; padding: 12px 24px; border-radius: 4px;'>Complete Your Profile</a>
                            </div>
                            <p style='color: #555555; font-size: 16px; line-height: 24px;'>
                                If you have any questions, our support team is here to assist you at any time.
                            </p>
                            <p style='color: #555555; font-size: 16px; line-height: 24px;'>
                                Best regards,<br>
                                The <strong>MedTrackPro</strong> Team
                            </p>
                        </td>
                    </tr>
                </table>
            </div>
        </body>
    </html>";
    }

    public static string GenerateWelcomeEmailHtml(string userFirstName, string verifyUrl)
    {
        return $@"
<html>
    <body style='font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #f4f4f4;'>
        <div style='width: 100%; padding: 20px 0;'>
            <table align='center' cellpadding='0' cellspacing='0' width='600' style='border-collapse: collapse; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);'>
                <tr>
                    <td align='center' style='padding: 0;'>
                        <img src='https://clipplytest.com/email.jpg' alt='Welcome to MedTrackPro' style='display: block; width: 100%; max-width: 600px; background-color: #673ab7;'>
                    </td>
                </tr>
                <tr>
                    <td align='left' style='padding: 40px 30px; background-color: #ffffff;'>
                        <h1 style='color: #673ab7; margin-bottom: 20px;'>Hi {userFirstName},</h1>
                        <p style='color: #555555; font-size: 16px; line-height: 24px;'>
                            Welcome to <strong>MedTrackPro!</strong> We're excited to have you on board.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 24px;'>
                            To get started, please verify your email address by clicking the button below:
                        </p>
                        <p style='text-align: center;'>
                            <a href='{verifyUrl}' style='background-color: #673ab7; color: #ffffff; padding: 15px 30px; text-decoration: none; font-size: 16px; border-radius: 5px;'>Verify Email</a>
                        </p>
                        <p style='color: #555555; font-size: 14px; line-height: 24px;'>
                            If you did not sign up for MedTrackPro, please ignore this email.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 24px;'>
                            Thank you for joining MedTrackPro. Together, let's revolutionize healthcare with seamless consultations and better patient care!
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 24px;'>
                            Best regards,<br>
                            The MedTrackPro Team
                        </p>
                    </td>
                </tr>
            </table>
        </div>
    </body>
</html>";

    }

}
