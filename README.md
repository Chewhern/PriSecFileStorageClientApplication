# PriSecFileStorageClientApplication

What exactly is PriSecFileStorage?
  <br>-> It's an online file storage system that was built based on zero trust towards the service provider.
  <br>-> This includes but not limited to:
<pre>
    <br>=> Encrypting/Decrypting on client device rather than server
    <br>=> File metadata won't be useful for service provider as symmetric encryption encrypted file content and a random file name was sent 
   instead of original file name
    <br>=> Public identity like phone was not collected(email is required by PayPal)
    <br>=> Login mechanism similar to SSH Key Login was used
</pre>
