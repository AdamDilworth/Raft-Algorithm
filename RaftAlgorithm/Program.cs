// Main

// create and start 3 test machines
var machine1 = new Machine(1);
var machine2 = new Machine(2);
var machine3 = new Machine(3);
_ = machine1.startMachine();
_ = machine2.startMachine();
_ = machine3.startMachine();

// Run machines for 1 second
await Task.Delay(1000);

// Pause machine 2
machine2.pauseMachine();

// Run machines for 1 second
await Task.Delay(1000);

// Resume machine 2
machine2.resumeMachine();

// Run machines for 2 more seconds
await Task.Delay(2000);

// Cancel Machine
machine1.stopMachine();
System.Console.WriteLine("Close machine1");
machine2.stopMachine();
System.Console.WriteLine("Close machine2");
machine3.stopMachine();
System.Console.WriteLine("Close machine3");
