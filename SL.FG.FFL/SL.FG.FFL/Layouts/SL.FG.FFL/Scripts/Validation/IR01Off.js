


function btnSupervisorSave_Click() {

    //var errorFlag = false;
    //var controlList = '';

    //var message = '';

    //if ($('[id$=HSEManagerName_div]').is(":visible")) {
    //    if ($("[id$=HSEManagerName_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=HSEManagerName_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
    //        errorFlag = true;
    //        $("[id$=HSEManagerName_PeopleEditor_msg]").show("fast");
    //    }
    //}
    //else $("[id$=HSEManagerName_PeopleEditor_msg]").hide("fast");

    //if (errorFlag == true) {

    //    return false;
    //}
    //else {

    //    return true;
    //}

}

function MOSave_Click() {

    var errorFlag = false;
    var controlList = '';

    var message = '';

    if ($('[id$=MORemarks_ta]').is(":visible") && $('[id$=MORemarks_ta]').val() == "") {
        errorFlag = true;
        $("[id$=MORemarks_msg]").show("fast");
    }
    else $("[id$=MORemarks_msg]").hide("fast");

    if (errorFlag == true) {

        return false;
    }
    else {

        return true;
    }

}


function Save_Click() {

    var errorFlag = false;
    var controlList = '';

    var message = '';


    if ($("[id$=IncidentCategory_hdn]").val() == "") {
        errorFlag = true;
        $("[id$=IncidentCategory_msg]").show("fast");
    }
    else {
        $("[id$=IncidentCategory_msg]").hide("fast");

        $("[id$=IncidentCategory_ddl]").each(function () {

            var value = $(this).val();

            if (value.indexOf("Injury") >= 0) {

                if ($("[id$=ConsentTaken_ddl] option:selected").val() == "0") {
                    errorFlag = true;
                    $("[id$=ConsentTaken_msg]").show("fast");

                }
                else {
                    $("[id$=ConsentTaken_msg]").hide("fast");

                        $("[id$=ConsentTaken_ddl]").each(function () {

                            var value = $(this).val();

                            if (value.indexOf("Yes") >= 0) {
                               
                                if ($("[id$=Date_dtcDate]").val() == "" || typeof ($("[id$=Date_dtcDate]").val()) == "undefined") {
                                    errorFlag = true;
                                    $("[id$=Date_msg]").show("fast");
                                }
                                else $("[id$=Date_msg]").hide("fast");

                                if ($("[id$=MOName_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=MOName_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
                                    errorFlag = true;
                                    $("[id$=MOName_PeopleEditor_msg]").show("fast");
                                }
                                else $("[id$=MOName_PeopleEditor_msg]").hide("fast");
                               
                            }                           

                        });

                }

            }

        });
    }

  


   


    //if ($("[id$=ConsentTaken_ddl] option:selected").val() == "0") {
    //    errorFlag = true;
    //    $("[id$=ConsentTaken_msg]").show("fast");

    //}
    //else
    //    $("[id$=ConsentTaken_msg]").hide("fast");



   


    if ($("[id$=InjuryCategory_hdn]").val() == "") {
        errorFlag = true;
        $("[id$=InjuryCategory_msg]").show("fast");
    }
    else
        $("[id$=InjuryCategory_msg]").hide("fast");


    if ($("[id$=EmployeeType_ddl] option:selected").val() == "0") {
        errorFlag = true;
        $("[id$=EmployeeType_msg]").show("fast");

    }
    else
        $("[id$=EmployeeType_msg]").hide("fast");


    //if ($('[id$=EmployeeType_ddl] ').val() == "") {
    //    errorFlag = true;
    //}

    //if ($('[id$=ConsentTaken_ddl] ').val() == "") {
    //    errorFlag = true;
    //}



    //if ($("[id$=Date_dtcDate]").val() == "" || typeof ($("[id$=Date_dtcDate]").val()) == "undefined") {
    //    errorFlag = true;
    //    $("[id$=Date_msg]").show("fast");
    //}
    //else $("[id$=Date_msg]").hide("fast");


    //if (typeof ($("[id$=MOName_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined" || $("[id$=MOName_PeopleEditor] span.ms-entity-resolved").attr("title") == "") {
    //    errorFlag = true;
    //    $("[id$=MOName_PeopleEditor_msg]").show("fast");
    //}
    //else $("[id$=MOName_PeopleEditor_msg]").hide("fast");




    if ($("[id$=DateOfIncident_dtcDate]").val() == "" || typeof ($("[id$=DateOfIncident_dtcDate]").val()) == "undefined") {
        errorFlag = true;
        $("[id$=DateOfIncident_msg]").show("fast");
    }
    else $("[id$=DateOfIncident_msg]").hide("fast");


    //if ($("[id$=TimeOfIncident_dtcDateHourse]").val() == "" || typeof ($("[id$=TimeOfIncident_dtcDateHourse]").val()) == "undefined" || $("[id$=TimeOfIncident_dtcDateMinuts]").val() == "" || typeof ($("[id$=TimeOfIncident_dtcDateMinuts]").val()) == "undefined") {
    //    errorFlag = true;
    //    $("[id$=TimeOfIncident_msg]").show("fast");
    //}
    //else
    //    $("[id$=TimeOfIncident_msg]").hide("fast");

    if ($('[id$=Unit_Area_ddl]').val() == "" || $('[id$=Unit_Area_ddl] option:selected').val() == "0") {
        errorFlag = true;
        $("[id$=Unit_Area_msg]").show("fast");
    }
    $("[id$=Unit_Area_msg]").hide("fast");

    if ($("[id$=IncidentScore_tf]").val() == "") {
        errorFlag = true;
        $("[id$=IncidentScore_msg]").show("fast");
    }
    else $("[id$=IncidentScore_msg]").hide("fast");


    if ($("[id$=SubmittedBy_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=SubmittedBy_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
        errorFlag = true;
        $("[id$=SubmittedBy_PeopleEditor_msg]").show("fast");
    }
    else $("[id$=SubmittedBy_PeopleEditor_msg]").hide("fast");


    if ($("[id$=SubmissionDate_dtcDate]").val() == "") {
        errorFlag = true;
        $("[id$=SubmissionDate_msg]").show("fast");
    }
    else $("[id$=SubmissionDate_msg]").hide("fast");

    if ($("[id$=Title_tf]").val() == "") {
        errorFlag = true;
        $("[id$=Title_msg]").show("fast");
    }
    else $("[id$=Title_msg]").hide("fast");


    if ($("[id$=Description_ta]").val() == "") {
        errorFlag = true;
        $("[id$=Description_msg]").show("fast");
    } else $("[id$=Description_msg]").hide("fast");

    if ($("[id$=ActionTaken_ta]").val() == "") {
        errorFlag = true;
        $("[id$=ActionTaken_msg]").show("fast");
    }
    else $("[id$=ActionTaken_msg]").hide("fast");

    if (errorFlag == true) {

        alert("please fill out the required fields");

        return false;
    }
    else {

        return true;
    }

}


