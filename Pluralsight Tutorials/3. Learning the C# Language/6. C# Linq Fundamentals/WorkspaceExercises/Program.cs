using LINQSamples;

// Create instance of view model
DeferredExecutionMV vm = new();

// Call Sample Method
var result = vm.UsingYieldAndTake();

// Display Results
vm.Display(result);