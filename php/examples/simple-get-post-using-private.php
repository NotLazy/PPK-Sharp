<?php
include("PPK.class.php");

// in production do NOT store your private key in a publicly accessible web directory!
$ppk = new PPK(file_get_contents("private.key"));

if(!empty($_POST)){ // POST request
  if(isset($_POST['decrypt'])){ // client gave us some data to decrypt
    echo $ppk->private->decrypt($_POST['decrypt']);
  }elseif(isset($_POST['encrypt'])){ // client gave us some data to encrypt so they can test decryption on their end.
    echo $ppk->private->encrypt($_POST['encrypt']);
  }
}else{ // GET request
  echo $ppk->public; // return public key
}
