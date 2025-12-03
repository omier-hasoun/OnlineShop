


namespace Infrastructure.BackgroundServices;

public sealed class EmailSenderFaker: IEmailSender<User>
{
    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {

        if(user.EmailConfirmed)
        {
            Console.WriteLine("Email is already confirmed.");
        }
        Console.WriteLine($"Send confirmation: {confirmationLink}");

        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {

        Console.WriteLine(@$"/////Send password reset: {resetLink}\\\\\\");

        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        Console.WriteLine($"Reset code: {resetCode}");
        return Task.CompletedTask;
    }

}
