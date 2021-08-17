# PriSecFileStorageClientApplication

```
Disclaimer: If you come from any country that blocked Signal/WhatsApp/Session/Threema/Matrix messaging application
such as China, you are advised to not use it publicly. The cyberlaw or cryptography law within China allows
only the use of cryptography in such a way that only technology like TLS/SSL/SSH exists. There's also a work
around to this problem, which was you store all the encryption keys in server/service provider side. However,
any people who has basic information security or cyber security or cryptography understanding, will know that,
this makes the server or service provider or network an extremely obvious and vulnerable target to both hackers
within the service provider side or outsider hacker. If that do happens or if the server do get hacked(which
is 100% or almost close to 100%), just to give you an idea.. Your card details, your passwords, your email,
your phone number will be published to internet in an unauthorized way(darkweb), law does not help as this
happens so frequently in open society let alone in close society. Using such mechanism is almost a suicide.

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
