NTttcp (a Winsock-based port of BSD’s TTCP tool) is a useful tool to help measure overall Windows networking performance with a multitude of networking adapters in different configurations.

For example, to measure the network performance between two multi-core serves running Windows Server 2012, NODE1 (192.168.0.1) and NODE2 (192.168.0.2), connected via a 10GigE connection, on NODE1 (the sender), run:

ntttcp.exe -s -m 8,*,192.168.0.2 -l 128k -a 2 -t 15
(Translation: Run ntttcp.exe as a sender, with eight threads dynamically allocated across all cores targeting 192.168.0.2, allocating a 128K buffer length and operating in asynchronous mode with 2 posted send overlapped buffers per thread for 15 seconds.)

And on NODE2 (the receiver), run:

ntttcp.exe -r -m 8,*,192.168.0.2 -rb 2M -a 16 -t 15
(Translation: Run ntttcp.exe as a receiver, with eight threads dynamically allocated across all cores listening on 192.168.0.2, allocating 64KB buffers [since -l is not specified], a 2MB SO_RCVBUF Winsock buffer and operating in asynchronous mode with 16 posted receive overlapped buffers per thread for 15 seconds.)

Using the above parameters, the program returns results on both the sender and receiver nodes, correlating network communication to CPU utilization.  Example sender-side output from a given run (which showcases a fully saturated 10GigE connection at 1131.4 MB/s):

D:\>NTttcp.exe -s -m 8,*,192.168.0.2 -l 128k -a 2 -t 15
Copyright Version 5.28
Network activity progressing...
Thread  Time(s) Throughput(KB/s) Avg B / Compl
======  ======= ================ =============
     0   30.016        77074.627    131072.000
     1   30.016       231692.964    131072.000
     2   30.016       115782.516    131072.000
     3   30.016       231496.802    131072.000
     4   30.016       116072.495    131072.000
     5   30.016        77155.650    131072.000
     6   30.016       231611.940    131072.000
     7   30.016        77633.262    131072.000
#####  Totals:  #####
   Bytes(MEG)    realtime(s) Avg Frame Size Throughput(MB/s) Throughput(Buffers/s) Cycles/Byte       Buffers DPCs(count/s) Pkts(num/DPC)   Intr(count/s) Pkts(num/intr) Packets Sent Packets Received Retransmits Errors Avg. CPU %
================ =========== ============== ================ ===================== =========== ============= ============= ============= =============== ============== ============ ================ =========== ====== ==========
    33959.125000      30.016       1456.355         1131.367              9050.939       2.453    271673.000     40284.215         1.228       51434.235          0.962     24450576          1485400           0      0      4.559

Additional Information

http://gallery.technet.microsoft.com/NTttcp-Version-528-Now-f8b12769
