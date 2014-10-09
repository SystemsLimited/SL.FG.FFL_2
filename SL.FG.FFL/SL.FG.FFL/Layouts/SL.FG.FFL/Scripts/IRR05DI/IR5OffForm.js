


$(document).ready(function () {

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

    function deleteRecommendation() {
        if ($('[id$=hdnIsChangesAllowed]').val() == "0") {
            return;
        }
        var par = $(this).closest('tr');
        par.remove();

        var count = $("[id$=recommendationDetails_table] tr.recommendationItem").length;
        $('[id$=noOfRecommendations_span]').text(count);
    }



    function updateRecommendationControls() {
        var html = $("[id$=responsiblePerson_PeopleEditor]").html();
        var emailList = extractEmails(html);

        if (emailList != 'undefined' && emailList != "" && emailList != null) {
            $('#responsiblePersonEmail_tf').val(emailList[0]);
        }

        var username = $('[id*=responsiblePerson_PeopleEditor_TopSpan_i]').attr('sid');

        if (username != 'undefined' && username != null && username != "") {
            var temp = username.split('|');

            if (temp.length > 1) {
                username = temp[1];
            }
        }

        if (username != 'undefined' && username != "" && username != null) {
            $('#responsiblePersonUsername_hd').val(username);
        }
    }


    $("[id$=recommendationDetails_table]").on("click", ".removeRecommendation", deleteRecommendation);

    //Attachment remove
    $('span.removeLink').on('click', function () {
        var par = $(this).closest('tr');
        var fileName = par.find('span.fileName');

        if (fileName != 'undefined' && fileName != "" && fileName != null) {
            var filenames = $('[id$=hdnFilesNames]').val();
            filenames += "~" + fileName.text();

            $('[id$=hdnFilesNames]').val(filenames);
        }
        par.remove();
    });


    //Get username and email of the selected user
    $('[id$=responsiblePerson_PeopleEditor]').on('focusout', function () {
        updateRecommendationControls();
    });

    //Add Recommendation in Grid
    $('[id$=addRecommendation_btn]').on('click', function () {
        if ($('[id$=hdnIsChangesAllowed]').val() == "0") {
            return;
        }
        updateRecommendationControls();

        var controlList = '';
        var errorFlag = false;
        var message = '**** Please Provide value for the required fields ****';

        if ($('[id$=responsibleDepartment_ddl] option:selected').val() == "0") {
            errorFlag = true;
            var controlName = "Responsible Department";
            controlList += controlName + ": ";
        }
        if ($('[id$=responsibleSection_ddl] option:selected').val() == "0") {
            errorFlag = true;
            var controlName = "Responsible Section";
            controlList += controlName + ": ";
        }
        if ($("[id$=responsiblePersonUsername_hd]").val() == "") {
            errorFlag = true;
            var controlName = "Responsible Person";
            controlList += controlName + ": ";
        }
        if ($("[id$=targetDate_dtcDate]").val() == "") {
            errorFlag = true;
            var controlName = "Target Date";
            controlList += controlName + ": ";
        }
        if ($("[id$=description_ta]").val() == "") {
            errorFlag = true;
            var controlName = "Description";
            controlList += controlName + ": ";
        }

        if ($("[id$=targetDate_dtcDate]").val() == "") {
            errorFlag = true;
            var controlName = "Target Date";
            controlList += controlName + ": ";
        }

        if (errorFlag == false && $("[id$=targetDate_dtcDate]").val() != "") {
            try {
                var targetDate = convertStringToDate($("[id$=targetDate_dtcDate]").val());

                if (targetDate == null) {
                    errorFlag = true;
                    message = 'Target date must be Valid';
                }
                else {
                    if ($("[id$=FRTargetDate_dtcDate]").val() != "") {

                        var FRDate = convertStringToDate($("[id$=FRTargetDate_dtcDate]").val());

                        if (targetDate != null && FRDate != null && FRDate > targetDate) {
                            errorFlag = true;
                            alert("IR Recommendation Target Date should be greater than Flash Report Target Date");

                            message = "";
                        }
                    }
                }
            }
            catch (ex) {
                errorFlag = true;
                message += '**** Enter Valid Date ****';
            }
        }

        if (errorFlag == false) {

            var responsiblePersonUsername = $('[id$=responsiblePersonUsername_hd]').val()
            var responsiblePersonEmail = $('[id$=responsiblePersonEmail_tf]').val();
            var description = $('[id$=description_ta]').val();
            var status = $('[id$=status_ddl] option:selected').val();

            var responsibleDepartment = $('[id$=responsibleDepartment_ddl] option:selected').text();
            var responsibleDepartmentId = $('[id$=responsibleDepartment_ddl] option:selected').val();

            if (responsibleDepartmentId != 'undefined' && responsibleDepartmentId == "0") {
                responsibleDepartmentId = "0";
                responsibleDepartment = "";
            }

            var responsibleSection = $('[id$=responsibleSection_ddl] option:selected').text();
            var responsibleSectionId = $('[id$=responsibleSection_ddl] option:selected').val();

            if (responsibleSectionId != 'undefined' && responsibleSectionId == "0") {
                responsibleSectionId = "0";
                responsibleSection = "";
            }

            var recommendationId = $('#recommendationId_hd').val();;

            var concurrenceOfRP = "Yes";

            var selected1 = $("input[type='radio'][name='concurrenceOfRP']:checked");
            if (selected1.length > 0) {
                concurrenceOfRP = selected1.val();
            }

            var targetDate = $('[id$=targetDate_dtcDate]').val();


            //add recommendation in grid
            if (true) {
                var count = $("[id$=recommendationDetails_table] tr.recommendationItem").length + 1;
                var actions = "<span class='btn btn-default editRecommendation' ><i class='glyphicon glyphicon-pencil'></i></span><span class='btn btn-danger removeRecommendation'><i class='glyphicon glyphicon-remove'></i></span>";
                var data = "<tr class='recommendationItem'><td>" + count + "</td><td style='display:none;'><span class='recommendationId'>" + recommendationId + "</span></td><td class='td-description'><span class='description'>" + description + "</span></td><td><span class='username'>" + responsiblePersonUsername + "</span></td><td style='display:none;'><span class='email'>" + responsiblePersonEmail + "</td><td><span class='sectionName'>" + responsibleSection + "</span></td><td style='display:none'><span class='sectionId'>" + responsibleSectionId + "</span></td><td><span class='departmentName'>" + responsibleDepartment + "</span></td><td style='display:none'><span class='departmentId'>" + responsibleDepartmentId + "</span></td><td><span class='targetDate'>" + targetDate + "</span></td><td><span class='concurrenceOfRP'>" + concurrenceOfRP + "</span></td><td><span class='status'>" + status + "</span></td><td style='display:none;'><span class='recommendationNo'>" + "" + "</span></td><td>" + actions + "</td></tr>";
                $("[id$=recommendationDetails_table]").append(data);

                $('[id$=recommendationNo_tf]').val("");

                $('[id$=noOfRecommendations_span]').text(count);
            }


            $('[id$=description_ta]').val("");
            $('[id$=responsibleDepartment_ddl]').val("0");
            $('[id$=responsibleSection_ddl]').val("0");
            //clear client people picker
            $('.sp-peoplepicker-delImage[id$=_DeleteUserLink]').trigger('click');
            $('[id$=responsiblePersonEmail_tf]').val("");
            $('[id$=responsiblePersonUsername_hd]').val("");
            $('[id$=detailedReportNo_rb]').prop("checked", "checked");

            var currentDateTime = new Date();
            var currentDate = currentDateTime.format("dd/MM/yyyy");

            if (currentDate != null && currentDate != "") {
                $('[id$=targetDate_dtcDate]').val(currentDate);
            }

            $('#collapse3').collapse('hide');
            $('#panel-title3').attr('data-toggle', 'collapse');

            alert('Recommendation added successfully!');
        }
        else
            ValidationSummary(message, controlList);
    });


    $('.panel-collapse').collapse('hide');
    $('.panel-title').attr('data-toggle', 'collapse');

    var count = $("[id$=recommendationDetails_table] tr.recommendationItem").length;
    $('[id$=noOfRecommendations_span]').text(count);


    // Capturing when the user modifies a field
    var warnMessage = 'You have unsaved changes on this page!';
    var formModified = new Boolean();
    formModified = false;
    $('input:not(:button,:submit),textarea,select').on('change', function () {
        formModified = true;
    });
    // Checking if the user has modified the form upon closing window
    $('input:submit').on('click', function (e) {
        formModified = false;
    });
    window.onbeforeunload = function () {
        if (formModified != false) return warnMessage;
    }
});