# PriSecFileStorageClientApplication

```
Disclaimer: If you come from any country that blocked Signal/WhatsApp/Session/Threema/Matrix messaging application
such as China excluding Macao,Taiwan & Hong Kong, you are advised to not use it publicly. The cyberlaw or cryptography
law within China allows only the use of cryptography in such a way that only technology like TLS/SSL/SSH exists.
There's also a work around to this problem, which was you store all the encryption keys in server/service provider
side. However, any people who has basic information security or cyber security or cryptography understanding, will
know that, this makes the server or service provider or network an extremely obvious and vulnerable target to
both hackers within the service provider side or outsider hacker. If that do happens or if the server do
get hacked(which is 100% or almost close to 100%), just to give you an idea.. Your card details,
your passwords, your email, your phone number will be published to internet in an unauthorized way(darkweb),
law does not help as this happens so frequently in open society let alone in close society.
Using such mechanism is almost a suicide.

There's one more workaround to this problem which is include backdoor in the cryptography algorithm. This
was also not logic at all because all the algorithm is publicly available, there're also papers that written
by scholars and show to public how it can be done, its strength, its vulnerability. Inserting backdoor
to the algorithm is impossible. If you do find a way to insert a backdoor to the algorithm, many just 
won't use it and just use only the algorithms without backdoor.

The statement - "Make the information opaque to the bad actors but transparent to the government" is
completely ludicrous. In the example above, you should see a serious problem which is "The information
can only be transparent to both bad actors and government" and the request of "Make the information 
opaque to the bad actors but transparent to the government" is completely nonsense.

Given I make my work open source(paid/subscription based), if in future any government like China demands
a backdoor, I am sorry I can't do that. There're many eyes watching. This is the great example of open
source. If there's backdoor, many will just clone and make their own. This movement can't be stopped.
(Based on my understanding) If there's backdoor in linux, it will affect not only a single country
users, it will also affect global users as the backdoor could be discovered and backfire. Given such 
case, linux will lose its popularity within corporate/businesses/programmer/developer/software engineering,
if it affects these people, normal non tech savvy users will also have a really bad influence.
Linux doing such thing is a suicide move. It will force people to use back only the close source software.

I need to make this very clear. What I am describing here is all factual. If you do come from China
or any other similar countries, just use it privately. In the worst case, expect that if you use VPN,
VPN will also block access to my stuffs(apply to China or any similar country netizens).
```

## Versions (Windows) --It's recommended to go for 0.0.5 version instead of going for older versions.
**If you want to download the application simply navigate to "releases", that's the compiled and running
windows application**
1. 0.0.3 is a client application that does not include problematic privacy based MFA
2. 0.0.4 is a client application that includes problematic privacy based MFA
3. 0.0.5 is a client application that switches from .Net Framework to .Net 5

## Versions (Cross Desktop OS- Web) --It's recommended to use this version for MacOS and Linux as a short-term solution.
Simply navigate to **Cross Desktop OS** and then navigate to **Web** to find instruction on how you can compile and
run the application by your own.

### What exactly is PriSecFileStorage?
  <br>-> It's an online file storage system that was built based on zero trust towards the service provider.
  <br>-> This is an application that uses no password in logging in or generating cryptography keys
  <br>-> The generation of cryptography keys be it symmetric encryption keys or public key cryptography keys
  are all relying on pure cryptography randomness(by default)
  <br>-> This uses SSH key style/public key digital signature algorithm with challenge and respond login
  mechanism
  <br>-> Symmetric encryption keys are all generated and consumed locally, these are never shared with
  server or send to server
  <br>-> A confusion was deliberately put on server side to further conceal what the file really is.

This readme will describe the cryptography element of how this file storage works.

This readme will also describe how this file storage works in general.

#### Login Mechanism

SSH key style login/public key cryptography digital signature with challenge and respond was used.

Majority of the user just need to take a good care towards the keys generated after making or
renewing the payment. The keys generated will be used to authenticate with the server in backend
to prove to the server who the user really is. Did they have access to access the files?

If there exists a special made corporate/company version, the keys generated through registration
or login do play a role. Otherwise, the normal version, user don't need to worry about the
registration keys as they don't play an important role.

#### Randomness and confusion in file name

File name is always a major concern as it can tell a little too much information regarding the file
itself. It is not certain whether the file name gives clear hint on what are the files that the
user store or upload. File name such as customer info, bank info, passwords do give a clear information
about the file. If these information were known by bad actors, safely speaking, they can muster up
the computational power to break the encrypted file content. This do seem like overworrying but it
does happen. Giving out the information of the original file name does make the confidentiality
or privacy aspect of the file gone by approximately 50% as people still require to break the
encryption on the file content.

By considering that this can happen, original filename was never sent to server. Instead, the
client software by default will generate random password excluding special characters as the
random file name. This random file name was then send to the server. This increases the file's
confidentiality/privacy as it brings confusion to the service provider and bad actor whether it's
a worthy decision to break or remove the encryption on the file content.

Original filename and its extension never leaves the client device/machine. However, this do means
that the client can't lose the file that stores its original file name and extension. Losing this
file do mean that decryption can't be done properly at client device in latter days.

#### Symmetric encryption and public key digital signature signing

Let's assume the file is 15 MB in size. The file will be split into 3 parts, which are
part A, B and C, each part size will be exactly the same. 

Client will generate a random symmetric encryption key and digital signature keypair
on their devices. Part A will then first encrypt with the random symmetric encryption
key, then part A will sign it with the digital signature keypair private key.

Part B and part C will follow the way like Part A. Each time the splitted file will
use different random key and sign with random private key.

These generated keypairs(both public and private key) and keys are not sent to the server.

The existence of signature public key was to let the file owner to verify its own file
(signature verification) or let their friends to verify the file(signature verification)
before the file can be decrypted.

Given this case, the public keys aren't meant to let the server hold. As the server is
not the verifier be it the file owner itself or the file owner friends.
