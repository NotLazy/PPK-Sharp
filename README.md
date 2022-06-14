# PPK-Sharp
Public/Private Key Encryption/Decryption with super simple integration with PHP and C#

# What is PPK-Sharp?
PPK-Sharp is a quick and easy way to quickly get your foot in the door with PHP and C# based RSA public/private key encryption.

# Requirements

### PHP 7.0 or higher

You may be able to use PHP 4.2 or PHP 5, but do not submit an issue if you aren't using PHP 7.0 or higher

### Any Version of .NET that supports BouncyCastle

Check Nuget for BouncyCastle, you'll need to add BouncyCastle to your project for this to work in C#

# Dependencies

- [PHP] OpenSSL 1.1.1 or higher
- [C#] BouncyCastle 1.8.10 or higher for `.Net Core 3.1` or equivilent BouncyCastle Package

# Installation

## PHP
- Drag and drop the files from `php/` into your php project directory, or to a location on your web server.
- Ensure OpenSSL has been installed or enabled
- Write a PHP document that includes `PPK.class.php`

## C# 
- Drag and drop the files from `csharp/` into your Visual Studio project directory
- Ensure BouncyCastle has been installed from Nuget in Visual Studio
- Write a class that uses PPK

# Usage

Check the `examples/` subfolder of both languages (eg. `php/examples/`) for more examples, but the basic usage is this:

## PHP
```
<?php
include("PPK.class.php");
$pem = new PPK();
$encrypted_data = $pem->private->encrypt("Hello World");
$decrypted_data = $pem->public->decrypt($encrypted_data);

echo $decrypted_data; // Output: Hello World
```

## C#
```
using System;
using PPKSharp;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // you can provide your key by fetching it from a web server, or by reading it from a file.
            string PrivateKey = "... your public or private key here...";
            string PublicKey = "... your public or private key here...";

            PPK ppkPublic = new PPK(PublicKey); // key should be in PEM format
            PPK ppkPrivate = new PPK(PrivateKey); // key should be in PEM format
            
            string encrypted = ppkPublic.Encrypt("Hello World");

            string decrypted = ppkPrivate.Decrypt(encrypted);
            Console.WriteLine(decrypted);
        }
    }
}
```

# Discrepancies between PHP and C#

- PPK-PHP supports generation of private keys, PPK-Sharp does not
- PPK-Sharp supports initialization using private OR public keys, PPK-PHP only supports initialization using private keys.
- PPK-Sharp cannot tell you what the public key of a private key is, but PPK-PHP can.
