using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.UI;

public class Test1
{
    private AuthSystem authSystem;

    [SetUp]
    public void SetUp()
    {
        GameObject authGameObject = new GameObject();
        authSystem = authGameObject.AddComponent<AuthSystem>();
        
        GameObject textObject = new GameObject();
        authSystem.statusText = textObject.AddComponent<Text>();
    }
    
    [Test]
    public void ValidateEmail_InvalidEmailNoAt_ChangesStatusTextAndReturnsFalse()
    {
        bool result = authSystem.TestValidateEmail("testexample.com");
        Assert.AreEqual("Некорректный email!", authSystem.statusText.text);
        Assert.IsFalse(result);
    }

    [Test]
    public void ValidateEmail_InvalidEmailNoDot_ChangesStatusTextAndReturnsFalse()
    {
        bool result = authSystem.TestValidateEmail("test@examplecom");
        Assert.AreEqual("Некорректный email!", authSystem.statusText.text);
        Assert.IsFalse(result);
    }

    [Test]
    public void ValidateEmail_ValidEmail_DoesNotChangeStatusTextAndReturnsTrue()
    {
        bool result = authSystem.TestValidateEmail("test@example.com");
        Assert.AreNotEqual("Некорректный email!", authSystem.statusText.text);
        Assert.IsTrue(result);
    }

    [Test]
    public void ValidatePassword_ValidPassword_ReturnsTrue()
    {
        Assert.IsTrue(authSystem.TestValidatePassword("strongPass1"));
    }

    [Test]
    public void ValidatePassword_ShortPassword_ReturnsFalse()
    {
        Assert.IsFalse(authSystem.TestValidatePassword("short"));
    }

    [Test]
    public void HashPassword_SameInputProducesSameHash()
    {
        string password = "securePassword123";
        string hash1 = authSystem.TestHashPassword(password);
        string hash2 = authSystem.TestHashPassword(password);
        Assert.AreEqual(hash1, hash2);
    }

    [Test]
    public void HashPassword_DifferentInputsProduceDifferentHashes()
    {
        string hash1 = authSystem.TestHashPassword("password1");
        string hash2 = authSystem.TestHashPassword("password2");
        Assert.AreNotEqual(hash1, hash2);
    }
}
