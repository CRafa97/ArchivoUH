var countrydelegate = function (){
    var selectedCountry = $("#Country").val();
    var provincesSelect = $('#Province');
    var l = $('#Locality');
    provincesSelect.empty();
    l.empty();
    if (selectedCountry != null && selectedCountry != '') {
        $.getJSON('/Graduates/GetProvinces', { id: selectedCountry }, function (provinces) {
            if (provinces != null && !jQuery.isEmptyObject(provinces)) {
                var prov = null;
                var t = 0;
                $.each(provinces, function (index, province) {
                    provincesSelect.append($('<option/>', {
                        value: province.Value,
                        text: province.Text
                    }));

                    if (t == 0) {
                        prov = province.Value;
                        t = 1;
                    };
                });

                if (t != 0) {
                    $.getJSON('/Graduates/GetLocalities', { id: prov }, function (ls) {
                        if (ls != null && !jQuery.isEmptyObject(ls)) {
                            $.each(ls, function (index, local) {
                                l.append($('<option/>', {
                                    value: local.Value,
                                    text: local.Text
                                }));
                            });
                        };
                    });
                };
            };
        });
    };
};

$('#Country').change(countrydelegate);

$('#Province').change(function () {
    var province = $('#Province').val();
    var l = $('#Locality');
    l.empty();
    if (province != null && province != '') {
        $.getJSON('/Graduates/GetLocalities', { id: province }, function (ls) {
            if (ls != null && !jQuery.isEmptyObject(ls)) {
                $.each(ls, function (index, local) {
                    l.append($('<option/>', {
                        value: local.Value,
                        text: local.Text
                    }));
                });
            };
        });
    };
});