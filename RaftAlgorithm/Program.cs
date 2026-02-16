// Main

// create test machine 1
var machine1 = new Machine(1);
System.Console.WriteLine("Made machine1");
_ = machine1.startMachine();

// create test machine 2
var machine2 = new Machine(2);
System.Console.WriteLine("Made machine2");
_ = machine2.startMachine();

// create test machine 3
var machine3 = new Machine(3);
System.Console.WriteLine("Made machine3");
_ = machine3.startMachine();

// Run machines for 3 more seconds
await Task.Delay(3000);

// Cancel Machine
machine1.stopMachine();
System.Console.WriteLine("Close machine1");
machine2.stopMachine();
System.Console.WriteLine("Close machine2");
machine3.stopMachine();
System.Console.WriteLine("Close machine3");
