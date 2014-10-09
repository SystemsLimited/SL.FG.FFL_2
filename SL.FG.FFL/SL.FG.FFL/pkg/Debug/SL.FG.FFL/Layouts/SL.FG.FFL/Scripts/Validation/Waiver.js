

function convertStringToDate(str) {
    try {
        var temp = str.split('/');

        if (temp.length > 2) {
            var d = new Date(temp[2], (parseInt(temp[1]) - 1), temp[0]);
            return d;
        }
    }
    catch (ex) {
    }
    return null;
}

function Save_Click() {

    var errorFlag = false;
    var controlList = '';

    var message = '';


    if ($("[id$=DescriptionOfTheRecommendations_ta]").val() == "") {
        errorFlag = true;
        $("[id$=DescriptionOfTheRecommendations_msg]").show("fast");
    }
    else $("[id$=DescriptionOfTheRecommendations_msg]").hide("fast");


    if ($("[id$=RecommendationReason_ta]").val() == "") {
        errorFlag = true;
        $("[id$=RecommendationReason_msg]").show("fast");
    }
    else $("[id$=RecommendationReason_msg]").hide("fast");


    


    if ($("[id$=NewTargetDate_dtcDate]").val() == "" || typeof ($("[id$=NewTargetDate_dtcDate]").val()) == "undefined") {
        errorFlag = true;
        $("[id$=NewTargetDate_msg]").show("fast");
    }
    else {

        $("[id$=NewTargetDate_msg]").hide("fast");


        if ($("[id$=RecommendationCompletionDueDate_dtcDate]").val() != "" || typeof ($("[id$=RecommendationCompletionDueDate_dtcDate]").val()) == "undefined" && $("[id$=NewTargetDate_dtcDate]").val() != "" || typeof ($("[id$=NewTargetDate_dtcDate]").val()) == "undefined") {

            var RecommendationCompletionDueDate = convertStringToDate($("[id$=RecommendationCompletionDueDate_dtcDate]").val());
            var NewTargetDate = convertStringToDate($("[id$=NewTargetDate_dtcDate]").val());

            if (RecommendationCompletionDueDate != null && NewTargetDate != null && RecommendationCompletionDueDate > NewTargetDate) {
                errorFlag = true;
                $("[id$=NewTargetDateCompare_msg]").show("fast");
            }
            else $("[id$=NewTargetDateCompare_msg]").hide("fast");
        }


    }



    if ($("[id$=InitiatedBy_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=InitiatedBy_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
        errorFlag = true;
        $("[id$=InitiatedBy_PeopleEditor_msg]").show("fast");
    }
    else $("[id$=InitiatedBy_PeopleEditor_msg]").hide("fast");




    if ($("[id$=InitiatedDate_dtcDate]").val() == "" || typeof ($("[id$=InitiatedDate_dtcDate]").val()) == "undefined") {
        errorFlag = true;
        $("[id$=InitiatedDate_msg]").show("fast");
    }
    else $("[id$=InitiatedDate_msg]").hide("fast");




    if ($("[id$=InitiaterComments_ta]").val() == "") {
        errorFlag = true;
        $("[id$=InitiaterComments_msg]").show("fast");
    }
    else $("[id$=InitiaterComments_msg]").hide("fast");


    //In Case Of Injury
    if ($("[id$=UM_div]").is(":visible")) {



        if ($("[id$=UM_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=UM_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
            errorFlag = true;
            $("[id$=UM_PeopleEditor_msg]").show("fast");
        }
        else $("[id$=UM_PeopleEditor_msg]").hide("fast");




        if ($("[id$=UMDate_dtcDate]").val() == "" || typeof ($("[id$=UMDate_dtcDate]").val()) == "undefined") {
            errorFlag = true;
            $("[id$=UMDate_msg]").show("fast");
        }
        else $("[id$=UMDate_msg]").hide("fast");




        if ($("[id$=UMComments_ta]").val() == "") {
            errorFlag = true;
            $("[id$=UMComments_msg]").show("fast");
        }
        else $("[id$=UMComments_msg]").hide("fast");





    }


    if ($("[id$=DM_div]").is(":visible")) {


        if ($("[id$=DM_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=DM_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
            errorFlag = true;
            $("[id$=DM_PeopleEditor_msg]").show("fast");
        }
        else $("[id$=DM_PeopleEditor_msg]").hide("fast");




        if ($("[id$=DMDate_dtcDate]").val() == "" || typeof ($("[id$=DMDate_dtcDate]").val()) == "undefined") {
            errorFlag = true;
            $("[id$=DMDate_msg]").show("fast");
        }
        else $("[id$=DMDate_msg]").hide("fast");




        if ($("[id$=DMComments_ta]").val() == "") {
            errorFlag = true;
            $("[id$=DMComments_msg]").show("fast");
        }
        else $("[id$=DMComments_msg]").hide("fast");



    }

    if ($("[id$=D0_div]").is(":visible")) {


        if ($("[id$=DO_PeopleEditor] span.ms-entity-resolved").attr("title") == "" || typeof ($("[id$=DO_PeopleEditor] span.ms-entity-resolved").attr("title")) == "undefined") {
            errorFlag = true;
            $("[id$=DO_PeopleEditor_msg]").show("fast");
        }
        else $("[id$=DO_PeopleEditor_msg]").hide("fast");




        if ($("[id$=DODate_dtcDate]").val() == "" || typeof ($("[id$=DODate_dtcDate]").val()) == "undefined") {
            errorFlag = true;
            $("[id$=DODate_msg]").show("fast");
        }
        else $("[id$=DODate_msg]").hide("fast");




        if ($("[id$=DOComments_ta]").val() == "") {
            errorFlag = true;
            $("[id$=DOComments_msg]").show("fast");
        }
        else $("[id$=DOComments_msg]").hide("fast");



    }

    if (errorFlag == true) {

        alert("please fill out the required fields");

        return false;
    }
    else {

        return true;
    }
}