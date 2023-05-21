namespace Sandbox.Ipcheck

module Program =

    open System.Net
    open System.Net.NetworkInformation

    [<EntryPoint>]
    let main argv =
        if NetworkInterface.GetIsNetworkAvailable() then
            printfn "Current IP Addresses: "

            let hostname = Dns.GetHostName()
            let host = Dns.GetHostEntry(hostname)

            for address in host.AddressList do
                printf $"\t{address}"
        else
            printfn "No Network Connection"
        
        0

