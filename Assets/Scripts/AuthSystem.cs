using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.Text;
using System.Security.Cryptography;
using System;
using UnityEngine.Rendering;

public class AuthSystem : MonoBehaviour
{
    [Header("UI References")]
    public InputField emailInput;   
    public InputField passwordInput; 
    public Text statusText;         
    public GameObject enterPanel;
    public GameObject mainMenuPanel;

    private string dbPath;           
    private IDbConnection dbConnection;

    void Start()
    {
        InitializeDatabase();
    }
    
    void InitializeDatabase()
    {
        dbPath = "URI=file:" + Application.dataPath + "/users.db";
        
        dbConnection = new SqliteConnection(dbPath);
        dbConnection.Open();

        // Создание таблицу users, если она не существует
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = @"
            CREATE TABLE IF NOT EXISTS users (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                email TEXT UNIQUE,         
                password_hash TEXT         
            )";
        dbCommand.ExecuteNonQuery();
    }

    // Обработчик нажатия кнопки "Регистрация"
    public void OnRegisterClick()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        
        if (!ValidateEmail(email) || !ValidatePassword(password)) return;
        
        if (UserExists(email))
        {
            statusText.text = "Пользователь с таким email уже существует!";
            return;
        }
        
        string hashedPassword = HashPassword(password);
        CreateUser(email, hashedPassword);
        enterPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        statusText.text = "Регистрация успешна!";
    }

    // Обработчик нажатия кнопки "Войти"
    public void OnLoginClick()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        
        if (!ValidateEmail(email) || !ValidatePassword(password)) return;
        
        if (CheckCredentials(email, password))
        {
            statusText.text = "Вход выполнен!";
            mainMenuPanel.SetActive(true);
            enterPanel.SetActive(false);
        }
        else
        {
            statusText.text = "Неверные данные!";
        }
    }
    
    private bool ValidateEmail(string email)
    {
        Debug.Log($"Checking email: {email}");
        if (string.IsNullOrEmpty(email) || 
            !email.Contains("@") || 
            !email.Contains("."))
        {
            Debug.Log("Invalid email!");
            statusText.text = "Некорректный email!";
            return false;
        }
        return true;
    }


    private bool ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password) || 
            password.Length < 8)
        {
            statusText.text = "Пароль должен быть не короче 8 символов!";
            return false;
        }
        return true;
    }
    
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    // Проверка существования пользователя в БД
    private bool UserExists(string email)
    {
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = "SELECT email FROM users WHERE email = @email";
        cmd.Parameters.Add(new SqliteParameter("@email", email));
        
        using (IDataReader reader = cmd.ExecuteReader())
        {
            return reader.Read(); // Возвращает true, если есть записи
        }
    }

    // Создание нового пользователя в БД
    private void CreateUser(string email, string passwordHash)
    {
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = "INSERT INTO users (email, password_hash) VALUES (@email, @password)";
        cmd.Parameters.Add(new SqliteParameter("@email", email));
        cmd.Parameters.Add(new SqliteParameter("@password", passwordHash));
        cmd.ExecuteNonQuery();
    }

    // Проверка совпадения логина и пароля
    private bool CheckCredentials(string email, string password)
    {
        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = "SELECT password_hash FROM users WHERE email = @email";
        cmd.Parameters.Add(new SqliteParameter("@email", email));

        string storedHash = "";
        using (IDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                storedHash = reader.GetString(0); // Получаем хеш из БД
            }
        }

        // Сравниваем хеш введенного пароля с сохраненным
        return HashPassword(password) == storedHash;
    }
    
    void OnApplicationQuit()
    {
        if (dbConnection != null && dbConnection.State == ConnectionState.Open)
        {
            dbConnection.Close();
            Debug.Log("Соединение с БД закрыто");
        }
    }
    
    public bool TestValidateEmail(string email)
    {
        return ValidateEmail(email);
    }

    public bool TestValidatePassword(string password)
    {
        return ValidatePassword(password);
    }

    public string TestHashPassword(string password)
    {
        return HashPassword(password);
    }
}