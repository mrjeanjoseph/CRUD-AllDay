﻿# Recipe7-7 - create Printer Pool
# Run on PSRV printer server

# 1. Add a port for the printer
$P2='SalesPP2'
rundll32.exe printui.dll,PrintUIEntry /Xs /n $p Portname $P1,$P2