var app = angular.module("Signatoryapp", []);

app.controller("SignatoryController", function ($scope,$http) {
    $scope.getSignatory = function () {
        $http({
            method: "GET",
            url:"/Home/GetSignatories"
        }).success(function (d) {

        })

    }
});