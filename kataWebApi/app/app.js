var app = angular.module('app', ['ngRoute', 'ngResource']);

app.config(function ($routeProvider) {
	$routeProvider
		.when('/',
			{
				templateUrl: 'app/views/home.html'
			})
		.when('/newspapers',
			{
				templateUrl: 'app/views/newspaper.html'
			})
		.when('/ads',
			{
				templateUrl: 'app/views/ad.html'
			})
		.otherwise({ redirectTo: '/' });
});


app.factory("Newspaper", function ($resource) {
	return $resource("/api/Newspapers/:id", null,
			{
				'update': { method: 'PUT' }
			});
});

app.factory("Ad", function ($resource) {
	return $resource("/api/Ads/:id", null,
			{
				'update': { method: 'PUT' }
			});
});

app.controller("NewspaperIndexController", function ($scope, Newspaper, Ad) {
	$scope.items = [];
	$scope.ads = [];
	$scope.model = {};

	Newspaper.query(function (data) {
		$scope.items = data;
	})

	Ad.query(function (data) {
		$scope.ads = data;
	})

	$scope.onDelete = function (idx) {
		var item = $scope.items[idx];

		Newspaper.delete({ id: item.Id });
		$scope.items.splice(idx, 1);
	};

	$scope.onAdd = function () {
		var item = {};
		item.Name = $scope.insert.Name;
		item.Description = $scope.insert.Description;

		Newspaper.save(item);
		$scope.items.push(item);
		$scope.insert = {};
	};

	$scope.onEdit = function (idx) {
		$scope.showAdd = false; // hide the add form
		$scope.editRow = idx; // set the index of the row being edited
		var cItem = $scope.items[idx]; // grab the current object 
		$scope.model.Name = cItem.Name;
		$scope.model.Description = cItem.Description;
	};

	$scope.onCancel = function () {
		$scope.editRow = -1;
	};

	$scope.onSave = function () {

		if ($scope.editRow > -1) {
			$scope.model.Id = $scope.items[$scope.editRow].Id;
			var item = { Id: $scope.model.Id, Name: $scope.model.Name, Description: $scope.model.Description };
			Newspaper.update({ id: $scope.model.Id }, item);

			$scope.items[$scope.editRow] = item; // update grid
			$scope.model = {}; // clear the model
			$scope.onCancel();
			$scope.showAdd = false;
		}
	};
});

app.controller("AdIndexController", function ($scope, Ad) {
	$scope.items = [];
	$scope.model = {};

	Ad.query(function (data) {
		$scope.items = data;
	})

	$scope.onDelete = function (idx) {
		var item = $scope.items[idx];

		Ad.delete({ id: item.Id });
		$scope.items.splice(idx, 1);
	};

	$scope.onAdd = function () {
		var item = {};
		item.Name = $scope.insert.Name;
		item.Description = $scope.insert.Description;

		Ad.save(item);

		$scope.items.push(item);
		$scope.insert = {};
	};

	$scope.onEdit = function (idx) {
		$scope.showAdd = false; // hide the add form
		$scope.editRow = idx; // set the index of the row being edited
		var cItem = $scope.items[idx]; // grab the current object 
		$scope.model.Name = cItem.Name;
		$scope.model.Description = cItem.Description;
	};

	$scope.onCancel = function () {
		$scope.editRow = -1;
	};

	$scope.onSave = function () {

		if ($scope.editRow > -1) {
			$scope.model.Id = $scope.items[$scope.editRow].Id;
			var item = { Id: $scope.model.Id, Name: $scope.model.Name, Description: $scope.model.Description };
			Ad.update({ id: $scope.model.Id }, item)

			$scope.items[$scope.editRow] = item; // update grid
			$scope.model = {}; // clear the model
			$scope.onCancel();
			$scope.showAdd = false;
		}
	};
});