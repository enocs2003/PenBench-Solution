var pension=angular.module('pension')
pension.controller('CountryController',CountryController);
CountryController.$inject= ['$scope','$http','$location','serviceCountry'];
function CountryController($scope,$http,$location,serviceCountry){
$scope.pagetitle='Country';
$scope.save = function(){  
modelCountry = {
int_index: $scope.int_index,
country: $scope.country,
nationality: $scope.nationality,
country_code: $scope.country_code,
zip_code: $scope.zip_code,
new_flag: $scope.new_flag,
upd_flag: $scope.upd_flag,
del_flag: $scope.del_flag,
console: $scope.console,
newedit: $scope.chknewedit,
uid: $scope.uid
}

var checkval;
debugger
console.log(modelCountry);
if(!!modelCountry.newedit){checkval='1';}
else{checkval='0';};
console.log(checkval);
if(checkval=='0'){
var pageconsavecountry=serviceCountry.postCountry(modelCountry);
pageconsavecountry.then(function(response){
alert("Record added successfully.");
$scope.clear();
self.init();
},
function(saveerr){
$scope.errormsg='Failure to save record', erroradd;
});
}
else{
var pageconeditcountry=serviceCountry.updatecountry(modelCountry.int_index,modelCountry);
pageconeditcountry.then(function(response){
alert("Record updated successfully.");
$scope.clear();
self.init();
},
function(errorupd){
$scope.error='Failed to update record', erroradd;
});
}
};
$scope.getCountryrow = function(Countrymodel){
var vault = serviceCountry.getspecificCountry(Countrymodel.int_index);
vault.then(function (response){
$scope.int_index=response.data.int_index;
$scope.country = response.data.country;
$scope.nationality = response.data.nationality;
$scope.country_code = response.data.country_code;
$scope.zip_code = response.data.zip_code;
$scope.new_flag = response.data.new_flag;
$scope.upd_flag = response.data.upd_flag;
$scope.del_flag = response.data.del_flag;
$scope.console = response.data.console;
$scope.chknewedit='1';
},
function(editerr){
console.log("An error occurred: "+errorslst)
});
};

var holderrors="";
$scope.deleteCountry = function(Countrymodel){
var ans=confirm('Are you sure you want to Delete selected record(s)?');
if(!ans){alert('The operation was cancelled by user');}
else{
    debugger
	var ids="";
    console.log('Array Length: '+ holdids.length)
	for(var i=0; i<holdids.length; i++){
            if(ids==""){ids=ids+holdids[i];}
            else{ids=ids+'-'+holdids[i];};

        var holdurl="api/Country/"+ parseInt(holdids[i]); // Countrymodel.int_index;
        //var holdurl="api/Country/DeleteMulti/";
        var pagecondelCountry = serviceCountry.deletecountry(holdurl);
        //var pagecondelCountry = serviceCountry.deletemulti(ids);
        pagecondelCountry.then(function(response){
            if(response.data !=""){
            //alert("Record deleted successfully");
            holderrors=holderrors+"Record deleted successfully"+ ' / ';
            $scope.clear();
            self.init();
            }
            else{
            //alert("Some error occured");
            holderrors=holderrors+"Some error occured"+' / ';
            }
        },
        function(error){
        console.log("Error: "+error);
        });        

        };
    alert(holderrors);
// var holdurl="api/Country/"+ id; // Countrymodel.int_index;
// //var holdurl="api/Country/DeleteMulti/";
// var pagecondelCountry = serviceCountry.deletecountry(holdurl);
// //var pagecondelCountry = serviceCountry.deletemulti(ids);
// pagecondelCountry.then(function(response){
//     if(response.data !=""){
//     //alert("Record deleted successfully");
//     holderrors=holderrors+"Record deleted successfully"+ ' / ';
//     $scope.clear();
//     self.init();
//     }
//     else{
//     //alert("Some error occured");
//     holderrors=holderrors+"Some error occured"+' / ';
//     }
// },
// function(error){
// console.log("Error: "+error);
// });


};
}

$scope.clear=function(){
$scope.int_index='';
$scope.country = '';
$scope.nationality = '';
$scope.country_code = '';
$scope.zip_code = '';
$scope.new_flag = 0;
$scope.upd_flag = 0;
$scope.del_flag = 0;
$scope.console = '';
$scope.chknewedit=0;
$scope.uid = '';
};



		$scope.gridOptions = [];
		var paginationOptions = {  
            pageNumber: 1,  
            pageSize: 5,  
        };

        // //Pagination
        // $scope.pagination = {
        //     paginationPageSizes: [5, 25, 50, 75, 100, "All"],
        //     ddlpageSize: 5,
        //     pageNumber: 1,
        //     pageSize: 5,

        //     getTotalPages: function () {
        //     	debugger
        //         return Math.ceil(this.totalItems / this.pageSize);
        //     },
        //     pageSizeChange: function () {
        //     	debugger
        //         if (this.ddlpageSize == "All")
        //             this.pageSize = $scope.pagination.totalItems;
        //         else
        //             this.pageSize = this.ddlpageSize

        //         this.pageNumber = 1
        //         self.init();
        //     },
        //     firstPage: function () {
        //     	debugger
        //         if (this.pageNumber > 1) {
        //             this.pageNumber = 1
        //             self.init();
        //         }
        //     },
        //     nextPage: function () {
        //     	debugger
        //         if (this.pageNumber < this.getTotalPages()) {
        //             this.pageNumber++;
        //             self.init();
        //         }
        //     },
        //     previousPage: function () {
        //     	debugger
        //         if (this.pageNumber > 1) {
        //             this.pageNumber--;
        //             self.init();
        //         }
        //     },
        //     lastPage: function () {
        //     	debugger
        //         if (this.pageNumber >= 1) {
        //             this.pageNumber = this.getTotalPages();
        //             self.init();
        //         }
        //     }
        // };

$scope.gridOptions = {

				useExternalPagination: false,
                useExternalSorting: false,
                enableFiltering: true,
                enableSorting: true,
                enableRowSelection: true,
                enableSelectAll: true,
                enableGridMenu: true,
                paginationPageSizes: [5,10,20,30,40],
				paginationPageSize: 5,

columnDefs: [
{
displayName: 'Sel.',
name: 'chk',
enableSorting: false,
cellTemplate: '<input tabindex="-1" class="ngSelectionCheckbox" type="checkbox" ng-model="row.entity.changeable" ng-checked="isChecked(row.entity)" /> &nbsp;',
width: 40
},
{
displayName: 'country',
enableSorting: true,
enableColumnResizing: true,
field: 'country'
},
{
displayName: 'nationality',
enableSorting: true,
field: 'nationality'
},
{
displayName: 'country_code',
enableSorting: true,
field: 'country_code'
},
{
displayName: 'zip_code',
enableSorting: true,
field: 'zip_code'
},
{
displayName: 'new_flag',
enableSorting: true,
field: 'new_flag'
},
{
displayName: 'upd_flag',
enableSorting: true,
field: 'upd_flag'
},
{
displayName: 'del_flag',
enableSorting: true,
field: 'del_flag'
},
{
displayName: 'console',
enableSorting: true,
field: 'console'
},
{
displayName: 'Action',
name: 'action',
width: 100,
cellTemplate: '<div class="ngSelectionCell"> <a id="editBtn" ng-click="grid.appScope.getCountryrow(row.entity)" data-toggle="modal" data-target="#myModal">Edit</a> &nbsp; <a id="deleteBtn" ng-click="grid.appScope.deleteCountry(row.entity)" >Delete</a></div>'
}
],
exporterMenuPdf: true,
exporterAllDataFn: function () {
    return getPage(1, $scope.gridOptions.totalItems, paginationOptions.sort)
    .then(function () {
        $scope.gridOptions.useExternalPagination = false;
        $scope.gridOptions.useExternalSorting = false;
        console.log('totalItems: '+ $scope.gridOptions.totalItems)
        getPage = null;
    });
    },

			exporterCsvFilename: 'EmployeeList_hold.csv',  
            exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),  
            onRegisterApi: function(gridApi) {  
                $scope.gridApi = gridApi;  
                gridApi.selection.on.rowSelectionChanged($scope, function(row) {  
                    var msg = 'row selected ' + row.isSelected; 
                    console.log(msg); 
                    console.log(row.entity);
                    if(row.isSelected){
                    	$scope.pushval(row.entity.int_index,'1');
                	}
                	else{$scope.pushval(row.entity.int_index,'0');}
                });  
                gridApi.selection.on.rowSelectionChangedBatch($scope, function(rows) {  
                    var msg = 'rows changed ' + rows.length;  
                    $log.log(msg);
                    console.log(msg);
                    console.log(row.entity);
                });  
                gridApi.pagination.on.paginationChanged($scope, function(newPage, pageSize) {    
                    paginationOptions.pageNumber = newPage;  
                    paginationOptions.pageSize = pageSize;  
                    $scope.pageSize = pageSize;  
                    $scope.currentPage = newPage;  
                    $scope.totalPage = Math.ceil($scope.gridOptions.totalItems / $scope.pageSize);  
                });  
            },

};



self.init=function(){
var querycountry=serviceCountry.getallcountry();
querycountry.then(function(response)
{
$scope.gridOptions.multiSelect = true;
//Added for paging
$scope.gridOptions.totalItems = response.data.length;  
$scope.totalPage = Math.ceil($scope.gridOptions.totalItems / $scope.pageSize);

$scope.gridOptions.data = response.data;
},
function(error){
console.log("Error: "+error);
});
};
self.init();

var holdids=[];
$scope.pushval=function(val,dir){	
	if(dir=='1'){
		holdids.push(val);
	}
	else{
		var index = holdids.indexOf(val);
		if (index >= 0) {
		  holdids.splice( index, 1 );
		}
	};
	console.log(holdids);
}

};



var CountryService = angular.module('pension')
pension.service('serviceCountry',function($http){
this.getallcountry = function(){
return $http.get("api/Country");
}
this.getspecificCountry=function(id){
return $http.get('api/Country/'+ id);
}
this.postCountry = function(modelCountry){
var request=$http({method:"post", url: "api/Country", data: modelCountry});
return request;
}
this.updatecountry = function(id,modelCountry){
var request=$http({method:"put", url: "api/Country/"+id, data: modelCountry});
return request;
}
this.deletecountry = function(routeid){
var request=$http({method:"delete", url: routeid});
return request;
}
this.deletemulti = function(param){
    var request=$http.delete("api/Country/DeleteMulti/", param);
    return request;
}

});

