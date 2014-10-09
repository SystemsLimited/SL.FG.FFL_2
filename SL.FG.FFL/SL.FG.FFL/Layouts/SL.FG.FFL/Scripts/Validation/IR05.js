


function Save_Click() {

    var errorFlag = false;
    var controlList = '';

    var message = '';



    if ($("[id$=IncidentCategory_hdn]").val() == "") {
        errorFlag = true;
        $("[id$=IncidentCategory_msg]").show("fast");

    }
    else
        $("[id$=IncidentCategory_msg]").hide("fast");


    if ($('[id$=Unit_Area_ddl]').val() == "") {
        errorFlag = true;
        $("[id$=Unit_Area_msg]").show("fast");
    }
    else $("[id$=Unit_Area_msg]").hide("fast");




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


    if ($("[id$=Title_tf]").val() == "") {
        errorFlag = true;
        $("[id$=Title_msg]").show("fast");
    }
    else $("[id$=Title_msg]").hide("fast");


    if ($('[id$=EmployeeType_ddl]').val() == "") {
        errorFlag = true;
        $("[id$=EmployeeType_msg]").show("fast");
    }
    else $("[id$=EmployeeType_msg]").hide("fast");


    // In Case Of Traffic Violation/Vehicle Incident
    if ($("[id$=Violation_div]").is(":visible")) {

    if ($("[id$=EmployeeName_tf]").val() == "") {
        errorFlag = true;
        $("[id$=EmployeeName_msg]").show("fast");
    }
    else $("[id$=EmployeeName_msg]").hide("fast");


    if ($("[id$=ViolationBy_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=ViolationBy_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
        errorFlag = true;
        $("[id$=ViolationBy_msg]").show("fast");
    }
    else $("[id$=ViolationBy_msg]").hide("fast");


    if ($("[id$=VehicleNo_tf]").val() == "") {
        errorFlag = true;
        $("[id$=VehicleNo_msg]").show("fast");
    }
    else $("[id$=VehicleNo_msg]").hide("fast");


    if ($("[id$=VehicleCategory_tf]").val() == "") {
        errorFlag = true;
        $("[id$=VehicleCategory_msg]").show("fast");
    }
    else $("[id$=VehicleCategory_msg]").hide("fast");

    if ($('[id$=TypeOfViolation_ddl]').val() == "0" || $('[id$=TypeOfViolation_ddl]').val() == "") {
        errorFlag = true;
        $("[id$=TypeOfViolation_msg]").show("fast");
    }
    else $("[id$=TypeOfViolation_msg]").hide("fast");


    if ($('[id$=Section_Violation_ddl]').val() == "0" || $('[id$=Section_Violation_ddl]').val() == "") {
        errorFlag = true;
        $("[id$=Section_Violation_msg]").show("fast");
    }
    else $("[id$=Section_Violation_msg]").hide("fast");


    if ($('[id$=Department_Violation_ddl]').val() == "0" || $('[id$=Department_Violation_ddl]').val() == "") {
        errorFlag = true;
        $("[id$=Department_Violation_msg]").show("fast");
    }
    else $("[id$=Department_Violation_msg]").hide("fast");


}

    //In Case Of Injury
    if ($("[id$=Injury_div]").is(":visible")) {


        if ($("[id$=NameOfInjured_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=NameOfInjured_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
            errorFlag = true;
            $("[id$=NameOfInjured_msg]").show("fast");
        }
        else $("[id$=NameOfInjured_msg]").hide("fast");


        if ($("[id$=PNO_tf]").val() == "") {
            errorFlag = true;
            $("[id$=PNO_msh]").show("fast");
        }
        else $("[id$=PNO_msh]").hide("fast");


        if ($("[id$=OccupationTrade_tf]").val() == "") {
            errorFlag = true;
            $("[id$=OccupationTrade_msg]").show("fast");
        }
        else $("[id$=OccupationTrade_msg]").hide("fast");

        if ($('[id$=Section_Injury_ddl]').val() == "0" || $('[id$=Section_Injury_ddl]').val() == "") {
            errorFlag = true;
            $("[id$=Injury_Section_msg]").show("fast");
        }
        else $("[id$=Injury_Section_msg]").hide("fast");

        if ($('[id$=Department_Injury_ddl]').val() == "0" || $('[id$=Department_Injury_ddl]').val() == "") {
            errorFlag = true;
            $("[id$=Injury_Department_msg]").show("fast");
        }
        else $("[id$=Injury_Department_msg]").hide("fast");

        if ($('[id$=InjuryCategory_ddl]').val() == "0" || $('[id$=InjuryCategory_ddl]').val() == "") {
            errorFlag = true;
            $("[id$=InjuryCategory_msg]").show("fast");
        }
        else $("[id$=InjuryCategory_msg]").hide("fast");

    }

    if ($("[id$=Description_ta]").val() == "") {
        errorFlag = true;
        $("[id$=Description_msg]").show("fast");
    } else $("[id$=Description_msg]").hide("fast");

    if ($("[id$=ActionTaken_ta]").val() == "") {
        errorFlag = true;
        $("[id$=ActionTaken_msg]").show("fast");

    } else $("[id$=ActionTaken_msg]").hide("fast");


        //if ($("[id$=approvedBy_tf]").val() == "") {
        //    errorFlag = true;
        //    $("[id$=approvedBy_msg]").show("fast");
        //}
        //else $("[id$=approvedBy_msg]").hide("fast");


        //if ($("[id$=approvalDate_dtcDate]").val() == "" || typeof ($("[id$=approvalDate_dtcDate]").val()) == "undefined") {
        //    errorFlag = true;
        //    $("[id$=approvalDate_msg]").show("fast");
        //}
        //else $("[id$=approvalDate_msg]").hide("fast");

        //if ($("[id$=rvf_reportViewed_ta]").val() == "") {
        //    errorFlag = true;
        //    $("[id$=rvf_reportViewed_msg]").show("fast");
        //} else $("[id$=rvf_reportViewed_msg]").hide("fast");
    
    if ($("[id$=HSEDepartment_div]").is(":visible")) {

        if ($("[id$=UM_HSE_Comments_ta]").val() == "") {
            errorFlag = true;
            $("[id$=UM_HSE_Comments_msg]").show("fast");
        } else $("[id$=UM_HSE_Comments_msg]").hide("fast");

    }




        if (errorFlag == true) {

            alert("please fill out the required fields");

            return false;
        }
        else {

            return true;
        }

    

}