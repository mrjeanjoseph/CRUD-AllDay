using LINQSamples;

// Create instance of view model
GroupDataAndCollectionsVM vm = new();

// Call Sample Method
var result = vm.GroupByDistinctMethod();

// Display Results
vm.Display(result);