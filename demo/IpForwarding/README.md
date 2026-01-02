# IpForwarding

This is a demo for forwarding `X-Forwarded-For` header properly in ASP.NET Core WebAPI.

## Demo

The client (First) is calling the proxy (Second) and is then forwarded to the destination (Third). It is expected to have 3 IP addresses inside the XFF when reached the destination.