# Infrastructure Manager

Infrastructure Manager makes it more convenient to provision and work with your virtualized infrastructure by pulling
together the main tools you need into one place.

Infrastructure Manager ties together the tools that you already use for managing your infrastructure, and makes it
quicker and easier to run those tools against your infrastructure inventory.  Some examples:
- Launch a new stack of Linux hosts on AWS with CloudFormation, and get single-click PuTTY shortcuts to make it easy to
remote into those hosts
- Launch a Windows Server 2012 R2 machine on Azure, and get single-click remote desktop access.

What Infrastructure Manager is not:
- A configuration management tool - there are plenty of strong configuration management tools out there, and we don't
want to build another one.  What we do want to do is make it easy to add a push-button provisioned system to your
inventory, with that system being configured by your choice of configuration management tools.

# Status

Infrastructure Manager is a new project attempting to solve the same problem as two separate tools I've previously
created for personal use, but in a more elegant fashion.  There are no official releases of Infrastructure Manager yet,
but you can see the roadmap for the project below.  Releases will be published through github as they become available.

# Roadmap

## Release 1 - Tool Launch

**Value:**
- Make it easy to manage remote connections (SSH via PuTTY and RDP) with consistent settings (e.g. screen dimensions for
  RDP) for hosts under management without needing to manually create and maintain PuTTY or RDP profiles by hand.
- Enable configuration history to be reviewed by tracking all configuration in Git.
- Able to run in portable mode, with the executable and all configuration held on a USB key or other removable media.

Feature list:

- Modeling of:
  - Hosts including NIC's
  - Infrastructure - specifically VMWare ESXi servers
  - Instances including NIC's and IP addresses
    - Registered manually for this release - no automated instance deployment
  - Services on Instances
    - Defined manually for this release - no automated discovery
    - Includes modeling of binding of services to IP addresses, including non-public addresses
- Workstation configuration including:
  - Key file locations
  - Configuration of external tool locations:
    - PuTTY
    - Remote Desktop
- Simple configuration of default PuTTY terminal window settings (e.g. font,
  width, height)
- Simple configuration of default RDP settings (e.g. screen width and height)
- Launching of PuTTY for SSH sessions
  - Direct connections only (no tunneled connections)
  - No creation of port forwards
- Launching of RDP
  - Direct connections only (no tunneled connections)

## Release - Port Forwarding

**Value**
- Can now have port forwards automatically defined, making it much easier to tunnel to services either on the same host
as the SSH daemon, or on other hosts accessible from the SSH host.
- Can quickly launch a SSH or RDP connection which gets to the destination Instance through a SSH tunnel.

Feature list:
- SSH sessions can create port forwards to local services
- Extend model to be able to define which SSH hosts can define port forwards to other hosts on other networks
- SSH sessions can create port forwards to remote services
- SSH connections can be made through a port forward
- RDP connections can be made through a port forward

## Release - Service Discovery

**Value**
- Eliminates the labour of manually registering services by scanning for them instead.  The user will be able to
register the instance (still manually) of the host, and then port-scan it to auto-register the services on that host.
The user will still be able to manually register services if they want to.

- Feature list:
  - Port-scanning of the public IP address of an instance to discover publicly available services
  - Port-scanning of non-public IP addresses including localhost from on-host via SSH to discover services that are not
  bound to the public IP address, but which could be made available via SSH tunnel

## Future

Configuration Management
- Export to shortcuts feature to create PuTTY and RDP shortcuts that can be used without Infrastructure Manager
- Generic stack modeling
- Simple VMWare ESXi configuration package - enables quick creation and launch of a VM on ESXi with some desired ISO
attached.  No other configuration management.
- AWS Cloud Formation configuration package
- Azure Resource Manager configuration package
- Ansible configuration package
- Vagrant configuration package
- Puppet configuration package

Advanced Port Forwarding
- For a service that can only be reached through a port forward, Infrastructure Manager should be able to figure out
which SSH connection opens up the appropriate tunnel and automatically launch that, if it is not already launched.

Diagnostics
- Ping test - ping an instance on it's public IP address
