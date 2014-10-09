function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function removeURLParameter(url, parameter) {
    //prefer to use l.search if you have a location/link object
    var urlparts = url.split('?');
    if (urlparts.length >= 2) {

        var prefix = encodeURIComponent(parameter) + '=';
        var pars = urlparts[1].split(/[&;]/g);

        //reverse iteration as may be destructive
        for (var i = pars.length; i-- > 0;) {
            //idiom for string.startsWith
            if (pars[i].lastIndexOf(prefix, 0) !== -1) {
                pars.splice(i, 1);
            }
        }

        url = urlparts[0] + '?' + pars.join('&');
        return url;
    } else {
        return url;
    }
}

function autoRefresh(forceGet) {
    location.reload(forceGet)
}

function convertStringToDate(str) {
    try {
        var temp = str.split('-');
        if (temp.length < 2) {
            temp = str.split('/');
        }
        if (temp.length > 2) {
            var d = new Date(temp[2], (parseInt(temp[1]) - 1), temp[0]);
            return d;
        }
    }
    catch (ex) {
    }
    return null;
}


$(document).ready(function () {

    //*************************************//
    /////////////Recommendation////////////// 
    //*************************************//

    $('[id$=grdMSARecommendationTask] tr:not(:first-child)').find("td:eq(3)").each(function () {
        var targetDate = convertStringToDate($(this).text());
        var currentDate = new Date();

        if (currentDate != null && targetDate != null && currentDate > targetDate && currentDate.toDateString() != targetDate.toDateString()) {
            var parentRow = $(this).parent();

            if (typeof parentRow != "undefined" && parentRow != null) {
                $(parentRow).addClass("overdue");
            }
        }
    });

    $('[id$=grdIRRecommendationsOnJob] tr:not(:first-child)').find("td:eq(3)").each(function () {
        var targetDate = convertStringToDate($(this).text());
        var currentDate = new Date();

        if (currentDate != null && targetDate != null && currentDate > targetDate && currentDate.toDateString() != targetDate.toDateString()) {
            var parentRow = $(this).parent();

            if (typeof parentRow != "undefined" && parentRow != null) {
                $(parentRow).addClass("overdue");
            }
        }
    });

    $('[id$=grdIR5OffJobRecomendationTask] tr:not(:first-child)').find("td:eq(3)").each(function () {
        var targetDate = convertStringToDate($(this).text());
        var currentDate = new Date();

        if (currentDate != null && targetDate != null && currentDate > targetDate && currentDate.toDateString() != targetDate.toDateString()) {
            var parentRow = $(this).parent();

            if (typeof parentRow != "undefined" && parentRow != null) {
                $(parentRow).addClass("overdue");
            }
        }
    });


    //*************************************//

    //Rizwan Work Queue
    //Start
    $("#searchInput1").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdMSATask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput2").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdMSARecommendationTask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput3").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdMSAScheduled]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput11").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIR01DITask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput12").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIR01DITask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput13").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIR03DITask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });
    //End Work Queue

    //Usama Work Queue
    //Start
    $("#searchInput30").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIIROffJobTask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput32").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdFROffJobTask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });


    $("#searchInput34").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIR05OffJobTask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });


    $("#searchInput35").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIR05OffJobDraft]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });


    $("#searchInput36").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIR5OffJobRecomendationTask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput37").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIR5OffJobRecomendationDraft]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });


    $("#searchInput38").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdWaiverOnJobTask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput39").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdWaiverOffJobTask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput40").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIR01Task]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput42").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdFlashReportTask]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    $("#searchInput44").keyup(function () {
        //split the current value of searchInput
        var data = this.value.split(" ");
        //create a jquery object of the rows
        var jo = $("[id$=grdIR05OffJobDraft]").find("tr");

        var header = $(jo).first("tr");

        if (this.value == "") {
            jo.show();
            return;
        }
        //hide all the rows
        jo.hide();

        //Recusively filter the jquery object to get results.
        var filterResult = jo.filter(function (i, v) {
            var $t = $(this);
            for (var d = 0; d < data.length; ++d) {
                var str = $t.html().toLowerCase();
                if (str.toLowerCase().indexOf(data[d].toLowerCase()) >= 0) {
                    return true;
                }
            }
            return false;
        });

        //show the rows that match.
        header.add(filterResult).show();
    }).focus(function () {
        this.value = "";
        $(this).css({
            "color": "black"
        });
        $(this).unbind('focus');
    }).css({
        "color": "#C0C0C0"
    });

    //End Work Queue

    $('.panel-collapse').collapse('show');

    var status = getParameterByName('Status');

    if (typeof status != 'undefined' && status != "" && status != null) {
        switch (status) {
            case "MSA_1":
                alert('MSA Submitted...');
                break;
            case "MSA_2":
                alert('MSA Saved As Draft...');
                break;
            case "MSAR_1":
                alert('Recommendation Saved...');
                break;
            case "MSAR_2":
                alert('Recommendation Sent...');
                break;
            case "MSAR_3":
                alert('Recommendation Approved...');
                break;
            case "MSAR_4":
                alert('Recommendation Rejected...');
                break;

            default:
                break;
        }
        var url = removeURLParameter(document.URL, "Status");

        if (typeof url != 'undefined' && url != "" && url != null && url.length > 0) {
            window.location.href = url;
        }
    }

    setInterval('autoRefresh(true)', 2160000); // this will reload page after every 5 secounds;
});