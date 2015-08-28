﻿$.fn.imageupload = function (url, opt) {
    //Author: Omar De la Rosa And Jose Maldonado!!!
    //Company: ESP!!!
    //Date: 17/3/2015
    $this = this;
    max = this.find("input[type='file'] ")[0].files.length;
    min = 0;
    now = 0;
    step = 0;
    files = this.find("input[type='file']")[0].files;
    current = 0;
    url = url || ""

    //Overloads
    opt.success = opt.success || function () { }
    opt.error = opt.error || function () { }
    opt.complete = opt.complete || function () { }

    //Setup
    //Progress bar....
    this.find(".progress-bar")
        .attr("aria-valuenow", "0")
        .attr("aria-valuemin", min)
        .css("width", "0%");
    //Progress bar text
    this.find(".sr-only")
        .text(now + "% Completado");
    //Set Max
    this.find(".progress-bar")
        .attr("aria-valuemax", max)
    //Set the xhrhttp to AJAX and the form data
    xhrhttp = new XMLHttpRequest();
    fData = new FormData();
    //Calcule the step
    step = (100 / max);
    //Function to upload.....

    upload = function () {

        xhrhttp.onreadystatechange = function () {
            if (xhrhttp.readyState == 4 && xhrhttp.status == 200) {

                now = now + step;
                $this.find(".progress-bar")
                        .attr("aria-valuenow", now)
                $this.find(".progress-bar")
                        .css("width", now + "%");
                $this.find(".sr-only")
                        .text(now + "% Completado");

                opt.success(files[current].name, xhrhttp.responseText, now);

            } else if (xhrhttp.status != 200 && xhrhttp.readyState != 1) {
                opt.error(files[current].name, xhrhttp.responseText, now);
            }
        } //onreadystatechange

        fData.append(files[current].name, files[current]);
        xhrhttp.open("POST", url, false);
        xhrhttp.setRequestHeader("Cache-Control", "no-cache");
        xhrhttp.send(fData);
        //Update the progress bar....
        //Clean Vars
        fData = null;
        xhrhttp = null;
        ////Update Vars
        fData = new FormData();
        xhrhttp = new XMLHttpRequest();
        current = current + 1;

        if (current >= max) {
            clearInterval(myUpload)
            opt.complete(xhrhttp.responseText);
        }
    }

    //Calls Functions
    myUpload = setInterval(upload, 500);

}