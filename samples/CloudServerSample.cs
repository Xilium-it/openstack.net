﻿using System;
using System.Linq;
using System.Threading.Tasks;
using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using Rackspace.CloudNetworks;
using Rackspace.CloudNetworks.v2;

public class CloudServerSamples : ISample
{
    public async Task Run(string username, string apiKey, string region)
    {
        const string region = "RegionOne";
        // Configure authentication
        var user = new CloudIdentityWithProject
        {
            Username = "user-name",
            Password = "password",
            ProjectName = "project-name"
        };
        var identity = new CloudIdentityProvider(user);
        var compute = new ComputeService(identity, region);

        // Create a new server

        var results = await compute.ListServersAsync();
        foreach

        Console.WriteLine("Creating Sample Network... ");
        var networkDefinition = new NetworkDefinition {Name = "Sample"};
        var sampleNetwork = await networks.CreateNetworkAsync(networkDefinition);

        Console.WriteLine("Adding a subnet to Sample Network...");
        var subnetDefinition = new SubnetCreateDefinition(sampleNetwork.Id, IPVersion.IPv4, "192.0.2.0/24")
        {
            Name = "Sample"
        };
        var sampleSubnet = await networks.CreateSubnetAsync(subnetDefinition);

        Console.WriteLine("Attaching a port to Sample Network...");
        var portDefinition = new PortCreateDefinition(sampleNetwork.Id)
        {
            Name = "Sample"
        };
        var samplePort = await networks.CreatePortAsync(portDefinition);

        Console.WriteLine("Listing Networks...");
        var networks = await networks.ListNetworksAsync();
        foreach (Network network in networks)
        {
            Console.WriteLine($"{network.Id}\t\t\t{network.Name}");
        }

        Console.WriteLine();
        Console.WriteLine("Sample Network Information:");
        Console.WriteLine();
        Console.WriteLine($"Network Id: {sampleNetwork.Id}");
        Console.WriteLine($"Network Name: {sampleNetwork.Name}");
        Console.WriteLine($"Network Status: {sampleNetwork.Status}");
        Console.WriteLine();
        Console.WriteLine($"Subnet Id: {sampleSubnet.Id}");
        Console.WriteLine($"Subnet Name: {sampleSubnet.Name}");
        Console.WriteLine($"Subnet IPs: {sampleSubnet.AllocationPools.First().Start} - {sampleSubnet.AllocationPools.First().End}");
        Console.WriteLine();
        Console.WriteLine($"Port Id: {samplePort.Id}");
        Console.WriteLine($"Port Name: {samplePort.Name}");
        Console.WriteLine($"Port Address: {samplePort.MACAddress}");
        Console.WriteLine($"Port Status: {samplePort.Status}");
        Console.WriteLine();

        Console.WriteLine("Deleting Sample Network...");
        await networks.DeletePortAsync(samplePort.Id);
        await networks.DeleteNetworkAsync(sampleNetwork.Id);
    }

    public void PrintTasks()
    {
        Console.WriteLine("This sample will perform the following tasks:");
        Console.WriteLine("\t* Create a network");
        Console.WriteLine("\t* Add a subnet to the network");
        Console.WriteLine("\t* Attach a port to the network");
        Console.WriteLine("\t* Delete the network");
    }

}