using LINQSamples;

// Create instance of view model
JoinCollectionsVM vm = new();

// Call Sample Method
var result = vm.InnerJoinQuery();

// Display Results
vm.Display(result);