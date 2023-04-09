// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//ListCategory ListSubCategory
//listCountryId lstCity
//listCountryCtrl lstCityId
function FillSubCategories(ListCategory, ListSubCategory) {
    var subcategorieslist = $('#' + ListSubCategory);
    subcategorieslist.empty();

    var selectedCategory = ListCategory.options[ListCategory.selectedIndex].value;

    if (selectedCategory != null && selectedCategory != '') {
        $.getJSON("/Product/GetSubCategoriesByCategory", { categoryId: selectedCategory }, function (subcategories) {
            if (subcategories != null && !jQuery.isEmptyObject(subcategories)) {
                $.each(subcategories, function (index, subcategory) {
                    subcategorieslist.append($('<option/>',
                        {
                            value: subcategory.value,
                            text: subcategory.text
                            
                    }))
                }); 
            };
        });
    }
    return;
}


function FillCities(lstCountryCtrl, lstCityId) {

    var lstCities = $("#" + lstCityId);
    lstCities.empty();

    var selectedCountry = lstCountryCtrl.options[lstCountryCtrl.selectedIndex].value;
    if (selectedCountry != null && selectedCountry != '') {
        $.getJSON("/Product/GetSubCategoriesByCategory", { categoryId: selectedCountry }, function (subcategories) {
            if (subcategories != null && !jQuery.isEmptyObject(subcategories)) {
                $.each(subcategories, function (index, subcategory) {
                    lstCities.append($('<option/>',
                        {
                            value: subcategory.value,
                            text: subcategory.text
                        }))
                });
            };
        });
    }
    return;
}