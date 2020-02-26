var app = angular.module("Homeapp", []);

app.controller("HomeController", function ($scope,$http) {
    $scope.btntext = "Save";
    $scope.savedata = function () {
        $scope.btntext = "Please wait...";
        $http({
            method: "POST",
            url: "/Home/Add_Signatory",
            data: $scope.signatory
        }).success(function (d) {
            $scope.btntext = "Save";
            $scope.signatory = null;
            alert(d);           
            $http.get("/Home/GetSignatories").then(function (res) {
                $scope.signatory = res.data;
            }, function (error) {
                alert(error);
            });

           
        }).error(function () {
            alert("failed");
        })
    };


   
});