# Readme - Code Samples for Chapter 44, Windows Communication Foundation

This chapter contains these samples:

* RoomReservation (a simple WCF client and server)
    * RoomReservationContracts (entity types and interface contracts)
    * RoomReservationData (Entity Framework Core)
    * RoomReservationService (service implementing the RoomReservation contracts)
    * RoomReservationHost (custom host using ServiceHost)
    * RoomReservationWebServiceHost (custom host using WebServiceHost)
    * RoomReservationClient (WPF client applicaiton using the WCF service)
    * RoomReservationClientSharedAssembly (WPF client application using the same contract assembly as the server)
* WebSocketsSample
    * WebSocketsSample (service host using CCallbackContract and netHttpBinding)
    * ClientApp (client application using InstanceContext and CallbackHandler)
* DuplexCommunication
    * MessageService (service using a duplex channel, GetCallbackChannel)
    * DuplexHost (host using wsDualHttpBinding)
    * MessageClient (client using DuplexChannelFactory)
* RoutingSample
    * ServiceContract (WCF service contract)
    * DemoService (simple WCF service)
    * HostOne (a host hosting DemoService)
    * HostTwo (another host hosting DemoService)
    * Router (router host using ServiceHost and MessageFilter)
    * ClientApp (calling the service via the router)

WCF needs (at least the server part) the full .NET Framework.

To build and run the .NET Core samples, please install
* Visual Studio 2015 Update 3
* .NET Core 1.0 for Visual Studio

Please download and install the tools from [.NET Core downloads](https://www.microsoft.com/net/core#windows).
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp6)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!