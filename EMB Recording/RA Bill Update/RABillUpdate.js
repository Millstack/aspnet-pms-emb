﻿
// Category Drop Down
$(document).ready(function () {

    // project master
    $('#ddProject').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddProject').on('select2:select', function (e) {
        var controlId = '<%= ddProject.ClientID %>';
        var script = "__doPostBack('" + controlId + "', '');";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), controlId, script, true);
    });

    // work order
    $('#ddWOName').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddWOName').on('select2:select', function (e) {
        __doPostBack('<%= ddWOName.ClientID %>', '');
    });

    // vendor name
    $('#ddVendorName').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });
    $('#ddVendorName').on('select2:select', function (e) {
        __doPostBack('<%= ddVendorName.ClientID %>', '');
    });

    //=====================================================

    $('#ddProjectMaster').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });

    $('#ddWorkOrder').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });

    $('#ddVender').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });

    $('#ddAbstractNo').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });





    $('#ddDocType').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });

    $('#ddStage').select2({
        placeholder: 'Select here.....',
        allowClear: false,
    });

    //=====================================================

    // Reinitialize Select2 after UpdatePanel partial postback
    var prm = Sys.WebForms.PageRequestManager.getInstance();

    // Reinitialize Select2 for all dropdowns
    prm.add_endRequest(function () {

        setTimeout(function () {

        }, 0);

         // project master
        $('#ddProject').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        // work order
        $('#ddWOName').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        // vendor name
        $('#ddVendorName').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });
        
        //=======================================

        $('#ddProjectMaster').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        $('#ddWorkOrder').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        $('#ddVender').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        $('#ddAbstractNo').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });









        $('#ddDocType').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });

        $('#ddStage').select2({
            placeholder: 'Select here.....',
            allowClear: false,
        });
    });
});