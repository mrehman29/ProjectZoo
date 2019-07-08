$(document).ready(function () {
    setTimeout(updateView(), 1);

    //Getting Zoo datetime from server
   $.ajax({
        url: "/Home/CurrentZooTime",
        type: "POST",
        success: function (result) {
            console.log(result);
            timerViewUpdate(result)
        }
        
    });
});

//Update zoo time in UI every second
function timerViewUpdate(datestring) {
    var currentTime = new Date(datestring);
    setInterval(function () {
        
        var hours = currentTime.getHours();
        var minutes = currentTime.getMinutes();
        var seconds = currentTime.getSeconds();

        // Add leading zeros
        hours = (hours < 10 ? "0" : "") + hours;
        minutes = (minutes < 10 ? "0" : "") + minutes;
        seconds = (seconds < 10 ? "0" : "") + seconds;

        // Compose the string for display
        var currentTimeString = hours + ":" + minutes + ":" + seconds;
        $(".zootime").html(currentTimeString);
        currentTime.setSeconds(currentTime.getSeconds() + 1);
    }, 1000);
}

//Updating Health every 20 sec
function updateView() {

    var timeoutmilisec = 20000 //20sec

    $.ajax({
        url: "/Home/GetUpdatedData",
        type: "POST",
        success: function (result) {
            console.log(result);
            $.each(result, function () {
                
                $("#span" + this.Type + this.Id).text(this.HealthLevel.toFixed(2) + "%");
                $("#div" + this.Type + this.Id).css('width', this.HealthLevel.toFixed(2)+ "%");
            });
        },
        complete: function () {
            setTimeout("updateView();", timeoutmilisec);
        }
    });
}

