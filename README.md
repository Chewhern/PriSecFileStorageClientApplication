# PriSecFileStorageClientApplication

What exactly is PriSecFileStorage?
  -> It's an online file storage system that was built based on zero trust towards the service provider.
  -> This includes but not limited to:
      => Encrypting/Decrypting on client device rather than server
      => File metadata won't be useful for service provider as file content and a random file name was sent instead of original file name
      => Metadata was significantly reduced
      => Public identity like email,phone was not collected
