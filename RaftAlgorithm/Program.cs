// Main

// create test machine
var machine1 = new Machine();
System.Console.WriteLine("Made machine1");
_ = machine1.startMachine();

// Run machine for 3 seconds
await Task.Delay(3000);

// Cancel Machine
machine1.stopMachine();
System.Console.WriteLine("Close machine1");
