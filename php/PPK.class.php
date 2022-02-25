<?php

class PPK{
  public $public;
  public $private;

  function __construct($private = null){
    if($private == null){
      $openssl_key = openssl_pkey_new();
      $key_details = openssl_pkey_get_details($openssl_key);
      $public_key = $key_details['key']; // PEM format
      openssl_pkey_export($openssl_key, $private_key);

      $this->public = new PublicPPKObject($public_key);
      $this->private = new PrivatePPKObject($private_key);
    }else{
      $openssl_key = openssl_pkey_get_private($private);
      $key_details = openssl_pkey_get_details($openssl_key);
      $public_key = $key_details['key']; // PEM format

      $this->public = new PublicPPKObject($public_key);
      $this->private = new PrivatePPKObject($private);
    }
  }
}

class PublicPPKObject{
  private $key;
  function __construct(string $public){
    $this->key = $public;
  }
  function encrypt($data){
    return base64_encode($this->encrypt($data));
  }
  function decrypt($data){
    return $this->decrypt(base64_decode($data));
  }
  function __toString(){
    return $this->key;
  }
}
class PrivatePPKObject{
  private $key;
  function __construct(string $private){
    $this->key = $private;
  }
  function encrypt($data){
    return base64_encode($this->encrypt($data));
  }
  function decrypt($data){
    return $this->decrypt(base64_decode($data));
  }
  function __toString(){
    return $this->key;
  }
}
