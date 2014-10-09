



    function Save_Click() {

        var errorFlag = false;
        var controlList = '';

        var message = '';



        if ($("[id$=IR_IReceivingDate_dtcDate]").val() == "" || typeof ($("[id$=IR_IReceivingDate_dtcDate]").val()) == "undefined") {
            errorFlag = true;
            $("[id$=IR_IReceivingDate_msg]").show("fast");
        }
        else $("[id$=IR_IReceivingDate_msg]").hide("fast");



        if ($("[id$=FlashIssueDate_dtcDate]").val() == "" || typeof ($("[id$=FlashIssueDate_dtcDate]").val()) == "undefined") {
            errorFlag = true;
            $("[id$=FlashIssueDate_msg]").show("fast");
        }
        else $("[id$=FlashIssueDate_msg]").hide("fast");


        if ($('[id$=Unit_Section_ddl]').val() == "") {
            errorFlag = true;
            $("[id$=Unit_Section_msg]").show("fast");
        }
        $("[id$=Unit_Section_msg]").hide("fast");



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


        if ($("[id$=Description1_ta]").val() == "") {
            errorFlag = true;
            $("[id$=Description1_msg]").show("fast");
        } else
            $("[id$=Description1_msg]").hide("fast");


        if ($("[id$=ActionTaken_ta]").val() == "") {
            errorFlag = true;
            $("[id$=ActionTaken_msg]").show("fast");
        } else
            $("[id$=ActionTaken_msg]").hide("fast");


        if ($("[id$=IncidentScore_tf]").val() == "") {
            errorFlag = true;
            $("[id$=IncidentScore_msg]").show("fast");
        }
        else $("[id$=IncidentScore_msg]").hide("fast");


        if ($('[id$=ActionRequired_Unit_ddl]').val() == "") {
            errorFlag = true;
            $("[id$=ActionRequired_Unit_msg]").show("fast");
        }
        $("[id$=ActionRequired_Unit_msg]").hide("fast");


        if ($('[id$=ResponsibleDepartmentt_ddl]').val() == "") {
            errorFlag = true;
            $("[id$=ResponsibleDepartmentt_msg]").show("fast");
        }
        $("[id$=ResponsibleDepartmentt_msg]").hide("fast");



        if ($("[id$=TargetDate_dtcDate]").val() == "" || typeof ($("[id$=TargetDate_dtcDate]").val()) == "undefined") {
            errorFlag = true;
            $("[id$=TargetDate_msg]").show("fast");
        }
        else $("[id$=TargetDate_msg]").hide("fast");



        if ($("[id$=ApprovingAuthority_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=ApprovingAuthority_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
            errorFlag = true;
            $("[id$=ApprovingAuthority_msg]").show("fast");
        }
        else $("[id$=ApprovingAuthority_msg]").hide("fast");


        if ($("[id$=TeamMembers_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=TeamMembers_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
            errorFlag = true;
            $("[id$=TeamMembers_msg]").show("fast");
        }
        else $("[id$=TeamMembers_msg]").hide("fast");

      

        if ($("[id$=TeamLead_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=TeamLead_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
            errorFlag = true;
            $("[id$=TeamLead_msg]").show("fast");
        }
        else $("[id$=TeamLead_msg]").hide("fast");

        if ($("[id$=Description2_ta]").val() == "") {
            errorFlag = true;
            $("[id$=Description2_msg]").show("fast");
        } else
            $("[id$=Description2_msg]").hide("fast");


        if (errorFlag == true) {

            alert("please fill out the required fields");

            return false;
        }
        else {

            return true;
        }

    }


