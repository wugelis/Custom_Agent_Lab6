using RentalCar.Domain.Exceptions;

namespace RentalCar.Domain.Entities;

/// <summary>
/// 帳號實體 - 負責租車用戶的帳號註冊與登入驗證
/// </summary>
public sealed class Account
{
    public Guid AccountId { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    private Account(Guid accountId, string username, string passwordHash, string email)
    {
        AccountId = accountId;
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// 建立新帳號（註冊）
    /// </summary>
    public static Account Create(string username, string password, string email)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new DomainException("用戶名稱不可為空");

        if (string.IsNullOrWhiteSpace(password))
            throw new DomainException("密碼不可為空");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("電子郵件不可為空");

        // 簡化版密碼雜湊（實際應使用 BCrypt 或 Identity）
        var passwordHash = Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(password));

        return new Account(Guid.NewGuid(), username, passwordHash, email);
    }

    /// <summary>
    /// 驗證登入
    /// </summary>
    public bool Login(string password)
    {
        var inputHash = Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(password));

        var success = inputHash == PasswordHash;
        if (success)
        {
            LoginSuccess();
        }
        return success;
    }

    /// <summary>
    /// 登入成功處理
    /// </summary>
    public void LoginSuccess()
    {
        LastLoginAt = DateTime.UtcNow;
    }
}
